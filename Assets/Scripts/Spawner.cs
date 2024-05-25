using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private PoolCubes _poolCubes;

    private ObjectPool<Cube> _pool;

    private void Start()
    {
        _pool = _poolCubes.Pool;
        InvokeRepeating(nameof(CreateCube), 0.0f, _spawnRate);
    }

    private void CreateCube()
    {
        Cube cube = _pool.Get();

        cube.CoroutineDoned += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);

        cube.CoroutineDoned -= ReleaseCube;
    }
}
