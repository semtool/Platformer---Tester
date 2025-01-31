using UnityEngine;
using System.Collections;

public class CircleVisualisator : MonoBehaviour
{
    [SerializeField] private VampireZona _vampireZona;
    [SerializeField] private Player _player;

    private float _sizeValue = 1000;
    private LineRenderer _lineRenderer;
    private Coroutine _coroutineForDrawing;
    private int _numberOfPeaks;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        _player.ActivityStarted += Draw;
        _player.ActivityStoped += StopDraw;
    }

    private void SetPeacks()
    {
        _sizeValue = 1000;
    }

    private void SetStopPeacks()
    {
        _sizeValue = _vampireZona.AbilityRadius;
    }

    private void ApplySettings()
    {
        _numberOfPeaks = (int)_sizeValue;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.positionCount = _numberOfPeaks;
    }

    private void Draw()
    {
        SetPeacks();

        ApplySettings();

        _coroutineForDrawing = StartCoroutine(ProvidDrawing()); 
    }

    private void StopDraw()
    {
        SetStopPeacks();

        ApplySettings();

        if (_coroutineForDrawing != null)
        {
            StopCoroutine(_coroutineForDrawing);
        }
    }

    private IEnumerator ProvidDrawing()
    {
        while (enabled)
        {
            DrawCircle();

            yield return null;
        }
    }

    private void DrawCircle()
    {
        float radian = 0f;
        float radianScale = 0.01f;

        for (int i = 0; i < _numberOfPeaks; i++)
        {
            radian += (2.0f * Mathf.PI * radianScale);
            float x = _vampireZona.AbilityRadius * Mathf.Cos(radian);
            float y = _vampireZona.AbilityRadius * Mathf.Sin(radian);

            x += gameObject.transform.position.x;
            y += gameObject.transform.position.y;

            Vector3 circlePosition = new Vector3(x, y, 0);

            _lineRenderer.SetPosition(i, circlePosition);
        }
    }

    private void OnDisable()
    {
        _player.ActivityStarted -= Draw;
        _player.ActivityStoped -= StopDraw;
    }
}