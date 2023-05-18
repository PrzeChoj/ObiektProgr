using System.Collections;
using System.Diagnostics;

namespace ConsoleApp;

public class DoublyLinkedList<T> : IMyCollection<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node? Prev { get; set; }
        public Node? Next { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node? _head;
    private Node? _tail;

    public void Add(T item)
    {
        var node = new Node(item);

        if (_head == null || _tail == null)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            node.Prev = _tail;
            _tail.Next = node;
            _tail = node;
        }
    }

    public void Remove(T item)
    {
        var node = FindNode(item);

        if (node == null)
        {
            return;
        }

        // Change the next
        if (node.Prev == null)
        {
            _head = node.Next;
        }
        else
        {
            node.Prev.Next = node.Next;
        }

        // Change the prev
        if (node.Next == null)
        {
            _tail = node.Prev;
        }
        else
        {
            node.Next.Prev = node.Prev;
        }
    }

    private Node? FindNode(T item)
    {
        var node = _head;

        while (node != null)
        {
            if (node.Value!.Equals(item))
            {
                return node;
            }

            node = node.Next;
        }

        return null;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var node = _head;

        while (node != null)
        {
            yield return node.Value;
            node = node.Next;
        }
    }

    public IEnumerator<T> GetReverseEnumerator()
    {
        var node = _tail;

        while (node != null)
        {
            yield return node.Value;
            node = node.Prev;
        }
    }
}

public class Vector<T> : IMyCollection<T>
{
    private T[] _items;
    private int _count;

    public Vector(int capacity = 4)
    {
        _items = new T[capacity];
    }

    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            Array.Resize(ref _items, _items.Length * 2);
        }

        _items[_count++] = item;
    }

    public void Remove(T item)
    {
        for (int i = 0; i < _count; i++)
        {
            if (!_items[i]!.Equals(item)) continue;
            
            // Found it!
            for (int j = i; j < _count - 1; j++)
            {
                _items[j] = _items[j + 1];
            }

            _count--;
            return;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }

    public IEnumerator<T> GetReverseEnumerator()
    {
        for (int i = _count - 1; i >= 0; i--)
        {
            yield return _items[i];
        }
    }
}


public static class AlgorithmsOnICollection
{
    public static T? Find<T>(IMyCollection<T?> myCollection, Func<T, bool> predicate, bool reverse = false)
    {
        var enumerator = reverse ? myCollection.GetReverseEnumerator() : myCollection.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;

            if (predicate(item!))
            {
                return item;
            }
        }

        return default;
    }
    
    public static void Print<T>(IMyCollection<T?> myCollection, Func<T, bool> predicate, bool reverse = false)
    {
        var enumerator = reverse ? myCollection.GetReverseEnumerator() : myCollection.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;

            if (predicate(item!))
            {
                Console.WriteLine(item);
            }
        }
    }
}