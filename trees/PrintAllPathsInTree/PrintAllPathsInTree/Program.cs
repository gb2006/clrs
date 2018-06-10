using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintAllPathsInTree
{
    class Program
    {
        class TreeNode
        {
            int val;
            TreeNode left;
            TreeNode right;
        }

        static void printAllPaths(TreeNode node)
        {
            if (node != null)
            {
                var sb = new System.Text.StringBuilder();
                PrintAllPaths(node, sb);
            }
        }

        private static void PrintAllPaths(TreeNode node, System.Text.StringBuilder sb)
        {
            int len = sb.Length;
            if (node.left == null && node.right == null)
            {
                sb.AppendFormat("{0}", node.val);
                Console.WriteLine(sb.ToString());
            }
            else
            {
                sb.AppendFormat("{0} ", node.val);
                if (node.left != null)
                {
                    PrintAllPaths(node.left, sb);
                }

                if (node.right != null)
                {
                    PrintAllPaths(node.right, sb);
                }
            }

            // Before exiting, reset buffer
            sb.Length = len;
        }
    }
}
