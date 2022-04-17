#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#nullable enable

namespace jwelloneEditor
{
	using GUI = ConfirmationInspectorGUI.IGUI;
	using GUIData = ConfirmationInspectorGUI.Data;

	public sealed class BoolGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(bool))
			{
				return false;
			}

			EditorGUILayout.Toggle(data.name, (bool)(data.value ?? false));
			return true;
		}
	}

	public sealed class ValueTypeGUI<T> : GUI where T : struct
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type == typeof(T))
			{
				var text = data.value != null ? ((T)data.value).ToString() : "none";
				EditorGUILayout.TextField(data.name, text);
				return true;
			}

			return false;
		}
	}

	public sealed class BaseValueGUI : GUI
	{
		static BaseValueGUI _instance;

		readonly EnumGUI _enumGUI = new EnumGUI();
		readonly Dictionary<Type, GUI> _dicGUI = new Dictionary<Type, GUI>();


		public static BaseValueGUI instance
		{
			get
			{
				if(_instance==null)
				{
					_instance = new BaseValueGUI();
				}

				return _instance;
			}
		}

		public int order => 0;

		BaseValueGUI()
		{
			_dicGUI.Add(typeof(bool), new BoolGUI());
			_dicGUI.Add(typeof(short), new ValueTypeGUI<short>());
			_dicGUI.Add(typeof(ushort), new ValueTypeGUI<ushort>());
			_dicGUI.Add(typeof(int), new ValueTypeGUI<int>());
			_dicGUI.Add(typeof(uint), new ValueTypeGUI<uint>());
			_dicGUI.Add(typeof(long), new ValueTypeGUI<long>());
			_dicGUI.Add(typeof(ulong), new ValueTypeGUI<ulong>());
			_dicGUI.Add(typeof(float), new ValueTypeGUI<float>());
			_dicGUI.Add(typeof(double), new ValueTypeGUI<double>());
			_dicGUI.Add(typeof(DateTime), new ValueTypeGUI<DateTime>());
			_dicGUI.Add(typeof(string), new StringGUI());
			_dicGUI.Add(typeof(Vector2), new Vector2GUI());
			_dicGUI.Add(typeof(Vector2Int), new Vector2IntGUI());
			_dicGUI.Add(typeof(Vector3), new Vector3GUI());
			_dicGUI.Add(typeof(Vector3Int), new Vector3IntGUI());
			_dicGUI.Add(typeof(Vector4), new Vector4GUI());
			_dicGUI.Add(typeof(Color), new ColorGUI());
			_dicGUI.Add(typeof(Color32), new Color32GUI());
		}

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if(_enumGUI.Show(owner, data))
			{
				return true;
			}

			if(_dicGUI.TryGetValue(data.type,out var gui))
			{
				return gui.Show(owner, data);
			}

			return false;
		}
	}


	public sealed class StringGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type == typeof(string))
			{
				EditorGUILayout.TextField(data.name, data.value?.ToString());
				return true;
			}

			return false;
		}
	}

	public sealed class ColorGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Color))
			{
				return false;
			}

			EditorGUILayout.ColorField(data.name, (Color)(data.value ?? Color.clear));
			return true;
		}
	}

	public sealed class Color32GUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Color32))
			{
				return false;
			}

			EditorGUILayout.ColorField(data.name, (Color)(data.value ?? Color.clear));
			return true;
		}
	}

	public sealed class Vector2IntGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Vector2Int))
			{
				return false;
			}

			EditorGUILayout.Vector2IntField(data.name, (Vector2Int)(data.value ?? Vector2Int.zero));
			return true;
		}
	}

	public sealed class Vector2GUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Vector2))
			{
				return false;
			}

			EditorGUILayout.Vector2Field(data.name, (Vector2)(data.value ?? Vector2.zero));
			return true;
		}
	}

	public sealed class Vector3IntGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Vector3Int))
			{
				return false;
			}

			EditorGUILayout.Vector3IntField(data.name, (Vector3Int)(data.value ?? Vector3Int.zero));
			return true;
		}
	}

	public sealed class Vector3GUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Vector3))
			{
				return false;
			}

			EditorGUILayout.Vector3Field(data.name, (Vector3)(data.value ?? Vector3.zero));
			return true;
		}
	}

	public sealed class Vector4GUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Vector4))
			{
				return false;
			}

			EditorGUILayout.Vector4Field(data.name, (Vector4)(data.value ?? Vector4.zero));
			return true;
		}
	}

	public sealed class EnumGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type.IsEnum)
			{
				EditorGUILayout.EnumPopup(data.name, (Enum)data.value!);
				return true;
			}

			return false;
		}
	}

	public sealed class UnityObjectGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.value is UnityEngine.Object value)
			{
				EditorGUILayout.ObjectField(data.name, value, data.type, false);
				return true;
			}

			return false;
		}
	}

	public sealed class ArrayGUI : GUI
	{
		readonly FoldoutLayout _foldout = new FoldoutLayout();
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (!data.type.IsArray)
			{
				return false;
			}

			var key = $"{data.type}_{data.name}";
			if (!_foldout.Show(data.name, key))
			{
				return true;
			}

			EditorGUI.indentLevel += 1;

			var count = data.value == null ? 0 : (int)data.type.GetProperty("Length")!.GetValue(data.value, null);
			for (var i = 0; i < count; ++i)
			{
				var value = ((Array)data.value!).GetValue(i);

				if(!BaseValueGUI.instance.Show(owner, new GUIData(value.GetType(), $"Element{i}", value)))
				{
					if (_foldout.Show($"Element{i}", $"{key}_{value.GetType()}_Element{i}"))
					{
						EditorGUI.indentLevel += 1;

						owner.Show(value);

						EditorGUI.indentLevel -= 1;
					}
				}
			}

			EditorGUI.indentLevel -= 1;

			return true;
		}
	}

	public sealed class ListGUI : GUI
	{
		readonly FoldoutLayout _foldout = new FoldoutLayout();
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (!data.type.IsList())
			{
				return false;
			}

			var key = $"{data.type}_{data.name}";
			if (_foldout.Show(data.name, key))
			{
				var count = (int)data.type.GetProperty("Count")!.GetValue(data.value, null);
				if (count > 0)
				{
					var property = data.type.GetProperty("Item");
					for (var i = 0; i < count; ++i)
					{
						var value = property!.GetValue(data.value, new object[] { i });
						if (!BaseValueGUI.instance.Show(owner, new GUIData(value.GetType(), $"Element{i}", value)))
						{
							EditorGUI.indentLevel += 1;

							if (_foldout.Show($"Element{i}", $"{key}_{value.GetType()}_Element{i}"))
							{
								EditorGUI.indentLevel += 1;
								owner.Show(value);
								EditorGUI.indentLevel -= 1;
							}

							EditorGUI.indentLevel -= 1;
						}
					}
				}
				else
				{
					EditorGUI.indentLevel += 1;
					EditorGUILayout.LabelField("none");
					EditorGUI.indentLevel -= 1;
				}
			}

			return true;
		}
	}

	public sealed class ClassOrStructGUI : GUI
	{
		readonly FoldoutLayout _foldout = new FoldoutLayout();

		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (!data.type.IsClassOrStruct())
			{
				return false;
			}

			if (_foldout.Show(data.name, $"{data.type}_{data.name}"))
			{
				EditorGUI.indentLevel += 1;
				owner.Show(data.value);
				EditorGUI.indentLevel -= 1;
			}

			return true;
		}
	}

	public class FoldoutLayout
	{
		readonly Dictionary<string, bool> _dicFlags = new Dictionary<string, bool>();

		public bool Show(string name, string key)
		{
			if (!_dicFlags.ContainsKey(key))
			{
				_dicFlags.Add(key, false);
			}

			_dicFlags[key] = EditorGUILayout.Foldout(_dicFlags[key], name);
			return _dicFlags[key];
		}
	}
}
#endif