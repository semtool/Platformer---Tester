using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool IsTouched { get; private set; }

    private void Update()
    {
        if (IsTouched == true)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeStatus()
    {
        IsTouched = true;         
    }
}