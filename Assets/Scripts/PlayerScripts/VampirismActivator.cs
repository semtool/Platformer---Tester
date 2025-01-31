using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class VampirismActivator : MonoBehaviour
{
    public event Action AbilityTurnedOn;
    public event Action AbilityTurnedOff;
    public event Action CountChanged;

    private InputReader _inputReader;
    private Coroutine _timerCoroutine;
    private WaitForSeconds _wait;
    private float _finishTime;
    private float _workinTime = 6;
    private float _endTime = 0;
    private float _preparingTime = 4;
    private int _timeInterval = 1;
    private float _stepOfChange = 1;
    private bool _isActive = true;

    public float MaxTime { get; private set; }

    public float CurrentTime { get; private set; }

    private void Awake()
    {
        CurrentTime = _workinTime;

        _inputReader = GetComponent<InputReader>();

        _wait = new WaitForSeconds(_timeInterval);
    }

    private void OnEnable()
    {
        _inputReader.VampirizmActivated += TurnOnAbilityProcess;
    }

    private void TurnOnAbilityProcess()
    {
        if (_isActive)
        {
            AbilityTurnedOn?.Invoke(); 

            TurnOnAbility();
        }
    }

    private void TurnOnAbility()
    {
        _isActive = false;

        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }

        MaxTime = _workinTime;

        _timerCoroutine = StartCoroutine(StartTimer(MaxTime));
    }

    private IEnumerator StartTimer(float startNumber)
    {
        CurrentTime = startNumber;

        _finishTime = _endTime;

        while (CurrentTime != _finishTime)
        {
            yield return _wait;

            CurrentTime = Mathf.MoveTowards(CurrentTime, _finishTime, _stepOfChange);

            if (CurrentTime == _finishTime)
            {
                MaxTime = _preparingTime;

                _finishTime = MaxTime;

                AbilityTurnedOff?.Invoke();
            }

            CountChanged?.Invoke();
        }

        _isActive = true;
    }

    private void OnDisable()
    {
        _inputReader.VampirizmActivated -= TurnOnAbilityProcess;
    }
}