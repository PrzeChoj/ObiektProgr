using System.Collections;
using System.Diagnostics;
using System.Xml;

namespace ConsoleApp;

public interface ICollection<T>
{
    void Add(T item);
    void Remove(T item);
    IEnumerator<T> GetEnumerator();
    IEnumerator<T> GetReverseEnumerator();
}

public class MyBinaryTree<T> : ICollection<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node? LChild { get; set; }
        public Node? RChild { get; set; }
        public Node? Parent { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node? _root;
    private Random _random = new Random();

    public void Add(T item)
    {
        if (_root == null)
        {
            _root = new Node(item);
        }
        else
        {
            Add(item, _root);
        }
    }
    
    private void Add(T item, Node thisNode)
    {
        var node = new Node(item);

        bool thisIsLeaf = thisNode.LChild == null && thisNode.RChild == null;

        if (thisIsLeaf)
        {
            node.Parent = thisNode;
            if (_random.Next(2) == 0)
            {
                thisNode.LChild = node;
            }
            else
            {
                thisNode.RChild = node;
            }
        }
        else if (thisNode.LChild == null)
        {
            node.Parent = thisNode;
            thisNode.LChild = node;
        }
        else if (thisNode.RChild == null)
        {
            node.Parent = thisNode;
            thisNode.RChild = node;
        }
        else
        {
            Add(item, _random.Next(2) == 0 ? thisNode.LChild : thisNode.RChild);
        }
    }

    public void Remove(T item)
    {
        var node = FindNode(item);

        if (node == null)
        {
            return;
        }

        if (node.Parent == null)
        {
            // This is root
            Node leaf2;
            if (_root!.LChild == null)
            {
                if (_root.RChild == null)
                {
                    _root = null;
                    return;
                }

                leaf2 = _DetachLeaf(_root.RChild);
            }
            else
            {
                leaf2 = _DetachLeaf();
            }
            _root = leaf2;
            _root.LChild = node.LChild;
            _root.RChild = node.RChild;
            return;
        }

        bool thisIsLeaf = node.LChild == null && node.RChild == null;

        if (thisIsLeaf)
        {
            if (node.Parent.LChild == node)
            {
                node.Parent.LChild = null;
            }
            else
            {
                node.Parent.RChild = null;
            }

            return;
        }
        
        if (node.LChild == null)
        {
            node.RChild!.Parent = node.Parent;
            if (node.Parent.LChild == node)
            {
                node.Parent.LChild = node.RChild;
            }
            else
            {
                node.Parent.RChild = node.RChild;
            }

            return;
        }
        
        if (node.RChild == null)
        {
            node.LChild!.Parent = node.Parent;
            if (node.Parent.LChild == node)
            {
                node.Parent.LChild = node.LChild;
            }
            else
            {
                node.Parent.RChild = node.LChild;
            }

            return;
        }
        
        // This has both childs and parent
        
        Node leaf = _DetachLeaf()!;
        leaf.LChild = node.LChild;
        node.LChild.Parent = leaf;
        leaf.RChild = node.RChild;
        node.RChild.Parent = leaf;
        leaf.Parent = node.Parent;
        if (node.Parent.LChild == node)
        {
            node.Parent.LChild = leaf;
        }
        else
        {
            node.Parent.RChild = leaf;
        }
    }

    private Node? _DetachLeaf(Node? startNode = null)
    {
        if (startNode == null)
        {
            startNode = _root;
        }
        
        Node? node = startNode;
        if (node == null)
        {
            return null;
        }

        while (node.LChild != null)
        {
            node = node.LChild;
        }

        return node;
    }

    private Node? FindNode(T item)
    {
        if (_root == null)
        {
            return null;
        }
        var enumerator = _GetEnumeratorNode();
        if (item!.Equals(enumerator.Current.Value))
        {
            return enumerator.Current;
        }
        while (enumerator.MoveNext())
        {
            if (item!.Equals(enumerator.Current.Value))
            {
                return enumerator.Current;
            }
        }

        return null;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var node = _root;

        while (node != null)
        {
            yield return node.Value;
            node = node.RChild;
        }
    }
    
    private IEnumerator<Node> _GetEnumeratorNode()
    {
        var node = _root;

        if (node == null)
        {
            yield return null;
        }
        else
        {
            while (node != null)
            {
                yield return node;
                node = _getNextNode(node);
            }            
        }
    }

    private Node? _getNextNode(Node node)
    {
        if (node.LChild != null)
        {
            return node.LChild;
        }

        if (node.RChild != null)
        {
            return node.RChild;
        }

        while (node.Parent != null && node.Parent.RChild == null)
        {
            node = node.Parent;
        }

        return node;
    }

    public IEnumerator<T> GetReverseEnumerator()
    {
        throw new NotImplementedException();
    }
}


public static class AlgorithmsOnTrees // TODO
{
    
}