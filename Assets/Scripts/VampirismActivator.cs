using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class VampirismActivator : MonoBehaviour
{
    public event Action AbilityTurnedOn;
    public event Action AbilityTurnedOff;
    public event Action CountChanged;

    private event Action _workStatusChanged;
    private InputReader _inputReader;
    private Coroutine _timerCoroutine;
    private int _minTime = 0;
    private int _workinTime = 6;
    private int _preparingTime = -4;
    private int _interval = 1;
    private int _stepOfChange = 1;
    private bool _isActive = true;

    public float MaxTime { get; private set; }

    public float CurrentLevel { get; private set; }

    private void Awake()
    {
        CurrentLevel = _workinTime;

        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.VampirizmActivated += TurnOnAbility;
        AbilityTurnedOff += PrepareAbility;
        _workStatusChanged += ChangeBoolIndicator;
    }

    private void OnDisable()
    {
        _inputReader.VampirizmActivated -= TurnOnAbility;
    }

    private void TurnOnAbility()
    {
        if (_isActive)
        {
            AbilityTurnedOn?.Invoke();

            TurnOffAbility();
        }
    }

    private void TurnOffAbility()
    {
        ChangeBoolIndicator();

        _timerCoroutine = null;

        MaxTime = _workinTime;

        _timerCoroutine = StartCoroutine(StartTimer(MaxTime, _minTime, AbilityTurnedOff));
    }

    private void PrepareAbility()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);

            _timerCoroutine = null;

            MaxTime = _preparingTime;

            _timerCoroutine = StartCoroutine(StartTimer(_minTime, MaxTime, _workStatusChanged));
        }
    }

    private void ChangeBoolIndicator()
    {
        if (_isActive)
        {
            _isActive = false;
        }
        else
        {
            _isActive = true;
        }
    }

    private IEnumerator StartTimer(float startNumber, float grade, Action action)
    {
        CurrentLevel = startNumber;

        while (CurrentLevel != grade)
        {
            var wait = new WaitForSeconds(_interval);

            yield return wait;

            CurrentLevel = Mathf.MoveTowards(CurrentLevel, grade, _stepOfChange);

            CountChanged?.Invoke();

        }

        action?.Invoke();
    }
}