using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    [SerializeField] private BaseScaner _scaner;
    [SerializeField] private List<Bot> _bots = new List<Bot>();

    public int BotsCount => _bots.Count;

    public Bot[] AvaliableBots
    {
        get
        {
            return _bots.Where((bot) => bot.IsBusy == false).ToArray();
        }
    }

    public void DispatchToFlag(Flag flag)
    {
        if (AvaliableBots.Count() > 0)
        {
            AvaliableBots[0].Deploy(flag);
        }
    }

    public void AddBot(Bot bot)
    {
        _bots.Add(bot);
    }

    public void Dispatch()
    {
        if (_scaner.AvaliableGems.Count > 0 && AvaliableBots.Count() > 0)
        {
            Gem gem = _scaner.AvaliableGems[0];

            if (gem.IsChosen == false)
            {
                gem.Choose();
                AvaliableBots[0].Deploy(gem);
            }

            _scaner.RemoveGem(_scaner.AvaliableGems[0]);
        }
    }

    public void LoseBot(Bot bot)
    {
        _bots.Remove(bot);
    }
}
