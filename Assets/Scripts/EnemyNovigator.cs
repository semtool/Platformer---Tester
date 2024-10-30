using System.Collections.Generic;
using UnityEngine;

public class EnemyNovigator : MonoBehaviour
{
    [SerializeField] private List<Vector2> _position;

    public IReadOnlyList<Vector2> GetAllPoints()
    {
        return _position;
    }
}