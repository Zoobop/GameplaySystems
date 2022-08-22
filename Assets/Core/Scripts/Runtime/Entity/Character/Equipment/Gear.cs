using System.Collections.Generic;
using UnityEngine;

namespace Entity.EquipmentSystem
{
    public enum GearType
    {
        Head,
        Torso,
        Arms,
        Hands,
        Legs,
        Feet
    }

    [CreateAssetMenu(menuName = "Equipment/Gear", fileName = "Gear_")]
    public class Gear : ScriptableObject
    {
        [SerializeField] private GameObject _gearPrefab;
        [SerializeField] private List<GearType> _occupyingSlots;

        public GameObject GearPrefab => _gearPrefab;
        public IEnumerable<GearType> OccupyingSlots => _occupyingSlots;
    }
}
