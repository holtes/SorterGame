using _Project.Develop.Runtime.Core.Enums;

namespace _Project.Develop.Runtime.Presentation.Figures.Models
{
    public class FigureModel
    {
        private readonly FigureType _type;
        private readonly float _speed;
        private FigureState _state;


        public FigureModel(FigureType type, float speed)
        {
            _type = type;
            _speed = speed;
            _state = FigureState.InLine;
        }

        public FigureType GetFigureType()
        {
            return _type;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetState(FigureState state)
        {
            _state = state;
        }

        public FigureState GetState()
        {
            return _state;
        }
    }
}