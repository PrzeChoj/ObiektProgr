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

    public void Add(T item)
    {
        Node newNode = new Node(item);

        if (_root == null)
        {
            _root = newNode;
            return;
        }

        Node current = _root;

        while (current != null)
        {
            bool currentFasBothChildren = current.LChild != null && current.RChild != null;
            if (currentFasBothChildren)
            {
                current = ((new Random().Next(2) == 0) ? current.LChild : current.RChild)!;
                continue;
            }

            bool currentIsLeaf = current.LChild == null && current.RChild == null;
            if (currentIsLeaf)
            {
                if (new Random().Next(2) == 0)
                {
                    current.LChild = newNode;
                    newNode.Parent = current;
                }
                else
                {
                    current.RChild = newNode;
                    newNode.Parent = current;
                }
            }
            else if (current.LChild == null)
            {
                current.LChild = newNode;
                newNode.Parent = current;
            }
            else if (current.RChild == null)
            {
                current.RChild = newNode;
                newNode.Parent = current;
            }
            else
            {
                throw new Exception("Error in logic");
            }
            
            return;
        }
    }

    public void Remove(T item)
    {
        Node? nodeToRemove = FindNode(item);
        
        // Case 0: Nothing to remove
        if (nodeToRemove == null)
        {
            return;
        }

        // Case 1: Node is a leaf node
        bool removeLeaf = nodeToRemove.LChild == null && nodeToRemove.RChild == null;
        if (removeLeaf)
        {
            if (Equals(nodeToRemove, _root))
            {
                _root = null;
            }
            else if (nodeToRemove.Parent == null)
            {
                throw new Exception("Only root is allowed to not have parents");
            }
            else if (Equals(nodeToRemove, nodeToRemove.Parent.LChild))
            {
                nodeToRemove.Parent.LChild = null;
            }
            else if (Equals(nodeToRemove, nodeToRemove.Parent.RChild))
            {
                nodeToRemove.Parent.RChild = null;
            }
            else
            {
                throw new Exception("Tree had unexpected structure");
            }
        }
        // Case 2: Node has one child
        else if (nodeToRemove.LChild == null && nodeToRemove.RChild != null)
        {
            // nodeToRemove.RChild będzie dzieckiem swojego dziadka
            if (nodeToRemove.Parent == null)
            {
                _root = nodeToRemove.RChild;
            }
            else if (Equals(nodeToRemove.Parent.LChild, nodeToRemove))
            {
                nodeToRemove.Parent.LChild = nodeToRemove.RChild;
            }
            else if (Equals(nodeToRemove.Parent.RChild, nodeToRemove))
            {
                nodeToRemove.Parent.RChild = nodeToRemove.RChild;
            }
            else
            {
                throw new Exception("Tree was wrongly build");
            }

            // Set new parent
            nodeToRemove.RChild.Parent = nodeToRemove.Parent;
        }
        else if (nodeToRemove.LChild != null && nodeToRemove.RChild == null)
        {
            // nodeToRemove.RChild będzie dzieckiem swojego dziadka
            if (nodeToRemove.Parent == null)
            {
                _root = nodeToRemove.LChild;
            }
            else if (Equals(nodeToRemove.Parent.LChild, nodeToRemove))
            {
                nodeToRemove.Parent.LChild = nodeToRemove.LChild;
            }
            else if (Equals(nodeToRemove.Parent.RChild, nodeToRemove))
            {
                nodeToRemove.Parent.RChild = nodeToRemove.LChild;
            }
            else
            {
                throw new Exception("Tree was wrongly build");
            }

            // Set new parent
            nodeToRemove.LChild.Parent = nodeToRemove.Parent;
        }
        // Case 3: Node has two children
        else if (nodeToRemove.LChild != null && nodeToRemove.RChild != null)
        {
            Node nodeWithoutRChild = nodeToRemove.RChild;
            bool changeWithChild = true;
            while (nodeWithoutRChild.RChild != null)
            {
                nodeWithoutRChild = nodeWithoutRChild.RChild;
                changeWithChild = false;
            }
            // Now, nodeWithoutRChild is what its name said

            // nodeWithoutRChild always has a parent
            nodeWithoutRChild.Parent!.RChild = nodeWithoutRChild.LChild;

            if (nodeWithoutRChild.LChild != null)
            {
                nodeWithoutRChild.LChild.Parent = nodeWithoutRChild.Parent;
            }
            // Now, the nodeWithoutRChild was removed
            
            // I want to place nodeWithoutRChild in the place of nodeToRemove
            
            // Change the parent:
            if (nodeToRemove.Parent == null)
            {
                _root = nodeWithoutRChild;
            }
            else if (Equals(nodeToRemove.Parent.LChild, nodeToRemove))
            {
                nodeToRemove.Parent.LChild = nodeWithoutRChild;
            }
            else
            {
                nodeToRemove.Parent.RChild = nodeWithoutRChild;
            }
            
            // Change the parent of children:
            nodeToRemove.LChild.Parent = nodeWithoutRChild;
            if (!changeWithChild) // This was pain to see. Took an hour and a half of debugging...
            {
                nodeToRemove.RChild.Parent = nodeWithoutRChild;
            }
            else // This was also pain to see. Took additional an hour and a half of debugging...
            {
                if (nodeWithoutRChild.LChild != null)
                    nodeWithoutRChild.LChild.Parent = nodeWithoutRChild;
            }

            // Change the pointers of the nodeWithoutRChild:
            nodeWithoutRChild.LChild = nodeToRemove.LChild;
            nodeWithoutRChild.RChild = nodeToRemove.RChild;
            nodeWithoutRChild.Parent = nodeToRemove.Parent;
        }
        else
        {
            throw new Exception("Logic is not working");
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (_root == null)
        {
            yield break;
        }

        Stack<Node> stack = new Stack<Node>();
        Node? current = _root;

        while (current != null || stack.Count > 0)
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.LChild;
            }
            
            current = stack.Pop();
            yield return current.Value;
            current = current.RChild;
        }
    }

    public IEnumerator<T> GetReverseEnumerator()
    {
        if (_root == null)
        {
            yield break;
        }

        Stack<Node> stack = new Stack<Node>();
        Node? current = _root;

        while (current != null || stack.Count > 0)
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.RChild;
            }

            current = stack.Pop();
            yield return current.Value;
            current = current.LChild;
        }
    }
    
    private Node? FindNode(T item)
    {
        if (_root == null)
        {
            return null;
        }
        
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();

            if (current.Value!.Equals(item))
            {
                return current;
            }

            if (current.LChild != null)
            {
                queue.Enqueue(current.LChild);
            }

            if (current.RChild != null)
            {
                queue.Enqueue(current.RChild);
            }
        }

        return null;
    }
}



public static class AlgorithmsOnTrees // 
{
    // Find(iterator , predicate) -> T?
    public static T? FindGame<T>(IEnumerator<T> enumerator, Func<T, bool> predicate)
    {
        bool anyTrue = false;
        while (enumerator.MoveNext() && !anyTrue)
        {
            anyTrue = predicate(enumerator.Current);
        }

        return anyTrue ? enumerator.Current : default;
    }
    
    // ForEach(iterator , function) -> void
    public static void ForEach<T>(IEnumerator<T> enumerator, Action<T> func)
    {
        while (enumerator.MoveNext())
        {
            func(enumerator.Current);
        }
    }
    
    // CountIf(iterator , predicate) -> int
    public static int CountIf<T>(IEnumerator<T> enumerator, Func<T, bool> predicate)
    {
        int sum = 0;
        while (enumerator.MoveNext())
        {
            if(predicate(enumerator.Current))
                sum++;
        }

        return sum;
    }
}