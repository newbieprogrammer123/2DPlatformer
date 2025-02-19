using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private TypeItem typeItem;
    [SerializeField] private int value;

    public TypeItem GetTypeItem
    {
        get { return typeItem; }
    }

    public int GetValue
    {
        get { return value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            GetComponent<Rigidbody2D>().Sleep();
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeItem(this);
        }
    }
}

public enum TypeItem
{
    Eat,
    Crystal,
    Crown
}
