using _Project.Develop.Runtime.Core.Enums;
using UnityEngine;

namespace _Project.Develop.Runtime.Core.Interfaces
{
    public interface IFigureController
    {
        public Vector3 MoveTo(Vector3 target);

        public FigureType GetFigureType();

        public void SetFigureState(FigureState state);

        public FigureState GetFigureState();

        public Transform GetTransform();

        public void OnLine();

        public void OnDrag();

        public void OnMiss();

        public void OnSort();
    }
}