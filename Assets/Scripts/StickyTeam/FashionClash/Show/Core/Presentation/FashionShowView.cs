using System;
using UniRx;

namespace StickyTeam.FashionClash.Show.Core.Presentation
{
    public interface FashionShowView
    {
        IObservable<Unit> OnEnable();
        IObservable<Unit> PlayStartAnimation();
        IObservable<Unit> PlayIncreaseScore(int amount);
        IObservable<Unit> PlayWinAnimation();
        IObservable<Unit> PlayLoseAnimation();
    }
}