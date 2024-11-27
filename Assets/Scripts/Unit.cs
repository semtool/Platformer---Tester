using UnityEngine;

public abstract class Unit: MonoBehaviour
{
    public void Disappear()
    {
        Destroy(gameObject);
    }
}