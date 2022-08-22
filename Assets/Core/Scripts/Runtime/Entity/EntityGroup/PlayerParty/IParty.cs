using System.Collections.Generic;

namespace Entity.Groups
{
    public interface IParty<TEntity> : IEntityGroup<TEntity> where TEntity : IEntity
    {
        public TEntity GetLeader();
        public ISet<TEntity> GetActiveMembers();

        public void SetLeader(in TEntity member);
        public bool MemberSetActive(in TEntity member);
        public bool MemberSetInactive(in TEntity member);
        public bool IsActiveMember(in ICharacter member);
    }
}