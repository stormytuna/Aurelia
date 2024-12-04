using UnityEditor;
using UnityEngine;

namespace Meta
{
	public class LayerAttribute : PropertyAttribute
	{
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(LayerAttribute))]
	public class LayerAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if (property.propertyType == SerializedPropertyType.Integer) {
				property.intValue = EditorGUI.LayerField(position, label, property.intValue);
			}
			else {
				EditorGUI.LabelField(position, label.text, "Use LayerAttribute with int.");
			}
		}
	}
#endif
}
