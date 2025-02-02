using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000039 RID: 57
	public class WidgetInfo
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000F510 File Offset: 0x0000D710
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000F518 File Offset: 0x0000D718
		public string Name { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000F521 File Offset: 0x0000D721
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000F529 File Offset: 0x0000D729
		public Type Type { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000F532 File Offset: 0x0000D732
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000F53A File Offset: 0x0000D73A
		public bool GotCustomUpdate { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000F543 File Offset: 0x0000D743
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000F54B File Offset: 0x0000D74B
		public bool GotCustomLateUpdate { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000F554 File Offset: 0x0000D754
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000F55C File Offset: 0x0000D75C
		public bool GotCustomParallelUpdate { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000F565 File Offset: 0x0000D765
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000F56D File Offset: 0x0000D76D
		public bool GotUpdateBrushes { get; private set; }

		// Token: 0x060003C8 RID: 968 RVA: 0x0000F576 File Offset: 0x0000D776
		static WidgetInfo()
		{
			WidgetInfo.Reload();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000F580 File Offset: 0x0000D780
		public static void Reload()
		{
			WidgetInfo._widgetInfos = new Dictionary<Type, WidgetInfo>();
			foreach (Type type in WidgetInfo.CollectWidgetTypes())
			{
				WidgetInfo._widgetInfos.Add(type, new WidgetInfo(type));
			}
			TextureWidget.RecollectProviderTypes();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		public static WidgetInfo GetWidgetInfo(Type type)
		{
			return WidgetInfo._widgetInfos[type];
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000F5FC File Offset: 0x0000D7FC
		public WidgetInfo(Type type)
		{
			this.Name = type.Name;
			this.Type = type;
			this.GotCustomUpdate = this.IsMethodOverridden("OnUpdate");
			this.GotCustomLateUpdate = this.IsMethodOverridden("OnLateUpdate");
			this.GotCustomParallelUpdate = this.IsMethodOverridden("OnParallelUpdate");
			this.GotUpdateBrushes = this.IsMethodOverridden("UpdateBrushes");
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000F668 File Offset: 0x0000D868
		private static bool CheckAssemblyReferencesThis(Assembly assembly)
		{
			Assembly assembly2 = typeof(Widget).Assembly;
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].Name == assembly2.GetName().Name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F6B8 File Offset: 0x0000D8B8
		public static List<Type> CollectWidgetTypes()
		{
			List<Type> list = new List<Type>();
			Assembly assembly = typeof(Widget).Assembly;
			foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (WidgetInfo.CheckAssemblyReferencesThis(assembly2) || assembly2 == assembly)
				{
					foreach (Type type in assembly2.GetTypesSafe(null))
					{
						if (typeof(Widget).IsAssignableFrom(type))
						{
							list.Add(type);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000F76C File Offset: 0x0000D96C
		private bool IsMethodOverridden(string methodName)
		{
			MethodInfo method = this.Type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			bool result;
			if (method == null)
			{
				result = false;
			}
			else
			{
				Type right = this.Type;
				Type type = this.Type;
				while (type != null)
				{
					if (type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) != null)
					{
						right = type;
					}
					type = type.BaseType;
				}
				result = (method.DeclaringType != right);
			}
			return result;
		}

		// Token: 0x040001D9 RID: 473
		private static Dictionary<Type, WidgetInfo> _widgetInfos;
	}
}
