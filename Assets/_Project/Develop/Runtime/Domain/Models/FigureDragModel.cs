using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Core.Signals;
using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class FigureDragModel
    {
        private readonly Dictionary<FigureState, Func<IFigureController, object>> _signalMap;

        private IFigureController _currentFigure;

        public FigureDragModel()
        {
            _signalMap = new Dictionary<FigureState, Func<IFigureController, object>>
        {
            { FigureState.Dragging, figure => new OnFigureRealesedSignal(figure) },
            { FigureState.Missing,  figure => new OnFigureMissedSignal(figure) },
            { FigureState.Sorting,  figure => new OnFigureSortedSignal(figure) },
        };
        }

        public object GetFigureStateSignal(FigureState state, IFigureController figure)
        {
            if (_signalMap.TryGetValue(state, out var factory)) return factory(figure);
            return null;
        }

        public void DragFigure(IFigureController figure)
        {
            _currentFigure = figure;
        }

        public void ReleaseFigure()
        {
            _currentFigure = null;
        }

        public IFigureController GetDraggedFigure()
        {
            return _currentFigure;
        }

        public bool IsDragging()
        {
            return _currentFigure != null;
        }
    }
}