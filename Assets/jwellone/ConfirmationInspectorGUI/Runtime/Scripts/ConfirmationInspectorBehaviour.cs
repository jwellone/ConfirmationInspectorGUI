using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
using jwelloneEditor;
#endif

#nullable enable

namespace jwellone
{
	public class ConfirmationInspectorBehaviour : MonoBehaviour
	{
#if UNITY_EDITOR
		[CustomEditor(typeof(ConfirmationInspectorBehaviour))]
		class CustomInspector : Editor
		{
			int _selectIndex = 0;
			readonly ConfirmationInspectorGUI _gui = new ConfirmationInspectorGUI();

			public override void OnInspectorGUI()
			{
				var self = (ConfirmationInspectorBehaviour)target;
				var list = new List<Component>();
				var options = new List<string>();
				var components = self.gameObject.GetComponents<Behaviour>();

				foreach (var component in components)
				{
					if (component.GetType() == self.GetType())
					{
						continue;
					}

					list.Add(component);
					options.Add(component.GetType().Name);
				}

				if (list.Count <= 0)
				{
					return;
				}

				_selectIndex = EditorGUILayout.Popup("source", _selectIndex, options.ToArray());
				EditorGUILayout.Space();
				_gui.Show(list[_selectIndex]);
			}
		}
#else
		void Awake()
		{
			Destroy(this);
		}
#endif
	}
}
