using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScaner : MonoBehaviour
{
    [SerializeField] private float _visionRadius;
    [SerializeField] private float _attackDistance;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LayerMask _layerMask;

    public event Action AttacIsReady;
    public event Action UnitOutZona;

    private WaitForSeconds _wait = new(0);
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _enemyMover = _enemy.GetComponent<EnemyMover>();
    }

    private void Start()
    {
        StartCoroutine(MonitorSpace());
    }

    private IEnumerator MonitorSpace()
    {
        while (enabled)
        {
            ToDetectPlayer();

            yield return _wait;
        }
    }

    private IList<Collider2D> ToDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _visionRadius, _layerMask);

        return Array.AsReadOnly(colliders);
    }

    private void ToDetectPlayer()
    {
        foreach (var collider in ToDetect())
        {
            if (collider.TryGetComponent(out Player player))
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);

                if (distance > _attackDistance)
                {
                    _enemyMover.Move(player.transform.position, _enemyMover.ApproachSpeed);
                }
            }
        }
    }
}