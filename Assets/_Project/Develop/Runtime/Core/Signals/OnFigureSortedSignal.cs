using _Project.Develop.Runtime.Core.Interfaces;

namespace _Project.Develop.Runtime.Core.Signals
{
    public class OnFigureSortedSignal
    {
        public IFigureController Figure;

        public OnFigureSortedSignal(IFigureController figure) => Figure = figure;
    }
}