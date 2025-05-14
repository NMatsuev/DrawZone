using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;

namespace DrawZone.Core
{
    public class HistoryStack
    {
        private readonly Stack<Shape> stack;

        private List<Shape> list;

        private int position;

        private readonly int maxSize;

        public HistoryStack(int maxSize = 100)
        {
            if (maxSize <= 0)
                throw new ArgumentException("Max size must be greater than 0", nameof(maxSize));

            this.maxSize = maxSize;
            stack = new Stack<Shape>(maxSize);
            list = new List<Shape>(maxSize);
            position = 0;
           
        }

        public void Push(Shape item)
        {
            stack.Push(item);
            if (position == list.Count)
            {
                list.Add(item);
                position++;
            }
            else
            {
                list = stack.ToList();
                list.Reverse();
                position = list.Count;
            }
        }

        public void Undo()
        {
            if (stack.Count > 0)
            {
                stack.Pop();
                position--;
            }
        }

        public void Redo()
        {
            if (position <= list.Count)
                stack.Push(list.ElementAt(position++));

        }

        public List<Shape> GetShapeList()
        {
            return stack.ToList();
        }

        public bool CanUndo() => stack.Count > 0;

        public bool CanRedo() => position < list.Count;
    }
}
