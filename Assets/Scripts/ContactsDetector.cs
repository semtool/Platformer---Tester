using UnityEngine;
using System;

public class ContactsDetector : MonoBehaviour
{
    public event Action PillUsed;

    public bool IsGrounded { get; private set; }

<<<<<<< Updated upstream
    public bool HasFind { get; private set; }
=======
    public bool IsCured { get; private set; }
>>>>>>> Stashed changes

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            IsGrounded = true;
        }

<<<<<<< Updated upstream
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
=======
        if (collision.gameObject.TryGetComponent(out ObjectUnit objectUnit))
        {
            if (objectUnit is Pill)
            {
                objectUnit.Disappear();

                IsCured = true;
            }

            if (objectUnit is Coin)
            {
                objectUnit.Disappear();
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

    public void ChgangeCureStatus()
    {
        IsCured = false;
>>>>>>> Stashed changes
    }
}