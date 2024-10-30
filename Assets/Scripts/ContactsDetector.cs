using UnityEngine;

public class ContactsDetector : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    public bool IsCetched { get; private set; }

    public bool IsCured { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            IsGrounded = true;
        }

        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.ChangeStatus();
        }

        if (collision.gameObject.TryGetComponent(out Pill pill))
        {
            IsCured = true;

            pill.ChangeStatus();
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
    }
}