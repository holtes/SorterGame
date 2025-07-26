using _Project.Develop.Runtime.Core.Services;
using _Project.Develop.Runtime.Core.Signals;
using _Project.Develop.Runtime.Data.Configs;
using _Project.Develop.Runtime.Presentation.Figures.Controllers;
using _Project.Develop.Runtime.Domain.Factories;
using _Project.Develop.Runtime.Domain.Models;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Bootstrap.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _gameCamera;
        [SerializeField] private Transform _sceneOrigin;
        [SerializeField] private FigureController _figurePrefab;
        [SerializeField] private GameConfig _gameConfig;


        public override void InstallBindings()
        {
            BindSignalBus();
            BindSignals();
            BindTimeService();
            BindFigureFactory();
            BindFigureDragModel();
            BindGameModel();
            BindLineModel();
            BindSorterModel();
            BindSorterSlotModel();
            BindSpawnZoneModel();
            BindHPBarModel();
            BindGameCamera();
            BindSceneOrigin();
            BindFigurePrefab();
        }

        private void BindSignalBus()
        {
            SignalBusInstaller.Install(Container);
        }

        private void BindSignals()
        {
            Container.DeclareSignal<OnInitGameSignal>();
            Container.DeclareSignal<OnGainScoreSignal>();
            Container.DeclareSignal<OnLoseLifeSignal>();
            Container.DeclareSignal<OnFigureMissedSignal>();
            Container.DeclareSignal<OnFigureRealesedSignal>();
            Container.DeclareSignal<OnFigureSortedSignal>();
            Container.DeclareSignal<OnRestartGameSignal>();
            Container.DeclareSignal<OnWinGameSignal>();
            Container.DeclareSignal<OnLoseGameSignal>();
        }

        private void BindTimeService()
        {
            Container
                .Bind<TimeService>()
                .AsSingle();
        }

        private void BindFigureFactory()
        {
            Container
            .Bind<FigureFactory>()
            .AsSingle()
            .WithArguments(_gameConfig, _figurePrefab);
        }

        private void BindFigureDragModel()
        {
            Container
            .Bind<FigureDragModel>()
            .AsSingle();
        }

        private void BindGameModel()
        {
            Container
                .Bind<GameModel>()
                .AsSingle()
                .WithArguments(_gameConfig);
        }

        private void BindLineModel()
        {
            Container
                .Bind<LineModel>()
                .AsTransient();
        }

        private void BindSorterModel()
        {
            Container
                .Bind<SorterModel>()
                .AsSingle()
                .WithArguments(_gameConfig);
        }

        private void BindSorterSlotModel()
        {
            Container
                .Bind<SorterSlotModel>()
                .AsTransient();
        }

        private void BindSpawnZoneModel()
        {
            Container
                .Bind<SpawnZoneModel>()
                .AsSingle()
                .WithArguments(_gameConfig);
        }

        private void BindHPBarModel()
        {
            Container
            .Bind<HPBarModel>()
            .AsSingle();
        }

        private void BindGameCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_gameCamera);
        }

        private void BindSceneOrigin()
        {
            Container
                .Bind<Transform>()
                .FromInstance(_sceneOrigin);
        }

        private void BindFigurePrefab()
        {
            Container
                .Bind<FigureController>()
                .FromInstance(_figurePrefab);
        }
    }
}