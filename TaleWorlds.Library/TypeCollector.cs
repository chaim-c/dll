using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaleWorlds.Library
{
	// Token: 0x02000099 RID: 153
	public class TypeCollector<T> where T : class
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001104C File Offset: 0x0000F24C
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x00011054 File Offset: 0x0000F254
		public Type BaseType { get; private set; }

		// Token: 0x06000530 RID: 1328 RVA: 0x0001105D File Offset: 0x0000F25D
		public TypeCollector()
		{
			this.BaseType = typeof(T);
			this._types = new Dictionary<string, Type>();
			this._currentAssembly = this.BaseType.Assembly;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00011094 File Offset: 0x0000F294
		public void Collect()
		{
			List<Type> list = this.CollectTypes();
			this._types.Clear();
			foreach (Type type in list)
			{
				this._types.Add(type.Name, type);
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00011100 File Offset: 0x0000F300
		public T Instantiate(string typeName, params object[] parameters)
		{
			T result = default(T);
			Type type;
			if (this._types.TryGetValue(typeName, out type))
			{
				result = (T)((object)type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new Type[0], null).Invoke(parameters));
			}
			return result;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00011148 File Offset: 0x0000F348
		public Type GetType(string typeName)
		{
			Type result;
			if (this._types.TryGetValue(typeName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00011168 File Offset: 0x0000F368
		private bool CheckAssemblyReferencesThis(Assembly assembly)
		{
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].Name == this._currentAssembly.GetName().Name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000111AC File Offset: 0x0000F3AC
		private List<Type> CollectTypes()
		{
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (this.CheckAssemblyReferencesThis(assembly) || assembly == this._currentAssembly)
				{
					foreach (Type type in assembly.GetTypesSafe(null))
					{
						if (this.BaseType.IsAssignableFrom(type))
						{
							list.Add(type);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04000187 RID: 391
		private Dictionary<string, Type> _types;

		// Token: 0x04000188 RID: 392
		private Assembly _currentAssembly;
	}
}
