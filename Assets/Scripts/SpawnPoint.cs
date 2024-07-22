using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _isOccupied;//To event 

    public bool IsOccupied => _isOccupied;

    public void Occupy()
    {
        _isOccupied = true;
    }

    public void SetFree()
    {
        _isOccupied = false;
    }
}
