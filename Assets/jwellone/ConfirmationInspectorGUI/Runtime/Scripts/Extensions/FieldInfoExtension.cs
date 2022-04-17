using System.Reflection;
using System.Runtime.CompilerServices;

public static class FieldInfoExtension
{
	public static bool IsBackingField(this FieldInfo self)
	{
		return self.IsDefined(typeof(CompilerGeneratedAttribute), false);
	}
}