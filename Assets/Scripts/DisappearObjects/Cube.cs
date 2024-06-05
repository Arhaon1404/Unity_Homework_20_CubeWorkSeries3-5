using System;
using System.Collections;
using UnityEngine;

public class Cube : DisappearObject
{
    private Color _originalColor;
    private bool _isColored;

    public event Action<Cube> LifeTimeDoned;

    protected override void Awake()
    {
        base.Awake();

        _isColored = false;

        _originalColor = Renderer.material.color;
    }

    protected override IEnumerator PasessLifeTimeCoroutine()
    {
        SetColor();

        yield return LifeTime;

        IsCoroutineDone = true;

        LifeTimeDoned?.Invoke(this);

        Refresh();
    }

    private void SetColor()
    {
        if (_isColored == false)
        {
            float redComponent = UnityEngine.Random.Range(0f, 1f);
            float greenComponent = UnityEngine.Random.Range(0f, 1f);
            float blueComponent = UnityEngine.Random.Range(0f, 1f);

            Renderer.material.color = new Color(redComponent, greenComponent, blueComponent);

            _isColored = true;
        }
    }

    private void Refresh()
    {
        Renderer.material.color = _originalColor;
        LifeTime = new WaitForSeconds(UnityEngine.Random.Range(MinLifeTime, MaxLifeTime));
        _isColored = false;
    }
}