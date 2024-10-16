using System;
using UnityEngine;

public class Gem : Interactable
{
    public event Action<Gem> PickedUp;

    public override void Interact(Bot bot)
    {
        PickUp(bot.GemContainer);
        bot.Interact(this);
    }

    private void PickUp(Transform origin)
    {
        transform.parent = origin;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        PickedUp?.Invoke(this);
    }
}
