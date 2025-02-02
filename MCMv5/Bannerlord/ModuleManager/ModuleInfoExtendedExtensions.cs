using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000140 RID: 320
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ModuleInfoExtendedExtensions
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0001C416 File Offset: 0x0001A616
		public static IEnumerable<DependentModuleMetadata> DependenciesAllDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesAll().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001C442 File Offset: 0x0001A642
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

		// Token: 0x0600087F RID: 2175 RVA: 0x0001C452 File Offset: 0x0001A652
		public static IEnumerable<DependentModuleMetadata> DependenciesToLoadDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesToLoad().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001C47E File Offset: 0x0001A67E
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

		// Token: 0x06000881 RID: 2177 RVA: 0x0001C48E File Offset: 0x0001A68E
		public static IEnumerable<DependentModuleMetadata> DependenciesLoadBeforeThisDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesLoadBeforeThis().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001C4BA File Offset: 0x0001A6BA
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

		// Token: 0x06000883 RID: 2179 RVA: 0x0001C4CA File Offset: 0x0001A6CA
		public static IEnumerable<DependentModuleMetadata> DependenciesLoadAfterThisDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesLoadAfterThis().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001C4F6 File Offset: 0x0001A6F6
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

		// Token: 0x06000885 RID: 2181 RVA: 0x0001C506 File Offset: 0x0001A706
		public static IEnumerable<DependentModuleMetadata> DependenciesIncompatiblesDistinct(this ModuleInfoExtended module)
		{
			return module.DependenciesIncompatibles().DistinctBy((DependentModuleMetadata x) => x.Id);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001C532 File Offset: 0x0001A732
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
