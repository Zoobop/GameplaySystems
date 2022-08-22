using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GameEventLog : MonoBehaviour
    {
        public static GameEventLog Instance { get; private set; }

        private static List<string> _eventLog = new();
        public static event Action<string> OnLogChanged = delegate { };

        private void Awake()
        {
            Instance = this;
        }

        public static void LogEvent(string message)
        {
            // Add message to log
            _eventLog.Add(message);

            // Invoke event
            OnLogChanged?.Invoke(message);
        }
    }
}