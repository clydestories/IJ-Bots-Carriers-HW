using System.Collections.Generic;

public class InteractState : State
{
    private Bot _bot;

    public InteractState(IReadOnlyList<ITransition> transitions, Bot bot) : base(transitions)
    {
        _bot = bot;
    }

    public override void Enter()
    {
        _bot.StartInteract();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }
}
