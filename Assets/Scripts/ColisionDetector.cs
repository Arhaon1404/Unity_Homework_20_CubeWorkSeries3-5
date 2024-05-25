using UnityEngine;

public class ColisionDetector : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            cube.StartLifeCycle();
        }
    }
}
