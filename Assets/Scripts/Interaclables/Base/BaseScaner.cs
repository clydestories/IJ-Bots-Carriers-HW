using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseScaner : MonoBehaviour
{
    [SerializeField] private GemSpawner _gemSpawner;

    private List<Gem> _avaliableGems = new List<Gem>();

    public List<Gem> AvaliableGems => _avaliableGems.ToList();

    private void OnEnable()
    {
        _gemSpawner.GemSpawned += AddGem;
    }

    private void OnDisable()
    {
        _gemSpawner.GemSpawned -= AddGem;
    }

    public void AddGem(Gem gem)
    {
        _avaliableGems.Add(gem);
    }

    public void RemoveGem(Gem gem)
    {
        _avaliableGems.Remove(gem);
    }
}
