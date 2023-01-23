using Installers;
using Interfaces;
using UniRx;

namespace Controllers
{
    public class CollisionController : IBindable
    {
        public Subject<bool> PlayerScorePointCollision { get; } = new();
        public Subject<bool> PlayerBarrierCollision { get; } = new();

        public void Initialize(ScriptBinder container)
        { }
        
        public void OnPlayerBarrierCollision()
        {
            PlayerBarrierCollision.OnNext(true);
        }

        public void OnPlayerScorePointCollision()
        {
            PlayerScorePointCollision.OnNext(true);
        }
    }
}