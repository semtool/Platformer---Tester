using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _visionRadius;

    public IList <Collider2D> ToDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _visionRadius);

        return  Array.AsReadOnly(colliders);       
    }
}