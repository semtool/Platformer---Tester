using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform _coinsTransform;

    private Transform[] _coins;

    private void Awake()
    {
        _coins = new Transform[_coinsTransform.childCount];
    }

    private void Start()
    {
        CreateSeveralCoins();
    }

    private void CreateSeveralCoins()
    {
        for (int i = 0; i < _coins.Length; i++)
        {
            _coins[i] = _coinsTransform.GetChild(i);

            Instantiate(_coinPrefab, _coins[i].position, Quaternion.identity);
        }
    }
}