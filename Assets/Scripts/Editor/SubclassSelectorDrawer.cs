using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Common.Utils;

namespace Editor
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference)
            {
                EditorGUI.LabelField(position, label.text, "Use [SerializeReference] only!");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            // 1. Рассчитываем позиции для заголовка и кнопки
            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
            Rect buttonRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, 
                                     position.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);

            // 2. Рисуем Foldout (стрелочку) и название поля
            property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded, label, true);

            // 3. Рисуем кнопку выбора типа
            string typeName = property.managedReferenceFullTypename.Split('.').LastOrDefault() ?? "None (Null)";
            if (GUI.Button(buttonRect, typeName, EditorStyles.popup))
            {
                ShowTypeMenu(property);
            }

            // 4. Если развернуто и объект не null, рисуем его содержимое
            if (property.isExpanded && property.managedReferenceValue != null)
            {
                EditorGUI.indentLevel++;
                
                float verticalOffset = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                float currentY = position.y + verticalOffset;

                // Получаем первый дочерний элемент свойств объекта
                SerializedProperty endProperty = property.GetEndProperty();
                SerializedProperty childProperty = property.Copy();
                bool hasChildren = childProperty.NextVisible(true);

                if (hasChildren)
                {
                    while (!SerializedProperty.EqualContents(childProperty, endProperty))
                    {
                        float childHeight = EditorGUI.GetPropertyHeight(childProperty, true);
                        Rect childRect = new Rect(position.x, currentY, position.width, childHeight);
                        
                        EditorGUI.PropertyField(childRect, childProperty, true);
                        
                        currentY += childHeight + EditorGUIUtility.standardVerticalSpacing;
                        if (!childProperty.NextVisible(false)) break;
                    }
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            if (property.isExpanded && property.managedReferenceValue != null)
            {
                SerializedProperty endProperty = property.GetEndProperty();
                SerializedProperty childProperty = property.Copy();
                bool hasChildren = childProperty.NextVisible(true);

                if (hasChildren)
                {
                    while (!SerializedProperty.EqualContents(childProperty, endProperty))
                    {
                        height += EditorGUI.GetPropertyHeight(childProperty, true) + EditorGUIUtility.standardVerticalSpacing;
                        if (!childProperty.NextVisible(false)) break;
                    }
                }
            }

            return height;
        }

        private void ShowTypeMenu(SerializedProperty property)
        {
            GenericMenu menu = new GenericMenu();
            Type baseType = fieldInfo.FieldType;
            
            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.List<>))
            {
                baseType = baseType.GetGenericArguments()[0];
            }

            menu.AddItem(new GUIContent("Null"), string.IsNullOrEmpty(property.managedReferenceFullTypename), () => 
            {
                property.managedReferenceValue = null;
                property.serializedObject.ApplyModifiedProperties();
            });

            var types = TypeCache.GetTypesDerivedFrom(baseType)
                .Where(t => !t.IsAbstract && !t.IsInterface);

            foreach (var type in types)
            {
                menu.AddItem(new GUIContent(type.Name), false, () => 
                {
                    property.managedReferenceValue = Activator.CreateInstance(type);
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        }
    }
}
