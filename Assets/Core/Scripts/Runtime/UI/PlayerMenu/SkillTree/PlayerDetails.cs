using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerDetails : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerPointsText;

        public void SetPointValue(int value)
        {
            _playerPointsText.text = $"Available Points: {value} SP";
        }
    }
}
