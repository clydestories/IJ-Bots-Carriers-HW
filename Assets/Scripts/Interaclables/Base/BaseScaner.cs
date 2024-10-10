using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BaseScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _scanLayers;

    [Inject] private Observer _observer;

    private List<Gem> _avaliableGems = new();

    public List<Gem> AvaliableGems => _avaliableGems.ToList();

    private void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius, _scanLayers);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Gem gem))
            {
                if (_avaliableGems.Contains(gem) == false && _observer.IsGemChosen(gem) == false)
                {
                    _avaliableGems.Add(gem);
                }
            }
        }
    }

    public void RemoveGem(Gem gem)
    {
        _avaliableGems.Remove(gem);
    }
}
