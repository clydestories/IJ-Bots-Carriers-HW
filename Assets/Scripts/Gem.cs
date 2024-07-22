using UnityEngine;

public class Gem : Interactable
{
    private SpawnPoint _spawnPoint;

    public void SetSpawnPoint(SpawnPoint point)
    {
        _spawnPoint = point;
    }

    public void PickUp(Transform origin)
    {
        transform.parent = origin;
        transform.position = origin.position;
        _spawnPoint.SetFree();
    }

    public override void Interact(Bot bot)
    {
        PickUp(bot.GemContainer);
        bot.TakeGem(this);
    }
}
