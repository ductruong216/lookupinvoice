﻿using System.Threading;
using System.Threading.Tasks;

namespace LookupInvoice.Domain.DataContext
{
	public interface IDataContextAsync : IDataContext
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);

		Task<int> SaveChangesAsync();
	}
}