#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(Goal), true)]
[CustomPropertyDrawer(typeof(Reward), true)]
public class PolymorphicDrawer : PropertyDrawer
{
    private string[] typeNames;
    private Type[] types;

    private void InitTypes(Type baseType)
    {
        if (typeNames != null) return;
        types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t))
            .ToArray();
        typeNames = types.Select(t => t.Name).Prepend("None").ToArray();
    }

    private Type GetBaseType()
    {
        Type fieldType = fieldInfo.FieldType;
        if (fieldType.IsArray)
            return fieldType.GetElementType();
        if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>))
            return fieldType.GetGenericArguments()[0];
        return fieldType;
    }

    private Type GetActualType(SerializedProperty property)
    {
        string typeName = property.managedReferenceFullTypename;
        if (string.IsNullOrEmpty(typeName)) return null;

        // Убираем лишние пробелы и символы
        typeName = typeName.Trim();
        // Попытка распарсить "TypeName, AssemblyName"
        if (typeName.Contains(','))
        {
            var parts = typeName.Split(',');
            string className = parts[0].Trim();
            string assemblyName = parts[1].Trim();
            return Type.GetType($"{className}, {assemblyName}");
        }
        // Иначе формат "AssemblyName TypeName"
        int space = typeName.IndexOf(' ');
        if (space > 0)
        {
            string assembly = typeName.Substring(0, space);
            string className = typeName.Substring(space + 1);
            return Type.GetType($"{className}, {assembly}");
        }
        return Type.GetType(typeName);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        InitTypes(GetBaseType());

        Rect popupRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        Type currentType = GetActualType(property);
        int currentIndex = currentType != null ? Array.IndexOf(types, currentType) + 1 : 0;

        EditorGUI.BeginChangeCheck();
        int newIndex = EditorGUI.Popup(popupRect, label.text, currentIndex, typeNames);
        if (EditorGUI.EndChangeCheck())
        {
            if (newIndex == 0)
                property.managedReferenceValue = null;
            else
                property.managedReferenceValue = Activator.CreateInstance(types[newIndex - 1]);

            property.serializedObject.ApplyModifiedProperties();
            // ✅ Ключевой момент: принудительно обновляем и перерисовываем
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            property.serializedObject.Update();
        }

        // Отображаем поля наследника, если объект создан
        if (property.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;
            float y = popupRect.y + EditorGUIUtility.singleLineHeight + 2;
            var child = property.Copy();
            var end = child.GetEndProperty();
            bool enter = true;
            while (child.NextVisible(enter))
            {
                if (SerializedProperty.EqualContents(child, end)) break;
                enter = false;
                float h = EditorGUI.GetPropertyHeight(child, true);
                EditorGUI.PropertyField(new Rect(position.x, y, position.width, h), child, true);
                y += h + 2;
            }
            EditorGUI.indentLevel--;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue == null)
            return EditorGUIUtility.singleLineHeight;

        float h = EditorGUIUtility.singleLineHeight + 2;
        var child = property.Copy();
        var end = child.GetEndProperty();
        bool enter = true;
        while (child.NextVisible(enter))
        {
            if (SerializedProperty.EqualContents(child, end)) break;
            enter = false;
            h += EditorGUI.GetPropertyHeight(child, true) + 2;
        }
        return h;
    }
}
#endif