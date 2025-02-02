using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Bannerlord.ButterLib.Common.Extensions;
using Bannerlord.ButterLib.SubSystems;
using Bannerlord.ButterLib.SubSystems.Settings;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.FluentBuilder.Models;
using MCM.Common;
using Microsoft.Extensions.DependencyInjection;
using TaleWorlds.Localization;

namespace MCM.UI.Extensions
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SettingsBuilderExtensions
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00007FB0 File Offset: 0x000061B0
		[NullableContext(2)]
		private static PropertyInfo GetPropertyInfo<T, TResult>([Nullable(1)] Expression<Func<T, TResult>> expression)
		{
			if (expression != null)
			{
				MemberExpression memberExpression = expression.Body as MemberExpression;
				if (memberExpression != null)
				{
					PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
					if (propertyInfo != null)
					{
						return propertyInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007FE4 File Offset: 0x000061E4
		private static void AddForSubSystem<[Nullable(0)] TSubSystem>(TSubSystem subSystem, ISettingsBuilder settingsBuilder) where TSubSystem : ISubSystem
		{
			ISubSystemSettings<TSubSystem> settings = subSystem as ISubSystemSettings<TSubSystem>;
			bool flag = settings == null;
			if (!flag)
			{
				using (IEnumerator<SubSystemSettingsDeclaration<TSubSystem>> enumerator = settings.Declarations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SubSystemSettingsDeclaration<TSubSystem> declaration = enumerator.Current;
						settingsBuilder.CreateGroup("SubSystem " + subSystem.Id, delegate(ISettingsPropertyGroupBuilder groupBuilder)
						{
							SubSystemSettingsDeclaration<TSubSystem> declaration = declaration;
							if (!true)
							{
							}
							SubSystemSettingsPropertyBool<TSubSystem> <sp>5__2 = declaration as SubSystemSettingsPropertyBool<TSubSystem>;
							if (<sp>5__2 != null)
							{
								PropertyInfo pi = SettingsBuilderExtensions.GetPropertyInfo<TSubSystem, bool>(<sp>5__2.Property);
								if (pi != null)
								{
									groupBuilder.AddBool(subSystem.GetType().Name + "_" + pi.Name, new TextObject(<sp>5__2.Name, null).ToString(), new PropertyRef(pi, subSystem), delegate(ISettingsPropertyBoolBuilder builder)
									{
										builder.SetHintText(<sp>5__2.Description);
									});
									goto IL_375;
								}
							}
							SubSystemSettingsPropertyDropdown<TSubSystem> <sp>5__3 = declaration as SubSystemSettingsPropertyDropdown<TSubSystem>;
							if (<sp>5__3 != null)
							{
								PropertyInfo pi2 = SettingsBuilderExtensions.GetPropertyInfo<TSubSystem, IList<string>>(<sp>5__3.Property);
								if (pi2 != null)
								{
									groupBuilder.AddDropdown(subSystem.GetType().Name + "_" + pi2.Name, new TextObject(<sp>5__3.Name, null).ToString(), <sp>5__3.SelectedIndex, new PropertyRef(pi2, subSystem), delegate(ISettingsPropertyDropdownBuilder builder)
									{
										builder.SetHintText(<sp>5__3.Description);
									});
									goto IL_375;
								}
							}
							SubSystemSettingsPropertyFloat<TSubSystem> <sp>5__4 = declaration as SubSystemSettingsPropertyFloat<TSubSystem>;
							if (<sp>5__4 != null)
							{
								PropertyInfo pi3 = SettingsBuilderExtensions.GetPropertyInfo<TSubSystem, float>(<sp>5__4.Property);
								if (pi3 != null)
								{
									groupBuilder.AddFloatingInteger(subSystem.GetType().Name + "_" + pi3.Name, new TextObject(<sp>5__4.Name, null).ToString(), <sp>5__4.MinValue, <sp>5__4.MaxValue, new PropertyRef(pi3, subSystem), delegate(ISettingsPropertyFloatingIntegerBuilder builder)
									{
										builder.SetHintText(<sp>5__4.Description);
									});
									goto IL_375;
								}
							}
							SubSystemSettingsPropertyInt<TSubSystem> <sp>5__5 = declaration as SubSystemSettingsPropertyInt<TSubSystem>;
							if (<sp>5__5 != null)
							{
								PropertyInfo pi4 = SettingsBuilderExtensions.GetPropertyInfo<TSubSystem, int>(<sp>5__5.Property);
								if (pi4 != null)
								{
									groupBuilder.AddInteger(subSystem.GetType().Name + "_" + pi4.Name, new TextObject(<sp>5__5.Name, null).ToString(), <sp>5__5.MinValue, <sp>5__5.MaxValue, new PropertyRef(pi4, subSystem), delegate(ISettingsPropertyIntegerBuilder builder)
									{
										builder.SetHintText(<sp>5__5.Description);
									});
									goto IL_375;
								}
							}
							SubSystemSettingsPropertyText<TSubSystem> <sp>5__6 = declaration as SubSystemSettingsPropertyText<TSubSystem>;
							if (<sp>5__6 != null)
							{
								PropertyInfo pi5 = SettingsBuilderExtensions.GetPropertyInfo<TSubSystem, string>(<sp>5__6.Property);
								if (pi5 != null)
								{
									groupBuilder.AddText(subSystem.GetType().Name + "_" + pi5.Name, new TextObject(<sp>5__6.Name, null).ToString(), new PropertyRef(pi5, subSystem), delegate(ISettingsPropertyTextBuilder builder)
									{
										builder.SetHintText(<sp>5__6.Description);
									});
									goto IL_375;
								}
							}
							if (!true)
							{
							}
							<PrivateImplementationDetails>.ThrowInvalidOperationException();
							IL_375:
							if (!true)
							{
							}
						});
					}
				}
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000080A4 File Offset: 0x000062A4
		public static ISettingsBuilder AddButterLibSubSystems(this ISettingsBuilder settings)
		{
			using (IEnumerator<ISubSystem> enumerator = ServiceProviderServiceExtensions.GetRequiredService<IEnumerable<ISubSystem>>(DependencyInjectionExtensions.GetServiceProvider(null)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SettingsBuilderExtensions.<>c__DisplayClass2_0 CS$<>8__locals1 = new SettingsBuilderExtensions.<>c__DisplayClass2_0();
					CS$<>8__locals1.subSystem = enumerator.Current;
					bool flag = !CS$<>8__locals1.subSystem.CanBeDisabled;
					if (!flag)
					{
						ProxyRef<bool> prop = new ProxyRef<bool>(() => CS$<>8__locals1.subSystem.IsEnabled, delegate(bool state)
						{
							if (state)
							{
								CS$<>8__locals1.subSystem.Enable();
							}
							else
							{
								CS$<>8__locals1.subSystem.Disable();
							}
						});
						settings = settings.CreateGroup("SubSystem " + CS$<>8__locals1.subSystem.Id, delegate(ISettingsPropertyGroupBuilder builder)
						{
							string id = CS$<>8__locals1.subSystem.Id + " Enabled";
							string name = "Enabled";
							IRef prop = prop;
							Action<ISettingsPropertyBoolBuilder> builder2;
							if ((builder2 = CS$<>8__locals1.<>9__3) == null)
							{
								builder2 = (CS$<>8__locals1.<>9__3 = delegate(ISettingsPropertyBoolBuilder bBuilder)
								{
									bBuilder.SetHintText(CS$<>8__locals1.subSystem.Description).SetRequireRestart(!CS$<>8__locals1.subSystem.CanBeSwitchedAtRuntime);
								});
							}
							builder.AddBool(id, name, prop, builder2);
						});
						Type subSystemType = CS$<>8__locals1.subSystem.GetType();
						bool flag2 = subSystemType.GetInterfaces().Any((Type x) => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ISubSystemSettings));
						if (flag2)
						{
							MethodInfo methodInfo = AccessTools2.DeclaredMethod(typeof(SettingsBuilderExtensions), "AddForSubSystem", null, null, true);
							MethodInfo method = (methodInfo != null) ? methodInfo.MakeGenericMethod(new Type[]
							{
								subSystemType
							}) : null;
							if (method != null)
							{
								method.Invoke(null, new object[]
								{
									CS$<>8__locals1.subSystem,
									settings
								});
							}
						}
					}
				}
			}
			return settings;
		}
	}
}
