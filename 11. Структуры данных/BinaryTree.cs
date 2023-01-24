using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Text;

namespace BinaryTrees
{
    public class TreeNode<T>
    {
        public TreeNode<T> Left;
        public TreeNode<T> Right;
        public T Value;

        public TreeNode(T value)
        {
            Value = value;
        }
    }

    public class BinaryTree<T> where T : IComparable
    {
        public TreeNode<T> RootTree;

        public bool Contains(T element)
        {
            TreeNode<T> temp = RootTree;

            while (temp != null)
            {
                if (temp.Value.CompareTo(element) == 0)
                {
                    return true;
                }
                else
                {
                    if (temp.Value.CompareTo(element) > 0) //значение после искомого
                    {
                        temp = temp.Right;
                    }
                    else //значение до искомого 
                    {
                        temp = temp.Left;
                    }
                }
            }
            return false;
        }

        public void Add(T element)
        {
            TreeNode<T> temp = RootTree;

            if (temp == null) //Дерева/ветки еще не существует
            {
                RootTree = new TreeNode<T>(element);
            }
            else
            {
                while (true)
                {
                    if (temp.Value.CompareTo(element) < 0)
                    {
                        if (temp.Left != null)
                        {
                            temp = temp.Left;
                        }
                        else
                        {
                            temp.Left = new TreeNode<T>(element);
                            break;
                        }
                    }
                    else
                    {
                        if (temp.Right != null)
                        {
                            temp = temp.Right;
                        }
                        else
                        {
                            temp.Right = new TreeNode<T>(element);
                            break;
                        }
                    }
                }
            }
        }
    }
}
