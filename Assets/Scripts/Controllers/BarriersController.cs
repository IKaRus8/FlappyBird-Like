using System.Collections.Generic;
using Installers;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class BarriersController : MonoBehaviour, IBindable
    {
        [SerializeField] 
        private List<Transform> _walls;

        private int _currentIndex;

        public int WallsCount => _walls.Count;

        public void Initialize(ScriptBinder container)
        { }

        public void SpawnWall(Vector3 position)
        {
            var wall = _walls[_currentIndex];

            wall.position = position;
        
            IncrementIndex();
        }

        private void IncrementIndex()
        {
            _currentIndex++;

            if (_currentIndex == _walls.Count)
            {
                _currentIndex = 0;
            }
        }
    }
}
