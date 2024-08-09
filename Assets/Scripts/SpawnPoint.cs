using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    public void Occupy(Gem gem)
    {
        gem.PickedUp += SetFree;
        IsOccupied = true;
    }

    private void SetFree(Gem gem)
    {
        gem.PickedUp -= SetFree;
        IsOccupied = false;
    }
}
