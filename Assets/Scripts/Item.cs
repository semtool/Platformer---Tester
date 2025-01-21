using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public void Disappear()
    {
        Destroy(gameObject);
    }
}