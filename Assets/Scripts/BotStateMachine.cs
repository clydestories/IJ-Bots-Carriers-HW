using System;
using System.Collections.Generic;

public class BotStateMachine : FiniteStateMachine
{
    public BotStateMachine(Dictionary<Type, IState> states) : base(states)
    {
    }
}
