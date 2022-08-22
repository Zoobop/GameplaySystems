namespace InteractionSystem
{

    using UI;

    public class LootOnlyContainer : ContainerBase
    {
        #region UnityEvents


        #endregion

        protected override void OnOpen()
        {
            GameEventLog.LogEvent($"You looted the items: [ {string.Join(" | ", _inventory.GetItemsAsList())} ]");
            print($"You looted the items: [ {string.Join(" | ", _inventory.GetItemsAsList())} ]");
        }

        protected override void OnClose()
        {
        }

        protected override void OnAnimationStart()
        {
        }

        protected override void OnAnimationEnd()
        {
        }
    }
}
