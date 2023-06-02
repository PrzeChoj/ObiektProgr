using System.Collections;

namespace ConsoleApp;

public class MyBinaryTreeAdapter<T> : IMyCollection<object>, IEnumerable
{
    private readonly MyBinaryTree<T> _tree;

    public MyBinaryTreeAdapter(MyBinaryTree<T> tree)
    {
        _tree = tree;
    }

    public void Add(object item)
    {
        if (item is T convertedItem)
        {
            _tree.Add(convertedItem);
        }
        else
        {
            throw new ArgumentException($"Invalid item type. Expected {typeof(T)}, but received {item.GetType()}.");
        }
    }

    public bool Remove(object item)
    {
        if (item is T convertedItem)
        {
            return _tree.Remove(convertedItem);
        }
        else
        {
            throw new ArgumentException($"Invalid item type. Expected {typeof(T)}, but received {item.GetType()}.");
        }
    }

    public IEnumerator<object> GetEnumerator()
    {
        foreach (T item in _tree)
        {
            yield return item;
        }
    }

    public IEnumerator<object> GetReverseEnumerator()
    {
        var reverseEnumerator = _tree.GetReverseEnumerator();
        while (reverseEnumerator.MoveNext())
        {
            yield return reverseEnumerator.Current;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
