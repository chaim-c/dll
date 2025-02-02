using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ModuleUtilities
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		public static bool AreDependenciesPresent(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended module)
		{
			using (IEnumerator<DependentModuleMetadata> enumerator = module.DependenciesLoadBeforeThisDistinct().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DependentModuleMetadata metadata = enumerator.Current;
					bool isOptional = metadata.IsOptional;
					if (!isOptional)
					{
						bool flag = modules.All((ModuleInfoExtended x) => !string.Equals(x.Id, metadata.Id, StringComparison.Ordinal));
						if (flag)
						{
							return false;
						}
					}
				}
			}
			using (IEnumerator<DependentModuleMetadata> enumerator2 = module.DependenciesIncompatiblesDistinct().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					DependentModuleMetadata metadata = enumerator2.Current;
					bool flag2 = modules.Any((ModuleInfoExtended x) => string.Equals(x.Id, metadata.Id, StringComparison.Ordinal));
					if (flag2)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public static IEnumerable<ModuleInfoExtended> GetDependencies(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended module)
		{
			HashSet<ModuleInfoExtended> visited = new HashSet<ModuleInfoExtended>();
			return ModuleUtilities.GetDependencies(modules, module, visited, new ModuleSorterOptions
			{
				SkipOptionals = false,
				SkipExternalDependencies = false
			});
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000EE10 File Offset: 0x0000D010
		public static IEnumerable<ModuleInfoExtended> GetDependencies(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended module, ModuleSorterOptions options)
		{
			HashSet<ModuleInfoExtended> visited = new HashSet<ModuleInfoExtended>();
			return ModuleUtilities.GetDependencies(modules, module, visited, options);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000EE34 File Offset: 0x0000D034
		public static IEnumerable<ModuleInfoExtended> GetDependencies(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended module, HashSet<ModuleInfoExtended> visited, ModuleSorterOptions options)
		{
			List<ModuleInfoExtended> dependencies = new List<ModuleInfoExtended>();
			ModuleSorter.Visit<ModuleInfoExtended>(module, (ModuleInfoExtended x) => ModuleUtilities.GetDependenciesInternal(modules, x, options), delegate(ModuleInfoExtended moduleToAdd)
			{
				bool flag = moduleToAdd != module;
				if (flag)
				{
					dependencies.Add(moduleToAdd);
				}
			}, visited);
			return dependencies;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000EE97 File Offset: 0x0000D097
		private static IEnumerable<ModuleInfoExtended> GetDependenciesInternal(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended module, ModuleSorterOptions options)
		{
			using (IEnumerator<DependentModuleMetadata> enumerator = module.DependenciesLoadBeforeThisDistinct().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ModuleUtilities.<>c__DisplayClass4_0 CS$<>8__locals1 = new ModuleUtilities.<>c__DisplayClass4_0();
					CS$<>8__locals1.dependentModuleMetadata = enumerator.Current;
					bool flag = CS$<>8__locals1.dependentModuleMetadata.IsOptional && options.SkipOptionals;
					if (!flag)
					{
						ModuleInfoExtended moduleInfo = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, CS$<>8__locals1.dependentModuleMetadata.Id, StringComparison.Ordinal));
						bool flag2 = !CS$<>8__locals1.dependentModuleMetadata.IsOptional && moduleInfo == null;
						if (!flag2)
						{
							bool flag3 = moduleInfo != null;
							if (flag3)
							{
								yield return moduleInfo;
							}
						}
						moduleInfo = null;
						CS$<>8__locals1 = null;
					}
				}
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			bool flag4 = !options.SkipExternalDependencies;
			if (flag4)
			{
				foreach (ModuleInfoExtended moduleInfo2 in modules)
				{
					foreach (DependentModuleMetadata dependentModuleMetadata in moduleInfo2.DependenciesLoadAfterThisDistinct())
					{
						bool flag5 = dependentModuleMetadata.IsOptional && options.SkipOptionals;
						if (!flag5)
						{
							bool flag6 = !string.Equals(dependentModuleMetadata.Id, module.Id, StringComparison.Ordinal);
							if (!flag6)
							{
								yield return moduleInfo2;
								dependentModuleMetadata = null;
							}
						}
					}
					IEnumerator<DependentModuleMetadata> enumerator3 = null;
					moduleInfo2 = null;
				}
				IEnumerator<ModuleInfoExtended> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000EEB5 File Offset: 0x0000D0B5
		public static IEnumerable<ModuleIssue> ValidateModule(IReadOnlyList<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, Func<ModuleInfoExtended, bool> isSelected)
		{
			HashSet<ModuleInfoExtended> visited = new HashSet<ModuleInfoExtended>();
			IReadOnlyList<ModuleInfoExtended> modules2 = modules;
			HashSet<ModuleInfoExtended> visitedModules = visited;
			Func<ModuleInfoExtended, bool> isSelected2 = isSelected;
			Func<ModuleInfoExtended, bool> <>9__0;
			Func<ModuleInfoExtended, bool> isValid;
			if ((isValid = <>9__0) == null)
			{
				isValid = (<>9__0 = ((ModuleInfoExtended x) => ModuleUtilities.ValidateModule(modules, x, isSelected).Count<ModuleIssue>() == 0));
			}
			foreach (ModuleIssue issue in ModuleUtilities.ValidateModule(modules2, targetModule, visitedModules, isSelected2, isValid))
			{
				yield return issue;
				issue = null;
			}
			IEnumerator<ModuleIssue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000EED3 File Offset: 0x0000D0D3
		public static IEnumerable<ModuleIssue> ValidateModule(IReadOnlyList<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, Func<ModuleInfoExtended, bool> isSelected, Func<ModuleInfoExtended, bool> isValid)
		{
			HashSet<ModuleInfoExtended> visited = new HashSet<ModuleInfoExtended>();
			foreach (ModuleIssue issue in ModuleUtilities.ValidateModule(modules, targetModule, visited, isSelected, isValid))
			{
				yield return issue;
				issue = null;
			}
			IEnumerator<ModuleIssue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public static IEnumerable<ModuleIssue> ValidateModule(IReadOnlyList<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, HashSet<ModuleInfoExtended> visitedModules, Func<ModuleInfoExtended, bool> isSelected, Func<ModuleInfoExtended, bool> isValid)
		{
			foreach (ModuleIssue issue in ModuleUtilities.ValidateModuleDependenciesDeclarations(modules, targetModule))
			{
				yield return issue;
				issue = null;
			}
			IEnumerator<ModuleIssue> enumerator = null;
			foreach (ModuleIssue issue2 in ModuleUtilities.ValidateModuleDependencies(modules, targetModule, visitedModules, isSelected, isValid))
			{
				yield return issue2;
				issue2 = null;
			}
			IEnumerator<ModuleIssue> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000EF25 File Offset: 0x0000D125
		public static IEnumerable<ModuleIssue> ValidateModuleDependenciesDeclarations(IReadOnlyList<ModuleInfoExtended> modules, ModuleInfoExtended targetModule)
		{
			foreach (string moduleId in (from x in targetModule.DependenciesToLoadDistinct()
			select x.Id).Intersect(from x in targetModule.DependenciesIncompatiblesDistinct()
			select x.Id))
			{
				yield return new ModuleIssue(targetModule, moduleId, ModuleIssueType.DependencyConflictDependentAndIncompatible)
				{
					Reason = "Module '" + moduleId + "' is both depended upon and marked as incompatible"
				};
				moduleId = null;
			}
			IEnumerator<string> enumerator = null;
			foreach (DependentModuleMetadata dependency in from x in targetModule.DependentModuleMetadatas
			where x.IsIncompatible && x.LoadType > LoadType.None
			select x)
			{
				yield return new ModuleIssue(targetModule, dependency.Id, ModuleIssueType.DependencyConflictDependentAndIncompatible)
				{
					Reason = "Module '" + dependency.Id + "' is both depended upon and marked as incompatible"
				};
				dependency = null;
			}
			IEnumerator<DependentModuleMetadata> enumerator2 = null;
			using (IEnumerator<DependentModuleMetadata> enumerator3 = targetModule.DependenciesLoadBeforeThisDistinct().GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					ModuleUtilities.<>c__DisplayClass8_1 CS$<>8__locals2 = new ModuleUtilities.<>c__DisplayClass8_1();
					CS$<>8__locals2.module = enumerator3.Current;
					DependentModuleMetadata metadata = targetModule.DependenciesLoadAfterThisDistinct().FirstOrDefault((DependentModuleMetadata x) => string.Equals(x.Id, CS$<>8__locals2.module.Id, StringComparison.Ordinal));
					bool flag = metadata != null;
					if (flag)
					{
						yield return new ModuleIssue(targetModule, metadata.Id, ModuleIssueType.DependencyConflictDependentLoadBeforeAndAfter)
						{
							Reason = "Module '" + metadata.Id + "' is both depended upon as LoadBefore and LoadAfter"
						};
					}
					metadata = null;
					CS$<>8__locals2 = null;
				}
			}
			IEnumerator<DependentModuleMetadata> enumerator3 = null;
			using (IEnumerator<DependentModuleMetadata> enumerator4 = (from x in targetModule.DependenciesToLoadDistinct()
			where x.LoadType > LoadType.None
			select x).GetEnumerator())
			{
				Func<DependentModuleMetadata, bool> <>9__7;
				while (enumerator4.MoveNext())
				{
					ModuleUtilities.<>c__DisplayClass8_2 CS$<>8__locals3 = new ModuleUtilities.<>c__DisplayClass8_2();
					CS$<>8__locals3.module = enumerator4.Current;
					ModuleInfoExtended moduleInfo = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, CS$<>8__locals3.module.Id, StringComparison.Ordinal));
					ModuleInfoExtended moduleInfoExtended = moduleInfo;
					DependentModuleMetadata dependentModuleMetadata;
					if (moduleInfoExtended == null)
					{
						dependentModuleMetadata = null;
					}
					else
					{
						IEnumerable<DependentModuleMetadata> source = from x in moduleInfoExtended.DependenciesToLoadDistinct()
						where x.LoadType > LoadType.None
						select x;
						Func<DependentModuleMetadata, bool> predicate;
						if ((predicate = <>9__7) == null)
						{
							predicate = (<>9__7 = ((DependentModuleMetadata x) => string.Equals(x.Id, targetModule.Id, StringComparison.Ordinal)));
						}
						dependentModuleMetadata = source.FirstOrDefault(predicate);
					}
					DependentModuleMetadata metadata2 = dependentModuleMetadata;
					bool flag2 = metadata2 != null;
					if (flag2)
					{
						bool flag3 = metadata2.LoadType == CS$<>8__locals3.module.LoadType;
						if (flag3)
						{
							yield return new ModuleIssue(targetModule, metadata2.Id, ModuleIssueType.DependencyConflictCircular)
							{
								Reason = string.Concat(new string[]
								{
									"Circular dependencies. '",
									targetModule.Id,
									"' and '",
									moduleInfo.Id,
									"' depend on each other"
								})
							};
						}
					}
					moduleInfo = null;
					metadata2 = null;
					CS$<>8__locals3 = null;
				}
			}
			IEnumerator<DependentModuleMetadata> enumerator4 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000EF3C File Offset: 0x0000D13C
		public static IEnumerable<ModuleIssue> ValidateModuleDependencies(IReadOnlyList<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, HashSet<ModuleInfoExtended> visitedModules, Func<ModuleInfoExtended, bool> isSelected, Func<ModuleInfoExtended, bool> isValid)
		{
			using (IEnumerator<DependentModuleMetadata> enumerator = targetModule.DependenciesToLoadDistinct().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ModuleUtilities.<>c__DisplayClass9_0 CS$<>8__locals1 = new ModuleUtilities.<>c__DisplayClass9_0();
					CS$<>8__locals1.metadata = enumerator.Current;
					bool isOptional = CS$<>8__locals1.metadata.IsOptional;
					if (!isOptional)
					{
						bool flag = !modules.Any((ModuleInfoExtended x) => string.Equals(x.Id, CS$<>8__locals1.metadata.Id, StringComparison.Ordinal));
						if (flag)
						{
							bool flag2 = CS$<>8__locals1.metadata.Version != ApplicationVersion.Empty;
							if (flag2)
							{
								yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.MissingDependencies)
								{
									Reason = string.Format("Missing '{0}' {1}", CS$<>8__locals1.metadata.Id, CS$<>8__locals1.metadata.Version),
									SourceVersion = new ApplicationVersionRange(CS$<>8__locals1.metadata.Version, CS$<>8__locals1.metadata.Version)
								};
							}
							else
							{
								bool flag3 = CS$<>8__locals1.metadata.VersionRange != ApplicationVersionRange.Empty;
								if (flag3)
								{
									yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.MissingDependencies)
									{
										Reason = string.Format("Missing '{0}' {1}", CS$<>8__locals1.metadata.Id, CS$<>8__locals1.metadata.VersionRange),
										SourceVersion = CS$<>8__locals1.metadata.VersionRange
									};
								}
								else
								{
									yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.MissingDependencies)
									{
										Reason = "Missing '" + CS$<>8__locals1.metadata.Id + "'"
									};
								}
							}
							yield break;
						}
						CS$<>8__locals1 = null;
					}
				}
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			ModuleSorterOptions opts = new ModuleSorterOptions
			{
				SkipOptionals = true,
				SkipExternalDependencies = true
			};
			ModuleInfoExtended[] dependencies = ModuleUtilities.GetDependencies(modules, targetModule, visitedModules, opts).ToArray<ModuleInfoExtended>();
			ModuleInfoExtended[] array = dependencies;
			int i = 0;
			while (i < array.Length)
			{
				ModuleUtilities.<>c__DisplayClass9_1 CS$<>8__locals2 = new ModuleUtilities.<>c__DisplayClass9_1();
				CS$<>8__locals2.dependency = array[i];
				DependentModuleMetadata metadata = targetModule.DependenciesAllDistinct().FirstOrDefault((DependentModuleMetadata x) => string.Equals(x.Id, CS$<>8__locals2.dependency.Id, StringComparison.Ordinal));
				bool flag4 = metadata != null;
				if (!flag4)
				{
					goto IL_530;
				}
				bool flag5 = metadata == null;
				if (!flag5)
				{
					bool flag6 = metadata.LoadType != LoadType.LoadBeforeThis;
					if (!flag6)
					{
						bool isOptional2 = metadata.IsOptional;
						if (!isOptional2)
						{
							bool isIncompatible = metadata.IsIncompatible;
							if (!isIncompatible)
							{
								ModuleInfoExtended depencencyModuleInfo = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, CS$<>8__locals2.dependency.Id, StringComparison.Ordinal));
								bool flag7 = depencencyModuleInfo == null;
								if (!flag7)
								{
									bool flag8 = !isValid(depencencyModuleInfo);
									if (flag8)
									{
										yield return new ModuleIssue(targetModule, CS$<>8__locals2.dependency.Id, ModuleIssueType.DependencyValidationError)
										{
											Reason = "'" + CS$<>8__locals2.dependency.Id + "' has unresolved issues!"
										};
									}
									depencencyModuleInfo = null;
									goto IL_530;
								}
								yield return new ModuleIssue(targetModule, CS$<>8__locals2.dependency.Id, ModuleIssueType.DependencyMissingDependencies)
								{
									Reason = "'" + CS$<>8__locals2.dependency.Id + "' is missing it's dependencies!"
								};
							}
						}
					}
				}
				IL_53F:
				i++;
				continue;
				IL_530:
				metadata = null;
				CS$<>8__locals2 = null;
				goto IL_53F;
			}
			array = null;
			using (IEnumerator<DependentModuleMetadata> enumerator2 = targetModule.DependenciesToLoadDistinct().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ModuleUtilities.<>c__DisplayClass9_2 CS$<>8__locals3 = new ModuleUtilities.<>c__DisplayClass9_2();
					CS$<>8__locals3.metadata = enumerator2.Current;
					bool flag9 = CS$<>8__locals3.metadata.Version == ApplicationVersion.Empty && CS$<>8__locals3.metadata.VersionRange == ApplicationVersionRange.Empty;
					if (!flag9)
					{
						ModuleInfoExtended metadataModule = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, CS$<>8__locals3.metadata.Id, StringComparison.Ordinal));
						bool flag10 = metadataModule == null;
						if (!flag10)
						{
							bool flag11 = CS$<>8__locals3.metadata.Version != ApplicationVersion.Empty;
							if (flag11)
							{
								bool flag12 = !CS$<>8__locals3.metadata.IsOptional && ApplicationVersionComparer.CompareStandard(CS$<>8__locals3.metadata.Version, metadataModule.Version) > 0;
								if (flag12)
								{
									yield return new ModuleIssue(targetModule, metadataModule.Id, ModuleIssueType.VersionMismatchLessThanOrEqual)
									{
										Reason = string.Format("'{0}' wrong version <= {1}", metadataModule.Id, CS$<>8__locals3.metadata.Version),
										SourceVersion = new ApplicationVersionRange(CS$<>8__locals3.metadata.Version, CS$<>8__locals3.metadata.Version)
									};
									continue;
								}
							}
							bool flag13 = CS$<>8__locals3.metadata.VersionRange != ApplicationVersionRange.Empty;
							if (flag13)
							{
								bool flag14 = !CS$<>8__locals3.metadata.IsOptional;
								if (flag14)
								{
									bool flag15 = ApplicationVersionComparer.CompareStandard(CS$<>8__locals3.metadata.VersionRange.Min, metadataModule.Version) > 0;
									if (flag15)
									{
										ModuleIssue moduleIssue = new ModuleIssue(targetModule, metadataModule.Id, ModuleIssueType.VersionMismatchLessThan);
										string format = "'{0}' wrong version < [{1}]";
										ModuleInfoExtended moduleInfoExtended = metadataModule;
										moduleIssue.Reason = string.Format(format, (moduleInfoExtended != null) ? moduleInfoExtended.Id : null, CS$<>8__locals3.metadata.VersionRange);
										moduleIssue.SourceVersion = CS$<>8__locals3.metadata.VersionRange;
										yield return moduleIssue;
										continue;
									}
									bool flag16 = ApplicationVersionComparer.CompareStandard(CS$<>8__locals3.metadata.VersionRange.Max, metadataModule.Version) < 0;
									if (flag16)
									{
										yield return new ModuleIssue(targetModule, metadataModule.Id, ModuleIssueType.VersionMismatchGreaterThan)
										{
											Reason = string.Format("'{0}' wrong version > [{1}]", metadataModule.Id, CS$<>8__locals3.metadata.VersionRange),
											SourceVersion = CS$<>8__locals3.metadata.VersionRange
										};
										continue;
									}
								}
							}
							metadataModule = null;
							CS$<>8__locals3 = null;
						}
					}
				}
			}
			IEnumerator<DependentModuleMetadata> enumerator2 = null;
			using (IEnumerator<DependentModuleMetadata> enumerator3 = targetModule.DependenciesIncompatiblesDistinct().GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					ModuleUtilities.<>c__DisplayClass9_3 CS$<>8__locals4 = new ModuleUtilities.<>c__DisplayClass9_3();
					CS$<>8__locals4.metadata = enumerator3.Current;
					ModuleInfoExtended metadataModule2 = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, CS$<>8__locals4.metadata.Id, StringComparison.Ordinal));
					bool flag17 = metadataModule2 == null || !isSelected(metadataModule2);
					if (!flag17)
					{
						bool flag18 = isSelected(metadataModule2);
						if (flag18)
						{
							yield return new ModuleIssue(targetModule, metadataModule2.Id, ModuleIssueType.Incompatible)
							{
								Reason = "'" + metadataModule2.Id + "' is incompatible with this module"
							};
						}
						metadataModule2 = null;
						CS$<>8__locals4 = null;
					}
				}
			}
			IEnumerator<DependentModuleMetadata> enumerator3 = null;
			foreach (ModuleInfoExtended module in modules)
			{
				bool flag19 = string.Equals(module.Id, targetModule.Id, StringComparison.Ordinal);
				if (!flag19)
				{
					bool flag20 = !isSelected(module);
					if (!flag20)
					{
						foreach (DependentModuleMetadata metadata2 in module.DependenciesIncompatiblesDistinct())
						{
							bool flag21 = !string.Equals(metadata2.Id, targetModule.Id, StringComparison.Ordinal);
							if (!flag21)
							{
								bool flag22 = isSelected(module);
								if (flag22)
								{
									yield return new ModuleIssue(targetModule, module.Id, ModuleIssueType.Incompatible)
									{
										Reason = "'" + module.Id + "' is incompatible with this module"
									};
								}
								metadata2 = null;
							}
						}
						IEnumerator<DependentModuleMetadata> enumerator5 = null;
						module = null;
					}
				}
			}
			IEnumerator<ModuleInfoExtended> enumerator4 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000EF69 File Offset: 0x0000D169
		public static IEnumerable<ModuleIssue> ValidateLoadOrder(IReadOnlyList<ModuleInfoExtended> modules, ModuleInfoExtended targetModule)
		{
			HashSet<ModuleInfoExtended> visited = new HashSet<ModuleInfoExtended>();
			foreach (ModuleIssue issue in ModuleUtilities.ValidateLoadOrder(modules, targetModule, visited))
			{
				yield return issue;
				issue = null;
			}
			IEnumerator<ModuleIssue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000EF80 File Offset: 0x0000D180
		public static IEnumerable<ModuleIssue> ValidateLoadOrder(IReadOnlyList<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, HashSet<ModuleInfoExtended> visitedModules)
		{
			int targetModuleIdx = modules.IndexOf(targetModule);
			bool flag = targetModuleIdx == -1;
			if (flag)
			{
				yield return new ModuleIssue(targetModule, targetModule.Id, ModuleIssueType.Missing)
				{
					Reason = string.Format("Missing '{0}' {1} in modules list", targetModule.Id, targetModule.Version),
					SourceVersion = new ApplicationVersionRange(targetModule.Version, targetModule.Version)
				};
				yield break;
			}
			using (IEnumerator<DependentModuleMetadata> enumerator = targetModule.DependenciesToLoad().DistinctBy((DependentModuleMetadata x) => x.Id).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ModuleUtilities.<>c__DisplayClass11_0 CS$<>8__locals1 = new ModuleUtilities.<>c__DisplayClass11_0();
					CS$<>8__locals1.metadata = enumerator.Current;
					int metadataIdx = modules.IndexOf((ModuleInfoExtended x) => x.Id == CS$<>8__locals1.metadata.Id);
					bool flag2 = metadataIdx == -1;
					if (flag2)
					{
						bool flag3 = !CS$<>8__locals1.metadata.IsOptional;
						if (flag3)
						{
							bool flag4 = CS$<>8__locals1.metadata.Version != ApplicationVersion.Empty;
							if (flag4)
							{
								yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.MissingDependencies)
								{
									Reason = string.Format("Missing '{0}' {1}", CS$<>8__locals1.metadata.Id, CS$<>8__locals1.metadata.Version),
									SourceVersion = new ApplicationVersionRange(CS$<>8__locals1.metadata.Version, CS$<>8__locals1.metadata.Version)
								};
							}
							else
							{
								bool flag5 = CS$<>8__locals1.metadata.VersionRange != ApplicationVersionRange.Empty;
								if (flag5)
								{
									yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.MissingDependencies)
									{
										Reason = string.Format("Missing '{0}' {1}", CS$<>8__locals1.metadata.Id, CS$<>8__locals1.metadata.VersionRange),
										SourceVersion = CS$<>8__locals1.metadata.VersionRange
									};
								}
								else
								{
									yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.MissingDependencies)
									{
										Reason = "Missing '" + CS$<>8__locals1.metadata.Id + "'"
									};
								}
							}
						}
					}
					else
					{
						bool flag6 = CS$<>8__locals1.metadata.LoadType == LoadType.LoadBeforeThis && metadataIdx > targetModuleIdx;
						if (flag6)
						{
							yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.DependencyNotLoadedBeforeThis)
							{
								Reason = string.Concat(new string[]
								{
									"'",
									targetModule.Id,
									"' should be loaded before '",
									CS$<>8__locals1.metadata.Id,
									"'"
								})
							};
						}
						bool flag7 = CS$<>8__locals1.metadata.LoadType == LoadType.LoadAfterThis && metadataIdx < targetModuleIdx;
						if (flag7)
						{
							yield return new ModuleIssue(targetModule, CS$<>8__locals1.metadata.Id, ModuleIssueType.DependencyNotLoadedAfterThis)
							{
								Reason = string.Concat(new string[]
								{
									"'",
									targetModule.Id,
									"' should be loaded after '",
									CS$<>8__locals1.metadata.Id,
									"'"
								})
							};
						}
						CS$<>8__locals1 = null;
					}
				}
			}
			IEnumerator<DependentModuleMetadata> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public static void EnableModule(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, Func<ModuleInfoExtended, bool> getSelected, Action<ModuleInfoExtended, bool> setSelected, Func<ModuleInfoExtended, bool> getDisabled, Action<ModuleInfoExtended, bool> setDisabled)
		{
			HashSet<ModuleInfoExtended> visited = new HashSet<ModuleInfoExtended>();
			ModuleUtilities.EnableModuleInternal(modules, targetModule, visited, getSelected, setSelected, getDisabled, setDisabled);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		private static void EnableModuleInternal(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, HashSet<ModuleInfoExtended> visitedModules, Func<ModuleInfoExtended, bool> getSelected, Action<ModuleInfoExtended, bool> setSelected, Func<ModuleInfoExtended, bool> getDisabled, Action<ModuleInfoExtended, bool> setDisabled)
		{
			bool flag = visitedModules.Contains(targetModule);
			if (!flag)
			{
				visitedModules.Add(targetModule);
				setSelected(targetModule, true);
				ModuleSorterOptions opt = new ModuleSorterOptions
				{
					SkipOptionals = true,
					SkipExternalDependencies = true
				};
				ModuleInfoExtended[] dependencies = ModuleUtilities.GetDependencies(modules, targetModule, opt).ToArray<ModuleInfoExtended>();
				using (IEnumerator<ModuleInfoExtended> enumerator = modules.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ModuleInfoExtended module = enumerator.Current;
						bool flag2 = !getSelected(module) && dependencies.Any((ModuleInfoExtended d) => string.Equals(d.Id, module.Id, StringComparison.Ordinal));
						if (flag2)
						{
							ModuleUtilities.EnableModuleInternal(modules, module, visitedModules, getSelected, setSelected, getDisabled, setDisabled);
						}
					}
				}
				using (IEnumerator<DependentModuleMetadata> enumerator2 = targetModule.DependenciesLoadAfterThisDistinct().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DependentModuleMetadata metadata = enumerator2.Current;
						bool isOptional = metadata.IsOptional;
						if (!isOptional)
						{
							bool isIncompatible = metadata.IsIncompatible;
							if (!isIncompatible)
							{
								ModuleInfoExtended metadataModule = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, metadata.Id, StringComparison.Ordinal));
								bool flag3 = metadataModule == null;
								if (!flag3)
								{
									bool flag4 = !getSelected(metadataModule);
									if (flag4)
									{
										ModuleUtilities.EnableModuleInternal(modules, metadataModule, visitedModules, getSelected, setSelected, getDisabled, setDisabled);
									}
								}
							}
						}
					}
				}
				using (IEnumerator<DependentModuleMetadata> enumerator3 = targetModule.DependenciesIncompatiblesDistinct().GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						DependentModuleMetadata metadata = enumerator3.Current;
						ModuleInfoExtended metadataModule2 = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, metadata.Id, StringComparison.Ordinal));
						bool flag5 = metadataModule2 == null;
						if (!flag5)
						{
							bool flag6 = getSelected(metadataModule2);
							if (flag6)
							{
								ModuleUtilities.DisableModuleInternal(modules, metadataModule2, visitedModules, getSelected, setSelected, getDisabled, setDisabled);
							}
							setDisabled(metadataModule2, true);
						}
					}
				}
				foreach (ModuleInfoExtended module2 in modules)
				{
					foreach (DependentModuleMetadata metadata2 in module2.DependenciesIncompatiblesDistinct())
					{
						bool flag7 = !string.Equals(metadata2.Id, targetModule.Id, StringComparison.Ordinal);
						if (!flag7)
						{
							bool flag8 = getSelected(module2);
							if (flag8)
							{
								ModuleUtilities.DisableModuleInternal(modules, module2, visitedModules, getSelected, setSelected, getDisabled, setDisabled);
							}
							setDisabled(module2, getDisabled(module2) | !ModuleUtilities.AreDependenciesPresent(modules, module2));
						}
					}
				}
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public static void DisableModule(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, Func<ModuleInfoExtended, bool> getSelected, Action<ModuleInfoExtended, bool> setSelected, Func<ModuleInfoExtended, bool> getDisabled, Action<ModuleInfoExtended, bool> setDisabled)
		{
			HashSet<ModuleInfoExtended> visited = new HashSet<ModuleInfoExtended>();
			ModuleUtilities.DisableModuleInternal(modules, targetModule, visited, getSelected, setSelected, getDisabled, setDisabled);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000F300 File Offset: 0x0000D500
		private static void DisableModuleInternal(IReadOnlyCollection<ModuleInfoExtended> modules, ModuleInfoExtended targetModule, HashSet<ModuleInfoExtended> visitedModules, Func<ModuleInfoExtended, bool> getSelected, Action<ModuleInfoExtended, bool> setSelected, Func<ModuleInfoExtended, bool> getDisabled, Action<ModuleInfoExtended, bool> setDisabled)
		{
			bool flag = visitedModules.Contains(targetModule);
			if (!flag)
			{
				visitedModules.Add(targetModule);
				setSelected(targetModule, false);
				ModuleSorterOptions opt = new ModuleSorterOptions
				{
					SkipOptionals = true,
					SkipExternalDependencies = true
				};
				Func<ModuleInfoExtended, bool> <>9__0;
				foreach (ModuleInfoExtended module in modules)
				{
					IEnumerable<ModuleInfoExtended> dependencies = ModuleUtilities.GetDependencies(modules, module, opt);
					bool flag2;
					if (getSelected(module))
					{
						IEnumerable<ModuleInfoExtended> source = dependencies;
						Func<ModuleInfoExtended, bool> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = ((ModuleInfoExtended d) => string.Equals(d.Id, targetModule.Id, StringComparison.Ordinal)));
						}
						flag2 = source.Any(predicate);
					}
					else
					{
						flag2 = false;
					}
					bool flag3 = flag2;
					if (flag3)
					{
						ModuleUtilities.DisableModuleInternal(modules, module, visitedModules, getSelected, setSelected, getDisabled, setDisabled);
					}
					foreach (DependentModuleMetadata metadata3 in module.DependenciesLoadAfterThisDistinct())
					{
						bool flag4 = !string.Equals(metadata3.Id, targetModule.Id, StringComparison.Ordinal);
						if (!flag4)
						{
							bool isOptional = metadata3.IsOptional;
							if (!isOptional)
							{
								bool isIncompatible = metadata3.IsIncompatible;
								if (!isIncompatible)
								{
									bool flag5 = getSelected(module);
									if (flag5)
									{
										ModuleUtilities.DisableModuleInternal(modules, module, visitedModules, getSelected, setSelected, getDisabled, setDisabled);
									}
								}
							}
						}
					}
					foreach (DependentModuleMetadata metadata2 in module.DependenciesIncompatiblesDistinct())
					{
						bool flag6 = !string.Equals(metadata2.Id, targetModule.Id, StringComparison.Ordinal);
						if (!flag6)
						{
							setDisabled(module, getDisabled(module) & !ModuleUtilities.AreDependenciesPresent(modules, module));
						}
					}
				}
				using (IEnumerator<DependentModuleMetadata> enumerator4 = targetModule.DependenciesIncompatiblesDistinct().GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						DependentModuleMetadata metadata = enumerator4.Current;
						ModuleInfoExtended metadataModule = modules.FirstOrDefault((ModuleInfoExtended x) => string.Equals(x.Id, metadata.Id, StringComparison.Ordinal));
						bool flag7 = metadataModule == null;
						if (!flag7)
						{
							setDisabled(metadataModule, false);
						}
					}
				}
			}
		}
	}
}
