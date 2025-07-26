using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Presentation.Figures.Models;
using _Project.Develop.Runtime.Presentation.Figures.Views;
using UnityEngine;

namespace _Project.Develop.Runtime.Presentation.Figures.Controllers
{
    public class FigureController : MonoBehaviour, IFigureController
    {
        [SerializeField] private FigureView _view;

        private FigureModel _model;

        public void Init(FigureModel model, Sprite sprite, Color spritecolor)
        {
            _model = model;
            _view.Init(sprite, spritecolor);
        }

        public Vector3 MoveTo(Vector3 target)
        {
            _view.MoveToTarget(target, _model.GetSpeed());
            return transform.position;
        }

        public FigureType GetFigureType()
        {
            return _model.GetFigureType();
        }

        public void SetFigureState(FigureState state)
        {
            _model.SetState(state);
        }

        public FigureState GetFigureState()
        {
            return _model.GetState();
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void OnLine()
        {
            _view.PlayImpactVFX();
        }

        public void OnDrag()
        {
            _view.StopImpactVFX();
        }

        public void OnMiss()
        {
            _view.StopImpactVFX();
            _view.PlayExplosionVFX();
        }

        public void OnSort()
        {
            _view.StopImpactVFX();
        }
    }
}