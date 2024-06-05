using System;
using System.Collections;
using UnityEngine;

public class SpawnerSphere : Spawner<Sphere>
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private void OnEnable()
    {
        _spawnerCube.CubeDisappeared += CreateSphere;
    }

    private void OnDisable()
    {
        _spawnerCube.CubeDisappeared -= CreateSphere;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTakeFromPool(Sphere sphere)
    {
        sphere.transform.position = SpawnPosition;
        base.OnTakeFromPool(sphere);
    }

    private void CreateSphere(Cube cube)
    {
        SpawnPosition = cube.transform.position;

        Sphere sphere = Pool.Get();

        sphere.StartLifeCycle();

        sphere.LifeTimeDoned += Release;
    }

    private void Release(Sphere sphere)
    {
        Pool.Release(sphere);

        sphere.LifeTimeDoned -= Release;
    }
}
