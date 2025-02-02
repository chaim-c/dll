using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000121 RID: 289
	[ExcludeFromCodeCoverage]
	internal static class ScopeLoader
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x000169A4 File Offset: 0x00014BA4
		public static object ValidateTrackedTransient(object instance, Scope scope)
		{
			bool flag = instance is IDisposable;
			if (flag)
			{
				bool flag2 = scope == null;
				if (flag2)
				{
					string message = string.Format("The disposable instance ({0}) was created outside a scope. If 'ContainerOptions.EnableCurrentScope=false',\nthe service must be requested directly from the scope. If `ContainerOptions.EnableCurrentScope=true`, the service can be requested from the container,\nbut either way the scope has to be started with 'container.BeginScope()'", instance.GetType());
					throw new InvalidOperationException(message);
				}
				scope.TrackInstance(instance);
			}
			return instance;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000169F0 File Offset: 0x00014BF0
		public static Scope ValidateScope<TService>(Scope scope)
		{
			bool flag = scope == null;
			if (flag)
			{
				string message = string.Format("Attempt to create a scoped instance ({0}) outside a scope. If 'ContainerOptions.EnableCurrentScope=false',\nthe service must be requested directly from the scope. If `ContainerOptions.EnableCurrentScope=true`, the service can be requested from the container,\nbut either way the scope has to be started with 'container.BeginScope()'", typeof(TService));
				throw new InvalidOperationException(message);
			}
			return scope;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00016A28 File Offset: 0x00014C28
		public static Scope GetThisOrCurrentScope(Scope scope, IScopeManager scopeManager)
		{
			bool flag = scope != null;
			Scope result;
			if (flag)
			{
				result = scope;
			}
			else
			{
				result = scopeManager.CurrentScope;
			}
			return result;
		}

		// Token: 0x04000214 RID: 532
		public static readonly MethodInfo GetThisOrCurrentScopeMethod = typeof(ScopeLoader).GetTypeInfo().GetDeclaredMethod("GetThisOrCurrentScope");

		// Token: 0x04000215 RID: 533
		public static readonly MethodInfo GetScopedInstanceMethod = typeof(Scope).GetTypeInfo().GetDeclaredMethod("GetScopedInstance");

		// Token: 0x04000216 RID: 534
		public static readonly MethodInfo ValidateScopeMethod = typeof(ScopeLoader).GetTypeInfo().GetDeclaredMethod("ValidateScope");

		// Token: 0x04000217 RID: 535
		public static readonly MethodInfo ValidateTrackedTransientMethod = typeof(ScopeLoader).GetTypeInfo().GetDeclaredMethod("ValidateTrackedTransient");
	}
}
