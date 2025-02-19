using UnityEngine;
using DG.Tweening;

public class DestroyTiles : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sprite.transform.DOLocalRotate(new Vector3(0, 0, 5), 0.1f).OnComplete(() =>
            {
                sprite.transform.DOLocalRotate(new Vector3(0, 0, -5), 0.1f).SetLoops(10, LoopType.Yoyo).OnComplete(() =>
                {
                    sprite.DOFade(0, 0.2f).OnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                });
            });           
        }
    }
}
