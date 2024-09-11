using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IterDiamondHeightMap : IGenerate
{
    private int _size;
    private float _roughness;
    private float[,] _heightMap;
    private int _seed;
    private float _standartValue;

    public IterDiamondHeightMap(int size, float roughness, int seed, float standartValue)
    {
        _size = (int)(Math.Pow(2, size) + 1);
        _roughness = roughness;
        _heightMap = new float[_size, _size];
        _seed = seed;
        _standartValue = standartValue;

        UnityEngine.Random.InitState(_seed);

        Debug.Log($"Сид: {_seed} \n" +
            $"Размер: {_size} \n");
    }

    public float[,] Generate()
    {
        InitHeights();
        InitAngles();

        GenerateHeights();

        ToSecondPower();
        Normilize();

        return _heightMap;
    }

    private void GenerateHeights()
    {
        int stepSize = _size - 1;
        float scale = _roughness;
        int iter = 1;

        while (stepSize > 1)
        {
            int halfStep = stepSize / 2;

            // Diamond step
            for (int y = halfStep; y < _size; y += stepSize)
            {
                for (int x = halfStep; x < _size; x += stepSize)
                {
                    float average = (GetHeight(x - halfStep, y - halfStep) +
                                     GetHeight(x + halfStep, y - halfStep) +
                                     GetHeight(x - halfStep, y + halfStep) +
                                     GetHeight(x + halfStep, y + halfStep)) / 4.0f;

                    _heightMap[x, y] = average + (UnityEngine.Random.value * 2.0f - 1.0f) * scale + (float)Math.Pow(UnityEngine.Random.Range(-1f, 1f), iter) *
                        UnityEngine.Random.Range(-stepSize * 2f * _roughness, stepSize * 2f * _roughness);
                }
            }

            // Square step
            for (int y = 0; y < _size; y += halfStep)
            {
                for (int x = (y + halfStep) % stepSize; x < _size; x += stepSize)
                {
                    float average = (GetHeight((x + halfStep) % _size, y) +
                                     GetHeight((x - halfStep + _size) % _size, y) +
                                     GetHeight(x, (y + halfStep) % _size) +
                                     GetHeight(x, (y - halfStep + _size) % _size)) / 4.0f;

                    _heightMap[x, y] = average + (UnityEngine.Random.value * 2.0f - 1.0f) * scale + (float)Math.Pow(UnityEngine.Random.Range(-1f, 1f), iter) *
                        UnityEngine.Random.Range(-stepSize * 2f * _roughness, stepSize * 2f * _roughness);
                }
            }

            stepSize /= 2;
            scale *= _roughness;
            iter++;
        }
    }

    private float GetHeight(int x, int y)
    {
        if ((x > _size || x < 0) || (y > _size || y < 0))
        {
            return _standartValue;
        }
        else
        {
            return _heightMap[x, y];
        }
    }

    private void InitHeights()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _heightMap[i, j] = _standartValue;
            }
        }
    }

    private void InitAngles()
    {
        _heightMap[0, 0] = UnityEngine.Random.Range(-_size, _size) % _size;
        _heightMap[0, _size - 1] = UnityEngine.Random.Range(-_size, _size) % _size;
        _heightMap[_size - 1, 0] = UnityEngine.Random.Range(-_size, _size) % _size;
        _heightMap[_size - 1, _size - 1] = UnityEngine.Random.Range(-_size, _size) % _size;
    }

    private void ToSecondPower()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                var value = Mathf.Pow(_heightMap[i, j], 2);
                _heightMap[i, j] = value;
            }
        }
    }

    private void Normilize()
    {
        var max = _heightMap[0, 0];

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                if (max < _heightMap[i, j])
                {
                    max = _heightMap[i, j];
                }
            }
        }

        if (max == float.MinValue) return;

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _heightMap[i, j] = _heightMap[i, j] / max;
            }
        }
    }
}
