using _Project.Develop.Runtime.Core.Enums;
using _Project.Develop.Runtime.Domain.Models;
using UnityEngine;
using Zenject;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class SorterController : MonoBehaviour
    {
        [SerializeField] private SorterSlotController[] _slotHoles;

        private SorterModel _model;

        [Inject]
        private void Construct(SorterModel sorterModel)
        {
            _model = sorterModel;
        }

        private void Awake()
        {
            InitSlotHoles();
        }

        private void InitSlotHoles()
        {
            for (int i = 0; i < _slotHoles.Length; i++)
            {
                var figureType = (FigureType)i;
                var slotData = _model.GetSlotSprites(figureType);
                _slotHoles[i].Init(figureType, slotData);
            }
        }
    }
}