using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
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
		public static readonly BaseValueGUI instance = new BaseValueGUI();

		readonly EnumGUI _enumGUI = new EnumGUI();
		readonly Dictionary<Type, GUI> _dicGUI = new Dictionary<Type, GUI>();

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
			_dicGUI.Add(typeof(Rect), new RectGUI());
			_dicGUI.Add(typeof(RectInt), new RectIntGUI());
			_dicGUI.Add(typeof(Quaternion), new QuaternionGUI());
			_dicGUI.Add(typeof(Matrix4x4), new Matrix4x4GUI());
		}

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (_enumGUI.Show(owner, data))
			{
				return true;
			}

			if (_dicGUI.TryGetValue(data.type, out var gui))
			{
				return gui.Show(owner, data);
			}

			return false;
		}
	}

	public sealed class ActionFuncDelegateGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (!(data.type.IsDelegate() || data.type.IsAction() || data.type.IsFunc()))
			{
				return false;
			}

			EditorGUILayout.LabelField(data.name);

			EditorGUI.indentLevel += 1;

			object? target = null;
			MethodInfo? methodInfo = null;

			if (data.value != null)
			{
				var propertyTarget = data.type.GetProperty("Target");
				target = propertyTarget.GetValue(data.value);

				var propertyMethod = data.type.GetProperty("Method");
				methodInfo = propertyMethod.GetValue(data.value) as MethodInfo;
			}

			if (target is UnityEngine.Object value)
			{
				EditorGUILayout.ObjectField("Target", value, data.type, false);
			}
			else
			{
				EditorGUILayout.TextField("Target", target?.ToString());
			}

			EditorGUILayout.TextField("Method", methodInfo?.Name);

			EditorGUI.indentLevel -= 1;


			return true;
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

	public sealed class RectGUI : GUI
	{

		public int order => 0;
		public bool Show(in ConfirmationInspectorGUI owrner, in GUIData data)
		{
			if (data.type != typeof(Rect))
			{
				return false;
			}

			EditorGUILayout.RectField(data.name, (Rect)(data.value ?? Rect.zero));

			return true;
		}
	}

	public sealed class RectIntGUI : GUI
	{

		public int order => 0;
		public bool Show(in ConfirmationInspectorGUI owrner, in GUIData data)
		{
			if (data.type != typeof(RectInt))
			{
				return false;
			}

			EditorGUILayout.RectIntField(data.name, (RectInt)(data.value ?? Rect.zero));

			return true;
		}
	}

	public sealed class QuaternionGUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Quaternion))
			{
				return false;
			}

			var q = (Quaternion)(data.value ?? Quaternion.identity);
			var vec4 = new Vector4(q.x, q.y, q.z, q.w);
			EditorGUILayout.Vector4Field(data.name, vec4);
			return true;
		}
	}

	public sealed class Matrix4x4GUI : GUI
	{
		public int order => 0;

		public bool Show(in ConfirmationInspectorGUI owner, in GUIData data)
		{
			if (data.type != typeof(Matrix4x4))
			{
				return false;
			}

			var matrix = (Matrix4x4)(data.value ?? Matrix4x4.identity);
			EditorGUILayout.LabelField(data.name);
			var option = GUILayout.Width((EditorGUIUtility.currentViewWidth - 200) / 4f);
			for (var i = 0; i < 4; ++i)
			{
				var index = i * 4;
				var row = index % 4;
				var column = index / 4;
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				ValueField($"m{column}{row}", matrix[row, column], option);
				ValueField($"m{column}{++row}", matrix[row, column], option);
				ValueField($"m{column}{++row}", matrix[row, column], option);
				ValueField($"m{column}{++row}", matrix[row, column], option);
				EditorGUILayout.EndHorizontal();
			}

			return true;
		}

		void ValueField(string name, float value, GUILayoutOption option)
		{
			GUILayout.Label(name, GUILayout.Width(30));
			GUILayout.TextField(value.ToString(CultureInfo.InvariantCulture), option);
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
		readonly FoldoutLayout _foldout = new();
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
				object? value = ((Array)data.value!).GetValue(i);

				if(value==null)
				{
					continue;
				}

				if (!BaseValueGUI.instance.Show(owner, new GUIData(value.GetType(), $"Element{i}", value)))
				{
					if (_foldout.Show($"Element{i}", $"{key}_{value?.GetType()}_Element{i}"))
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
				var count = data.value == null ? 0 : (int)data.type.GetProperty("Count")!.GetValue(data.value, null);
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

			if (data.value == null)
			{
				EditorGUILayout.TextField(data.name, "none");
			}
			else
			{
				if (_foldout.Show(data.name, $"{data.type}_{data.name}"))
				{
					EditorGUI.indentLevel += 1;
					owner.Show(data.value);
					EditorGUI.indentLevel -= 1;
				}
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

	public static class TypeExtension
	{
		public static bool IsClassOrStruct(this Type self)
		{
			return self.IsClass || (self.IsValueType && !self.IsPrimitive && !self.IsEnum);
		}

		public static bool IsAction(this Type self)
		{
			return self == typeof(Action) ||
				self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Action<>) ||
				self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Action<,>) ||
				self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Action<,,>) ||
				self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Action<,,,>);
		}

		public static bool IsFunc(this Type self)
		{
			return self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Func<>) ||
				self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Func<,>) ||
				self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Func<,,>) ||
				self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Func<,,,>);
		}

		public static bool IsDelegate(this Type self)
		{
			return self.IsSubclassOf(typeof(Delegate)) || self.Equals(typeof(Delegate));
		}

		public static bool IsList(this Type self)
		{
			return self.IsGenericType && self.GetGenericTypeDefinition() == typeof(List<>);
		}

		public static bool IsDictionary(this Type self)
		{
			return self.IsGenericType && self.GetGenericTypeDefinition() == typeof(Dictionary<,>);
		}
	}
}