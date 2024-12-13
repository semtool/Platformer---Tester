using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(ContactsDetector))]
[RequireComponent(typeof(Health))]

public class Player : CharacterUnit
{
    private PlayerMover _playerMover;
    private ContactsDetector _contactsDetector;
    private InputReader _inputReader;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _contactsDetector = GetComponent<ContactsDetector>();
    }

    private void OnEnable()
    {
        _contactsDetector.PillUsed += TakePill;
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
    }

    private void OnDisable()
    {
        _contactsDetector.PillUsed -= TakePill;
    }
}