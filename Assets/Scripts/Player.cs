using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(ContactsDetector))]
[RequireComponent(typeof(Pill))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private PlayerMover _playerMover;
    private ContactsDetector _contactsDetector;
    private InputReader _inputReader;
    private Pill _pill;
    private float _currentHealth;
    private float _minHealth = 0;


    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _contactsDetector = GetComponent<ContactsDetector>();
        _pill = GetComponent<Pill>();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void FixedUpdate()
    {
        ShowHealth();

        if (_contactsDetector.IsGrounded)
        {
            _playerMover.Move(_inputReader.Direction);
        }

        if (_inputReader.IsJumped && _contactsDetector.IsGrounded)
        {
            _playerMover.Jump();
            _inputReader.StopToJump();
        }

        if (_contactsDetector.IsCured)
        {
            TakePill();

            _contactsDetector.ChgangeCureStatus();
        }

        Disappear();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    private void TakePill()
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += _pill.HealthDose;

            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
    }

    private void Disappear()
    {
        if (_currentHealth <= _minHealth)
        {
            Destroy(gameObject);
        }
    }

    private void ShowHealth()
    {
        Debug.Log(_currentHealth);
    }
}