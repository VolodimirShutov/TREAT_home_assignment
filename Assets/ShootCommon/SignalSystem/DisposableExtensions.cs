using System;

namespace ShootCommon.SignalSystem
{
    public static class DisposableExtensions
    {
        public static IDisposable AddTo(this IDisposable disposable, CompositeDisposable compositeDisposable)
        {
            if (disposable == null)
                throw new ArgumentNullException(nameof(disposable));
            if (compositeDisposable == null)
                throw new ArgumentNullException(nameof(compositeDisposable));

            compositeDisposable.Add(disposable);
            return disposable;
        }
    }
}