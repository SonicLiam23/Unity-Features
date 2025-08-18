#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(ProjectileBehaviour), true)]
public class ProjectileBehaviourDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Type currentType = property.managedReferenceValue?.GetType();
        string buttonLabel = currentType != null ? currentType.Name : "(Select Behaviour)";

        if (GUI.Button(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), buttonLabel))
        {
            GenericMenu menu = new GenericMenu();

            Type baseType = typeof(ProjectileBehaviour);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var t in types)
            {
                menu.AddItem(new GUIContent(t.Name), false, () =>
                {
                    property.managedReferenceValue = Activator.CreateInstance(t);
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        }

        // Draw fields if a type is selected
        if (property.managedReferenceValue != null)
        {
            var fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
                                     position.width, position.height - EditorGUIUtility.singleLineHeight - 2);
            EditorGUI.PropertyField(fieldRect, property, true);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + 2;
        if (property.managedReferenceValue != null)
            height += EditorGUI.GetPropertyHeight(property, true);
        return height;
    }
}
#endif