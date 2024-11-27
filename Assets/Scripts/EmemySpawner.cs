using UnityEngine;

public class EmemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPointsTransform;

    private Transform[] _allPointsTransform;
    private Vector2 _startPosition;

    private void Awake()
    {
        _allPointsTransform = new Transform[_spawnPointsTransform.childCount];
    }
    private void Start()
    {
        CreateSeveralEnemies();
    }

    private void CreateSeveralEnemies()
    {
        for (int i = 0; i < _allPointsTransform.Length; i++)
        {
            _allPointsTransform[i] = _spawnPointsTransform.GetChild(i);

            _startPosition = _allPointsTransform[i].position;

            Enemy enemy = Instantiate(_enemyPrefab, _startPosition, Quaternion.identity);

            if (_allPointsTransform[i].TryGetComponent(out EnemyNavigator enemyNavigator))
            {
                if (enemyNavigator.Router.Count > 0)
                {
                    enemy.MoveToNextPoint(enemyNavigator.Router);
                }
            }
        }
    }
}