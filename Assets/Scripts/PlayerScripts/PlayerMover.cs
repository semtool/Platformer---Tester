using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedX;
    [SerializeField] private float _jumpForce;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        _playerAnimator.SetMovingAnimation(direction);

        _playerAnimator.TurnFrontToRight(direction);

        _playerAnimator.TurnFrontToLeft(direction);

        _rigidbody.velocity = new Vector2(_speedX * direction * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);

        _rigidbody.AddForce(new Vector2(0, _jumpForce));

        _playerAnimator.SetJumpingAnimation();
    }
}