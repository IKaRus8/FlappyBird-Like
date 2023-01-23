using System;
using Installers;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public class PlayerMoveController : MonoBehaviour
    {
        private const float MaxHeight = 4f;
        private const float MinHeight = -4f;
        private const float Speed = 2f;

        private float _modifier = 1f;
    
        private IUpButton _upButton;
        private Transform _transform;
        private IDisposable _timerSubscription;

        private void Awake()
        {
            _transform = transform;

            _upButton = SceneContainer.Container.Get<IUpButton>();
        }

        protected void Update()
        {
            var newY = _transform.position.y;
        
            if (_upButton.ButtonHold)
            {
                newY += Speed * _modifier * Time.deltaTime;
            }
            else
            {
                newY += - Speed * _modifier * Time.deltaTime;
            }

            _transform.localPosition = ClampPosition(0, newY);
        }

        public void ResetSession()
        {
            _modifier = 1f;
            
            _timerSubscription?.Dispose();
            _timerSubscription = Observable.Timer(TimeSpan.FromSeconds(15d)).Repeat().Subscribe(OnTime);
            
            transform.localPosition = ClampPosition(0, 0);
        }

        private Vector3 ClampPosition(float newX, float newY)
        {
            var height = Mathf.Clamp(newY, MinHeight, MaxHeight);

            return new Vector3(newX, height);
        }

        private void OnTime(long t)
        {
            _modifier += 0.3f;
        }

        private void OnDestroy()
        {
            _timerSubscription?.Dispose();
        }
    }
}
