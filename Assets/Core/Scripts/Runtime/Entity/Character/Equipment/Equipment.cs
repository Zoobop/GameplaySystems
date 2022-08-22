using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity.EquipmentSystem
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private Gear _head;
        [SerializeField] private Gear _torso;
        [SerializeField] private Gear _arms;
        [SerializeField] private Gear _hands;
        [SerializeField] private Gear _legs;
        [SerializeField] private Gear _feet;

        private MeshCustomization _meshCustomization;
        private IDictionary<GearType, Gear> _equippedGear = new Dictionary<GearType, Gear>();
        private IDictionary<GearType, int> _occupiedSlots = new Dictionary<GearType, int>();
        public static event Action<IEnumerable<Gear>> OnGearChanged = delegate { };

        private void Awake()
        {
            // Sets up gear
            InitializeGearMapping();

            // Sets up mesh customization
            _meshCustomization = GetComponent<MeshCustomization>();
        }

        private void OnValidate()
        {
            if (_head) Equip(_head);
            if (_torso) Equip(_torso);
            if (_arms) Equip(_arms);
            if (_hands) Equip(_hands);
            if (_legs) Equip(_legs);
            if (_feet) Equip(_feet);
        }

        public Gear Equip(Gear gear)
        {
            // Return if null
            if (gear == null) return gear;

            // Insert and/or replace equipped gear
            var type = gear.OccupyingSlots.First();
            _meshCustomization.AddFeature(type);
            if (_equippedGear.TryGetValue(type, out var oldGear))
            {
                // Add occupying slots
                //OccupySlots(gear.OccupyingSlots);

                // Equip new gear
                _equippedGear[type] = gear;

                // Invoke event
                OnGearChanged?.Invoke(_equippedGear.Values.AsEnumerable());
                return oldGear;
            }

            // Equip new gear
            _equippedGear[type] = gear;

            // Invoke event
            OnGearChanged?.Invoke(_equippedGear.Values.AsEnumerable());
            return null;
        }

        public Gear Unequip(Gear gear)
        {
            // Return if null
            if (gear == null) return gear;

            // Insert and/or replace equipped gear
            var type = gear.OccupyingSlots.First();
            if (_equippedGear.TryGetValue(type, out var oldGear))
            {
                // Unequip gear and remove its occupied slot
                _equippedGear[type] = null;
                _occupiedSlots[type]--;

                // Disable game object
                _meshCustomization.RemoveFeature(type);

                // Invoke event
                OnGearChanged?.Invoke(_equippedGear.Values.AsEnumerable());
                return oldGear;
            }

            // No gear to unequip
            return null;
        }

        public Gear Unequip(GearType gearType)
        {
            // Check if gear is equipped
            if (_equippedGear.TryGetValue(gearType, out var oldGear))
            {
                // Unequip gear and remove its occupied slot
                _equippedGear[gearType] = null;
                _occupiedSlots[gearType]--;

                // Disable game object
                _meshCustomization.RemoveFeature(gearType);

                // Invoke event
                OnGearChanged?.Invoke(_equippedGear.Values.AsEnumerable());
                return oldGear;
            }

            // No gear to unequip
            return null;
        }

        private void OccupySlots(IEnumerable<GearType> occupyingSlots)
        {
            // Increment slots counter
            foreach (var gearType in occupyingSlots)
            {
                _occupiedSlots[gearType]++;
            }
        }

        private void InitializeGearMapping()
        {
            // Initialize gear slots and occupied slots
            var gearTypes = Enum.GetValues(typeof(GearType));
            foreach (GearType type in gearTypes)
            {
                _equippedGear.Add(type, null);
                _occupiedSlots.Add(type, 0);
            }
        }
    }
}
