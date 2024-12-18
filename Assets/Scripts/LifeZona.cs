using UnityEngine;

public class LifeZona : MonoBehaviour
{
    private UnitLife _playerHealth;

    private void Awake()
    {
        _playerHealth = gameObject.GetComponent<UnitLife>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _playerHealth.TakeDamage(enemy.Damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _playerHealth.StopToTakeDamage();
        }
    }
}