using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : Interactable
{
    [SerializeField] private BaseScaner _scaner;
    [SerializeField] private List<Bot> _bots = new List<Bot>();

    private int _gemsAmount = 0;

    public event Action<int> GemAmountChanged;

    private Bot[] _avaliableBots
    {
        get
        {
            return _bots.Where((bot) => bot.IsBusy == false).ToArray();
        }
    }

    private void Update()
    {
        if (_scaner.AvaliableGems.Count > 0 && _avaliableBots.Count() > 0)
        {
            Gem gem = _scaner.AvaliableGems[0];
            gem.Choose();
            _avaliableBots[0].Deploy(gem);
            _scaner.RemoveGem(_scaner.AvaliableGems[0]);
        }
    }

    public void Unload()
    {
        _gemsAmount++;
        GemAmountChanged?.Invoke(_gemsAmount);
    }

    public override void Interact(Bot bot)
    {
        Unload();
        bot.LoadOut();
    }
}
