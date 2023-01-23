using Controllers;
using Interfaces;
using Services;
using UI;
using UnityEngine;

namespace Installers
{
    public class SceneContainer : MonoBehaviour
    {
        public static readonly ScriptBinder Container = new();

        [SerializeField] 
        private GameManager _gameManager;
        [SerializeField] 
        private BarriersController _barriersController;
        [SerializeField] 
        private UpButton _upButton;
        [SerializeField] 
        private DifficultyManager _difficultyManager;
        [SerializeField] 
        private EndScreenController _endScreenController;

        private void Awake()
        {
            Container.Bind(new BarriersSpawner());
            
            Container.Bind(new CollisionController());
            
            Container.Bind(new ProgressManager());
            
            Container.Bind(new SaveManager());
            
            Container.Bind(_barriersController);
            
            Container.Bind(_gameManager);

            Container.Bind(_difficultyManager);
            
            Container.Bind(_endScreenController);
            
            Container.Bind<IUpButton>(_upButton);
            
            Container.Initialize();
        }
    }
}