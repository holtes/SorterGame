using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Domain.Factories;
using _Project.Develop.Runtime.Domain.Models;
using _Project.Develop.Runtime.Presentation.SpawnZone.Views;
using UnityEngine;
using System;
using System.Threading;
using Zenject;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;


namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class SpawnZoneController : MonoBehaviour
    {
        [SerializeField] private SpawnZoneView _view;
        [SerializeField] private LineController[] _lines;

        private CancellationTokenSource _spawnCancellationToken = new CancellationTokenSource();

        private FigureFactory _factory;
        private SpawnZoneModel _model;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SpawnZoneModel spawnZoneModel, FigureFactory figureFactory, SignalBus signalBus)
        {
            _model = spawnZoneModel;
            _factory = figureFactory;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnInitGameSignal>(StartSpawning);
            _signalBus.Subscribe<OnLoseGameSignal>(StopSpawning);
            _signalBus.Subscribe<OnWinGameSignal>(StopSpawning);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnInitGameSignal>(StartSpawning);
            _signalBus.Unsubscribe<OnLoseGameSignal>(StopSpawning);
            _signalBus.Unsubscribe<OnWinGameSignal>(StopSpawning);
        }

        public void StartSpawning(OnInitGameSignal signal)
        {
            var figuresToSpawn = signal.FiguresToSpawn;

            _model.SetFiguresToSpawn(figuresToSpawn);

            _view.PlayVFX();

            SpawnLoopAsync(_model.GetFiguresToSpawn()).Forget();
        }

        private async UniTaskVoid SpawnLoopAsync(int total)
        {
            var spawnDelayRange = _model.GetSpawnDelayRange();
            var speedRange = _model.GetSpeedRange();

            for (int i = 0; i < total; i++)
            {
                var delay = Random.Range(spawnDelayRange.x, spawnDelayRange.y);
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _spawnCancellationToken.Token);

                var lineIndex = Random.Range(0, _lines.Length);
                var line = _lines[lineIndex];

                var type = (FigureType)Random.Range(0, Enum.GetValues(typeof(FigureType)).Length);
                var speed = Random.Range(speedRange.x, speedRange.y);

                var figure = _factory.Create(type, speed, line.transform, line.transform.position);

                line.AddFigureOnLine(figure);
            }
            _view.StopVFX();
        }

        private void StopSpawning()
        {
            _spawnCancellationToken.Cancel();
            _view.StopVFX();
        }
    }
}