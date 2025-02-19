using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(DataCharacter))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    [SerializeField] protected TriggerDamage leftTriggerDamage, rightTriggerDamage;

    protected DataCharacter data;
    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected Vector2 move;
    protected bool isAlive, directionRight;

    public bool GetIsAlive
    {
        get { return isAlive; }
    }

    private void Awake()
    {
        isAlive = true;
        directionRight = true;

        data = GetComponent<DataCharacter>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        data.Init();

        data.DeathEvent += Death;

        leftTriggerDamage.SetDamage = data.CurrentDamage;
        rightTriggerDamage.SetDamage = data.CurrentDamage;
    }

    protected void ShowAnimation(TypeAnimation typeAnimation)
    {
        if(!isAlive && typeAnimation != TypeAnimation.Death)
        {
            return;
        }

        animator.SetTrigger(typeAnimation.ToString());
    }

    protected void ChangeFlipX(bool isX)
    {
        spriteRenderer.flipX = isX;
        directionRight = !isX;
    }

    public void GetDamage(int damage)
    {
        data.GetDamage(damage);

        AudioController.Instance.PlaySound(TypeSound.PlayerHit);
        ShowAnimation(TypeAnimation.Hit);

        DOTween.Sequence().AppendInterval(0.3f).OnComplete(() => ShowAnimation(TypeAnimation.Idle));
    }

    protected virtual void Death()
    {
        isAlive = false;

        DOTween.Sequence().AppendInterval(0.2f).OnComplete(() =>
        {
            ShowAnimation(TypeAnimation.Death);
        });
    }

    protected void Attack()
    {
        ShowAnimation(TypeAnimation.Attack);

        TriggerDamage currentTrigger;

        if (directionRight)
        {
            currentTrigger = rightTriggerDamage;
        }
        else
        {
            currentTrigger = leftTriggerDamage;
        }

        currentTrigger.gameObject.SetActive(true);

        DOTween.Sequence().AppendInterval(0.3f).OnComplete(() => currentTrigger.gameObject.SetActive(false));
    }
}

public enum TypeAnimation
{
    Idle,
    Run,
    Jump,
    Attack,
    Hit,
    Death
}
