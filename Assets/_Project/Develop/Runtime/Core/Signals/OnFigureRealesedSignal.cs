using _Project.Develop.Runtime.Core.Interfaces;

namespace _Project.Develop.Runtime.Core.Signals
{
    public class OnFigureRealesedSignal
    {
        public IFigureController Figure;

        public OnFigureRealesedSignal(IFigureController figure) => Figure = figure;
    }
}