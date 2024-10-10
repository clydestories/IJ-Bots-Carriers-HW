using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Observer : MonoBehaviour
{
    [SerializeField] private List<Base> _bases = new List<Base>();
    [SerializeField] private BaseCreator _creator;
    [SerializeField] private List<Gem> _chosenGems;

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

    public void ChooseGem(Gem gem)
    {
        _chosenGems.Add(gem);
    }

    public bool IsGemChosen(Gem gem)
    {
        return _chosenGems.Contains(gem);
    }

    private void ReleaseGem(Gem gem)
    {
        _gemPool.Release(gem);
        _chosenGems.Remove(gem);
    }

    private void SubscribeToBase(Base newBase)
    {
        _bases.Add(newBase);
        newBase.Unloaded += ReleaseGem;
    }
}
