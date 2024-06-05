using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int PoolCapacity;
    [SerializeField] protected int MaxPoolCapacity;

    protected Vector3 SpawnPosition;
    protected ObjectPool<T> Pool;

    public event Action ObjectCreated;
    public event Action<bool> ObjectIsActived;

    protected virtual void Awake()
    {
        SpawnPosition = transform.position;

        Pool = new ObjectPool<T>(
        createFunc: () => CreateObject(),
        actionOnGet: (poolObject) => OnTakeFromPool(poolObject),
        actionOnRelease: (poolObject) => OnReturnedToPool(poolObject),
        actionOnDestroy: (poolObject) => Destroy(poolObject),
        collectionCheck: false,
        defaultCapacity: PoolCapacity, 
        maxSize: MaxPoolCapacity
        );
    }

    protected virtual void OnTakeFromPool(T poolObject)
    {
        ObjectIsActived?.Invoke(true);
        poolObject.gameObject.SetActive(true);
    }

    private T CreateObject()
    {
        ObjectCreated?.Invoke();
        return Instantiate(Prefab, SpawnPosition, transform.rotation);
    }

    private void OnReturnedToPool(T poolObject)
    {
        ObjectIsActived?.Invoke(false);
        poolObject.gameObject.SetActive(false);
    }
}
