public class ReferenceArray<T>
{
    private T[,] array;

    public T[,] Array => array;

    public ReferenceArray(T[,] array)
    {
        this.array = array;
    }
}
