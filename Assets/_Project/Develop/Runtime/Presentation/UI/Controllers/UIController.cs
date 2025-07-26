using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Presentation.UI.Views;
using UnityEngine;
using TSS;
using Zenject;

namespace _Project.Develop.Runtime.Presentation.UI.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TSSCore _tSSCore;
        [SerializeField] private WinPanelView _winPanelView;
        [SerializeField] private LosePanelView _losePanelView;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnWinGameSignal>(OpenWinPanel);
            _signalBus.Subscribe<OnLoseGameSignal>(OpenLosePanel);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnWinGameSignal>(OpenWinPanel);
            _signalBus.Unsubscribe<OnLoseGameSignal>(OpenLosePanel);
        }

        private void OpenPanel(GameState gameState)
        {
            _tSSCore.SelectState(gameState.ToString());
        }

        private void OpenWinPanel(OnWinGameSignal signal)
        {
            OpenPanel(GameState.Win);
            _winPanelView.SetScore(signal.Score);
        }

        private void OpenLosePanel()
        {
            OpenPanel(GameState.Lose);
        }
    }
}