using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDamageZona : MonoBehaviour
{
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _selfDamagePower;

    private Collider2D _selfCollider;
    private UnitLife _selfLife;
    private float _abilityRadius;
    private float _minRadius = 0;
    private float _timeInterval = 1;

    public float AbilityRadius { get; private set; }

    private void Awake()
    {
        _abilityRadius = _maxRadius;
        AbilityRadius = _abilityRadius;
 
        _selfCollider = gameObject.GetComponent<Collider2D>();
        _selfLife = gameObject.GetComponent<UnitLife>();
    }

    public void SetMaxRadius()
    {
        _abilityRadius = _maxRadius;
    }

    public void SetMinRadius()
    {
        _abilityRadius = _minRadius;
    }

    public void MonitorArea()
    {
        StartCoroutine(Monitor());
    }

    private IEnumerator Monitor()
    {
        while (enabled)
        {
            var wait = new WaitForSeconds(_timeInterval);

            FindEnemy();

            yield return wait;
        }
    }

    private IList<Collider2D> ToDetect()
    {
        return Physics2D.OverlapCircleAll(transform.position, _abilityRadius);
    }

    private void FindEnemy()
    {
        foreach (var collider in ToDetect())
        {
            if (collider != _selfCollider)
            {
                if (collider.TryGetComponent(out PersonUnit person))
                {
                    if (person is Player && gameObject.TryGetComponent(out Enemy enemy) == true)
                    {
                        GetPersonLife(person).DecreaseHealth(_selfDamagePower);
                    }

                    if (person is Enemy && gameObject.TryGetComponent(out Player player) == true)
                    {
                        GetPersonLife(person).DecreaseHealth(_selfDamagePower);

                        _selfLife.IncreaseHealth(_selfDamagePower);
                    }
                }
            }
        }
    }

    private UnitLife GetPersonLife(PersonUnit person)
    {
        return person.GetComponent<UnitLife>();
    }
}