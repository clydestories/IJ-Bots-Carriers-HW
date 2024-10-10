using System;
using UnityEngine;

public class Flag : Interactable
{
    public event Action<Bot, Vector3, Flag> Interacred;

    public override void Interact(Bot bot)
    {
        Interacred?.Invoke(bot, transform.position, this);
        Destroy(gameObject, 3);
    }
}
