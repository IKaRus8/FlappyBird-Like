using System;
using Installers;
using Interfaces;
using UI;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public class DifficultyManager : MonoBehaviour, IBindable
    {
        [SerializeField] 
        private DifficultyController _startDifficultyController;
        [SerializeField] 
        private DifficultyController _endDifficultyController;

        public ReactiveProperty<float> SelectedDifficultyRx { get; } = new ReactiveProperty<float>(1f);

        public void Initialize(ScriptBinder container)
        { }

        private void Awake()
        {
            if (_startDifficultyController == null
                || _endDifficultyController == null)
            {
                throw new NullReferenceException();
            }

            _startDifficultyController.SelectedModifierRx.Subscribe(OnDifficultyChanged).AddTo(this);
            _endDifficultyController.SelectedModifierRx.Subscribe(OnDifficultyChanged).AddTo(this);
        }

        private void OnDifficultyChanged(float value)
        {
            SelectedDifficultyRx.Value = value;
        }
    }
}