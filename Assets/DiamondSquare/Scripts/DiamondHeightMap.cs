using UnityEngine;

public class DiamondHeightMap
{
    private float[,] _map;
    private int _size;
    private int _max;
    private float _basicHeight;

    public DiamondHeightMap(int size, float basicHeight)
    {
        _size = (int)Mathf.Pow(2, size) + 1; //алгоритм работает с картой размера 2 в степени i + 1
        _map = new float[_size, _size];
        _max = _size - 1; //возможно, свойство массивов в шарпах
        _basicHeight = basicHeight; //будет использовываться в случае, когда идет обращение к внешним клеткам

        InitHeights();
        RandomAngles();
    }

    private void InitHeights() // устанавливаем дефолтные значения высот
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
                _map[i, j] = 0f;
        }
    }

    private void RandomAngles() // инициализируем случайные значения в углах перед началом генерации
    {
        var initVal = _max; //почему именно такие значения, пока не понял
        _map[0, 0] = Random.Range(-initVal, initVal);
        _map[0, _max] = Random.Range(-initVal, initVal);
        _map[_max, 0] = Random.Range(-initVal, initVal);
        _map[_max, _max] = Random.Range(-initVal, initVal);
    }

    public float[,] GenerateMap()
    {
        CreateHeightMap();
        return _map;
    }

    private void CreateHeightMap()
    {
        var step = _max / 2; // шаг для прохождения всех точек на карте
        int iter = 1; // используется для возведения в степень случайного числа, что мы добавляем к среднему значению 4 точек

        while (step >= 1)
        {
            for (int i = step; i < _size; i += step)
            {
                _map[i, i] = GetDiomandOne(i, i, step, iter);
            }
            for (int i = step; i < _size; i += step)
            {
                for (int j = step; j < _size; j += step)
                {
                    _map[i, j] = GetSquareOne(i, j, step, iter);
                }
            }

            step = step / 2;
            iter++;
        }
    }

    private float GetDiomandOne(int x, int y, int step, int iter)
    {
        return Displace((GetHeight(x - step, y - step) +
                        GetHeight(x - step, y + step) +
                        GetHeight(x + step, y + step) +
                        GetHeight(x + step, y - step)) / 4,
                        iter);
    }

    private float GetSquareOne(int x, int y, int step, int iter)
    {
        return Displace((GetHeight(x, y - step) +
                        GetHeight(x, y + step) +
                        GetHeight(x + step, y) +
                        GetHeight(x + step, y)) / 4,
                        iter);
    }

    private float Displace(float val, int iter)
    {
        float rnd = Mathf.Pow(Random.Range(0, 1), iter);
        Debug.Log(val + rnd);
        return val + rnd;
    }

    private float GetHeight(int x, int y)
    {
        if ((x < 0 || x > _max) || (y < 0 || y > _max))
        {
            return _basicHeight;
        }
        else
        {
            return _map[x, y];
        }
    }
}
