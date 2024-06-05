using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlowingSphere : DisappearObject
{
    [SerializeField] private float _powerExplosion;
    [SerializeField] private float _radius;
    [SerializeField] private float _upwardModifier;

    private Color _finalColor;

    public event Action<BlowingSphere> LifeTimeDoned;

    protected override void Awake()
    {
        base.Awake();

        Renderer.material.color = Color.gray;
        _finalColor = new Color(Renderer.material.color.r, Renderer.material.color.g, Renderer.material.color.b, 0.0f);
    }

    protected override IEnumerator PasessLifeTimeCoroutine()
    {
        float lifeTime = Random.Range(MinLifeTime, MaxLifeTime);

        float ElapsedTime = 0;

        Color originalColor = Renderer.material.color;

        while (ElapsedTime < lifeTime)
        {
            Renderer.material.color = Color.Lerp(originalColor, _finalColor, ElapsedTime / lifeTime);

            ElapsedTime += Time.deltaTime;

            yield return null;
        }

        IsCoroutineDone = true;

        Explode();

        LifeTimeDoned?.Invoke(this);

        Refresh();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_powerExplosion, transform.position, _radius, _upwardModifier);
        }
    }

    private void Refresh()
    {
        Renderer.material.color = Color.gray;
    }
}
