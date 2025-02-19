using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    private Character myCharacter;
    private int damage;

    public Character SetMyCharacter
    {
        set => myCharacter = value;
    }

    public int SetDamage
    {
        set => damage = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Character character))
        {
            if (character != myCharacter)
            {
                character.GetDamage(damage);
            }
        }
    }
}
