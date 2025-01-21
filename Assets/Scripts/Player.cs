using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(ContactsDetector))]
[RequireComponent(typeof(UnitLife))]
[RequireComponent(typeof(UnitDamageZona))]
[RequireComponent(typeof(VampirismActivator))]

public class Player : PersonUnit
{
    public event Action ActivityStarted;
    public event Action ActivityStoped;

    private PlayerMover _playerMover;
    private ContactsDetector _contactsDetector;
    private InputReader _inputReader;
    private UnitDamageZona _playerDamageZona;
    private VampirismActivator _activator;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _contactsDetector = GetComponent<ContactsDetector>();   
        _playerDamageZona = GetComponent<UnitDamageZona>();
        _activator = GetComponent<VampirismActivator>();
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

    private void OnEnable()
    {
        _activator.AbilityTurnedOn += UseAbility;
        _activator.AbilityTurnedOff += StopToUseAbility;
    }

    private void UseAbility()
    {
        if (gameObject.TryGetComponent(out Player player))
        {
            _playerDamageZona.SetMaxRadius();

            _playerDamageZona.MonitorArea();

            ActivityStarted?.Invoke();
        }
    }

    private void StopToUseAbility()
    {
        _playerDamageZona.SetMinRadius();

        ActivityStoped?.Invoke();
    }

    private void OnDisable()
    {
        _activator.AbilityTurnedOn -= UseAbility;
        _activator.AbilityTurnedOff -= StopToUseAbility;
    }
}