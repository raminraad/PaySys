using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PaySys.ModelAndBindLib.Model
{
	public class TitledCompositeCollection
	{
		public string Title { get; set; }

		public CompositeCollection CompositeCollection { get; set; }
	}
}