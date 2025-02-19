using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float speedMove, offsetY;

    private Player player;
    private Vector3 nextPos;

    private void Start()
    {
        player = GameController.Instance.GetPlayer;
    }

    private void Update()
    {
        nextPos = player.transform.position;
        nextPos.z = -10;
        nextPos.y += offsetY;
        transform.position = Vector3.Lerp(transform.position, nextPos, speedMove * Time.deltaTime);
    }
}
