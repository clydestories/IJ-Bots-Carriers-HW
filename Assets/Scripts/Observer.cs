using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using Zenject;

public class Observer : MonoBehaviour
{
    [SerializeField] private List<Base> _bases = new List<Base>();
    [SerializeField] private BaseCreator _creator;

    [Inject] private GemPool _gemPool;

    private void OnEnable()
    {
        foreach (Base newBase in _bases)
        {
            newBase.Unloaded += ReleaseGem;
        }

        _creator.BaseCreated += SubscribeToBase;
    }

    private void OnDisable()
    {
        foreach (Base newBase in _bases)
        {
            newBase.Unloaded -= ReleaseGem;
        }

        _creator.BaseCreated -= SubscribeToBase;
    }

    private void ReleaseGem(Gem gem)
    {
        _gemPool.Release(gem);
    }

    private void SubscribeToBase(Base newBase)
    {
        _bases.Add(newBase);
        newBase.Unloaded += ReleaseGem;
    }
}
