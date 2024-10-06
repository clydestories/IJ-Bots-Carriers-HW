using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Dispatcher))]
public class Base : Interactable
{
    private readonly int _minAmountOfBots = 1;

    [SerializeField] private BaseShop _shop;
    [SerializeField] private Dispatcher _dispatcher;
    [SerializeField] private FlagCreator _flagCreator;

    private bool _isBuildingNewBase = false;

    public event Action<Gem> Unloaded;

    public Dispatcher Dispatcher => _dispatcher;

    private void Awake()
    {
        _dispatcher = GetComponent<Dispatcher>();
    }

    private void Update()
    {
        if (_flagCreator.CurrentFlag != null && _dispatcher.AvaliableBots.Count() > 0 && _dispatcher.BotsCount > _minAmountOfBots) 
        {
            if (_isBuildingNewBase == false) 
            {
                if (_shop.TryBuyBase())
                {
                    _isBuildingNewBase = true;
                    _dispatcher.DispatchToFlag(_flagCreator.CurrentFlag);
                }
            }
        }

        _dispatcher.Dispatch();
    }

    public void OnBaseBuilt(Bot bot, Vector3 position, Flag flag)
    {
        _isBuildingNewBase = false;
    }

    public void Unload(Gem gem)
    {
        Unloaded?.Invoke(gem);
        _shop.AddGem();
    }

    public override void Interact(Bot bot)
    {
        Unload(bot.HandItem as Gem);
        bot.LoadOut();
    }
}
