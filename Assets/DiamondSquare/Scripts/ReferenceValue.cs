public class ReferenceValue<T>
{
    public T Value;

    public readonly int x;
    public readonly int y;

    public ReferenceValue(T value, int X, int Y)
    {
        Value = value;
        x = X; y = Y;
    }

    public ReferenceValue(T value)
    {
        Value = value;
    }
}