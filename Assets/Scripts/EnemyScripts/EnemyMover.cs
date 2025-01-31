using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _approachSpeed;

    public float PatrolSpeed { get; private set; }

    public float ApproachSpeed { get; private set; }

    private void Awake()
    {
        PatrolSpeed = _patrolSpeed;
        ApproachSpeed = _approachSpeed;
    }

    public void MoveByNavigator(IReadOnlyList<Vector2> vectors, float speed)
    {
        StartCoroutine(MoveToNextPo(vectors, speed));
    }

    public void Move(Vector2 point, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);
    }

    private IEnumerator MoveToNextPo(IReadOnlyList<Vector2> pointsOfRout, float speed)
    {
        while (enabled)
        {
            for (int i = 0; i < pointsOfRout.Count; i++)
            {
                while (transform.position.x != pointsOfRout[i].x && transform.position.y != pointsOfRout[i].y)
                {
                    Move(pointsOfRout[i], speed);

                    yield return null;
                }
            }
        }
    }
}