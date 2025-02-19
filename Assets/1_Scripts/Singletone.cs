using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject.GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }

        AfterAwake();
    }

    protected virtual void AfterAwake() { }
}
