using System;
using System.Collections.Generic;

public static class TypeExtension
{
	public static bool IsClassOrStruct(this Type self)
	{
		return self.IsClass || (self.IsValueType && !self.IsPrimitive && !self.IsEnum);
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