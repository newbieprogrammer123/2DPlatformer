using DG.Tweening;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private TriggerCheckDistance checkDistanceRight, checkDistanceLeft;
    [SerializeField] private float speedAttack;

    private StateEnemy state;
    private Vector3 movePos;
    private float distanceAttack = 1.5f;
    private float timeBeforeAttack;
    private bool showAnimationAttack;

    private Player player;

    private void Start()
    {
        ChangeState(StateEnemy.Idle);

        DOTween.Sequence().AppendInterval(1.5f).OnComplete(() => ChangeState(StateEnemy.Search));
    }

    private void Update()
    {
        if (!isAlive || state == StateEnemy.Idle)
        {
            return;
        }

        if (state == StateEnemy.Search)
        {
            bool isRight = Random.Range(0, 2) == 1;
            float rndX = Random.Range(2, 5);

            movePos = transform.position;

            ChangeFlipX(isRight);

            if (isRight)
            {
                movePos.x += rndX;
            }
            else
            {
                movePos.x -= rndX;
            }

            ChangeState(StateEnemy.Move);
        }
        else if (state == StateEnemy.Move)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos, data.SpeedMove * Time.deltaTime);

            if (transform.position == movePos)
            {
                ChangeState(StateEnemy.Idle);

                DOTween.Sequence().AppendInterval(1.5f).OnComplete(() => ChangeState(StateEnemy.Search));
            }
        }
        else if (state == StateEnemy.Attack)
        {
            if(!showAnimationAttack && timeBeforeAttack <= 0.5f)
            {
                showAnimationAttack = true;

                ShowAnimation(TypeAnimation.Attack);
            }

            if (timeBeforeAttack <= 0)
            {
                if (player.GetIsAlive)
                {
                    AudioController.Instance.PlaySound(TypeSound.PlayerAttack);

                    player.GetDamage(data.CurrentDamage);

                    timeBeforeAttack = speedAttack;

                    showAnimationAttack = false;
                }
                else
                {
                    ChangeState(StateEnemy.Idle);
                }
            }
            else
            {
                timeBeforeAttack -= Time.deltaTime;
            }
        }
    }

    private void ChangeState(StateEnemy state)
    {
        this.state = state;

        if (state == StateEnemy.Idle)
        {
            ShowAnimation(TypeAnimation.Idle);
        }
        else if (state == StateEnemy.Move || state == StateEnemy.GoToPlayer)
        {
            ShowAnimation(TypeAnimation.Run);
        }       
    }

    public void GoToPlayer(Player player)
    {
        this.player = player;

        movePos = transform.position;
        movePos.x = player.transform.position.x;

        if (player.transform.position.x > transform.position.x)
        {
            directionRight = true;
        }
        else
        {
            directionRight = false;
        }

        ChangeFlipX(directionRight);


        if (directionRight)
        {
            checkDistanceRight.gameObject.SetActive(true);
        }
        else
        {
            checkDistanceLeft.gameObject.SetActive(true);
        }

        ChangeState(StateEnemy.Move);
    }

    public void SearchPlayer()
    {
        ChangeState(StateEnemy.Search);
    }

    public void StartAttackPlayer()
    {
        ChangeState(StateEnemy.Attack);
    }

    public void EndAttackPlayer()
    {
        ChangeState(StateEnemy.Search);
    }

    protected override void Death()
    {
        base.Death();

        DOTween.Sequence().AppendInterval(1f).OnComplete(() =>
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        });

        AudioController.Instance.PlaySound(TypeSound.EnemyDeath);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBorder")
        {
            bool isRight = collision.transform.position.x < transform.position.x;
            float rndX = Random.Range(2, 5);

            movePos = transform.position;

            ChangeFlipX(isRight);

            if (isRight)
            {
                movePos.x += rndX;
            }
            else
            {
                movePos.x -= rndX;
            }
        }
    }
}

public enum StateEnemy
{
    Idle,
    Search,
    Move,
    GoToPlayer,
    Attack
}
