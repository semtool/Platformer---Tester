using UnityEngine;

public class ContactsDetector : MonoBehaviour
{
    private UnitLife _playerHealth;
    
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _playerHealth = gameObject.GetComponent<UnitLife>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Surface surface))
        {
            IsGrounded = true;
        }

        if (collision.gameObject.TryGetComponent(out ObjectUnit unit))
        {
            if (unit is Pill pill)
            {
                pill.Disappear();

                _playerHealth.IncreaseHealth(pill.HeathDose);
            }

            if (unit is Coin)
            {
                unit.Disappear();
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