using System;
using System.Collections.Generic;

namespace ShootCommon.SignalSystem
{
    public class CompositeDisposable
    {
        private readonly List<IDisposable> _disposables = new();
    
        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }
    }
}