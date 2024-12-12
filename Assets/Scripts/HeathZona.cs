using System;
using UnityEngine;

public class HeathZona : MonoBehaviour
{
    public event Action DamageZonaIsEntered;
    public event Action DamageZonaIsLeaved;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            DamageZonaIsEntered?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            DamageZonaIsLeaved?.Invoke();
        }
    }
}