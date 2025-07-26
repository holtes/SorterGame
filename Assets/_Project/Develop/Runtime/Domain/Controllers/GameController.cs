using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Domain.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class GameController : MonoBehaviour
    {
        private GameModel _model;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(GameModel gameModel, SignalBus signalBus)
        {
            _model = gameModel;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnFigureSortedSignal>(SortFigure);
            _signalBus.Subscribe<OnFigureMissedSignal>(MissFigure);
            _signalBus.Subscribe<OnRestartGameSignal>(RestartGame);
        }

        private void Start()
        {
            _signalBus.Fire(new OnInitGameSignal(_model.GetPlayerLives(), _model.GetScore(), _model.GetFiguresToSpawn()));
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnFigureSortedSignal>(SortFigure);
            _signalBus.Unsubscribe<OnFigureMissedSignal>(MissFigure);
            _signalBus.Unsubscribe<OnRestartGameSignal>(RestartGame);
        }

        private void SortFigure()
        {
            _model.ProcessFigure();
            _model.AddScore(1);
            _signalBus.Fire(new OnGainScoreSignal(_model.GetScore()));
            CheckEndGame();
        }

        private void MissFigure()
        {
            _model.ProcessFigure();
            _model.LoseLife(1);
            _signalBus.Fire(new OnLoseLifeSignal(_model.GetPlayerLives()));
            CheckEndGame();
        }

        private void CheckEndGame()
        {
            if (_model.IsPlayerDead())
            {
                LoseGame();
                return;
            }

            if (!_model.IsFiguresLeft()) WinGame();
        }

        private void LoseGame()
        {
            _signalBus.Fire(new OnLoseGameSignal());
        }

        private void WinGame()
        {
            _signalBus.Fire(new OnWinGameSignal(_model.GetScore()));
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}