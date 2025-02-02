using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200000A RID: 10
	public class BrightnessDemoWidget : TextureWidget
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000028CA File Offset: 0x00000ACA
		public BrightnessDemoWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "BrightnessDemoTextureProvider";
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000028E5 File Offset: 0x00000AE5
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000028F0 File Offset: 0x00000AF0
		[Editor(false)]
		public BrightnessDemoWidget.DemoTypes DemoType
		{
			get
			{
				return this._demoType;
			}
			set
			{
				if (this._demoType != value)
				{
					this._demoType = value;
					base.OnPropertyChanged<string>(Enum.GetName(typeof(BrightnessDemoWidget.DemoTypes), value), "DemoType");
					base.SetTextureProviderProperty("DemoType", (int)value);
				}
			}
		}

		// Token: 0x04000016 RID: 22
		private BrightnessDemoWidget.DemoTypes _demoType = BrightnessDemoWidget.DemoTypes.None;

		// Token: 0x02000188 RID: 392
		public enum DemoTypes
		{
			// Token: 0x04000939 RID: 2361
			None = -1,
			// Token: 0x0400093A RID: 2362
			BrightnessWide,
			// Token: 0x0400093B RID: 2363
			ExposureTexture1,
			// Token: 0x0400093C RID: 2364
			ExposureTexture2,
			// Token: 0x0400093D RID: 2365
			ExposureTexture3
		}
	}
}
