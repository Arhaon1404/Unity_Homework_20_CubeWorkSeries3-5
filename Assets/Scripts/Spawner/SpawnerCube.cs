using System;
using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private float _spawnRate;

    private WaitForSeconds _cubeSpawnRate;

    public event Action<Cube> CubeDisappeared;

    protected override void Awake()
    {
        base.Awake();

        _cubeSpawnRate = new WaitForSeconds(_spawnRate);

        StartCoroutine(CreateCubeCoroutine());
    }

    protected override void OnTakeFromPool(Cube cube)
    {
        cube.transform.position = transform.position + CreateRandomPosition();
        base.OnTakeFromPool(cube);
    }

    private IEnumerator CreateCubeCoroutine()
    {
        while (true)
        {
            Cube cube = Pool.Get();

            cube.LifeTimeDoned += Release;

            yield return _cubeSpawnRate;
        }
    }

    private void Release(Cube cube)
    {
        CubeDisappeared?.Invoke(cube);

        Pool.Release(cube);

        cube.LifeTimeDoned -= Release;
    }

    private Vector3 CreateRandomPosition()
    {
        float bisection = 2;

        float localWidth = transform.localScale.x / bisection;
        float localLenght = transform.localScale.z / bisection;

        float axesX = UnityEngine.Random.Range(-localWidth, localWidth);
        float axesZ = UnityEngine.Random.Range(-localLenght, localLenght);

        return new Vector3(axesX, 0f, axesZ);
    }
}
