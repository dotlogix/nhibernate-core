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
using NHibernate.Cache.Access;

namespace NHibernate.Cache
{
	using System.Threading.Tasks;
	using System.Threading;
	public partial class NonstrictReadWriteCache : ICacheConcurrencyStrategy
	{

		/// <summary>
		/// Get the most recent version, if available.
		/// </summary>
		public async Task<object> GetAsync(CacheKey key, long txTimestamp, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (log.IsDebugEnabled())
			{
				log.Debug("Cache lookup: {0}", key);
			}

			object result = await (cache.GetAsync(key, cancellationToken)).ConfigureAwait(false);
			if (result != null)
			{
				log.Debug("Cache hit");
			}
			else
			{
				log.Debug("Cache miss");
			}
			return result;
		}

		/// <summary>
		/// Add an item to the cache
		/// </summary>
		public async Task<bool> PutAsync(CacheKey key, object value, long txTimestamp, object version, IComparer versionComparator,
		                bool minimalPut, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (txTimestamp == long.MinValue)
			{
				// MinValue means cache is disabled
				return false;
			}

			if (minimalPut && await (cache.GetAsync(key, cancellationToken)).ConfigureAwait(false) != null)
			{
				if (log.IsDebugEnabled())
				{
					log.Debug("item already cached: {0}", key);
				}
				return false;
			}
			if (log.IsDebugEnabled())
			{
				log.Debug("Caching: {0}", key);
			}
			await (cache.PutAsync(key, value, cancellationToken)).ConfigureAwait(false);
			return true;
		}

		/// <summary>
		/// Do nothing
		/// </summary>
		public Task<ISoftLock> LockAsync(CacheKey key, object version, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<ISoftLock>(cancellationToken);
			}
			try
			{
				return Task.FromResult<ISoftLock>(Lock(key, version));
			}
			catch (Exception ex)
			{
				return Task.FromException<ISoftLock>(ex);
			}
		}

		public Task RemoveAsync(CacheKey key, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<object>(cancellationToken);
			}
			try
			{
				if (log.IsDebugEnabled())
				{
					log.Debug("Removing: {0}", key);
				}
				return cache.RemoveAsync(key, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		public Task ClearAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<object>(cancellationToken);
			}
			try
			{
				if (log.IsDebugEnabled())
				{
					log.Debug("Clearing");
				}
				return cache.ClearAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		/// <summary>
		/// Invalidate the item
		/// </summary>
		public Task EvictAsync(CacheKey key, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<object>(cancellationToken);
			}
			try
			{
				if (log.IsDebugEnabled())
				{
					log.Debug("Invalidating: {0}", key);
				}
				return cache.RemoveAsync(key, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		/// <summary>
		/// Invalidate the item
		/// </summary>
		public async Task<bool> UpdateAsync(CacheKey key, object value, object currentVersion, object previousVersion, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await (EvictAsync(key, cancellationToken)).ConfigureAwait(false);
			return false;
		}

		/// <summary>
		/// Invalidate the item (again, for safety).
		/// </summary>
		public Task ReleaseAsync(CacheKey key, ISoftLock @lock, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<object>(cancellationToken);
			}
			try
			{
				if (log.IsDebugEnabled())
				{
					log.Debug("Invalidating (again): {0}", key);
				}

				return cache.RemoveAsync(key, cancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		/// <summary>
		/// Invalidate the item (again, for safety).
		/// </summary>
		public async Task<bool> AfterUpdateAsync(CacheKey key, object value, object version, ISoftLock @lock, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await (ReleaseAsync(key, @lock, cancellationToken)).ConfigureAwait(false);
			return false;
		}

		/// <summary>
		/// Do nothing
		/// </summary>
		public Task<bool> AfterInsertAsync(CacheKey key, object value, object version, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<bool>(cancellationToken);
			}
			try
			{
				return Task.FromResult<bool>(AfterInsert(key, value, version));
			}
			catch (Exception ex)
			{
				return Task.FromException<bool>(ex);
			}
		}
	}
}
