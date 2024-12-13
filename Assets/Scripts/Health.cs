using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private CharacterUnit _unit;
    [SerializeField] private float _maxHealth;

    public event Action CurrentHealthChanged;

    private HeathZona _heathZona;
    private Pill _pill; 
    private Enemy _enemy;
    private float _timeToLoseNextPieceOfHealth = 3;
    private Coroutine _coroutineForChanging;
    private float _minHealth = 0;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        _pill = GetComponent<Pill>();
        _enemy = GetComponent<Enemy>();
        _heathZona = GetComponent<HeathZona>();

        MaxHealth = _maxHealth;
        CurrentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        if (_unit is Player)
        {
            _unit.PillIsTaken += IncreaseHealth;
            _heathZona.DamageZonaIsEntered += ActivateCoroutine;
            _heathZona.DamageZonaIsLeaved += DectivateCoroutine;
        }

        if (_unit is Enemy)
        {
            Debug.Log(_maxHealth);   // Заглушка
        }
    }

    private void OnDisable()
    {

        if (_unit is Player)
        {
            _unit.PillIsTaken -= IncreaseHealth;
            _heathZona.DamageZonaIsEntered -= ActivateCoroutine;
            _heathZona.DamageZonaIsLeaved -= DectivateCoroutine;
        }

        if (_unit is Enemy)
        {
            Debug.Log(_maxHealth);   // Заглушка
        }

    }

    private void IncreaseHealth()
    {
        CurrentHealth += _pill.HealthDose;

        if (CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }

        InformHealthIsChanged();
    }

    private void DecreaseHealth()
    {

        CurrentHealth -= _enemy.Damage;

        if (CurrentHealth <= _minHealth)
        {
            Destroy(gameObject);

        }

        InformHealthIsChanged();
    }

    private void InformHealthIsChanged()
    {
        CurrentHealthChanged?.Invoke();
    }


    private void ActivateCoroutine()
    {
        _coroutineForChanging = StartCoroutine(SpendHealth());
    }

    private void DectivateCoroutine()
    {
        if (_coroutineForChanging != null)
        {
            StopCoroutine(_coroutineForChanging);
        }
    }

    private IEnumerator SpendHealth()
    {
        while (true)
        {
            DecreaseHealth();

            var wait = new WaitForSeconds(_timeToLoseNextPieceOfHealth);

            yield return wait;
        }
    }
}