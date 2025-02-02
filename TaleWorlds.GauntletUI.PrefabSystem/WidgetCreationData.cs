using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000017 RID: 23
	public class WidgetCreationData
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002F35 File Offset: 0x00001135
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002F3D File Offset: 0x0000113D
		public Widget Parent { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002F46 File Offset: 0x00001146
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002F4E File Offset: 0x0000114E
		public UIContext Context { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002F57 File Offset: 0x00001157
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002F5F File Offset: 0x0000115F
		public WidgetFactory WidgetFactory { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002F68 File Offset: 0x00001168
		public BrushFactory BrushFactory
		{
			get
			{
				return this.Context.BrushFactory;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002F75 File Offset: 0x00001175
		public SpriteData SpriteData
		{
			get
			{
				return this.Context.SpriteData;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002F82 File Offset: 0x00001182
		public PrefabExtensionContext PrefabExtensionContext
		{
			get
			{
				return this.WidgetFactory.PrefabExtensionContext;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002F8F File Offset: 0x0000118F
		public WidgetCreationData(UIContext context, WidgetFactory widgetFactory, Widget parent)
		{
			this.Context = context;
			this.WidgetFactory = widgetFactory;
			this.Parent = parent;
			this._extensionData = new Dictionary<string, object>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002FB7 File Offset: 0x000011B7
		public WidgetCreationData(UIContext context, WidgetFactory widgetFactory)
		{
			this.Context = context;
			this.WidgetFactory = widgetFactory;
			this.Parent = null;
			this._extensionData = new Dictionary<string, object>();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002FE0 File Offset: 0x000011E0
		public WidgetCreationData(WidgetCreationData widgetCreationData, WidgetInstantiationResult parentResult)
		{
			this.Context = widgetCreationData.Context;
			this.WidgetFactory = widgetCreationData.WidgetFactory;
			this.Parent = parentResult.Widget;
			this._extensionData = new Dictionary<string, object>();
			foreach (KeyValuePair<string, object> keyValuePair in widgetCreationData._extensionData)
			{
				this._extensionData.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (WidgetInstantiationResultExtensionData widgetInstantiationResultExtensionData in parentResult.ExtensionDatas)
			{
				if (widgetInstantiationResultExtensionData.PassToChildWidgetCreation)
				{
					this.AddExtensionData(widgetInstantiationResultExtensionData.Name, widgetInstantiationResultExtensionData.Data);
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000030CC File Offset: 0x000012CC
		public void AddExtensionData(string name, object data)
		{
			if (this._extensionData.ContainsKey(name))
			{
				this._extensionData[name] = data;
				return;
			}
			this._extensionData.Add(name, data);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000030F8 File Offset: 0x000012F8
		public T GetExtensionData<T>(string name) where T : class
		{
			if (this._extensionData.ContainsKey(name))
			{
				return this._extensionData[name] as T;
			}
			return default(T);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003133 File Offset: 0x00001333
		public void AddExtensionData(object data)
		{
			this.AddExtensionData(data.GetType().Name, data);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003147 File Offset: 0x00001347
		public T GetExtensionData<T>() where T : class
		{
			return this.GetExtensionData<T>(typeof(T).Name);
		}

		// Token: 0x04000031 RID: 49
		private Dictionary<string, object> _extensionData;
	}
}
