using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxPoolCapacity;

    private ObjectPool<Cube> _pool;

    private WaitForSeconds _cubeSpawnRate;

    private void Awake()
    {
        _cubeSpawnRate = new WaitForSeconds(_spawnRate);

        _pool = new ObjectPool<Cube>(
        createFunc: () => CreatePooledItem(),
        actionOnGet: (poolObject) => OnTakeFromPool(poolObject),
        actionOnRelease: (poolObject) => OnReturnedToPool(poolObject),
        actionOnDestroy: (poolObject) => Destroy(poolObject),
        collectionCheck: false,
        defaultCapacity: _poolCapacity,
        maxSize: _maxPoolCapacity
        );

        StartCoroutine(CreateCubeCoroutine());
    }

    private IEnumerator CreateCubeCoroutine()
    {
        while (true)
        {
            Cube cube = _pool.Get();

            cube.CoroutineDoned += ReleaseCube;

            yield return _cubeSpawnRate;
        }
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);

        cube.CoroutineDoned -= ReleaseCube;
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
        float bisection = 2;

        float localWidth = transform.localScale.x / bisection;
        float localLenght = transform.localScale.z / bisection;

        float axesX = Random.Range(-localWidth, localWidth);
        float axesZ = Random.Range(-localLenght, localLenght);

        return new Vector3(axesX, 0f, axesZ);
    }
}
