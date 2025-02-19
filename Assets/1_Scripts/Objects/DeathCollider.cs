using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameController.Instance.PlayerDeath();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
