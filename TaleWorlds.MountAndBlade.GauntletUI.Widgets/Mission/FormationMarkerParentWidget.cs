using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D6 RID: 214
	public class FormationMarkerParentWidget : Widget
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0001F65C File Offset: 0x0001D85C
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0001F664 File Offset: 0x0001D864
		public float FarAlphaTarget { get; set; } = 0.2f;

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0001F66D File Offset: 0x0001D86D
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x0001F675 File Offset: 0x0001D875
		public float FarDistanceCutoff { get; set; } = 50f;

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0001F67E File Offset: 0x0001D87E
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0001F686 File Offset: 0x0001D886
		public float CloseDistanceCutoff { get; set; } = 25f;

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0001F68F File Offset: 0x0001D88F
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0001F697 File Offset: 0x0001D897
		public float ClosestFadeoutRange { get; set; } = 3f;

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		public FormationMarkerParentWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			MathF.Clamp(dt * 12f, 0f, 1f);
			if (this._isMarkersDirty)
			{
				Sprite sprite = null;
				if (!string.IsNullOrEmpty(this.MarkerType))
				{
					sprite = base.Context.SpriteData.GetSprite("General\\compass\\" + this.MarkerType);
				}
				if (sprite != null && this.FormationTypeMarker != null)
				{
					this.FormationTypeMarker.Sprite = sprite;
				}
				if (this.TeamTypeMarker != null)
				{
					this.TeamTypeMarker.RegisterBrushStatesOfWidget();
					if (this.TeamType == 0)
					{
						this.TeamTypeMarker.SetState("Player");
					}
					else if (this.TeamType == 1)
					{
						this.TeamTypeMarker.SetState("Ally");
					}
					else
					{
						this.TeamTypeMarker.SetState("Enemy");
					}
				}
				this._isMarkersDirty = false;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		[DataSourceProperty]
		public Widget FormationTypeMarker
		{
			get
			{
				return this._formationTypeMarker;
			}
			set
			{
				if (this._formationTypeMarker != value)
				{
					this._formationTypeMarker = value;
					base.OnPropertyChanged<Widget>(value, "FormationTypeMarker");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0001F7E5 File Offset: 0x0001D9E5
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0001F7ED File Offset: 0x0001D9ED
		[DataSourceProperty]
		public Widget TeamTypeMarker
		{
			get
			{
				return this._teamTypeMarker;
			}
			set
			{
				if (this._teamTypeMarker != value)
				{
					this._teamTypeMarker = value;
					base.OnPropertyChanged<Widget>(value, "TeamTypeMarker");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001F812 File Offset: 0x0001DA12
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0001F81A File Offset: 0x0001DA1A
		public int TeamType
		{
			get
			{
				return this._teamType;
			}
			set
			{
				if (this._teamType != value)
				{
					this._teamType = value;
					base.OnPropertyChanged(value, "TeamType");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0001F83F File Offset: 0x0001DA3F
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0001F847 File Offset: 0x0001DA47
		[DataSourceProperty]
		public string MarkerType
		{
			get
			{
				return this._markerType;
			}
			set
			{
				if (this._markerType != value)
				{
					this._markerType = value;
					base.OnPropertyChanged<string>(value, "MarkerType");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x04000514 RID: 1300
		private bool _isMarkersDirty = true;

		// Token: 0x04000515 RID: 1301
		private string _markerType;

		// Token: 0x04000516 RID: 1302
		private int _teamType;

		// Token: 0x04000517 RID: 1303
		private Widget _formationTypeMarker;

		// Token: 0x04000518 RID: 1304
		private Widget _teamTypeMarker;
	}
}
