using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyVision))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDictance;
    [SerializeField] private float _monitoringInterval;

    private EnemyVision _enemyVision;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_monitoringInterval);

        _enemyVision = GetComponent<EnemyVision>();
    }

    private void Start()
    {
        StartCoroutine(MonitorSpace());
    }

    public void MoveByNavigator(IReadOnlyList<Vector2> vectors)
    {
        StartCoroutine(MoveToNextPoint(vectors));
    }

    private IEnumerator MonitorSpace()
    {
        while (enabled)
        {
            ToDetectPlayer();

            yield return _wait;
        }
    }

    private void ToDetectPlayer()
    {
        foreach (var collider in _enemyVision.ToDetect())
        {
            if (collider.TryGetComponent(out Player player))
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);

                if (distance > _attackDictance)
                {
                    Move(player.transform.position);
                }
            }
        }
    }

    private IEnumerator MoveToNextPoint(IReadOnlyList<Vector2> pointsOfRout)
    {
        while (enabled)
        {
            for (int i = 0; i < pointsOfRout.Count; i++)
            {
                while (transform.position.x != pointsOfRout[i].x && transform.position.y != pointsOfRout[i].y)
                {
                    Move(pointsOfRout[i]);

                    yield return null;
                }
            }
        }
    }

    private void Move(Vector2 point)
    {
        transform.position = Vector2.MoveTowards(transform.position, point, _speed * Time.deltaTime);
    }
}