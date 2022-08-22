
namespace InteractionSystem
{
    using Entity;

    public interface ICollectable : IEntity
    {
        public ItemPack Collect();
    }
}