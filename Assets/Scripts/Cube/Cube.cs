using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private float _lifeTime;
    private float _minLifeTime;
    private float _maxLifeTime;

    private Color _originalColor;
    private bool _isColored;

    private Renderer _renderer;

    private Coroutine _lifeCoroutine;
    private bool _isCoroutineDone;

    public event Action<Cube> CoroutineDoned;

    private void Awake()
    {
        _isColored = false;
        _isCoroutineDone = true;

        _minLifeTime = 2;
        _maxLifeTime = 6;

        _lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    public void StartLifeCycle()
    {
        if (_lifeCoroutine != null && _isCoroutineDone == true)
        {
            StopCoroutine(_lifeCoroutine);
        }

        if (_isCoroutineDone == true)
        {
            _isCoroutineDone = false;
            _lifeCoroutine = StartCoroutine(PasessLifeTimeCoroutine());
        }
    }

    public void Refresh()
    {
        _renderer.material.color = _originalColor;
        _lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        _isColored = false;
    }

    private IEnumerator PasessLifeTimeCoroutine()
    {
        SetColor();

        float elapsedTime = 0;

        while (_lifeTime > elapsedTime)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        _isCoroutineDone = true;

        CoroutineDoned?.Invoke(this);
    }

    private void SetColor()
    {
        if (_isColored == false)
        {
            float redComponent = UnityEngine.Random.Range(0f, 1f);
            float greenComponent = UnityEngine.Random.Range(0f, 1f);
            float blueComponent = UnityEngine.Random.Range(0f, 1f);

            _renderer.material.color = new Color(redComponent, greenComponent, blueComponent);

            _isColored = true;
        }
    }
}