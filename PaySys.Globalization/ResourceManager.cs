using System.Reflection;
using System.Resources;

namespace PaySys.Globalization
{
	public static class ResourceAccessor
	{
		private static string NamespacePrefix => "PaySys.Globalization.Fa";
		public static ResourceManager Messages => new ResourceManager($"{NamespacePrefix}.Messages", Assembly.GetExecutingAssembly());
		public static ResourceManager Labels => new ResourceManager($"{NamespacePrefix}.Labels", Assembly.GetExecutingAssembly());
		public static ResourceManager FieldicEmployee => new ResourceManager($"{NamespacePrefix}.FieldicEmployee", Assembly.GetExecutingAssembly());
		public static ResourceManager FieldicContractMaster => new ResourceManager($"{NamespacePrefix}.FieldicContractMaster", Assembly.GetExecutingAssembly());
	}
}
