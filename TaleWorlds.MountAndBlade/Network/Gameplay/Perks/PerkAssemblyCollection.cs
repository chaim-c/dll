using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Network.Gameplay.Perks
{
	// Token: 0x0200039F RID: 927
	internal static class PerkAssemblyCollection
	{
		// Token: 0x06003251 RID: 12881 RVA: 0x000D0048 File Offset: 0x000CE248
		public static List<Type> GetPerkAssemblyTypes()
		{
			List<Type> list = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Assembly> list2 = new List<Assembly>();
			foreach (Assembly assembly in assemblies)
			{
				try
				{
					if (PerkAssemblyCollection.CheckAssemblyForPerks(assembly))
					{
						list2.Add(assembly);
					}
				}
				catch
				{
				}
			}
			foreach (Assembly assembly2 in list2)
			{
				try
				{
					List<Type> typesSafe = assembly2.GetTypesSafe(null);
					list.AddRange(typesSafe);
				}
				catch
				{
				}
			}
			return list;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000D0104 File Offset: 0x000CE304
		private static bool CheckAssemblyForPerks(Assembly assembly)
		{
			Assembly assembly2 = Assembly.GetAssembly(typeof(MPPerkObject));
			if (assembly == assembly2)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].FullName == assembly2.FullName)
				{
					return true;
				}
			}
			return false;
		}
	}
}
