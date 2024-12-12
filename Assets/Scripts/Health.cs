using System;
<<<<<<< Updated upstream
=======
using System.Collections;
>>>>>>> Stashed changes
using UnityEngine;

public class Health : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] private PillSimulator _pillSimulator;
    [SerializeField] private DamageSimulator _damageSimulator;

    public event Action CurrentHealthChanged;

    private float _maxHealth = 100;
=======
    [SerializeField] private CharacterUnit _unit;
    [SerializeField] private float _maxHealth;

    public event Action CurrentHealthChanged;

    private HeathZona _heathZona;
    private Pill _pill; 
    private Enemy _enemy;
    private float _timeToLoseNextPieceOfHealth = 3;
    private Coroutine _coroutineForChanging;
>>>>>>> Stashed changes
    private float _minHealth = 0;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

<<<<<<< Updated upstream
    private void Start()
    {
=======
    private void Awake()
    {
        _pill = GetComponent<Pill>();
        _enemy = GetComponent<Enemy>();
        _heathZona = GetComponent<HeathZona>();
>>>>>>> Stashed changes
        MaxHealth = _maxHealth;
        CurrentHealth = _maxHealth;
    }

    private void OnEnable()
    {
<<<<<<< Updated upstream
        _pillSimulator.HealthChanged += IncreaseHealth;
        _damageSimulator.HealthChanged += DecreaseHealth;
=======
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
>>>>>>> Stashed changes
    }

    private void OnDisable()
    {
<<<<<<< Updated upstream
        _pillSimulator.HealthChanged -= IncreaseHealth;
        _damageSimulator.HealthChanged -= DecreaseHealth;
=======
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
>>>>>>> Stashed changes
    }

    private void IncreaseHealth()
    {
<<<<<<< Updated upstream
        CurrentHealth += _pillSimulator.PieceOfHealth;
=======
        CurrentHealth += _pill.HealthDose;
>>>>>>> Stashed changes

        if (CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }

        InformHealthIsChanged();
    }

    private void DecreaseHealth()
    {
<<<<<<< Updated upstream
        CurrentHealth -= _damageSimulator.PieceOfHealth;

        if (CurrentHealth < _minHealth)
        {
            CurrentHealth = _minHealth;
=======
        CurrentHealth -= _enemy.Damage;

        if (CurrentHealth <= _minHealth)
        {
            Destroy(gameObject);
>>>>>>> Stashed changes
        }

        InformHealthIsChanged();
    }

    private void InformHealthIsChanged()
    {
        CurrentHealthChanged?.Invoke();
    }
<<<<<<< Updated upstream
=======

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
>>>>>>> Stashed changes
}