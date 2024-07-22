using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Transform _gemContainer;

    private BotStateMachine _stateMachine;
    private bool _isBusy = false;
    private Interactable _target;
    private NavMeshAgent _agent;
    private Interactable _handItem;

    public bool IsBusy => _isBusy;
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
                throw new System.Exception("Don't have target");
            }
        }
    }

    public bool HasTarget => _target != null;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine = BotStateMachineFactory.CreateBotStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.EnterState<IdleState>();
    }

    private void Update()
    {
        _stateMachine.UpdateState();
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
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void LoadOut()//from base
    {
        _isBusy = false;
        Destroy(_handItem.gameObject);
        _target = null;
    }

    public void TakeGem(Gem gem)//form gem
    {
        _handItem = gem;
        _target = _base;
    }

    public void Interact()
    {
        _target.Interact(this);
    }
}
