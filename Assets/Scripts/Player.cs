using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(ContactsDetector))]
<<<<<<< Updated upstream
[RequireComponent(typeof(Pill))]
public class Player : MonoBehaviour
=======

public class Player : CharacterUnit
>>>>>>> Stashed changes
{
    [SerializeField] private Health health;

    private PlayerMover _playerMover;
    private ContactsDetector _contactsDetector;
    private InputReader _inputReader;
<<<<<<< Updated upstream
    private Pill _pill;
    private float _currentHealth;
    private float _minHealth = 0;
=======
>>>>>>> Stashed changes

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _contactsDetector = GetComponent<ContactsDetector>();
<<<<<<< Updated upstream
        _pill = GetComponent<Pill>();
    }

    private void OnEnable()
    {
        _contactsDetector.PillUsed += TakePill;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
=======
>>>>>>> Stashed changes
    }

    private void FixedUpdate()
    {
        if (_contactsDetector.IsGrounded)
        {
            _playerMover.Move(_inputReader.Direction);
        }

        if (_inputReader.IsJumped && _contactsDetector.IsGrounded)
        {
            _playerMover.Jump();
            _inputReader.StopToJump();
        }

<<<<<<< Updated upstream
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
=======
        if (_contactsDetector.IsCured)
        {
            TakePill();

            _contactsDetector.ChgangeCureStatus();
        }
>>>>>>> Stashed changes
    }

    private void OnDisable()
    {
        _contactsDetector.PillUsed -= TakePill;
    }
}