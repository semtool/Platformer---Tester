using UnityEngine;

<<<<<<< Updated upstream
public class Pill : Unit 
{
    [SerializeField] float _healthDose;
=======
public class Pill : ObjectUnit
{
    [SerializeField] private float _healthDose;
>>>>>>> Stashed changes

    public float HealthDose { get; private set; }

    private void Awake()
    {
        HealthDose = _healthDose;
    }
}