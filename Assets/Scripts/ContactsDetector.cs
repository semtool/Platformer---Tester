using UnityEngine;
using System;

public class ContactsDetector : MonoBehaviour
{
    public event Action PillUsed;

    public bool IsGrounded { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            IsGrounded = true;
        }

        if (collision.gameObject.TryGetComponent(out ObjectUnit Unit))
        {
            if (Unit is Pill)
            {
                Unit.Disappear();

                PillUsed?.Invoke();
            }

            if (Unit is Coin)
            {
                Unit.Disappear();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            IsGrounded = false;
        }
    }
}