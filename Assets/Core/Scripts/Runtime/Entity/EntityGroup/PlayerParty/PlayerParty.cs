using System.Collections.Generic;

namespace Entity.Groups
{
    public class PlayerParty : IParty<ICharacter>
    {
        private const int MaxPartySize = 3;
        private const int MinPartySize = 1;

        private readonly ICollection<ICharacter> _totalPartyMembers = new HashSet<ICharacter>(MaxPartySize);
        private readonly ISet<ICharacter> _activePartyMembers = new HashSet<ICharacter>(MaxPartySize);

        private ICharacter _partyLeader;

        public ICharacter PartyLeader => _partyLeader;
        public bool IsFull => _activePartyMembers.Count == MaxPartySize;

        #region IParty

        public ICharacter GetLeader()
        {
            return _partyLeader;
        }

        public ISet<ICharacter> GetActiveMembers()
        {
            return _activePartyMembers;
        }

        public void SetLeader(in ICharacter member)
        {
            // Return if member not in group
            if (!_activePartyMembers.Contains(member)) return;

            // Set as leader
            _partyLeader = member;
        }

        public bool MemberSetActive(in ICharacter member)
        {
            // Return if member not in group
            if (!_totalPartyMembers.Contains(member)) return false;

            // Return if active member limit has been reached
            if (_activePartyMembers.Count >= MaxPartySize) return false;

            // Add member to active group if valid
            return _activePartyMembers.Add(member);
        }

        public bool MemberSetInactive(in ICharacter member)
        {
            // Return if member not in group
            if (!_totalPartyMembers.Contains(member)) return false;

            // Return if only one active member left
            if (_activePartyMembers.Count <= MinPartySize) return false;

            // Remove member from active group if valid
            return _activePartyMembers.Remove(member);
        }

        public bool IsActiveMember(in ICharacter member)
        {
            return _activePartyMembers.Contains(member);
        }

        #endregion

        #region IEntityGroup

        public IEnumerable<ICharacter> GetGroup()
        {
            return _totalPartyMembers;
        }

        public int MemberCount()
        {
            return _totalPartyMembers.Count;
        }

        public bool AddMember(in ICharacter entity)
        {
            // Add new member
            _totalPartyMembers.Add(entity);
            return true;
        }

        public bool RemoveMember(in ICharacter entity)
        {
            // Remove member if valid
            return _totalPartyMembers.Remove(entity) || _activePartyMembers.Remove(entity);
        }

        public bool HasMember(in ICharacter entity)
        {
            return _totalPartyMembers.Contains(entity);
        }

        #endregion
    }
}