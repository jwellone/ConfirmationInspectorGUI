using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#nullable enable

namespace jwelloneEditor
{
	public class ConfirmationInspector : EditorWindow
	{
		[SerializeField] int _selectIndex;
		[SerializeField] Vector2 _scrollPosition;
		ConfirmationInspectorGUI? _gui;

		[MenuItem("jwellone/Window/Confirmation Inspector")]
		static void Open()
		{
			GetWindow<ConfirmationInspector>("Confirmation Inspector");
		}

		void OnEnable()
		{
			Selection.selectionChanged += OnSelectionChanged;
		}

		private void OnDisable()
		{
			Selection.selectionChanged -= OnSelectionChanged;
			_gui = null;
		}

		void OnGUI()
		{
			var target = Selection.activeObject as GameObject;
			if (target == null)
			{
				return;
			}

			_gui ??= new ConfirmationInspectorGUI();

			var list = new List<Component>();
			var options = new List<string>();
			var components = target.GetComponents<Component>();

			foreach (var component in components)
			{
				if (component == null)
				{
					Debug.LogWarning($"There is a missing scripts.");
					continue;
				}
				
				list.Add(component);
				options.Add(component.GetType().Name);
			}

			if (list.Count <= 0)
			{
				return;
			}

			if (_selectIndex >= list.Count)
			{
				_selectIndex = 0;
			}

			EditorGUILayout.ObjectField("target", target, target.GetType(), false);
			_selectIndex = EditorGUILayout.Popup("select component", _selectIndex, options.ToArray());
			EditorGUILayout.Space();

			_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition,GUI.skin.window);
			_gui.Show(list[_selectIndex]);
			EditorGUILayout.EndScrollView();
		}

		void OnInspectorUpdate()
		{
			Repaint();
		}

		void Reset()
		{
			_selectIndex = 0;
			_scrollPosition = Vector2.zero;
			_gui = null;
		}

		void OnSelectionChanged()
		{
			Reset();
			Repaint();
		}
	}
}