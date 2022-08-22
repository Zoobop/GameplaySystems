using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity.EquipmentSystem
{
    public class MeshCustomization : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform _accessoryComponentsHolder;

        [SerializeField] private Transform _bodyComponentsHolder;
        [SerializeField] private Transform _facialComponentsHolder;

        private IDictionary<string, Transform> _accessories;
        private IDictionary<string, Transform> _bodyParts;
        private IDictionary<string, Transform> _facialFeatures;

        private IDictionary<GearType, IEnumerable<Transform>> _meshMapping =
            new Dictionary<GearType, IEnumerable<Transform>>();

        #region Initialization

        private void Awake()
        {
            _accessories = _accessoryComponentsHolder.GetComponentsInChildren<Transform>(true)
                .ToDictionary(x => x.name);
            _bodyParts = _bodyComponentsHolder.GetComponentsInChildren<Transform>(true).ToDictionary(x => x.name);
            _facialFeatures = _facialComponentsHolder.GetComponentsInChildren<Transform>(true)
                .ToDictionary(x => x.name);

            // Get mappings
            InitializeMapping();
        }

        private void InitializeMapping()
        {
            // Mappings
            var headMapping = new List<Transform>
            {
                _facialFeatures["Hair"]
            };
            var torsoMapping = new List<Transform>
            {
                _bodyParts["Body"]
            };
            var armMapping = new List<Transform>
            {
                _bodyParts["Arms"]
            };
            var handMapping = new List<Transform>
            {
                _bodyParts["Hands"]
            };
            var legMapping = new List<Transform>
            {
                _bodyParts["Legs"],
                _accessories["Underwear"]
            };
            var feetMapping = new List<Transform>
            {
                _bodyParts["Feet"]
            };

            // Initialization
            _meshMapping.Add(GearType.Head, headMapping);
            _meshMapping.Add(GearType.Torso, torsoMapping);
            _meshMapping.Add(GearType.Arms, armMapping);
            _meshMapping.Add(GearType.Hands, handMapping);
            _meshMapping.Add(GearType.Legs, legMapping);
            _meshMapping.Add(GearType.Feet, feetMapping);
        }

        #endregion

        public void AddFeature(GearType type)
        {
            // Enable all objects
            foreach (var obj in _meshMapping[type])
            {
                obj.gameObject.SetActive(true);
            }
        }

        public void RemoveFeature(GearType type)
        {
            // Disable all objects
            foreach (var obj in _meshMapping[type])
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}