namespace notify.Configs;

public class ThreadSafeList<T>
{
    private readonly List<T> _list = new();
    private readonly object _lock = new();

    public void Add(T item)
    {
        lock (_lock)
        {
            _list.Add(item);
        }
    }

    public bool Remove(T item)
    {
        lock (_lock)
        {
            return _list.Remove(item);
        }
    }

    public T Get(int index)
    {
        lock (_lock)
        {
            if (index < 0 || index >= _list.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            return _list[index];
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _list.Clear();
        }
    }

    public List<T> ToList()
    {
        lock (_lock)
        {
            return new List<T>(_list);
        }
    }
}