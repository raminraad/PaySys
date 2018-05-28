using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaySys.UI.Resource
{
	public static class ResourceAccessor
	{
		private static string NamespacePrefix => "PaySys.UI.Resource.Globalizaton.Fa";
		public static ResourceManager MessageLib => new ResourceManager($"{NamespacePrefix}.MessageLib", Assembly.GetExecutingAssembly());
		public static ResourceManager LabelLib => new ResourceManager($"{NamespacePrefix}.LabelLib", Assembly.GetExecutingAssembly());
		public static ResourceManager FieldicEmployee => new ResourceManager($"{NamespacePrefix}.FieldicEmployee", Assembly.GetExecutingAssembly());
	}
}
