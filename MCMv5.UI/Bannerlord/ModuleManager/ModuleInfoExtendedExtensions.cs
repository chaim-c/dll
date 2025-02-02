using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000053 RID: 83
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ModuleInfoExtendedExtensions
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000E752 File Offset: 0x0000C952
		public static IEnumerable<DependentModuleMetadata> DependenciesAllDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesAll().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000E77E File Offset: 0x0000C97E
		public static IEnumerable<DependentModuleMetadata> DependenciesAll(this ModuleInfoExtended module)
		{
			foreach (DependentModuleMetadata metadata in module.DependentModuleMetadatas)
			{
				yield return metadata;
				metadata = null;
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			foreach (DependentModule metadata2 in module.DependentModules)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata2.Id,
					LoadType = LoadType.LoadBeforeThis,
					IsOptional = metadata2.IsOptional,
					Version = metadata2.Version
				};
				metadata2 = null;
			}
			IEnumerator<DependentModule> enumerator2 = null;
			foreach (DependentModule metadata3 in module.ModulesToLoadAfterThis)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata3.Id,
					LoadType = LoadType.LoadAfterThis,
					IsOptional = metadata3.IsOptional,
					Version = metadata3.Version
				};
				metadata3 = null;
			}
			IEnumerator<DependentModule> enumerator3 = null;
			foreach (DependentModule metadata4 in module.IncompatibleModules)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata4.Id,
					IsIncompatible = true,
					IsOptional = metadata4.IsOptional,
					Version = metadata4.Version
				};
				metadata4 = null;
			}
			IEnumerator<DependentModule> enumerator4 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000E78E File Offset: 0x0000C98E
		public static IEnumerable<DependentModuleMetadata> DependenciesToLoadDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesToLoad().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000E7BA File Offset: 0x0000C9BA
		public static IEnumerable<DependentModuleMetadata> DependenciesToLoad(this ModuleInfoExtended module)
		{
			foreach (DependentModuleMetadata metadata in from x in module.DependentModuleMetadatas
			where !x.IsIncompatible
			select x)
			{
				yield return metadata;
				metadata = null;
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			foreach (DependentModule metadata2 in module.DependentModules)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata2.Id,
					LoadType = LoadType.LoadBeforeThis,
					IsOptional = metadata2.IsOptional,
					Version = metadata2.Version
				};
				metadata2 = null;
			}
			IEnumerator<DependentModule> enumerator2 = null;
			foreach (DependentModule metadata3 in module.ModulesToLoadAfterThis)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata3.Id,
					LoadType = LoadType.LoadAfterThis,
					IsOptional = metadata3.IsOptional,
					Version = metadata3.Version
				};
				metadata3 = null;
			}
			IEnumerator<DependentModule> enumerator3 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000E7CA File Offset: 0x0000C9CA
		public static IEnumerable<DependentModuleMetadata> DependenciesLoadBeforeThisDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesLoadBeforeThis().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000E7F6 File Offset: 0x0000C9F6
		public static IEnumerable<DependentModuleMetadata> DependenciesLoadBeforeThis(this ModuleInfoExtended module)
		{
			foreach (DependentModuleMetadata metadata in from x in module.DependentModuleMetadatas
			where x.LoadType == LoadType.LoadBeforeThis
			select x)
			{
				yield return metadata;
				metadata = null;
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			foreach (DependentModule metadata2 in module.DependentModules)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata2.Id,
					LoadType = LoadType.LoadBeforeThis,
					IsOptional = metadata2.IsOptional,
					Version = metadata2.Version
				};
				metadata2 = null;
			}
			IEnumerator<DependentModule> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000E806 File Offset: 0x0000CA06
		public static IEnumerable<DependentModuleMetadata> DependenciesLoadAfterThisDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesLoadAfterThis().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000E832 File Offset: 0x0000CA32
		public static IEnumerable<DependentModuleMetadata> DependenciesLoadAfterThis(this ModuleInfoExtended module)
		{
			foreach (DependentModuleMetadata metadata in from x in module.DependentModuleMetadatas
			where x.LoadType == LoadType.LoadAfterThis
			select x)
			{
				yield return metadata;
				metadata = null;
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			foreach (DependentModule metadata2 in module.ModulesToLoadAfterThis)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata2.Id,
					LoadType = LoadType.LoadAfterThis,
					IsOptional = metadata2.IsOptional,
					Version = metadata2.Version
				};
				metadata2 = null;
			}
			IEnumerator<DependentModule> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E842 File Offset: 0x0000CA42
		public static IEnumerable<DependentModuleMetadata> DependenciesIncompatiblesDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesIncompatibles().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000E86E File Offset: 0x0000CA6E
		public static IEnumerable<DependentModuleMetadata> DependenciesIncompatibles(this ModuleInfoExtended module)
		{
			foreach (DependentModuleMetadata metadata in from x in module.DependentModuleMetadatas
			where x.IsIncompatible
			select x)
			{
				yield return metadata;
				metadata = null;
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			foreach (DependentModule metadata2 in module.IncompatibleModules)
			{
				yield return new DependentModuleMetadata
				{
					Id = metadata2.Id,
					IsIncompatible = true,
					IsOptional = metadata2.IsOptional,
					Version = metadata2.Version
				};
				metadata2 = null;
			}
			IEnumerator<DependentModule> enumerator2 = null;
			yield break;
			yield break;
		}
	}
}
