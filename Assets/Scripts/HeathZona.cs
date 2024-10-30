using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Enemy))]
public class HeathZona : MonoBehaviour
{
    private Player _player;
    private Enemy _enemy;
    private Coroutine _coroutineForChanging;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            ActivateCoroutine(_enemy.Damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            DectivateCoroutine();
        }
    }

    private void ActivateCoroutine(float damage)
    {
        _coroutineForChanging = StartCoroutine(SpendHealth(damage));
    }

    private void DectivateCoroutine()
    {
        if (_coroutineForChanging != null)
        {
            StopCoroutine(_coroutineForChanging);
        }
    }

    private IEnumerator SpendHealth(float damage)
    {
        while (true)
        {
            _player.TakeDamage(damage);

            var wait = new WaitForSeconds(5);

            yield return wait;
        }
    }
}