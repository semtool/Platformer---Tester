using System;
using UnityEngine;

public abstract class CharacterUnit : MonoBehaviour
{
    public event Action PillIsTaken;

    public void TakePill()
    {
        PillIsTaken?.Invoke();
    }
}