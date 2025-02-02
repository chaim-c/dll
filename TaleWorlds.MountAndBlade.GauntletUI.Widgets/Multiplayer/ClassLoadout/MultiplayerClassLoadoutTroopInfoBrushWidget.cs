using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C7 RID: 199
	public class MultiplayerClassLoadoutTroopInfoBrushWidget : BrushWidget
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public MultiplayerClassLoadoutTroopInfoBrushWidget(UIContext context) : base(context)
		{
			this.SetAlpha(this.DefaultAlpha);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001D8BC File Offset: 0x0001BABC
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
			this.SetAlpha(1f);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001D8CF File Offset: 0x0001BACF
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
			this.SetAlpha(this.DefaultAlpha);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001D8E3 File Offset: 0x0001BAE3
		public override void OnBrushChanged()
		{
			base.OnBrushChanged();
			this.SetAlpha(this.DefaultAlpha);
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001D8F7 File Offset: 0x0001BAF7
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0001D8FF File Offset: 0x0001BAFF
		[Editor(false)]
		public float DefaultAlpha
		{
			get
			{
				return this._defaultAlpha;
			}
			set
			{
				if (value != this._defaultAlpha)
				{
					this._defaultAlpha = value;
					base.OnPropertyChanged(value, "DefaultAlpha");
				}
			}
		}

		// Token: 0x040004C6 RID: 1222
		private float _defaultAlpha = 0.7f;
	}
}
