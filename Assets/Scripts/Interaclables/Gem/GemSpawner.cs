using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Zenject;
using System;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    [SerializeField] private float _delay;

    [Inject] private GemPool _pool;

    public event Action<Gem> GemSpawned;

    private void Start()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;
            Spawn();
        }
    }

    private void Spawn()
    {
        var freeSpawnPoints = _spawnPoints.Where((point) => point.IsOccupied == false);

        if (freeSpawnPoints.Count() > 0)
        {
            int pointIndex = UnityEngine.Random.Range(0, freeSpawnPoints.Count());
            SpawnPoint point = freeSpawnPoints.ToArray()[pointIndex];
            Debug.Log(point.gameObject.name);
            Gem gem = _pool.Get(point.transform.position);
            gem.SetSpawnPoint(point);
            point.Occupy();
            GemSpawned?.Invoke(gem);
        }
    }
}
