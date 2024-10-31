using UnityEngine;

public class Pill : Unit 
{
    [SerializeField] float _healthDose;

    public float HealthDose { get; private set; }

    private void Awake()
    {
        HealthDose = _healthDose;
    }
}