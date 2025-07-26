using _Project.Develop.Runtime.Core.Interfaces;

namespace _Project.Develop.Runtime.Core.Signals
{
    public class OnFigureMissedSignal
    {
        public IFigureController Figure;

        public OnFigureMissedSignal(IFigureController figure) => Figure = figure;
    }
}