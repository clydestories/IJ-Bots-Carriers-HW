using System;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(FlagCreator))]
public class BaseCreator : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    [Inject] private IInstantiator _instatiator;

    public event Action<Base> BaseCreated;

    public void CreateBase(Bot bot, Vector3 position, Flag flag)
    {
        flag.Interacred -= CreateBase;
        Base newBase = _instatiator.InstantiatePrefabForComponent<Base>(_basePrefab);
        newBase.transform.position = position;
        newBase.transform.rotation = _basePrefab.transform.rotation;
        BaseCreated?.Invoke(newBase);
        bot.LinkBase(newBase);
        bot.ResetSelf();
    }
}
