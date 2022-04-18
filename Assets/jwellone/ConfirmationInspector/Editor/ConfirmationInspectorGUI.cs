using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable enable

namespace jwelloneEditor
{
	public class ConfirmationInspectorGUI
	{
		protected const BindingFlags BIND_FLAGS = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		public class Data
		{
			public readonly Type type;
			public readonly string name;
			public readonly object? value;

			public Data(Type type, string name, object? value)
			{
				this.type = type;
				this.name = name;
				this.value = value;
			}
		}

		public interface IGUI
		{
			int order { get; }
			bool Show(in ConfirmationInspectorGUI owner, in Data data);
		}

		readonly List<IGUI> _guis = new List<IGUI>();

		public ConfirmationInspectorGUI()
		{
			Add(BaseValueGUI.instance);
			Add(new UnityObjectGUI());
			Add(new ArrayGUI());
			Add(new ListGUI());
			Add(new ClassOrStructGUI(), true);
		}

		protected void Add(in IGUI gui, bool isSort = false)
		{
			_guis.Add(gui);
			if (isSort)
			{
				_guis.Sort((a, b) => a.order - b.order);
			}
		}

		public void Show(object? obj)
		{
			if (obj == null)
			{
				return;
			}

			foreach (var d in GetData(obj))
			{
				foreach (var gui in _guis)
				{
					if (gui.Show(this, d))
					{
						break;
					}
				}
			}
		}

		protected virtual List<Data> GetData(object obj)
		{
			var data = new List<Data>();

			foreach (var field in obj.GetType().GetFields(BIND_FLAGS))
			{
				if (IsBackingField(field) || field.FieldType.IsDictionary())
				{
					continue;
				}

				var name = $"{field.Name}({field.FieldType.Name})";
				data.Add(new Data(field.FieldType, name, field.GetValue(obj)));
			}

			foreach (var property in obj.GetType().GetProperties(BIND_FLAGS))
			{
				try
				{
					if (property.PropertyType.IsDictionary())
					{
						continue;
					}
					
					var name = $"{property.Name}({property.PropertyType.Name})";
					data.Add(new Data(property.PropertyType, name, property.GetValue(obj, null)));
				}
				catch
				{
					// ignore.
				}
			}

			return data;
		}
		
		static bool IsBackingField(in FieldInfo fieldInfo)
		{
			return fieldInfo.IsDefined(typeof(CompilerGeneratedAttribute), false);
		}
	}
}
