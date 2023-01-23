using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DifficultyController : MonoBehaviour
    {
        [SerializeField] 
        private Button _easyButton;
        [SerializeField] 
        private Button _normalButton;
        [SerializeField] 
        private Button _hardButton;
        [SerializeField] 
        private Transform _selectedMark;

        public Subject<float> SelectedModifierRx { get; } = new Subject<float>();

        private void Awake()
        {
            if(_easyButton == null
               || _normalButton == null
               || _hardButton == null)
            {
#if UNITY_EDITOR
                throw new NullReferenceException();
#endif
                return;
            }
        
            _easyButton.onClick.AddListener(() => OnDifficultySelected(_easyButton.transform, 0.5f));
            _normalButton.onClick.AddListener(() => OnDifficultySelected(_normalButton.transform, 1f));
            _hardButton.onClick.AddListener(() => OnDifficultySelected(_hardButton.transform, 1.5f));
        }

        private void OnDifficultySelected(Transform t, float modifier)
        {
            _selectedMark.SetParent(t, false);

            SelectedModifierRx.OnNext(modifier);
        }
    }
}
