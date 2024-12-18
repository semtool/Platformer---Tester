using System;
using System.Collections;
using UnityEngine;

public class UnitLife : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public event Action CurrentHealthChanged;

    private float _timeToLoseNextPieceOfHealth = 3;
    private Coroutine _coroutineForChanging;
    private float _minHealth = 0;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = _maxHealth;
    }

    public void IncreaseHealth(float healthDose)
    {
        CurrentHealth += healthDose;

        if (CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }

        CurrentHealthChanged?.Invoke();
    }

    public void TakeDamage(float damageDose)
    {
        _coroutineForChanging = StartCoroutine(SpendHealth(damageDose));
    }

    public void StopToTakeDamage()
    {
        if (_coroutineForChanging != null)
        {
            StopCoroutine(_coroutineForChanging);
        }
    }

    private void InformHealthIsChanged()
    {
        CurrentHealthChanged?.Invoke();
    }

    private IEnumerator SpendHealth(float damageDose)
    {
        while (enabled)
        {
            DecreaseHealth(damageDose);

            var wait = new WaitForSeconds(_timeToLoseNextPieceOfHealth);

            yield return wait;
        }
    }

    private void DecreaseHealth(float damageDose)
    {
        CurrentHealth -= damageDose;

        if (CurrentHealth <= _minHealth)
        {
            Destroy(gameObject);
        }

        InformHealthIsChanged();
    }
}