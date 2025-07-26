using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Core.Interfaces;
using UnityEngine;
using Zenject;
using R3;
using R3.Triggers;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class DeathZoneController : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                 .Subscribe(collider => MissFigure(collider))
                 .AddTo(this);
        }

        private void MissFigure(Collider2D collider)
        {
            if (!collider.TryGetComponent<IFigureController>(out var figure) ||
                figure.GetFigureState() != FigureState.InLine) return;

            _signalBus.Fire(new OnFigureMissedSignal(figure));
        }
    }
}