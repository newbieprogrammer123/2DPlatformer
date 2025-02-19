using DG.Tweening;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerUI playerUI;   

    private bool isGround, isRun, isDrop;

    private int countCrystals;
    private int countCrown;

    public int GetCountCrystals
    {
        get => countCrystals;
    }

    public int GetCountCrown
    {
        get => countCrown;
    }

    private void Start()
    {
        UpdateHealthUi(data.CurrentHealth, data.MaxHealth);
        playerUI.ChangeCounterCrystalsText(countCrystals, GameController.Instance.GetNeedCrystals);
        playerUI.ChangeCounterCrownText(countCrown, GameController.Instance.GetNeedCrown);
        data.ChangeHealthEvent += UpdateHealthUi;
    }

    private void UpdateHealthUi(int health, int maxHealth)
    {
        playerUI.ChangeHealthSlider(health, maxHealth);
    }

    private void Update()
    {
        if (isDrop || !isAlive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();

            AudioController.Instance.PlaySound(TypeSound.PlayerAttack);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float horizontal = Input.GetAxis("Horizontal");

            move = new Vector2(horizontal * data.SpeedMove, rigidbody.velocity.y);

            rigidbody.velocity = move;

            if (!isRun)
            {
                isRun = true;

                ShowAnimation(TypeAnimation.Run);
            }

            ChangeFlipX(horizontal < 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            move = new Vector2(rigidbody.velocity.x, data.ForceJump);
            rigidbody.AddForce(move, ForceMode2D.Impulse);

            ShowAnimation(TypeAnimation.Jump);
            AudioController.Instance.PlaySound(TypeSound.PlayerJump);
        }

        if(rigidbody.velocity == Vector2.zero)
        {
            if (isRun)
            {
                isRun = false;

                ShowAnimation(TypeAnimation.Idle);
            }
        }
    }

    public void ChangeStateGround(bool isGround)
    {
        this.isGround = isGround;

        if (isGround)
        {
            ShowAnimation(TypeAnimation.Idle);
        }
    }

    public void TakeItem(Item item)
    {
        if (item.GetTypeItem == TypeItem.Eat)
        {
            data.AddHealth(item.GetValue);
        }
        else if(item.GetTypeItem == TypeItem.Crystal)
        {
            countCrystals++;

            int prevCountCrystals = SaveController.Instance.LoadPoints();
            SaveController.Instance.SavePoints(prevCountCrystals + 1);

            playerUI.ChangeCounterCrystalsText(countCrystals, GameController.Instance.GetNeedCrystals);
        }
        else if(item.GetTypeItem == TypeItem.Crown)
        {
            countCrown++;

            int prevCountCrown = SaveController.Instance.LoadPoints();
            SaveController.Instance.SavePoints(prevCountCrown + 1);

            playerUI.ChangeCounterCrownText(countCrown, GameController.Instance.GetNeedCrown);
        }

        AudioController.Instance.PlaySound(TypeSound.TakeItem);

        Destroy(item.gameObject);
    }

    public void Drop(Vector2 dropPos)
    {
        isDrop = true;

        rigidbody.Sleep();
        rigidbody.AddForce(dropPos * 10);

        DOTween.Sequence().AppendInterval(0.7f).OnComplete(() =>
        {
            isDrop = false;
        });
    }

    protected override void Death()
    {
        base.Death();

        DOTween.Sequence().AppendInterval(1.2f).OnComplete(() =>
        {
            GameController.Instance.PlayerDeath();
        });

        AudioController.Instance.PlaySound(TypeSound.PlayerDeath);
    }
}
