using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Bogus;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.ModelAndBindLib.Engine
{
	public class PaySysDbInitializer : DropCreateDatabaseIfModelChanges<PaySysContext>
	{
		public PaySysDbInitializer()
		{

		}

		protected override void Seed(PaySysContext context)
		{
		}
	}
}