using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public static class ColorUtils
    {
        private static readonly Color _highlightOffset = new(0.2f, 0.2f, 0.2f, 0f);
        private static readonly Color _pressedOffset = new(0.2f, 0.2f, 0.2f, 0f);

        public static ColorBlock ApplyColor(Color color)
        {
            // Create color block and set colors
            var colorBlock = ColorBlock.defaultColorBlock;
            colorBlock.normalColor = color;
            colorBlock.highlightedColor = color + _highlightOffset;
            colorBlock.pressedColor = color + _pressedOffset;
            colorBlock.selectedColor = color;

            return colorBlock;
        }
    }
}
