using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireZona : MonoBehaviour
{
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _selfDamagePower;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _healthDose;

    private Coroutine _coroutineForVampirism;
    private UnitLife _selfLife;
    private float _abilityRadius;
    private float _minRadius = 0;
    private float _scanningInterval = 0.5f;
    private WaitForSeconds _wait;
    private IList<Collider2D> _enemyList;
    private float _vampireRudius;

    public float AbilityRadius { get; private set; }

    private void Awake()
    {
        _abilityRadius = _maxRadius;
        _vampireRudius = _maxRadius;
        _wait = new WaitForSeconds(_scanningInterval);

        _selfLife = GetComponent<UnitLife>();
    }

    public void SetMaxRadius()
    {
        AbilityRadius = _maxRadius;
    }

    public void SetMinRadius()
    {
        AbilityRadius = _minRadius;
    }

    public void MonitorArea()
    {
        _coroutineForVampirism = StartCoroutine(MonitorSpace());
    }

    public void StopMonitoringArea()
    {
        StopCoroutine(_coroutineForVampirism);
    }

    private IEnumerator MonitorSpace()
    {
        while (enabled)
        {
            FindeNearestEnemy();

            yield return _wait;
        }
    }

    private IList<Collider2D> ToDetect()
    {
        return Physics2D.OverlapCircleAll(transform.position, _abilityRadius, _enemy);
    }

    private void FindeNearestEnemy()
    {
        _enemyList = ToDetect();

        if (_enemyList.Count == 0)
        {
            _vampireRudius = _maxRadius;
        }

        foreach (var emeny in _enemyList)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, emeny.gameObject.transform.position);

            TakeEnemyHealth(distanceToEnemy, emeny);

            _vampireRudius = distanceToEnemy;
        }
    }

    private void TakeEnemyHealth(float distanceToEnemy, Collider2D collider)
    {
        if (distanceToEnemy <= _vampireRudius)
        {
            if (_selfLife.CurrentHealth < _selfLife.MaxHealth)
            {
                collider.GetComponent<UnitLife>().DecreaseHealth(_healthDose);

                _selfLife.IncreaseHealth(_healthDose);
            }
        }
    }
}