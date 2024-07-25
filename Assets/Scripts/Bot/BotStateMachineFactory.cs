using System;
using System.Collections.Generic;
using UnityEngine;

public static class BotStateMachineFactory
{
    private const float InteractDistance = 2f;

    public static BotStateMachine CreateBotStateMachine(Bot bot)
    {
        Dictionary<Type, IState> states = new Dictionary<Type, IState>()
        {
            [typeof(IdleState)] = new IdleState(CreateTransitionsFromIdle(bot)),
            [typeof(MoveState)] = new MoveState(CreateTransitionsFromMove(bot), bot),
            [typeof(InteractState)] = new InteractState(CreateTransitionsFromInteract(bot), bot),
        };

        return new BotStateMachine(states);
    }

    private static IReadOnlyList<ITransition> CreateTransitionsFromMove(Bot bot)
    {
        return new List<ITransition>()
        {
            new TransitionTo<InteractState>(() => bot.HasTarget && Vector3.Distance(bot.TargetPosition, bot.transform.position) < InteractDistance)
        };
    }

    private static IReadOnlyList<ITransition> CreateTransitionsFromIdle(Bot bot)
    {
        return new List<ITransition>()
        {
            new TransitionTo<MoveState>(() => bot.HasTarget)
        };
    }

    private static IReadOnlyList<ITransition> CreateTransitionsFromInteract(Bot bot)
    {
        return new List<ITransition>()
        {
            new TransitionTo<MoveState>(() => bot.IsInteracting == false && bot.HasTarget),
            new TransitionTo<IdleState>(() => bot.HasTarget == false)
        };
    }
}