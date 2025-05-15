using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using DrawZone.Shapes;

namespace DrawZone.Core
{
    public class HistoryStack
    {
        private Stack<CustomShape> stack;

        private List<CustomShape> list;

        private readonly int maxSize;

        public HistoryStack(int maxSize = 100)
        {
            if (maxSize <= 0)
                throw new ArgumentException("Max size must be greater than 0", nameof(maxSize));

            this.maxSize = maxSize;
            stack = new Stack<CustomShape>(maxSize);
            list = new List<CustomShape>(maxSize);
           
        }

        public void Push(CustomShape item)
        {
            if (stack.Count > 0)
            {
                stack.Clear();
            }
            list.Add(item);
        }

        public void Undo()
        {
            if (list.Count > 0)
            {
                stack.Push(list.ElementAt(list.Count-1));
                list.RemoveAt(list.Count - 1);
            }
        }

        public void Redo()
        {
            if (stack.Count > 0)
            {
                var elem = stack.Pop();
                list.Add(elem);
            }

        }

        public List<CustomShape> GetShapeList()
        {
            return list;
        }

        public void SetShapeList(List<CustomShape> list)
        {
            this.list = list;
        }

        public void Clear()
        {
            list.Clear();
            stack.Clear();
        }

        public bool CanUndo() => list.Count > 0;

        public bool CanRedo() => stack.Count > 0;
    }
}
