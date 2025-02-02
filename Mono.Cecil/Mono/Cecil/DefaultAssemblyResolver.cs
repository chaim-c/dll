using System;
using System.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000A3 RID: 163
	public class DefaultAssemblyResolver : BaseAssemblyResolver
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00017B2C File Offset: 0x00015D2C
		public DefaultAssemblyResolver()
		{
			this.cache = new Dictionary<string, AssemblyDefinition>(StringComparer.Ordinal);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00017B44 File Offset: 0x00015D44
		public override AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			AssemblyDefinition assemblyDefinition;
			if (this.cache.TryGetValue(name.FullName, out assemblyDefinition))
			{
				return assemblyDefinition;
			}
			assemblyDefinition = base.Resolve(name);
			this.cache[name.FullName] = assemblyDefinition;
			return assemblyDefinition;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00017B94 File Offset: 0x00015D94
		protected void RegisterAssembly(AssemblyDefinition assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			string fullName = assembly.Name.FullName;
			if (this.cache.ContainsKey(fullName))
			{
				return;
			}
			this.cache[fullName] = assembly;
		}

		// Token: 0x04000424 RID: 1060
		private readonly IDictionary<string, AssemblyDefinition> cache;
	}
}
