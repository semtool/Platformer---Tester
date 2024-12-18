using UnityEngine;

public class Pill : ObjectUnit
{
    [SerializeField] private float _healthDose;

    public float HeathDose { get; private set; }

    private void Awake()
    {
        HeathDose = _healthDose;
    }
}