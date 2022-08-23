using UnityEditor;
using UnityEngine;

using Entity.InventorySystem;
using Entity.Item;

namespace Editor.UI
{

    [CustomPropertyDrawer(typeof(ItemSlot))]
    public class ItemSlotDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var item = property.FindPropertyRelative("_item");
            var amount = property.FindPropertyRelative("_amount");
            var maxStack = property.FindPropertyRelative("_maxStack");

            // Value check
            var itemValue = (Item) item.objectReferenceValue;
            if (itemValue)
            {
                var maxStackValue = itemValue.MaxStack;
                var amountValue = amount.intValue;

                // Set max stack amount
                maxStack.intValue = maxStackValue;
                // Clamp the current amount between 0 and max stack value
                amount.intValue = Mathf.Clamp(amountValue, 1, maxStackValue);
            }
            // Item is not valid
            else
            {
                // Set both values to zero
                maxStack.intValue = 0;
                amount.intValue = 0;
            }

            // Position calculate
            const float labelOffset = 50f;
            var labelPosition = new Rect(position.x, position.y, position.width - labelOffset, position.height);
            var itemName = item.objectReferenceValue ? itemValue.Name : "Empty";

            position = EditorGUI.PrefixLabel(labelPosition, GUIUtility.GetControlID(FocusType.Passive),
                new GUIContent(itemName));

            // Layout
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var widthSize = position.width / 3;

            var itemPropPos = new Rect(position.x - 40, position.y, widthSize + labelOffset + 40, position.height);
            var amountPropPos = new Rect(position.x + widthSize + labelOffset + 10, position.y, widthSize,
                position.height);
            var maxStackPos = new Rect(position.x + labelOffset + 20 + widthSize * 2, position.y, widthSize - 20,
                position.height);

            EditorGUI.PropertyField(itemPropPos, item, GUIContent.none);
            EditorGUI.PropertyField(amountPropPos, amount, GUIContent.none);
            EditorGUI.PropertyField(maxStackPos, maxStack, GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}