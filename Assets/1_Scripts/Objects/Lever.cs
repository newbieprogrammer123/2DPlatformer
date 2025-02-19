using DG.Tweening;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject panelInfo;
    [SerializeField] private GameObject leverSprite;

    private bool isCanUse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCanUse = true;
            panelInfo.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCanUse = false;
            panelInfo.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCanUse)
        {
            panelInfo.SetActive(false);

            leverSprite.transform.DORotate(new Vector3(0, 0, -40), 0.5f).OnComplete(() =>
            {
                Debug.Log("Open door");
            });

            AudioController.Instance.PlaySound(TypeSound.ChestOpen);
        }
    }
}
