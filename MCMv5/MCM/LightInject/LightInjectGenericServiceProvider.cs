using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;

namespace MCM.LightInject
{
	// Token: 0x020000C5 RID: 197
	[NullableContext(1)]
	[Nullable(0)]
	internal class LightInjectGenericServiceProvider : IGenericServiceProvider, IDisposable
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public LightInjectGenericServiceProvider(IServiceContainer serviceContainer)
		{
			this._serviceContainer = serviceContainer;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
		public IGenericServiceProviderScope CreateScope()
		{
			return new LightInjectGenericServiceProviderScope(this._serviceContainer.BeginScope());
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		[return: Nullable(2)]
		public TService GetService<TService>() where TService : class
		{
			TService value = this._serviceContainer.GetInstance<TService>();
			Type type = typeof(TService);
			IEnumerable enumerable;
			bool flag;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				enumerable = (value as IEnumerable);
				flag = (enumerable != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			TService result;
			if (flag2)
			{
				LightInjectGenericServiceProvider.OfTypeDelegate<TService> ofType = (LightInjectGenericServiceProvider.OfTypeDelegate<TService>)LightInjectGenericServiceProvider.OfTypeCache.GetOrAdd(typeof(TService), (Type x) => typeof(Enumerable).GetMethod("OfType").MakeGenericMethod(new Type[]
				{
					x.GenericTypeArguments[0]
				}).CreateDelegate(typeof(LightInjectGenericServiceProvider.OfTypeDelegate<TService>)));
				result = ofType(enumerable.Cast<object>().Distinct(LightInjectGenericServiceProvider.Comparer));
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000C690 File Offset: 0x0000A890
		public void Dispose()
		{
			this._serviceContainer.Dispose();
		}

		// Token: 0x04000169 RID: 361
		private static readonly LightInjectGenericServiceProvider.ReferenceEqualityComparer Comparer = new LightInjectGenericServiceProvider.ReferenceEqualityComparer();

		// Token: 0x0400016A RID: 362
		private static readonly ConcurrentDictionary<Type, object> OfTypeCache = new ConcurrentDictionary<Type, object>();

		// Token: 0x0400016B RID: 363
		private readonly IServiceContainer _serviceContainer;

		// Token: 0x020001DF RID: 479
		[Nullable(0)]
		private class ReferenceEqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x06000C1F RID: 3103 RVA: 0x000261B4 File Offset: 0x000243B4
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x000261BA File Offset: 0x000243BA
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x020001E0 RID: 480
		// (Invoke) Token: 0x06000C23 RID: 3107
		[NullableContext(0)]
		private delegate TService OfTypeDelegate<[Nullable(2)] TService>(IEnumerable<object> enumerable);
	}
}
