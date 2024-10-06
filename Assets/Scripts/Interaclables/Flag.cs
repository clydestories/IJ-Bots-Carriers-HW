using System;
using UnityEngine;

public class Flag : Interactable
{
    [SerializeField] private Base _basePrefab;

    public event Action<Bot, Vector3, Flag> Interacred;

    public override void Interact(Bot bot)
    {
        Interacred?.Invoke(bot, transform.position, this);
        Destroy(gameObject);
    }
}
