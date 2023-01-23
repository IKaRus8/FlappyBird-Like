using System;
using Controllers;
using Installers;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Services
{
    public class ProgressManager : IBindable, IDisposable
    {
        public TimeSpan SessionTime { get; set; } = new TimeSpan();
        
        private CollisionController _collisionController;
        private IDisposable _disposable;
        private IDisposable _timerSubscription;

        public ReactiveProperty<int> ScoreCount { get; } = new ReactiveProperty<int>();
        
        public void Initialize(ScriptBinder container)
        {
            _collisionController = container.Get<CollisionController>();

            _disposable = _collisionController.PlayerScorePointCollision.Subscribe(IncrementScore);
        }

        public void StartSession()
        {
            ScoreCount.Value = 0;
            SessionTime = TimeSpan.Zero;
            _timerSubscription?.Dispose();

            _timerSubscription = Observable.EveryUpdate().Subscribe(l => 
                SessionTime += TimeSpan.FromSeconds(Time.deltaTime));
        }

        private void IncrementScore(bool value)
        {
            if (!value)
            {
                return;
            }
            
            ScoreCount.Value++;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _timerSubscription?.Dispose();
        }
    }
}