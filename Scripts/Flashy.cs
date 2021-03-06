﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flashy : MonoBehaviour
{
    public float period = 1;
    public float speed = 90;
    public int numLines = 20;
    public int lineLength = 30;
    public Color32 lineColor = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
    public Color32 bgColor = new Color32(0, 0, 0, byte.MaxValue);

    private Texture2D _texture;
    private Color32[] _colors;
    private const int _height = 128;
    private const int _width = 128;
    private const int _size = _height * _width;

    private List<Vector2[]> _linePositions;
    private Vector2[] _dirs;

    private float _time = 0;

	// Use this for initialization
	void Start ()
    {
        Material material = GetComponent<MeshRenderer>().material;
        _texture = new Texture2D(_height, _width);

        _colors = new Color32[_height * _width];
        for (int i = 0; i < _size; i++)
            _colors[i] = bgColor;
        _texture.SetPixels32(_colors);
        _texture.Apply();

        material.mainTexture = _texture;

        _linePositions = new List<Vector2[]>();
        _dirs = new Vector2[numLines];
        for (int i = 0; i < numLines; i++)
        {
            float angle = Mathf.PI * 2 * i / numLines;
            _dirs[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        _time = period;
	}
	
	// Update is called once per frame
	void Update ()
    {
        DefaultAnimation();
	}

    void DefaultAnimation()
    {
        int startX = _width / 2;
        int startY = _height / 2;

        _time += Time.deltaTime;
        if (_time > period)
        {
            _time -= period;

            Vector2[] lines = new Vector2[numLines];
            for (int i = 0; i < numLines; i++)
            {
                lines[i].x = startX;
                lines[i].y = startY;
            }
            _linePositions.Add(lines);
        }

        for (int i = 0; i < _size; i++)
            _colors[i] = bgColor;

        for (int l = 0; l < _linePositions.Count; l++)
        {
            Vector2[] lines = _linePositions[l];

            bool linesDrawn = false;
            for (int i = 0; i < numLines; i++)
            {
                float angle = (float)i / numLines * 2 * Mathf.PI;
                float xDir = _dirs[i].x;
                float yDir = _dirs[i].y;

                for (int j = 0; j < lineLength; j++)
                {
                    float newX = lines[i].x + j * xDir;
                    float newY = lines[i].y + j * yDir;
                    if (Mathf.Abs(newX) < _width && Mathf.Abs(newY) < _height)
                    {
                        _colors[toPos(newX, newY)] = lineColor;
                        linesDrawn = true;
                    }
                }
                lines[i].x = lines[i].x + xDir * speed * Time.deltaTime;
                lines[i].y = lines[i].y + yDir * speed * Time.deltaTime;
            }
            if (!linesDrawn)
            {
                _linePositions.RemoveAt(l);
                l--;
            }
        }

        _texture.SetPixels32(_colors);
        _texture.Apply();
    }

    private int toPos(float x, float y)
    {
        return (int)Mathf.Abs(y % _height) * _width + (int)Mathf.Abs(x % _width);
    }
}
