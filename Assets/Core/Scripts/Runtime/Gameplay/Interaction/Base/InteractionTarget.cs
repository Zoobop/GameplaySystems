using System.Collections.Generic;
using UnityEngine;


namespace InteractionSystem
{

    using Utility.ExtensionMethods;
    
    public class InteractionTarget : MonoBehaviour, IInteractionTarget
    {
        private readonly IList<IInteraction> _interactions = new List<IInteraction>();
        private readonly IList<IInteraction> _activeInteractions = new List<IInteraction>();

        #region UnityEvents

        protected virtual void Awake()
        {
            // Get interactions
            _interactions.Clear();
            GetAttachedInteractions();
        }

        protected virtual void OnValidate()
        {
            // Get interactions
            _interactions.Clear();
            GetAttachedInteractions();

            // Warn if not at least one interaction option
            if (_interactions.IsEmpty())
            {
                Debug.LogWarning(
                    $"Interaction Missing on {transform.root.name}! Need at least one interaction on this game object!");
            }
        }

        #endregion

        #region IInteractionTarget

        public string GetInteractionText()
        {
            return "Interact";
        }

        public IEnumerable<IInteraction> GetInteractions()
        {
            return GetActiveInteractions();
        }

        public int ActiveInteractionsCount()
        {
            GetInteractions();
            return _activeInteractions.Count;
        }

        public bool HasInteractions()
        {
            GetInteractions();
            return _activeInteractions.Count > 0;
        }

        #endregion

        #region Helpers

        private void GetAttachedInteractions()
        {
            // Gets the attached interactions
            var interactions = GetComponents<IInteraction>();

            // Add interactions to list if valid
            if (interactions != null)
            {
                _interactions.AddRange(interactions);
            }

            GetActiveInteractions();
        }

        private IEnumerable<IInteraction> GetActiveInteractions()
        {
            // Get active interaction
            _activeInteractions.Clear();
            foreach (var interaction in _interactions)
            {
                if (interaction.IsActive())
                {
                    _activeInteractions.Add(interaction);
                }
            }

            return _activeInteractions;
        }

        #endregion
    }
}