using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005C RID: 92
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class HasTableauCache : Attribute
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00007085 File Offset: 0x00005285
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0000708D File Offset: 0x0000528D
		public Type TableauCacheType { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00007096 File Offset: 0x00005296
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0000709E File Offset: 0x0000529E
		public Type MaterialCacheIDGetType { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x000070A7 File Offset: 0x000052A7
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x000070AE File Offset: 0x000052AE
		internal static Dictionary<Type, MaterialCacheIDGetMethodDelegate> TableauCacheTypes { get; private set; }

		// Token: 0x060007AD RID: 1965 RVA: 0x000070B6 File Offset: 0x000052B6
		public HasTableauCache(Type tableauCacheType, Type materialCacheIDGetType)
		{
			this.TableauCacheType = tableauCacheType;
			this.MaterialCacheIDGetType = materialCacheIDGetType;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000070CC File Offset: 0x000052CC
		public static void CollectTableauCacheTypes()
		{
			HasTableauCache.TableauCacheTypes = new Dictionary<Type, MaterialCacheIDGetMethodDelegate>();
			HasTableauCache.CollectTableauCacheTypesFrom(typeof(HasTableauCache).Assembly);
			Assembly[] viewAssemblies = HasTableauCache.GetViewAssemblies();
			for (int i = 0; i < viewAssemblies.Length; i++)
			{
				HasTableauCache.CollectTableauCacheTypesFrom(viewAssemblies[i]);
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00007114 File Offset: 0x00005314
		private static void CollectTableauCacheTypesFrom(Assembly assembly)
		{
			object[] customAttributesSafe = assembly.GetCustomAttributesSafe(typeof(HasTableauCache), true);
			if (customAttributesSafe.Length != 0)
			{
				foreach (HasTableauCache hasTableauCache in customAttributesSafe)
				{
					MethodInfo method = hasTableauCache.MaterialCacheIDGetType.GetMethod("GetMaterialCacheID", BindingFlags.Static | BindingFlags.Public);
					MaterialCacheIDGetMethodDelegate value = (MaterialCacheIDGetMethodDelegate)Delegate.CreateDelegate(typeof(MaterialCacheIDGetMethodDelegate), method);
					HasTableauCache.TableauCacheTypes.Add(hasTableauCache.TableauCacheType, value);
				}
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00007190 File Offset: 0x00005390
		private static Assembly[] GetViewAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(HasTableauCache).Assembly;
			foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
				for (int j = 0; j < referencedAssemblies.Length; j++)
				{
					if (referencedAssemblies[j].ToString() == assembly.GetName().ToString())
					{
						list.Add(assembly2);
						break;
					}
				}
			}
			return list.ToArray();
		}
	}
}
