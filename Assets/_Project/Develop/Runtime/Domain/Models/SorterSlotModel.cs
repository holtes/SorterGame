using _Project.Develop.Runtime.Core.Enums;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class SorterSlotModel
    {
        private FigureType _figureType;

        public void SetFigureType(FigureType type)
        {
            _figureType = type;
        }

        public FigureType GetFigureType()
        {
            return _figureType;
        }
    }
}