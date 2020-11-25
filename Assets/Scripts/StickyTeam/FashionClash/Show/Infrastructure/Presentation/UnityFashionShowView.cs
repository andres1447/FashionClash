using System;
using StickyTeam.FashionClash.Show.Core.Presentation;
using UniRx;

namespace StickyTeam.FashionClash.Show.Infrastructure.Presentation
{
    public class UnityFashionShowView : FashionShowView
    {
        public IObservable<Unit> OnEnable()
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> PlayStartAnimation()
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> PlayIncreaseScore(int amount)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> PlayWinAnimation()
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> PlayLoseAnimation()
        {
            throw new NotImplementedException();
        }
    }
}