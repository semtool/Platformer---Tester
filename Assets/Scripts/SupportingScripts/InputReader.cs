using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action VampirizmActivated;
 
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode Jump = KeyCode.Space;
    private const KeyCode Activation = KeyCode.V;

    public bool IsJumped { get; private set; }

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(HorizontalAxis);

        if (Input.GetKeyDown(Jump))
        {
            IsJumped = true;
        }

        if (Input.GetKeyDown(Activation))
        {
            VampirizmActivated?.Invoke();
        }
    }

    public void StopToJump()
    {
        IsJumped = false;
    }
}