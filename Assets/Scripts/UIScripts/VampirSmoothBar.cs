using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirSmoothBar : MonoBehaviour
{
    [SerializeField] private VampirismActivator _activator;
    [SerializeField] private Slider _slider;

    private Coroutine _coroutine;
    private float _volueStep = 0.4f;
    private float _volueMultiplier = 0.01f;
    private int _maxTimeInPercent = 100;

    private void Start()
    {
        _slider.value = GetCurrentBarVolue();
    }

    private void OnEnable()
    {
        _activator.CountChanged += ChangeBarView;
    } 

    private void ChangeBarView()
    {
        ChangeSmoothBarView(GetCurrentBarVolue());
    }

    private void ChangeSmoothBarView(float targetVolue)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Move(targetVolue));
    }

    private IEnumerator Move(float targetValue)
    {
        while (_slider.value != targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _volueStep * Time.deltaTime);

            yield return null;
        }
    }

    private float GetCurrentBarVolue()
    {
        return CalkulateDataForBar() * _volueMultiplier;
    }

    private float CalkulateDataForBar()
    {
        return _activator.CurrentTime * _maxTimeInPercent / _activator.MaxTime;
    }

    private void OnDisable()
    {
        _activator.CountChanged -= ChangeBarView;
    }
}