using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode Jump = KeyCode.Space;

    public bool IsJumped { get; private set; }

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(HorizontalAxis);

        if (Input.GetKeyDown(Jump))
        {
            IsJumped = true;
        }
    }

    public void StopToJump()
    {
        IsJumped = false;
    }
}