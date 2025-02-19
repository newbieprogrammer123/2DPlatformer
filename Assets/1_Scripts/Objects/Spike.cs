using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().SetTrigger("Attack");

        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().GetDamage(damage);

            Vector2 dropPos = collision.transform.position;
            dropPos.y += 30;

            if(transform.position.x > collision.transform.position.x) // Drop player in left
            {
                dropPos.x -= 50;
            }
            else
            {
                dropPos.x += 50;
            }

            collision.GetComponent<Player>().Drop(dropPos);
        }
    }
}
