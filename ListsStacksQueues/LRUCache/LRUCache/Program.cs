using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(-202 % 10);
            LRUCache cache = new LRUCache(3);

            cache.Set(1, 100);
            cache.Display();

            cache.Set(2, 200);
            cache.Display();

            cache.Set(3, 300);
            cache.Display();

            cache.Set(3, 3000);
            cache.Display();

            cache.Get(3);
            cache.Get(2);
            cache.Get(1);

            cache.Display();
        }
    }

    class LRUCache
    {
        private Dictionary<int, DoublyLinkedListNode<CacheData>> map;
        private DoublyLinkedList<CacheData> list;
        private int capacity;
        private int currentSize;

        public LRUCache(int maxSize)
        {
            map = new Dictionary<int, DoublyLinkedListNode<CacheData>>();
            list = new DoublyLinkedList<CacheData>();
            capacity = maxSize;
            currentSize = 0;
        }

        public void Set(int key, int val)
        {
            if (map.ContainsKey(key))
            {
                list.Remove(map[key]);                
            }
            else
            {
                if (currentSize == capacity)
                {
                    var node = list.GetFirst();

                    map.Remove(node.data.Key);
                    list.Remove(node);
                }
                else
                {
                    currentSize++;
                }
            }

            map[key] = list.Add(new CacheData() { Key = key, Val = val });
        }

        public int Get(int key)
        {
            if (map.ContainsKey(key))
            {
                var node = map[key];
                int val = node.data.Val;

                list.Remove(node);

                node = list.Add(new CacheData() { Key = key, Val = val });
                map[key] = node;

                return val;
            }
            throw new ArgumentException("Key not present");
        }

        public void Remove(int key)
        {
            if (map.ContainsKey(key))
            {
                list.Remove(map[key]);
                map.Remove(key);
                currentSize--;

                return;
            }
            throw new ArgumentException("Key not present");
        }

        public void Display()
        {
            list.Display();
        }
    }

    class CacheData
    {
        public int Val;
        public int Key;

        public override string ToString()
        {
            return string.Format("[{0}={1}]", Key, Val);
        }
    }

    class DoublyLinkedList<T> where T : class
    {
        private DoublyLinkedListNode<T> head;
        private DoublyLinkedListNode<T> tail;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
        }

        public DoublyLinkedListNode<T> GetFirst()
        {
            return head;
        }

        public DoublyLinkedListNode<T> Add(T data)
        {
            DoublyLinkedListNode<T> node = new DoublyLinkedListNode<T>();
            node.data = data;
            node.next = null;
            node.prev = null;

            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                tail.next = node;
                node.prev = tail;
                tail = node;
            }

            return node;
        }

        public void Remove(DoublyLinkedListNode<T> node)
        {
            if (node == null) return;
            if (head == null)
            {
                // this serves as an assert
                //
                if (tail != null) throw new InvalidOperationException("error");

                return;
            }

            if (node.prev != null)
            {
                node.prev.next = node.next;
            }
            else
            {
                // This is head node being deleted
                // But we should compare it with internal head just to make sure.
                //
                if (head != node) throw new InvalidOperationException("Invalid node");

                head = head.next;
                if (head != null)
                {
                    head.prev = null;
                }
            }

            if (node.next != null)
            {
                node.next.prev = node.prev;
            }
            else
            {
                // This is tail node being deleted;
                // But we should compare it with internal tail just to make sure.
                //
                if (tail != node) throw new InvalidOperationException("Invalid node");

                tail = tail.prev;
                if (tail != null)
                {
                    tail.next = null;
                }
            }

            node.next = null;
            node.prev = null;
        }

        public void Display()
        {
            var node = head;

            Console.WriteLine("Forward:");
            while (node != null)
            {
                Console.Write(node.data.ToString() + "->");
                node = node.next;
            }
            Console.WriteLine("null");
            Console.WriteLine();

            node = tail;

            Console.WriteLine("Backward:");
            while (node != null)
            {
                Console.Write(node.data.ToString() + "->");
                node = node.prev;
            }
            Console.WriteLine("null");
            Console.WriteLine();
        }
    }

    class DoublyLinkedListNode<T> 
    {
        public T data;
        public DoublyLinkedListNode<T> next;
        public DoublyLinkedListNode<T> prev;
    }
}
