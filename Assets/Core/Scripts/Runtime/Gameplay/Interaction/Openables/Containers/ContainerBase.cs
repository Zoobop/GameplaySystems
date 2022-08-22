namespace InteractionSystem
{

    using Entity.InventorySystem;

    public class ContainerBase : Openable
    {
        protected IInventory _inventory;

        #region UnityEvents

        #endregion

        protected override void OnOpen()
        {
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