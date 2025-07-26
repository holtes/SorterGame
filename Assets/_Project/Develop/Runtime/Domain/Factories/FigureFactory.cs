using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Data.Configs;
using _Project.Develop.Runtime.Presentation.Figures.Controllers;
using _Project.Develop.Runtime.Presentation.Figures.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Domain.Factories
{
    public class FigureFactory : IFactory<FigureType, float, Transform, Vector3, FigureController>
    {
        private readonly DiContainer _container;
        private readonly FigureController _prefab;
        private readonly Dictionary<FigureType, Figure> _figures;

        public FigureFactory(DiContainer container, GameConfig config, FigureController prefab)
        {
            _container = container;
            _prefab = prefab;
            _figures = config.Figures.ToDictionary(f => f.Type, f => f);
        }

        public FigureController Create(FigureType type, float speed, Transform parent, Vector3 position)
        {
            var model = new FigureModel(type, speed);
            var config = _figures[type];

            var controller = _container.InstantiatePrefabForComponent<FigureController>(_prefab);
            controller.transform.SetParent(parent);
            controller.transform.position = position;
            controller.Init(model, config.Sprite, config.Color);

            return controller;
        }
    }
}