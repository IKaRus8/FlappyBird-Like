using System;
using Controllers;
using Installers;
using Interfaces;
using UI;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services
{
    public class BarriersSpawner : IBindable, IDisposable
    {
        private const float BarrierOffset = 8f;
        private const float MinBarrierHeight = -3f;
        private const float MaxBarrierHeight = 3f;

        private BarriersController _barriersController;
        private ProgressManager _progressManager;
        private IDisposable _disposable;

        private float _baseOffset;
        private EndScreenController _endScreenController;

        public void Initialize(ScriptBinder container)
        {
            _barriersController = container.Get<BarriersController>();
            _progressManager = container.Get<ProgressManager>();

            _endScreenController = container.Get<EndScreenController>();
            _endScreenController.CloseEvent += GenerateStartBarriers;
            
            GenerateStartBarriers();

            _disposable = _progressManager.ScoreCount.Subscribe(GenerateNewBarrier);
        }

        private void GenerateStartBarriers()
        {
            var count = _barriersController.WallsCount;
            _baseOffset = BarrierOffset;

            for (var i = 1; i < count; i++)
            {
                _baseOffset += BarrierOffset;

                var posY = Random.Range(MinBarrierHeight, MaxBarrierHeight);
            
                _barriersController.SpawnWall(new Vector3(_baseOffset, posY));
            }
        }

        private void GenerateNewBarrier(int point)
        {
            if (point == 0)
            {
                return;
            }
            
            var posX = _baseOffset + BarrierOffset * point;

            var posY = Random.Range(MinBarrierHeight, MaxBarrierHeight);
            
            _barriersController.SpawnWall(new Vector3(posX, posY));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _endScreenController.CloseEvent -= GenerateStartBarriers;
        }
    }
}