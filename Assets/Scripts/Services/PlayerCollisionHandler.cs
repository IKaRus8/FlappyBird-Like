using Constants;
using Controllers;
using Installers;
using UniRx;
using UnityEngine;

namespace Services
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private CollisionController _collisionController;
    
        private void Awake()
        {
            SceneContainer.Container.IsInitialised.Subscribe(Initialize).AddTo(this);
        }

        private void Initialize(bool value)
        {
            if (!value)
            {
                return;
            }
        
            _collisionController = SceneContainer.Container.Get<CollisionController>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var go = col.gameObject;
        
            if (go.CompareTag(TagsConstants.ScorePointTag))
            {
                _collisionController.OnPlayerScorePointCollision();
            }
            else if(go.CompareTag(TagsConstants.BarrierTag))
            {
                _collisionController.OnPlayerBarrierCollision();
            }
        }
    }
}
