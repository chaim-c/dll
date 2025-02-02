using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.PrefabSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.Engine.GauntletUI
{
	// Token: 0x02000008 RID: 8
	public static class UIResourceManager
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003163 File Offset: 0x00001363
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000316A File Offset: 0x0000136A
		public static ResourceDepot UIResourceDepot { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003172 File Offset: 0x00001372
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003179 File Offset: 0x00001379
		public static WidgetFactory WidgetFactory { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003181 File Offset: 0x00001381
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003188 File Offset: 0x00001388
		public static SpriteData SpriteData { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003190 File Offset: 0x00001390
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003197 File Offset: 0x00001397
		public static BrushFactory BrushFactory { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000319F File Offset: 0x0000139F
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000031A6 File Offset: 0x000013A6
		public static FontFactory FontFactory { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000031AE File Offset: 0x000013AE
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000031B5 File Offset: 0x000013B5
		public static TwoDimensionEngineResourceContext ResourceContext { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000031BD File Offset: 0x000013BD
		private static bool _uiDebugMode
		{
			get
			{
				return UIConfig.DebugModeEnabled || NativeConfig.GetUIDebugMode;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000031D0 File Offset: 0x000013D0
		public static void Initialize(ResourceDepot resourceDepot, List<string> assemblyOrder)
		{
			UIResourceManager.UIResourceDepot = resourceDepot;
			UIResourceManager.WidgetFactory = new WidgetFactory(UIResourceManager.UIResourceDepot, "Prefabs");
			UIResourceManager.WidgetFactory.PrefabExtensionContext.AddExtension(new PrefabDatabindingExtension());
			UIResourceManager.WidgetFactory.Initialize(assemblyOrder);
			UIResourceManager.SpriteData = new SpriteData("SpriteData");
			UIResourceManager.SpriteData.Load(UIResourceManager.UIResourceDepot);
			UIResourceManager.FontFactory = new FontFactory(UIResourceManager.UIResourceDepot);
			UIResourceManager.FontFactory.LoadAllFonts(UIResourceManager.SpriteData);
			UIResourceManager.BrushFactory = new BrushFactory(UIResourceManager.UIResourceDepot, "Brushes", UIResourceManager.SpriteData, UIResourceManager.FontFactory);
			UIResourceManager.BrushFactory.Initialize();
			UIResourceManager.ResourceContext = new TwoDimensionEngineResourceContext();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003284 File Offset: 0x00001484
		public static void Update()
		{
			if (UIResourceManager._latestUIDebugModeState != UIResourceManager._uiDebugMode)
			{
				if (UIResourceManager._uiDebugMode)
				{
					UIResourceManager.UIResourceDepot.StartWatchingChangesInDepot();
				}
				else
				{
					UIResourceManager.UIResourceDepot.StopWatchingChangesInDepot();
				}
				UIResourceManager._latestUIDebugModeState = UIResourceManager._uiDebugMode;
			}
			if (UIResourceManager._uiDebugMode)
			{
				UIResourceManager.UIResourceDepot.CheckForChanges();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000032D5 File Offset: 0x000014D5
		public static void OnLanguageChange(string newLanguageCode)
		{
			UIResourceManager.FontFactory.OnLanguageChange(newLanguageCode);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000032E2 File Offset: 0x000014E2
		public static void Clear()
		{
			UIResourceManager.WidgetFactory = null;
			UIResourceManager.SpriteData = null;
			UIResourceManager.BrushFactory = null;
			UIResourceManager.FontFactory = null;
		}

		// Token: 0x0400001B RID: 27
		private static bool _latestUIDebugModeState;
	}
}
