using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MCM.LightInject
{
	// Token: 0x0200010A RID: 266
	[ExcludeFromCodeCoverage]
	internal class Scope : IServiceFactory, IDisposable
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00013DCB File Offset: 0x00011FCB
		public Scope(IScopeManager scopeManager, Scope parentScope)
		{
			this.scopeManager = scopeManager;
			this.serviceFactory = (ServiceContainer)scopeManager.ServiceFactory;
			this.ParentScope = parentScope;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00013E0A File Offset: 0x0001200A
		public Scope(ServiceContainer serviceFactory)
		{
			this.serviceFactory = serviceFactory;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000666 RID: 1638 RVA: 0x00013E34 File Offset: 0x00012034
		// (remove) Token: 0x06000667 RID: 1639 RVA: 0x00013E6C File Offset: 0x0001206C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> Completed;

		// Token: 0x06000668 RID: 1640 RVA: 0x00013EA4 File Offset: 0x000120A4
		public void TrackInstance(object disposable)
		{
			object obj = this.lockObject;
			lock (obj)
			{
				bool flag2 = this.disposableObjects == null;
				if (flag2)
				{
					this.disposableObjects = new List<object>();
				}
				this.disposableObjects.Add(disposable);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00013F0C File Offset: 0x0001210C
		public void Dispose()
		{
			bool flag = this.disposableObjects != null && this.disposableObjects.Count > 0;
			if (flag)
			{
				HashSet<IDisposable> disposedObjects = new HashSet<IDisposable>(Scope.ReferenceEqualityComparer<IDisposable>.Default);
				for (int i = this.disposableObjects.Count - 1; i >= 0; i--)
				{
					IDisposable disposable = this.disposableObjects[i] as IDisposable;
					bool flag2 = disposable != null;
					if (!flag2)
					{
						throw new InvalidOperationException(string.Format("The type {0} only implements `IAsyncDisposable` and can only be disposed in an asynchronous scope started with `BeginScopeAsync()`", this.disposableObjects[i].GetType()));
					}
					bool flag3 = disposedObjects.Add(disposable);
					if (flag3)
					{
						disposable.Dispose();
					}
				}
			}
			this.EndScope();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00013FCC File Offset: 0x000121CC
		private void EndScope()
		{
			IScopeManager scopeManager = this.scopeManager;
			if (scopeManager != null)
			{
				scopeManager.EndScope(this);
			}
			EventHandler<EventArgs> completedHandler = this.Completed;
			if (completedHandler != null)
			{
				completedHandler(this, new EventArgs());
			}
			this.IsDisposed = true;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001400D File Offset: 0x0001220D
		public Scope BeginScope()
		{
			return this.serviceFactory.BeginScope();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001401A File Offset: 0x0001221A
		public object GetInstance(Type serviceType)
		{
			return this.serviceFactory.GetInstance(serviceType, this);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00014029 File Offset: 0x00012229
		public object GetInstance(Type serviceType, string serviceName)
		{
			return this.serviceFactory.GetInstance(serviceType, this, serviceName);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00014039 File Offset: 0x00012239
		public object GetInstance(Type serviceType, object[] arguments)
		{
			return this.serviceFactory.GetInstance(serviceType, arguments, this);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00014049 File Offset: 0x00012249
		public object GetInstance(Type serviceType, string serviceName, object[] arguments)
		{
			return this.serviceFactory.GetInstance(serviceType, serviceName, arguments, this);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001405A File Offset: 0x0001225A
		public object TryGetInstance(Type serviceType)
		{
			return this.serviceFactory.TryGetInstance(serviceType, this);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00014069 File Offset: 0x00012269
		public object TryGetInstance(Type serviceType, string serviceName)
		{
			return this.serviceFactory.TryGetInstance(serviceType, serviceName, this);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00014079 File Offset: 0x00012279
		public IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return this.serviceFactory.GetAllInstances(serviceType, this);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00014088 File Offset: 0x00012288
		public object Create(Type serviceType)
		{
			return this.serviceFactory.Create(serviceType, this);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00014098 File Offset: 0x00012298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal object GetScopedInstance(GetInstanceDelegate getInstanceDelegate, object[] arguments, int instanceDelegateIndex)
		{
			object createdInstance = this.createdInstances.Search(instanceDelegateIndex);
			bool flag = createdInstance != null;
			object result;
			if (flag)
			{
				result = createdInstance;
			}
			else
			{
				object obj = this.lockObject;
				lock (obj)
				{
					createdInstance = this.createdInstances.Search(instanceDelegateIndex);
					bool flag3 = createdInstance == null;
					if (flag3)
					{
						createdInstance = getInstanceDelegate(arguments, this);
						bool flag4 = createdInstance is IDisposable;
						if (flag4)
						{
							this.TrackInstance(createdInstance);
						}
						Interlocked.Exchange<ImmutableMapTree<object>>(ref this.createdInstances, this.createdInstances.Add(instanceDelegateIndex, createdInstance));
					}
					result = createdInstance;
				}
			}
			return result;
		}

		// Token: 0x040001D8 RID: 472
		public bool IsDisposed;

		// Token: 0x040001D9 RID: 473
		public Scope ParentScope;

		// Token: 0x040001DA RID: 474
		private readonly object lockObject = new object();

		// Token: 0x040001DB RID: 475
		private readonly IScopeManager scopeManager;

		// Token: 0x040001DC RID: 476
		private readonly ServiceContainer serviceFactory;

		// Token: 0x040001DD RID: 477
		private List<object> disposableObjects;

		// Token: 0x040001DE RID: 478
		private ImmutableMapTree<object> createdInstances = ImmutableMapTree<object>.Empty;

		// Token: 0x02000223 RID: 547
		private class ReferenceEqualityComparer<T> : IEqualityComparer<T>
		{
			// Token: 0x06000CFD RID: 3325 RVA: 0x000276C4 File Offset: 0x000258C4
			public bool Equals(T x, T y)
			{
				return x == y;
			}

			// Token: 0x06000CFE RID: 3326 RVA: 0x000276E4 File Offset: 0x000258E4
			public int GetHashCode(T obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x040004C7 RID: 1223
			public static readonly Scope.ReferenceEqualityComparer<T> Default = new Scope.ReferenceEqualityComparer<T>();
		}
	}
}
