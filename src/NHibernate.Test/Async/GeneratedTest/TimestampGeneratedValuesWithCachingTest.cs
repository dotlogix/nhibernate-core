﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections;

using NHibernate.Dialect;

using NUnit.Framework;

namespace NHibernate.Test.GeneratedTest
{
	using System.Threading.Tasks;
	[TestFixture]
	public class TimestampGeneratedValuesWithCachingTestAsync : AbstractGeneratedPropertyTestAsync
	{
		protected override IList Mappings
		{
			get { return new string[] { "GeneratedTest.MSSQLGeneratedPropertyEntity.hbm.xml" }; }
		}

		protected override bool AppliesTo(Dialect.Dialect dialect)
		{
			// this test is specific to SQL Server as it is testing support
			// for its TIMESTAMP datatype...
			return dialect is MsSql2000Dialect;
		}
	}
}
