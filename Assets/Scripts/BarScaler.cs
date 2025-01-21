using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class BarScaler : MonoBehaviour
{
    [SerializeField] public Slider _barScale;
    [SerializeField] public UnitLife _health;

    private float _volueMultiplier = 0.01f;
    private int _maxHealthInPercent = 100;

    private void Start()
    {
        _barScale.value = GetCurrentBarVolue();
    }

    private void OnEnable()
    {
        _health.CurrentHealthChanged += ChangeBarView;
    }

    private void OnDisable()
    {
        _health.CurrentHealthChanged -= ChangeBarView;
    }

    public abstract void ChangeBarView();

    public float GetCurrentBarVolue()
    {
        return CalkulateDataForBar(_health.CurrentHealth, _health.MaxHealth) * _volueMultiplier;
    }

    private float CalkulateDataForBar(float ñurrentData, float maxData)
    {
        return ñurrentData * _maxHealthInPercent / maxData;
    }
}