using UnityEngine;
using DG.Tweening;

public class SecretPlarform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] sprites;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach (var sprite in sprites)
            {
                sprite.DOFade(1, 0.5f);
            }
        }
    }
}
