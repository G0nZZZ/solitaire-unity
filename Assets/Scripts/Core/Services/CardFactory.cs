// Assets/Scripts/Services/CardFactory.cs
using UnityEngine;

/// <summary>
/// Factory that provides Card GameObjects via an ObjectPool.
/// </summary>
public class CardFactory
{
    private readonly ObjectPool<GameObject> _pool;
    private readonly GameObject _cardPrefab;

    public CardFactory(GameObject cardPrefab)
    {
        _cardPrefab = cardPrefab;
        _pool = new ObjectPool<GameObject>(
            create: () => Object.Instantiate(_cardPrefab),
            onGet: go => go.SetActive(true),
            onRelease: go => go.SetActive(false)
        );
    }

    public GameObject Create()
    {
        return _pool.Get();
    }

    public void Release(GameObject go)
    {
        _pool.Release(go);
    }
}