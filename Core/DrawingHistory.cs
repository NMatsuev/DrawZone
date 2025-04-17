using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace DrawZone.Core
{
    public class DrawingHistory
    {
        private readonly HistoryStack<CanvasState> _history = new HistoryStack<CanvasState>(50);

        public void SaveState(Canvas canvas)
        {
            var state = new CanvasState(canvas);
            _history.Push(state);
        }

        public void Undo(Canvas canvas)
        {
            if (_history.CanUndo())
            {
                _history.Undo();
                var state = _history.PeekUndo();
                state.Restore(canvas);
            }
        }

        public void Redo(Canvas canvas)
        {
            if (_history.CanRedo())
            {
                var state = _history.Redo();
                state.Restore(canvas);
            }
        }

        public bool CanUndo()
        {
            return _history.CanUndo();
        }

        public bool CanRedo()
        {
            return _history.CanRedo();
        }
    }

    public class CanvasState
    {
        private readonly List<Shape> _elements = new List<Shape>();

        public CanvasState(Canvas canvas)
        {
            foreach (Shape element in canvas.Children)
            {
                _elements.Add(element);
            }
        }

        public void Restore(Canvas canvas)
        {
            canvas.Children.Clear();
            foreach (Shape element in _elements)
            {
                canvas.Children.Add(element);
            }
        }

    }
}
