

using System.Collections.Generic;

namespace InteractionSystem
{

    public interface IInteractionTarget
    {
        public string GetInteractionText();
        public IEnumerable<IInteraction> GetInteractions();
        public int ActiveInteractionsCount();
        public bool HasInteractions();
    }
}