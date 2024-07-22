using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Transform _gemContainer;
    [SerializeField] private BotAnimator _animator;

    private BotStateMachine _stateMachine;
    private bool _isBusy = false;
    private bool _isInteracting = false;
    private Interactable _target;
    private NavMeshAgent _agent;
    private Interactable _handItem;

    public event Action StartedMoving;
    public event Action StoppedMoving;
    public event Action Interacting;
    public event Action StoppedInteracting;

    public bool IsBusy => _isBusy;
    public bool IsInteracting => _isInteracting;
    public Transform GemContainer => _gemContainer;

    public Vector3 TargetPosition
    {
        get
        {
            if (HasTarget)
            {
                return _target.transform.position;
            }
            else
            {
                throw new Exception("Don't have target");
            }
        }
    }

    public bool HasTarget => _target != null;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine = BotStateMachineFactory.CreateBotStateMachine(this);
    }

    private void OnEnable()
    {
        Interacting += () => _isInteracting = true;
        StoppedInteracting += () => _isInteracting = false;
        _animator.StoppedInteracting += () => CompleteInteract();
    }

    private void Start()
    {
        _stateMachine.EnterState<IdleState>();
    }

    private void Update()
    {
        _stateMachine.UpdateState();
    }

    private void OnDisable()
    {
        Interacting -= () => _isInteracting = true;
        StoppedInteracting -= () => _isInteracting = false;
        _animator.StoppedInteracting -= () => CompleteInteract();
    }

    public void Send(Gem gem)
    {
        _isBusy = true;
        _target = gem;
    }

    public void Move()
    {
        _agent.SetDestination(TargetPosition);
        _agent.isStopped = false;
        StartedMoving?.Invoke();
    }

    public void Stop()
    {
        _agent.isStopped = true;
        StoppedMoving?.Invoke();
    }

    public void LoadOut()
    {
        _isBusy = false;
        Destroy(_handItem.gameObject);
        _target = null;
    }

    public void TakeGem(Gem gem)
    {
        _handItem = gem;
        _target = _base;
    }

    public void StartInteract()
    {
        Interacting?.Invoke();
    }

    private void CompleteInteract()
    {
        _target.Interact(this);
        StoppedInteracting?.Invoke();
    }
}
