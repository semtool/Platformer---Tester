using UnityEngine;

public class Pill : ObjectUnit
{
    [SerializeField] private float _healthDose;

    public float HealthDose { get; private set; }

    private void Awake()
    {
        HealthDose = _healthDose;
    }
}