using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Presentation.InfoBars.Views;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Presentation.InfoBars.Controllers
{
    public class ScoreBarController : MonoBehaviour
    {
        [SerializeField] private ScoreBarView _view;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnInitGameSignal>(InitScore);
            _signalBus.Subscribe<OnGainScoreSignal>(UpdateScore);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnInitGameSignal>(InitScore);
            _signalBus.Unsubscribe<OnGainScoreSignal>(UpdateScore);
        }

        private void InitScore(OnInitGameSignal signal)
        {
            var score = signal.Score;
            _view.InitScore(score);
        }

        private void UpdateScore(OnGainScoreSignal signal)
        {
            var score = signal.Score;
            _view.SetScore(score);
            _view.PlayGainScoreVFX();
        }
    }
}