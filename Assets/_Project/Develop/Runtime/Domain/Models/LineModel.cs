using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Domain.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class LineModel
    {
        private readonly Dictionary<IFigureController, Vector3> _activeFigures = new();

        public void AddFigure(IFigureController figure, Vector3 position)
        {
            _activeFigures[figure] = position;
        }

        public void RemoveFigure(IFigureController figure)
        {
            _activeFigures.Remove(figure);
        }

        public List<IFigureController> GetFigures()
        {
            return _activeFigures.Keys.ToList();
        }

        public void SetFigureLastPosition(IFigureController figure, Vector3 lastPos)
        {
            _activeFigures[figure] = lastPos;
        }

        public bool TryGetFigureLastPosition(IFigureController figure, out Vector3 lastPos)
        {
            return _activeFigures.TryGetValue(figure, out lastPos);
        }
    }
}