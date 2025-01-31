using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(ContactsDetector))]
[RequireComponent(typeof(UnitLife))]
[RequireComponent(typeof(VampireZona))]
[RequireComponent(typeof(VampirismActivator))]

public class Player : MonoBehaviour
{
    public event Action ActivityStarted;
    public event Action ActivityStoped;

    private PlayerMover _playerMover;
    private ContactsDetector _contactsDetector;
    private InputReader _inputReader;
    private VampirismActivator _activator;
    private VampireZona _playerDamageZona;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _contactsDetector = GetComponent<ContactsDetector>();
        _playerDamageZona = GetComponent<VampireZona>();
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

        _playerDamageZona.StopMonitoringArea();

        ActivityStoped?.Invoke();
    }

    private void OnDisable()
    {
        _activator.AbilityTurnedOn -= UseAbility;
        _activator.AbilityTurnedOff -= StopToUseAbility;
    }
}