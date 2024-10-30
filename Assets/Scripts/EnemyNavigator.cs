using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator : MonoBehaviour
{
    [SerializeField] private List<Vector2> _routPoints;
    public List<Vector2> Router { get; private set; }

    private void Awake()
    {
        Router = new List<Vector2>();
    }

    private void Start()
    {
        CreateRout();
    }

    private void CreateRout()
    {
        Router.Add(transform.position);

        foreach (var point in _routPoints)
        {
            Router.Add(point);
        }
    }
}