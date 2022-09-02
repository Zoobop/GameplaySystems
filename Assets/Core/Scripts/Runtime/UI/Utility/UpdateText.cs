using System;
using TMPro;
using UnityEngine;

namespace UI
{

    public class UpdateText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void ChangeTextFromFloat(float single)
        {
            _text.text = $"{Convert.ToInt32(single * 100f)}";
        }
    }
}
