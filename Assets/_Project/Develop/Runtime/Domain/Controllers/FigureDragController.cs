using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Domain.Models;
using UnityEngine;
using System;
using Zenject;
using R3;
using R3.Triggers;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class FigureDragController : MonoBehaviour
    {
        private IDisposable _draggingStream; 

        private Camera _camera;
        private SignalBus _signalBus;
        private FigureDragModel _model;

        [Inject]
        private void Construct(Camera gameCamera, FigureDragModel figureDragModel, SignalBus signalBus)
        {
            _camera = gameCamera;
            _model = figureDragModel;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnLoseGameSignal>(CancelDragging);
            _signalBus.Subscribe<OnWinGameSignal>(CancelDragging);

            _draggingStream =
                this.UpdateAsObservable()
                .Subscribe(_ => TryDragFigure());
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnLoseGameSignal>(CancelDragging);
            _signalBus.Unsubscribe<OnWinGameSignal>(CancelDragging);
            _draggingStream.Dispose();
        }

        private void TryDragFigure()
        {
            if (Input.GetMouseButtonDown(0)) StartDrag();

            if (_model.IsDragging() && Input.GetMouseButton(0)) Drag();

            if (_model.IsDragging() && Input.GetMouseButtonUp(0)) EndDrag();
        }

        private void StartDrag()
        {
            var worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                var figure = hit.collider.GetComponent<IFigureController>();
                if (figure != null && figure.GetFigureState() == FigureState.InLine)
                {
                    _model.DragFigure(figure);
                    figure.SetFigureState(FigureState.Dragging);
                    figure.OnDrag();
                }
            }
        }

        private void Drag()
        {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var draggedFigure = _model.GetDraggedFigure();
            draggedFigure.GetTransform().position = pos;
        }

        private void EndDrag()
        {
            var figure = _model.GetDraggedFigure();
            var state = figure.GetFigureState();

            _signalBus.Fire(_model.GetFigureStateSignal(state, figure));

            _model.ReleaseFigure();
        }

        private void CancelDragging()
        {
            var figure = _model.GetDraggedFigure();

            if (figure == null) return;

            _signalBus.Fire(_model.GetFigureStateSignal(FigureState.Dragging, figure));

            _model.ReleaseFigure();

            _draggingStream?.Dispose();
        }
    }
}