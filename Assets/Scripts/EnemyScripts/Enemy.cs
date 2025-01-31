using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private float _damage;

    public float Damage { get; private set; }

    private void Awake()
    {
        Damage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.GetComponent<UnitLife>().TakeDamage(Damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.GetComponent<UnitLife>().StopToTakeDamage();
        }
    }

    public void MoveToNextPoint(IReadOnlyList<Vector2> nextPoint, float speed)
    {
        _enemyMover.MoveByNavigator(nextPoint, speed);
    }
}