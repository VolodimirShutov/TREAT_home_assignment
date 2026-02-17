using System;
using System.Collections.Generic;

namespace ShootCommon.SignalSystem
{
    public class SignalService
    {
        private readonly Dictionary<Type, List<Delegate>> _signalHandlers = new();
        private readonly List<string> _signalHistory = new();
        private const int MaxHistorySize = 10;
        
        private static SignalService _instance;
        public static SignalService Instance => _instance ??= new SignalService();
        public List<string> GetSignalHistory() => new List<string>(_signalHistory);
        public Dictionary<Type, List<Delegate>> GetActiveSubscriptions() => new(_signalHandlers);

        public void Send<T>(T signal) where T : Signal
        {
            var type = typeof(T);
            if (_signalHandlers.TryGetValue(type, out var handlers))
            {
                // Створюємо копію списку, щоб уникнути модифікації під час ітерації
                var handlersCopy = new List<Delegate>(handlers);
                foreach (var handler in handlersCopy)
                {
                    (handler as Action<T>)?.Invoke(signal);
                }
            }

            // Логування останніх сигналів
            string signalInfo = $"[{DateTime.Now}] Signal: {type.Name}";
            _signalHistory.Add(signalInfo);
            if (_signalHistory.Count > MaxHistorySize)
            {
                _signalHistory.RemoveAt(0);
            }
        }

        public IDisposable Subscribe<T>(Action<T> action) where T : Signal
        {
            var type = typeof(T);
            if (!_signalHandlers.ContainsKey(type))
            {
                _signalHandlers[type] = new List<Delegate>();
            }

            _signalHandlers[type].Add(action);
            return new Subscription<T>(this, action);
        }

        // Новий публічний метод Unsubscribe
        public void Unsubscribe<T>(Action<T> action) where T : Signal
        {
            var type = typeof(T);
            if (_signalHandlers.TryGetValue(type, out var handlers))
            {
                handlers.Remove(action);
                if (handlers.Count == 0)
                {
                    _signalHandlers.Remove(type);
                }
            }
        }

        // Приватний клас для IDisposable (залишаємо як є)
        private class Subscription<T> : IDisposable where T : Signal
        {
            private readonly SignalService _service;
            private readonly Action<T> _action;

            public Subscription(SignalService service, Action<T> action)
            {
                _service = service;
                _action = action;
            }

            public void Dispose()
            {
                _service.Unsubscribe(_action);
            }
        }
    }
}