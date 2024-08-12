using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public abstract class Pool<T>  where T : Component
{
    protected ObjectPool<T> ObjectPool;

    private T _prefab;
    private int _defaultCapacity;
    private int _maxSize;

    [Inject] private DiContainer _diContainer;

    public Pool(T prefab, int defaultCapacity, int maxSize)
    {
        _prefab = prefab;
        _defaultCapacity = defaultCapacity;
        _maxSize = maxSize;

        ObjectPool = new ObjectPool<T>
            (
                createFunc: OnCreate,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: (obj) => Object.Destroy(obj),
                collectionCheck: false,
                defaultCapacity: _defaultCapacity,
                maxSize: _maxSize
            );
    }

    public virtual T Get(Vector3 origin)
    {
        return ObjectPool.Get();
    }

    public void Release(T instance)
    {
        ObjectPool.Release(instance);
    }

    protected virtual void OnGet(T instance)
    {
        instance.gameObject.SetActive(true);
    }

    private T OnCreate()
    {
        return _diContainer.InstantiatePrefab(_prefab).GetComponent<T>();
    }

    private void OnRelease(T instance)
    {
        instance.gameObject.SetActive(false);
    }
}
