using UnityEngine;

public class SpawnerSphere : Spawner<BlowingSphere>
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

    protected override void OnTakeFromPool(BlowingSphere blowingSphere)
    {
        blowingSphere.transform.position = SpawnPosition;
        base.OnTakeFromPool(blowingSphere);
    }

    private void CreateSphere(Cube cube)
    {
        SpawnPosition = cube.transform.position;

        BlowingSphere blowingSphere = Pool.Get();

        blowingSphere.StartLifeCycle();

        blowingSphere.LifeTimeDoned += Release;
    }

    private void Release(BlowingSphere blowingSphere)
    {
        Pool.Release(blowingSphere);

        blowingSphere.LifeTimeDoned -= Release;
    }
}
