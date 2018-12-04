using System.Data.Entity;

namespace PaySys.Model.EntityFramework
{
	public class PaySysDbInitializer : DropCreateDatabaseIfModelChanges<PaySysContext>
	{
		public PaySysDbInitializer()
		{

		}

//		protected override void Seed(PaySysContext context)
//		{
//		}
	}
}