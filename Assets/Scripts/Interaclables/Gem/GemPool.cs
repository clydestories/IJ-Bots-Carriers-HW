using UnityEngine;

public class GemPool : Pool<Gem>
{
    private Vector3 _nextOrigin;

    public GemPool(Gem prefab, int defaultCapacity, int maxSize) : base(prefab, defaultCapacity, maxSize)
    {
    }

    public override Gem Get(Vector3 origin)
    {
        _nextOrigin = origin;
        return ObjectPool.Get();
    }

    protected override void OnGet(Gem instance)
    {
        instance.transform.position = _nextOrigin;
        instance.gameObject.SetActive(true);
    }
}
