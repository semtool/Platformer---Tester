using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;
    private bool _hasUpType = true;
    private bool _hasDownType = false;
    private float _positiveLimit = 0.1f;
    private float _negativeLimit = -0.1f;
    private WaitForSeconds _intervalToNewFase;
    private float _timeOfDelay = 0.3f;

    public int Jumping { get; private set; }

    public int Moving { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _intervalToNewFase = new WaitForSeconds(_timeOfDelay);

        AppointParameters();
    }

    private void Start()
    {
        ExtractParameters();
    }

    public void SetMovingAnimation(float direction)
    {
        _animator.SetFloat(Moving, Math.Abs(direction));
    }

    public void SetJumpingAnimation()
    {
        StartCoroutine(ChangeFaseOfJump());
    }

    public void TurnFrontToRight(float direction)
    {
        if (direction > _positiveLimit)
        {
            _renderer.flipX = false;
        }
    }

    public void TurnFrontToLeft(float direction)
    {
        if (direction < _negativeLimit)
        {
            _renderer.flipX = true;
        }
    }

    private IEnumerator ChangeFaseOfJump()
    {
        ChangeJumpPhaseToUp();

        yield return _intervalToNewFase;

        ChangeJumpPhaseToDown();
    }

    private void AppointParameters()
    {
        Jumping = Animator.StringToHash(nameof(Jumping));
        Moving = Animator.StringToHash(nameof(Moving));
    }

    private void ExtractParameters()
    {
        _animator.GetBool(Jumping);
        _animator.GetBool(Moving);
    }

    private void ChangeJumpPhaseToUp()
    {
        _animator.SetBool(Jumping, _hasUpType);
    }

    private void ChangeJumpPhaseToDown()
    {
        _animator.SetBool(Jumping, _hasDownType);
    }
}