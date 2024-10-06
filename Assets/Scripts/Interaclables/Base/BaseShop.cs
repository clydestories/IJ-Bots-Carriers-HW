using System;
using UnityEngine;

public class BaseShop : MonoBehaviour
{
    private const int BotPrice = 3;
    private const int BasePrice = 5;

    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Base _base;
    [SerializeField] private FlagCreator _flags;

    private int _gemsAmount = 0;

    public event Action<int> GemAmountChanged;

    public void AddGem()
    {
        _gemsAmount++;
        GemAmountChanged?.Invoke(_gemsAmount);
    }

    public void BuyBot()
    {
        if (TrySpendGems(BotPrice))
        {
            Bot newBot = Instantiate(_botPrefab, _base.transform.position, Quaternion.identity);
            newBot.LinkBase(_base);
        }
    }

    public bool TryBuyBase()
    {
        if (TrySpendGems(BasePrice))
        {
            return true;
        }

        return false;
    }

    public void BuyFlag()
    {
        _flags.enabled = true;
    }

    private bool TrySpendGems(int amount)
    {
        if (_gemsAmount >= amount) 
        { 
            _gemsAmount -= amount;
            GemAmountChanged?.Invoke(_gemsAmount);
            return true;
        }

        return false;
    }
}
