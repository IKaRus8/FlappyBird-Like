using Installers;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class SaveManager : IBindable
    {
        private const string SessionCountKey = "SessionsCount";
        
        public void IncrementSessionCount()
        {
            var value = GetSessionCount() + 1;
            
            PlayerPrefs.SetInt(SessionCountKey, value);
            
            PlayerPrefs.Save();
        }

        public int GetSessionCount()
        {
            return PlayerPrefs.GetInt(SessionCountKey);
        }

        public void Initialize(ScriptBinder container)
        { }
    }
}