using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : Interactable
{
    [SerializeField] private BaseCheckArea _area;
    [SerializeField] private List<Bot> _bots = new List<Bot>();

    private int _count = 0;

    private Bot[] _avaliableBots
    {
        get
        {
            return _bots.Where((bot) => bot.IsBusy == false).ToArray();
        }
    }

    private void Update()
    {
        if (_area.AvaliableGems.Count > 0 && _avaliableBots.Count() > 0)
        {
            Debug.Log($"{_avaliableBots[0].gameObject.name} sent for {_area.AvaliableGems[0].gameObject.GetInstanceID()}");
            _avaliableBots[0].Send(_area.AvaliableGems[0]);
            _area.RemoveGem(_area.AvaliableGems[0]);
        }

        foreach (Bot bot in _avaliableBots)
        {
            Debug.Log(bot.gameObject.name);
        }
    }

    public void Unload()
    {
        _count++;
    }

    public override void Interact(Bot bot)
    {
        Unload();
        bot.LoadOut();
    }
}
