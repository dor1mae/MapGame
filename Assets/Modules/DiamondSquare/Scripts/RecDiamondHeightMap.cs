using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RecDiamondHeightMap
{
    private float[,] _map;
    private ReferenceArray<float> map;
    private float _roughness;
    private float _standart;
    private float _angle;
    private int _size;
    private int _max;
    private float _basicHeight;
    private Func<float, float, float> OnUnityRandom;
    private System.Random _random;

    public RecDiamondHeightMap(int size, float basicHeight, float roughness, float standart, float angle)
    {
        _size = (int)Mathf.Pow(2, size) + 1; //алгоритм работает с картой размера 2 в степени i + 1
        _map = new float[_size, _size];
        _max = _size - 1; //возможно, свойство массивов в шарпах
        _basicHeight = basicHeight; //будет использовываться в случае, когда идет обращение к внешним клеткам
        _roughness = roughness;
        _standart = standart;
        _angle = angle;
        _random = new System.Random();
    }

    private void InitHeights() // устанавливаем дефолтные значения высот
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
                _map[i, j] = _standart;
        }
    }

    private void RandomAngles() // инициализируем случайные значения в углах перед началом генерации
    {
        _map[0, 0] = UnityEngine.Random.Range(-_angle, _angle);
        _map[0, _max] = UnityEngine.Random.Range(-_angle, _angle);
        _map[_max, 0] = UnityEngine.Random.Range(-_angle, _angle);
        _map[_max, _max] = UnityEngine.Random.Range(-_angle, _angle);
    }

    public float[,] GenerateMap()
    {
        InitHeights();
        RandomAngles();

        OnUnityRandom += (left, right) =>
        {
            float range = right - left;
            float sample = (float)_random.NextDouble();
            return sample * range + left;
        };

        CreateHeightMap();
        ToSecondPower();
        Normilize();
        return map.Array;
    }

    private void CreateHeightMap()
    {
        int step = _max / 2; // шаг для прохождения всех точек на карте
        int iter = 1; // используется для возведения в степень случайного числа, что мы добавляем к среднему значению 4 точек

        map = new(_map);

        //System.Random rnd = new System.Random();
        Start(step, iter);
    }

    private void Start(int step, int iter)
    {
        int x = step; int y = step;
        float value = GetDiomandOne(x, y, step, iter);
        map.Array[x, y] = value;

        RecSquareOne(x, y + step, step, iter++);
        RecSquareOne(x, y - step, step, iter++);
        RecSquareOne(x + step, y, step, iter++);
        RecSquareOne(x - step, y, step, iter++);
    }

    private float GetDiomandOne(int x, int y, int step, int iter)
    {
        return Displace((GetHeight(x - step, y - step) +
                        GetHeight(x - step, y + step) +
                        GetHeight(x + step, y + step) +
                        GetHeight(x + step, y - step)) / 4,
                        iter, step);
    }

    private float GetSquareOne(int x, int y, int step, int iter)
    {
        return Displace((GetHeight(x, y - step) +
                        GetHeight(x, y + step) +
                        GetHeight(x + step, y) +
                        GetHeight(x + step, y)) / 4,
                        iter, step);
    }

    private void RecSquareOne(int x, int y, int step, int iter)
    {
        bool isEnd = false;

        if (GetHeight(x, y) == _basicHeight) // обращаемся ли мы к ячейке вне карты
        {
            return;
        }

        if (step == 1) { isEnd = true; }

        float value = GetSquareOne(x, y, step, iter);
        map.Array[x, y] = value;

        if (!isEnd)
        {
            step = step / 2;
            if (step < 1)
            {
                step = 1;
            }
        }
        else return;

        RecDiamondOne(x + step, y + step, step, iter);
        RecDiamondOne(x + step, y - step, step, iter);
        RecDiamondOne(x - step, y + step, step, iter);
        RecDiamondOne(x - step, y - step, step, iter);
    }

    private void RecDiamondOne(int x, int y, int step, int iter)
    {
        if (GetHeight(x, y) == _basicHeight)
        {
            return;
        }

        float value = GetDiomandOne(x, y, step, iter);
        map.Array[x, y] = value;

        RecSquareOne(x, y + step, step, iter++);
        RecSquareOne(x, y - step, step, iter++);
        RecSquareOne(x + step, y, step, iter++);
        RecSquareOne(x - step, y, step, iter++);
    }

    private float Displace(float val, int iter, int step)
    {
        float rnd2 = UnityEngine.Random.Range(-step * 2f * _roughness, step * 2f * _roughness);
        float rnd = Mathf.Pow(UnityEngine.Random.Range(0f, 1f), iter);
        return val + rnd * rnd2;
    }

    private float GetHeight(int x, int y)
    {
        if ((x < 0 || x > _max) || (y < 0 || y > _max))
        {
            return _basicHeight;
        }
        else
        {
            return map.Array[x, y];
        }
    }

    private void ToSecondPower()
    {
        for(int i = 0; i < _size; i++)
        {
            for(int j = 0; j < _size; j++)
            {
                var value = Mathf.Pow(map.Array[i, j], 2);
                map.Array[i, j] = value;
            }
        }
    }

    private void Normilize()
    {
        var max = map.Array[0, 0];

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                if(max < map.Array[i, j])
                {
                    max = map.Array[i, j];
                }
            }
        }

        if (max == float.MinValue) return;

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                map.Array[i, j] = map.Array[i, j] / max;
                //Debug.Log(map.Array[i, j]);
            }
        }
    }
}
