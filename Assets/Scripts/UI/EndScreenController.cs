using System;
using Installers;
using Interfaces;
using Services;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EndScreenController : MonoBehaviour, IBindable
    {
        [SerializeField] 
        private TextMeshProUGUI _sessionTime;
        [SerializeField]
        private TextMeshProUGUI _sessionsCount;
    
        private ProgressManager _progressManager;
        private SaveManager _saveManager;

        public event Action CloseEvent;

        public void Initialize(ScriptBinder container)
        {
            _progressManager = SceneContainer.Container.Get<ProgressManager>();
            _saveManager = SceneContainer.Container.Get<SaveManager>();
        }

        private void Awake()
        {
            if (_sessionTime == null
                || _sessionsCount == null)
            {
                throw new NullReferenceException();
            }
        }

        private void OnEnable()
        {
            _sessionTime.text = _progressManager.SessionTime.ToString("g");

            _sessionsCount.text = _saveManager.GetSessionCount().ToString();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
            CloseEvent?.Invoke();
        }
    }
}