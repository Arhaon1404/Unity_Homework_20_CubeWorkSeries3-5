using UnityEngine;
using UnityEngine.Pool;

public class PoolCubes : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxPoolCapacity;

    private ObjectPool<Cube> _pool;

    public ObjectPool<Cube> Pool => _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => CreatePooledItem(), 
        actionOnGet: (poolObject) => OnTakeFromPool(poolObject), 
        actionOnRelease: (poolObject) => OnReturnedToPool(poolObject), 
        actionOnDestroy: (poolObject) => Destroy(poolObject), 
        collectionCheck: false,
        defaultCapacity: _poolCapacity, 
        maxSize: _maxPoolCapacity
        );
    }

    private Cube CreatePooledItem()
    {
        return Instantiate(_prefab, transform.position, transform.rotation);
    }

    private void OnTakeFromPool(Cube poolObject)
    {
        poolObject.transform.position = transform.position + CreateRandomPosition();
        poolObject.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Cube poolObject)
    {
        poolObject.Refresh();
        poolObject.gameObject.SetActive(false);
    }

    private Vector3 CreateRandomPosition()
    {
        float localWidth = transform.localScale.x / 2;
        float localLenght = transform.localScale.z / 2;

        float axesX = Random.Range(-localWidth, localWidth);
        float axesZ = Random.Range(-localLenght, localLenght);

        return new Vector3(axesX, 0f, axesZ);
    }
}
