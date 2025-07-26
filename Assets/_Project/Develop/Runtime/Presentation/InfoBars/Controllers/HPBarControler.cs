using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Presentation.InfoBars.Views;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Presentation.InfoBars.Controllers
{
    public class HPBarControler : MonoBehaviour
    {
        [SerializeField] private HPBarView _view;

        private HPBarModel _model;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(HPBarModel HPBarModel, SignalBus signalBus)
        {
            _model = HPBarModel;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnInitGameSignal>(InitHP);
            _signalBus.Subscribe<OnLoseLifeSignal>(UpdateHP);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnInitGameSignal>(InitHP);
            _signalBus.Unsubscribe<OnLoseLifeSignal>(UpdateHP);
        }

        private void InitHP(OnInitGameSignal signal)
        {
            var startHP = signal.PlayerLives;
            _model.SetStartHP(startHP);
            _view.InitHP(_model.GetStartHP());
        }

        private void UpdateHP(OnLoseLifeSignal signal)
        {
            var playerLives = signal.PlayerLives;
            _view.SetHealth(playerLives);

            if (_model.IsLowHP(playerLives)) _view.PlayLowHPVFX();
            else _view.StopLowHPVFX();
        }
    }
}