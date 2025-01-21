using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitLife))]
[RequireComponent(typeof(UnitDamageZona))]
public class Enemy : PersonUnit
{
    [SerializeField] private EnemyMover _enemyMover;

    private UnitDamageZona _enemyDamageZona;

    private void Awake()
    {
        _enemyDamageZona = GetComponent<UnitDamageZona>();
    }

    private void Start()
    {
        ControlMonitoring();
    }

    public void MoveToNextPoint(IReadOnlyList<Vector2> nextPoint)
    {
        _enemyMover.MoveByNavigator(nextPoint);
    }

    private void ControlMonitoring()
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemyDamageZona.MonitorArea();
        }
    }
}