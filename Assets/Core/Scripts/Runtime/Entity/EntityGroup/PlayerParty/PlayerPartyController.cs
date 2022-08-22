using System;
using System.Text;
using UnityEngine;

namespace Entity.Groups
{
    public class PlayerPartyController : MonoBehaviour
    {
        public static PlayerPartyController Instance { get; private set; }

        private readonly PlayerParty _playerParty = new PlayerParty();

        public static event Action<PlayerParty> OnPartyChanged = delegate { };

        #region UnityEvents

        private void Awake()
        {
            Instance = this;

            GameManager.OnPlayerChanged += OnPlayerChangedCallback;

            OnPartyChanged += PrintParty;
        }

        #endregion

        private void PrintParty(PlayerParty party)
        {
            var line = new StringBuilder();

            var group = party.GetGroup();
            foreach (var member in group)
            {
                var active = party.IsActiveMember(member) ? "ACTIVE" : "INACTIVE";
                line.Append($"[{member.GetName()} - ({active})] ");
            }

            print(line);
        }

        private void OnPlayerChangedCallback(ICharacter player)
        {
            // Add player to party if not already
            if (!_playerParty.HasMember(player))
            {
                _playerParty.AddMember(player);
                _playerParty.MemberSetActive(player);
            }

            // Set player as party leader
            _playerParty.SetLeader(player);

            // Invoke event
            OnPartyChanged?.Invoke(_playerParty);
        }

        #region PartyUtilities

        public void AddToParty(in ICharacter character)
        {
            // Add to party
            _playerParty.AddMember(character);

            // Set as active member if not full
            if (!_playerParty.IsFull)
            {
                _playerParty.MemberSetActive(character);
            }

            // Invoke event
            OnPartyChanged?.Invoke(_playerParty);
        }

        public void RemoveFromParty(in ICharacter character)
        {
            _playerParty.RemoveMember(character);

            // Invoke event
            OnPartyChanged?.Invoke(_playerParty);
        }

        public bool IsPartyMember(in ICharacter character)
        {
            return _playerParty.HasMember(character);
        }

        public bool IsActivePartyMember(in ICharacter character)
        {
            return _playerParty.IsActiveMember(character) && _playerParty.HasMember(character);
        }

        #endregion
    }
}