using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkNodesOfTree
{
    // Given a binary tree, design an algo that creates linked list of nodes
    // at each level. If there're D levels in the tree, there're D linked lists.

    class Program
    {
        static void Main(string[] args)
        {
            Node tree = BuildRandomTree();
            // LinkNodes(tree);
            LinkNodes2(tree);
            PrintLists(tree);
        }

        static Node BuildRandomTree()
        {
            Node root = new Node();
            root.val = 10;

            root.left = new Node();
            root.left.val = 20;
            root.left.left = new Node();
            root.left.left.val = 30;

            root.right = new Node();
            root.right.val = 50;
            root.right.right = new Node();
            root.right.right.val = 60;

            return root;
        }

        static void PrintLists(Node root)
        {
            while (root != null)
            {
                Node head = root;
                while (head != null)
                {
                    Console.Write("{0}->", head.val);
                    head = head.next;
                }
                Console.WriteLine("{null}");
                root = root.left;
            }
        }

        // Simplesest approach is to use Breadth first approach.
        // Put a null value in the queue as a level separator.
        // After all nodes at a given level, there's a null value
        // to indicate completion of that level.
        static void LinkNodes(Node root)
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            q.Enqueue(null);

            while (q.Any())
            {
                Node curr = null;
                Node prev = null;
                bool nonEmptyLevel = false;

                while ((curr = q.Dequeue()) != null)
                {
                    if (curr.left != null) q.Enqueue(curr.left);
                    if (curr.right != null) q.Enqueue(curr.right);
                    if (prev != null)
                    {
                        prev.next = curr;
                    }
                    curr.next = null;
                    prev = curr;
                    nonEmptyLevel = true;
                }

                // when above loop terminates it means the level is 
                // done. so add a null marker to indicate start of next
                // level.
                // This needs to be done only if nonEmptyLevel is set to
                // true. If it stayed false, it means level was empty
                // and nothing more to be done
                if (nonEmptyLevel)
                {
                    q.Enqueue(null);
                }
            }
        }

        // Another approach is to use PreOrder traversal to do this
        // recursively.
        // This requires maintaining a dictionary that maps a level
        // to last node seen on that level.
        static void LinkNodes2(Node root)
        {
            Dictionary<int, Node> dict = new Dictionary<int, Node>();
            PreOrder(dict, root, 0);
        }

        static void PreOrder(Dictionary<int, Node> dict, Node n, int level)
        {
            if (n == null) return;
            if (dict.ContainsKey(level))
            {
                Node prev = dict[level];
                prev.next = n;
            }

            dict[level] = n;
            PreOrder(dict, n.left, level + 1);
            PreOrder(dict, n.right, level + 1);
        }
    }

    class Node
    {
        public int val;
        public Node left;
        public Node right;
        public Node next;
    }
}
