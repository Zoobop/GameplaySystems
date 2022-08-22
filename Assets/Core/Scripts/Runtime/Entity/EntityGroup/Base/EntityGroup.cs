using System.Collections.Generic;

namespace Entity.Groups
{
    public class EntityGroup<TEntity> : IEntityGroup<TEntity> where TEntity : IEntity
    {
        protected readonly ICollection<TEntity> _groupEntities = new List<TEntity>();

        public IEnumerable<TEntity> GetGroup()
        {
            throw new System.NotImplementedException();
        }

        public int MemberCount()
        {
            throw new System.NotImplementedException();
        }

        public bool AddMember(in TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveMember(in TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public bool HasMember(in TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
