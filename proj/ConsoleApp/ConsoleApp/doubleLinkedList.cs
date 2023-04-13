using System.Collections;
using System.Diagnostics;

namespace ConsoleApp;

public class DoubleLinkedList<T>
{
    private Node? _start { get; set; }
    private Node? _end { get; set; }

    public class Node
    {
        public Node? Next { get; set; }
        public Node? Prev { get; set; }
        public T Value { get; set; }

        public void Append(T value)
        {
            if (Next == null)
            {
                Next = new Node { Value = value, Prev = this};
                return;
            }
            Next.Append(value);
        }

        public T Get(int i) => i == 0 ? Value : Next!.Get(--i);
    }

    public void Add(T value)
    {
        if (_start == null)
        {
            _start = new Node { Value = value };
            _end = _start;
            return;
        }
        _end!.Append(value);
        _end = _end.Next;
    }

    public T Get(int i)
    {
        Debug.Assert(_start != null, nameof(_start) + " != null");
        return _start.Get(i);
    }

    public ForwardIteratorClass ForwardIterator
    {
        get
        {
            Debug.Assert(_start != null, nameof(_start) + " != null");
            return new ForwardIteratorClass(_start);
        }
    }

    public class ForwardIteratorClass
    {
        private readonly Node _root;
        private Node? _current;
        public ForwardIteratorClass(Node root) => _root = _current = root;

        public T Next()
        {
            Debug.Assert(_current != null, nameof(_current) + " != null");
            var value = _current.Value;
            _current = _current.Next;
            return value;
        }

        public bool Complete => _current == null;

        public void Reset()
        {
            _current = _root;
        }
    }
    
    public ReverseIteratorClass ReverseIterator
    {
        get
        {
            Debug.Assert(_end != null, nameof(_end) + " != null");
            return new ReverseIteratorClass(_end);
        }
    }

    public class ReverseIteratorClass
    {
        private readonly Node _tail;
        private Node? _current;
        public ReverseIteratorClass(Node tail) => _tail = _current = tail;

        public T Next()
        {
            Debug.Assert(_current != null, nameof(_current) + " != null");
            var value = _current.Value;
            _current = _current.Prev;
            return value;
        }

        public bool Complete => _current == null;

        public void Reset()
        {
            _current = _tail;
        }
    }
}