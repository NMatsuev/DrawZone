using System;
using System.Collections.Generic;

namespace DrawZone.Core
{
    public class HistoryStack<T>
    {
        private readonly Stack<T> undoStack;

        private readonly Stack<T> redoStack;

        private readonly int maxSize;

        public int UndoCount => undoStack.Count;
        public int RedoCount => redoStack.Count;
        public int MaxSize => maxSize;

        public HistoryStack(int maxSize = 100)
        {
            if (maxSize <= 0)
                throw new ArgumentException("Max size must be greater than 0", nameof(maxSize));

            this.maxSize = maxSize;
            undoStack = new Stack<T>(maxSize);
            redoStack = new Stack<T>(maxSize);
        }

        public void Push(T item)
        {
            redoStack.Clear();

            if (undoStack.Count >= maxSize)
            {
                var temp = new List<T>(undoStack);
                temp.RemoveAt(0);
                undoStack.Clear();
                foreach (var element in temp)
                {
                   undoStack.Push(element);
                }
            }

            undoStack.Push(item);
        }

        public T Undo()
        {
            if (undoStack.Count == 0)
                throw new InvalidOperationException("Undo stack is empty");

            var item = undoStack.Pop();
            redoStack.Push(item);

            return item;
        }

        public T Redo()
        {
            if (redoStack.Count == 0)
                throw new InvalidOperationException("Redo stack is empty");

            var item = redoStack.Pop();
            undoStack.Push(item);

            return item;
        }

        public T PeekUndo()
        {
            return undoStack.Peek();
        }

        public T PeekRedo()
        {
            return redoStack.Peek();
        }

        public bool CanUndo() => undoStack.Count > 1;

        public bool CanRedo() => redoStack.Count > 0;
    }
}
