using UnityEngine;

public class PillsSpawner : MonoBehaviour
{
    [SerializeField] private Pill _medicinePrefab;
    [SerializeField] private Transform _medicineTransform;

    private Transform[] _pills;

    private void Awake()
    {
        _pills = new Transform[_medicineTransform.childCount];
    }

    private void Start()
    {
        CreatePills();
    }

    private void CreatePills()
    {
        for (int i = 0; i < _pills.Length; i++)
        {
            _pills[i] = _medicineTransform.GetChild(i);

            Instantiate(_medicinePrefab, _pills[i].position, Quaternion.identity);
        }
    }
}