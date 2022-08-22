using System;
using System.Collections;
using UnityEngine;

namespace ExternalEnvironment
{
    using Utility.ExtensionMethods;

    public enum TimeRegion
    {
        Morning,
        Midday,
        Evening,
        Midnight
    }
    
    public class DayCycle : MonoBehaviour
    {
        public static DayCycle Instance { get; private set; }

        [SerializeField] private Light _sun;

        [SerializeField, Min(1)] private int _day = 1;
        [SerializeField] private float _time = 0f;
        [SerializeField] private TimeRegion _timeRegion = TimeRegion.Morning;
        
        [SerializeField, Min(0.00001f)] private float _tickSpeed = 0.01f;

        public const float MaxTime = 2000f;
        public const float MinTime = 0f;

        private Coroutine _coroutine;
        private WaitForSeconds _tickDelay;
        private float _rotationAmount;

        public int Day => _day;
        public float Time => _time;
        public TimeRegion TimeRegion => _timeRegion;
        
        #region UnityEvents

        private void Awake()
        {
            Instance = this;
            
            _tickDelay = new WaitForSeconds(_tickSpeed);
        }

        private void Start()
        {
            StartCycle();
        }

        #endregion

        private IEnumerator Cycle()
        {
            var startEulerAngles = new Vector3(0f, -_sun.transform.rotation.eulerAngles.y, 0f);
            var endEulerAngles = new Vector3(359.999f, startEulerAngles.y, 0f);
            
            const float timeIncrement = .005f;
            while (_sun is not null)
            {
                _time += timeIncrement;
                if (_time > MaxTime)
                {
                    _time = MinTime;
                    _day++;
                }

                var elapsedTime = _time / MaxTime;
                _sun.transform.SetRotation(Vector3.Lerp(startEulerAngles, endEulerAngles, elapsedTime));

                // Set correct time region
                _timeRegion = GetTimeRegion(_time);
                yield return _tickDelay;
            }
        }

        public void StopCycle()
        {
            StopCoroutine(_coroutine);
        }

        public void StartCycle()
        {
            _coroutine = StartCoroutine(Cycle());
        }

        public static TimeRegion GetTimeRegion(float time)
        {
            /*
             *  Morning     => (1750 - 249)
             *  Midday      => (250  - 749)
             *  Evening     => (750  - 1249)
             *  Midnight    => (1250 - 1749)
             */
            
            return time switch
            {
                // 500 ticks per region (1/8 based)
                //<= MaxTime * 0.875f and < MaxTime * 0.125f => TimeRegion.Morning,
                >= MaxTime * 0.125f and < MaxTime * 0.375f => TimeRegion.Midday,
                >= MaxTime * 0.375f and < MaxTime * 0.625f => TimeRegion.Evening,
                >= MaxTime * 0.625f and < MaxTime * 0.875f => TimeRegion.Midnight,
                _ => TimeRegion.Morning,
            };
        }
    }
}
