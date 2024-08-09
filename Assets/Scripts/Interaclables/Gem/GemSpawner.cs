using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Zenject;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    [SerializeField] private float _delay;

    [Inject] private GemPool _pool;

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
        int freeSpawnPointsAmount = freeSpawnPoints.Count();

        if (freeSpawnPointsAmount > 0)
        {
            int pointIndex = UnityEngine.Random.Range(0, freeSpawnPointsAmount);
            SpawnPoint point = freeSpawnPoints.ElementAt(pointIndex);
            Gem gem = _pool.Get(point.transform.position);
            point.Occupy(gem);
        }
    }
}
