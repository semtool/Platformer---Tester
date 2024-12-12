using UnityEngine;

public abstract class ObjectUnit : MonoBehaviour
{
    public void Disappear()
    {
        Destroy(gameObject);
    }
}