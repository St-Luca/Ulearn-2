using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        public LimitedSizeStack<Tuple<CommandType, TItem, int>> LSStack; //В кортеже хранится какая это команда, элемент и индекс элемента

        public ListModel(int limit)
        {
            LSStack = new LimitedSizeStack<Tuple<CommandType, TItem, int>>(limit);
            Items = new List<TItem>();
            Limit = limit;
        }

        public enum CommandType
        {
            RemoveItem,
            AddItem
        }

        public void RemoveItem(int index)
        {
            LSStack.Push(Tuple.Create(CommandType.RemoveItem, Items[index], index));
            Items.RemoveAt(index);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            LSStack.Push(Tuple.Create(CommandType.AddItem, item, Items.Count - 1));
        }

        public bool CanUndo()
        {
            return LSStack.Count > 0;
        }

        public void Undo()
        {
            var (commandType, item, index) = LSStack.Pop();
            if (commandType == CommandType.RemoveItem)
            {
                if (LSStack.Count == 1)
                {
                    Items.Insert(index - 1, item);
                }
                else
                {
                    Items.Insert(index, item);
                }
            }
            else if (commandType == CommandType.AddItem)
            {
                Items.RemoveAt(LSStack.Count);
            }
        }
    }
}
