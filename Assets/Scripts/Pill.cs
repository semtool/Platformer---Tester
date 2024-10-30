using UnityEngine;

public class Pill : MonoBehaviour
{
    private float _healthDose = 10;
    private bool _isUsed;

    public float HealthDose { get; private set; }

    private void Awake()
    {
        HealthDose = _healthDose;
    }

    private void FixedUpdate()
    {
        if (_isUsed)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeStatus()
    {
        _isUsed = true;
    }
}