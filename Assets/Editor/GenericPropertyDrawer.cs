using UnityEditor;
using UnityEngine;

// leaving the Stat<> EMPTY is VERY IMPORTANT
[CustomPropertyDrawer(typeof(Stat<>), true)]
public class GenericPropertyDrawer : PropertyDrawer
{
    const float rows = 2; // total number of rows
    const string refa = "overriding";
    const string refb = "value";

    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        var rowOneRect = new Rect(pos.x + (pos.width / 2), pos.y, pos.width, pos.height / rows);
        var rowTwoRect = new Rect(pos.x, pos.y += pos.height / rows, pos.width, pos.height / rows);
        var rowTwoLabelRect = new Rect(pos.x, rowOneRect.y, rowTwoRect.width, rowTwoRect.height);

        EditorGUI.PropertyField(
            rowOneRect,
            prop.FindPropertyRelative(refa),
            GUIContent.none
        );

        EditorGUI.LabelField(rowTwoLabelRect, label);
        if (prop.FindPropertyRelative(refa).boolValue)
        {
            EditorGUI.PropertyField(
                rowTwoRect,
                prop.FindPropertyRelative(refb),
                GUIContent.none
            );
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * rows; // assuming original is one row
    }
}