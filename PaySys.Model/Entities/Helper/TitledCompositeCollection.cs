using System.Windows.Data;

namespace PaySys.Model.Helper
{
	public class TitledCompositeCollection
	{
		public string Title { get; set; }

		public CompositeCollection CompositeCollection { get; set; }
	}
}