using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject panelInfo;
    [SerializeField] private Item item;

    private bool isCanOpen, isOpen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isOpen)
            {
                isCanOpen = true;
                panelInfo.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCanOpen = false;
            panelInfo.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCanOpen)
        {
            if (!isOpen)
            {
                isOpen = true;

                panelInfo.SetActive(false);

                GetComponent<Animator>().SetTrigger("Open");

                DOTween.Sequence().AppendInterval(0.3f).OnComplete(() =>
                {
                    Vector2 spawnPos = transform.position;
                    spawnPos.y += 1;
                    Item newItem = Instantiate(item, spawnPos, Quaternion.identity);

                    Vector2 rndPos = new Vector2(Random.Range(-2, 2), Random.Range(5, 10));
                    newItem.GetComponent<Rigidbody2D>().AddForce(rndPos * 30);
                });

                AudioController.Instance.PlaySound(TypeSound.ChestOpen);
            }
        }
    }
}
