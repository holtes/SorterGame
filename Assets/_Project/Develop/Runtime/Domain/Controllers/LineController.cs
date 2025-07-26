using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Domain.Models;
using UnityEngine;
using System;
using Zenject;
using R3;
using R3.Triggers;
using DG.Tweening;


namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class LineController : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        private IDisposable _moveFiguresStream;

        private LineModel _model;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(LineModel lineModel, SignalBus signalBus)
        {
            _model = lineModel;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnFigureRealesedSignal>(ReturnToLine);
            _signalBus.Subscribe<OnFigureSortedSignal>(RemoveFigure);
            _signalBus.Subscribe<OnFigureMissedSignal>(RemoveFigure);
            _signalBus.Subscribe<OnWinGameSignal>(StopFigures);
            _signalBus.Subscribe<OnLoseGameSignal>(StopFigures);

            _moveFiguresStream =
                this.UpdateAsObservable()
                .Subscribe(_ => MoveFigures());
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnFigureRealesedSignal>(ReturnToLine);
            _signalBus.Unsubscribe<OnFigureSortedSignal>(RemoveFigure);
            _signalBus.Unsubscribe<OnFigureMissedSignal>(RemoveFigure);
            _signalBus.Unsubscribe<OnWinGameSignal>(StopFigures);
            _signalBus.Unsubscribe<OnLoseGameSignal>(StopFigures);
            _moveFiguresStream.Dispose();
        }

        public void AddFigureOnLine(IFigureController figure)
        {
            figure.GetTransform().position = _startPoint.position;
            _model.AddFigure(figure, _startPoint.position);
        }

        private void RemoveFigure(OnFigureMissedSignal signal)
        {
            var figure = signal.Figure;
            figure.OnMiss();
            RemoveFigure(figure);
        }

        private void RemoveFigure(OnFigureSortedSignal signal)
        {
            var figure = signal.Figure;
            figure.OnSort();
            RemoveFigure(figure);
        }

        private void RemoveFigure(IFigureController figure)
        {
            _model.RemoveFigure(figure);
            Destroy(figure.GetTransform().gameObject);
        }

        private void MoveFigures()
        {
            foreach (var movingFigure in _model.GetFigures())
            {
                var figure = movingFigure;

                if (figure.GetFigureState() != FigureState.InLine) continue;

                var newPosition = figure.MoveTo(_endPoint.position);

                _model.SetFigureLastPosition(figure, newPosition);
            }
        }

        private void StopFigures()
        {
            _moveFiguresStream?.Dispose();
        }

        private void ReturnToLine(OnFigureRealesedSignal signal)
        {
            var figure = signal.Figure;

            if (!_model.TryGetFigureLastPosition(figure, out var lastPos)) return;

            figure.GetTransform()
                .DOMove(lastPos, 1f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    figure.SetFigureState(FigureState.InLine);
                    figure.OnLine();
                });
        }
    }
}