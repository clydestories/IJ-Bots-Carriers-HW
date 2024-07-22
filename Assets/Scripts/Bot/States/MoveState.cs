using System.Collections.Generic;

public class MoveState : State
{
    private Bot _bot;

    public MoveState(IReadOnlyList<ITransition> transitions, Bot bot) : base(transitions)
    {
        _bot = bot;
    }

    public override void Enter()
    {
        _bot.Move();
    }

    public override void Exit()
    {
        _bot.Stop();
    }

    public override void Update()
    {
        
    }
}
