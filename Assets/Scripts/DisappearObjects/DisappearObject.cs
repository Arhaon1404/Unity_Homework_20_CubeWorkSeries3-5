using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class DisappearObject : MonoBehaviour
{
    protected float MinLifeTime;
    protected float MaxLifeTime;
    protected WaitForSeconds LifeTime;

    protected Renderer Renderer;

    protected Coroutine LifeCoroutine;
    protected bool IsCoroutineDone;

    protected virtual void Awake()
    {
        IsCoroutineDone = true;

        MinLifeTime = 2;
        MaxLifeTime = 6;

        Renderer = GetComponent<Renderer>();

        LifeTime = new WaitForSeconds(Random.Range(MinLifeTime, MaxLifeTime));
    }

    public void StartLifeCycle()
    {
        if (LifeCoroutine != null && IsCoroutineDone == true)
        {
            StopCoroutine(LifeCoroutine);
        }

        if (IsCoroutineDone == true)
        {
            IsCoroutineDone = false;
            LifeCoroutine = StartCoroutine(PasessLifeTimeCoroutine());
        }
    }

    protected virtual IEnumerator PasessLifeTimeCoroutine()
    {
        yield return null;
    }
}
