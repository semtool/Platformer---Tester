using UnityEngine;
using System;

public class ContactsDetector : MonoBehaviour
{
    public event Action PillUsed;

    public bool IsGrounded { get; private set; }

    public bool HasFind { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            IsGrounded = true;
        }

        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if (unit is Coin)
            {
                unit.Disappear();
            }

            if (unit is Pill)
            {
                unit.Disappear();

                PillUsed?.Invoke();
            }          
        }
    }
}