using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SerializableKeyValuePair<,>))]
public class SerializableKeyValuePairDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        EditorGUI.LabelField(labelPosition, "Key & Value");

        float fieldWidth = (position.width - EditorGUIUtility.labelWidth) / 2.0f - 2.0f;

        Rect keyPosition = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, fieldWidth, position.height);
        SerializedProperty keyProperty = property.FindPropertyRelative("_key");
        EditorGUI.PropertyField(keyPosition, keyProperty, GUIContent.none);

        Rect valuePosition = new Rect(keyPosition.x + fieldWidth + 4.0f, position.y, fieldWidth, position.height);
        SerializedProperty valueProperty = property.FindPropertyRelative("_value");
        EditorGUI.PropertyField(valuePosition, valueProperty, GUIContent.none);

        EditorGUI.EndProperty();
    }
}