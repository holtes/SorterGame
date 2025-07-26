using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Core.Interfaces;
using _Project.Develop.Runtime.Domain.Models;
using _Project.Develop.Runtime.Presentation.Sorter.Views;
using UnityEngine;
using Zenject;
using R3;
using R3.Triggers;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class SorterSlotController : MonoBehaviour
    {
        [SerializeField] private SorterSlotView _view;

        private SorterSlotModel _model;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SorterSlotModel sorterSlotModel, SignalBus signalBus)
        {
            _model = sorterSlotModel;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnFigureSortedSignal>(PlayCorrectVFX);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(collider => EnterSlotTrigger(collider))
                .AddTo(this);
            this.OnTriggerExit2DAsObservable()
                .Subscribe(collider => ExitSlotTrigger(collider))
                .AddTo(this);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnFigureSortedSignal>(PlayCorrectVFX);
        }

        public void Init(FigureType type, SlotData slotData)
        {
            _view.SetSprite(slotData.Sprite, slotData.FrameSprite);
            _view.SetColor(slotData.SpriteColor);
            _model.SetFigureType(type);
        }

        private void EnterSlotTrigger(Collider2D collider)
        {
            if (!collider.TryGetComponent<IFigureController>(out var figure)) return;

            if (_model.GetFigureType() == figure.GetFigureType()) figure.SetFigureState(FigureState.Sorting);
            else figure.SetFigureState(FigureState.Missing);
        }

        private void ExitSlotTrigger(Collider2D collider)
        {
            if (!collider.TryGetComponent<IFigureController>(out var figure)) return;

            figure.SetFigureState(FigureState.Dragging);
        }

        private void PlayCorrectVFX(OnFigureSortedSignal signal)
        {
            var figure = signal.Figure;

            if (_model.GetFigureType() != figure.GetFigureType()) return;

            _view.PlayCorrectVFX();
        }
    }
}