using DG.Tweening;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer pillar, banner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.GetComponent<Player>().GetCountCrystals >= GameController.Instance.GetNeedCrystals && 
                collision.GetComponent<Player>().GetCountCrown == GameController.Instance.GetNeedCrown)
            {
                banner.DOFade(1, 1f).OnComplete(() => GameController.Instance.FinishLevel());

                AudioController.Instance.PlaySound(TypeSound.Finish);
            }
            else
            {
                Debug.Log("Не нашел все кристаллы или корону");
            }            
        }
    }
}
