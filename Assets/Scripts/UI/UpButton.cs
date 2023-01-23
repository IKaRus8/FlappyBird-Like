using Installers;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UpButton : MonoBehaviour, IUpButton, IPointerDownHandler, IPointerUpHandler
    {
        public bool ButtonHold { get; private set; }

        public void Initialize(ScriptBinder container)
        { }
    
        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonHold = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonHold = false;
        }
    }
}
