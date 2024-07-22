using System;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class BotAnimator : MonoBehaviour
{
    private readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
    private readonly int Interacting = Animator.StringToHash(nameof(Interacting));

    [SerializeField] private Bot _bot;

    private Animator _animator;

    public event Action StoppedInteracting;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _bot.StartedMoving += StartMovingAnimation;
        _bot.StoppedMoving += StartIdleAnimation;
        _bot.Interacting += StartInteractionAnimation;
    }

    private void OnDisable()
    {
        _bot.StartedMoving -= StartMovingAnimation;
        _bot.StoppedMoving -= StartIdleAnimation;
        _bot.Interacting += StartInteractionAnimation;
    }

    private void StartMovingAnimation()
    {
        _animator.SetBool(IsMoving, true);
    }

    private void StartIdleAnimation()
    {
        _animator.SetBool(IsMoving, false);
    }

    private void StartInteractionAnimation()
    {
        _animator.SetTrigger(Interacting);
    }

    public void Interacted()
    {
        StoppedInteracting?.Invoke();
    }
}
