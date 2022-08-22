using System.Collections.Generic;

namespace Entity.Groups
{
    public interface IEntityGroup<TEntity> where TEntity : IEntity
    {
        public IEnumerable<TEntity> GetGroup();
        public int MemberCount();

        public bool AddMember(in TEntity entity);
        public bool RemoveMember(in TEntity entity);
        public bool HasMember(in TEntity entity);
    }
}