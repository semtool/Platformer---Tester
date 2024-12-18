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

    public void MoveToNextPoint(IReadOnlyList<Vector2> nextPoint)
    {
        _enemyMover.MoveByNavigator(nextPoint);
    }
}