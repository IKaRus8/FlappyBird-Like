using Installers;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public class MoveForwardController : MonoBehaviour
    {
        private const float StartSpeed = 4f;
        
        private float _speedModifier = 1f;
        private float _horizontalSpeed;

        private Transform _transform;
        private Vector3 _startPosition;

        private void Awake()
        {
            _transform = transform;
            _startPosition = _transform.position;

            SceneContainer.Container.IsInitialised.Subscribe(Initialize).AddTo(this);
        }

        private void Initialize(bool value)
        {
            if (!value)
            {
                return;
            }

            var difficultyManager = SceneContainer.Container.Get<DifficultyManager>();
            
            difficultyManager.SelectedDifficultyRx.Subscribe(SetModifier).AddTo(this);
        }
        
        protected virtual void Update()
        {
            var position = _transform.position;
            var newX = position.x + _horizontalSpeed * _speedModifier * Time.deltaTime;

            position = new Vector3(newX, position.y, position.z);
            _transform.position = position;
        }

        public void SetModifier(float modifier)
        {
            _speedModifier = modifier;
        }

        public void Reset()
        {
            _transform.position = _startPosition;
            _horizontalSpeed = StartSpeed;
        }
    }
}
