using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeDiameter
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeNode root = SpecialTree.MakeFrom("{0,3,{5,2,{8,0},{7,0}},{5,2,{9,0},{8,0}},{5,2,{10,0},{9,0}}}");
            var diaInfo = GetDiameter(root);
            int diameter = diaInfo.Item1;
            if (diaInfo.Item2.HasValue && diaInfo.Item2 > diameter)
            {
                diameter = diaInfo.Item2.Value;
            }
            Console.WriteLine(diameter);
        }

        // Returns a tuple.
        // First item in the tuple is the highest weight under given node.
        // Second item in the tuple is distance between two leaf nodes at a given node
        // (if the given node has leaf nodes under it)
        //
        static Tuple<int, int?> GetDiameter(TreeNode node)
        {
            if (node != null && node.weights != null)
            {
                int max1 = -1; // highest
                int max2 = -1; // second highest
                int maxSubtreeWeight = -1;

                for (int i = 0; i < node.weights.Length; i++)
                {
                    var diaInfo = GetDiameter(node.children[i]);
                    int dia = node.weights[i] + diaInfo.Item1;
                    if (dia > max1)
                    {
                        max2 = max1;
                        max1 = dia;
                    }
                    else if (dia > max2)
                    {
                        max2 = dia;
                    }

                    if (diaInfo.Item2.HasValue)
                    {
                        if (diaInfo.Item2.Value > maxSubtreeWeight)
                        {
                            maxSubtreeWeight = diaInfo.Item2.Value;
                        }
                    }
                }

                int? subtreeWeight = null;
                if (max2 != -1)
                {
                    subtreeWeight = max1 + max2;
                }

                if (subtreeWeight.HasValue)
                {
                    if (maxSubtreeWeight > subtreeWeight.Value)
                    {
                        subtreeWeight = maxSubtreeWeight;
                    }
                }
                else
                {
                    if (maxSubtreeWeight != -1)
                    {
                        subtreeWeight = maxSubtreeWeight;
                    }
                }
                return new Tuple<int, int?>(max1, subtreeWeight);
            }
            else
            {
                return new Tuple<int, int?>(0, null);
            }
        }

        static void Print(TreeNode node)
        {
            if (node != null)
            {
                if (node.weights != null)
                {
                    Console.Write("Number of children {0} => ", node.weights.Length);
                    for (int i = 0; i < node.weights.Length; i++)
                    {
                        Console.Write("{0} ", node.weights[i]);
                    }

                    Console.WriteLine();
                    for (int i = 0; i < node.weights.Length; i++)
                    {
                        Print(node.children[i]);
                    }
                }
            }
        }
    }

    class SpecialTree
    {
        // {0,0}
        // {0,1,{2,0}}
        // {0,3,{5,2,{8,0},{7,0}},{5,2,{9,0},{8,0}},{5,2,{10,0},{9,0}}}
        public static TreeNode MakeFrom(string str)
        {
            int weight;
            int endIdx;
            var node = MakeFrom(str, 0, out endIdx, out weight);
            if (endIdx != str.Length - 1)
            {
                throw new InvalidOperationException("Invalid string");
            }
            return node;
        }

        private static TreeNode MakeFrom(string str, int startIdx, out int endIdx, out int weight)
        {
            if (str[startIdx] != '{') throw new ArgumentException("Expected {");

            startIdx++;
            int nextDelim = str.IndexOf(',', startIdx);
            if (nextDelim == -1 || startIdx == nextDelim) throw new ArgumentException("Missing comma or missing number");

            weight = Int32.Parse(str.Substring(startIdx, nextDelim - startIdx));

            startIdx = nextDelim + 1;
            nextDelim = startIdx;

            while (str[nextDelim] != ',' && str[nextDelim] != '}')
            {
                nextDelim++;
            }

            if (nextDelim == startIdx) throw new ArgumentException("Invalid sequence");

            int childCount = 0;

            if (str[nextDelim] == '}')
            {
                // we expect in this case the child count to be 0
                childCount = Int32.Parse(str.Substring(startIdx, nextDelim - startIdx));
                if (childCount != 0) throw new ArgumentException("Expected 0 child count");

                endIdx = nextDelim;
                return new TreeNode() { children = null, weights = null };
            }

            childCount = Int32.Parse(str.Substring(startIdx, nextDelim - startIdx));

            // we expect child count to be > 0
            if (childCount <= 0) throw new ArgumentException("Expected > 0 child count");

            TreeNode node = new TreeNode();
            node.children = new TreeNode[childCount];
            node.weights = new int[childCount];

            startIdx = nextDelim + 1;

            for (int i = 0; i < childCount; i++)
            {
                int childWeight;
                int nextIdx;

                node.children[i] = MakeFrom(str, startIdx, out nextIdx, out childWeight);
                node.weights[i] = childWeight;

                if (str[nextIdx] != '}') throw new ArgumentException("Expected }");
                startIdx = nextIdx + 1;

                if (i == childCount - 1)
                {
                    if (str[startIdx] != '}') throw new ArgumentException("Expected another }");
                }
                else
                {
                    if (str[startIdx] != ',') throw new ArgumentException("Expected comma");
                    startIdx++;
                }
            }

            endIdx = startIdx;
            return node;
        }
    }

    class TreeNode
    {
        public TreeNode[] children;
        public int[] weights;
    }
}
