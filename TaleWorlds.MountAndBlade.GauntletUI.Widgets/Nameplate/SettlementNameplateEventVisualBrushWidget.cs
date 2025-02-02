using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Nameplate
{
	// Token: 0x02000077 RID: 119
	public class SettlementNameplateEventVisualBrushWidget : BrushWidget
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x00013BBE File Offset: 0x00011DBE
		public SettlementNameplateEventVisualBrushWidget(UIContext context) : base(context)
		{
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.LateUpdateAction), 1);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00013BE7 File Offset: 0x00011DE7
		private void LateUpdateAction(float dt)
		{
			if (!this._determinedVisual)
			{
				this.RegisterBrushStatesOfWidget();
				this.UpdateVisual(this.Type);
				this._determinedVisual = true;
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00013C0C File Offset: 0x00011E0C
		private void UpdateVisual(int type)
		{
			switch (type)
			{
			case 0:
				this.SetState("Tournament");
				break;
			case 1:
				this.SetState("AvailableIssue");
				break;
			case 2:
				this.SetState("ActiveQuest");
				break;
			case 3:
				this.SetState("ActiveStoryQuest");
				break;
			case 4:
				this.SetState("TrackedIssue");
				break;
			case 5:
				this.SetState("TrackedStoryQuest");
				break;
			case 6:
				this.SetState(this.AdditionalParameters);
				base.MarginLeft = 2f;
				base.MarginRight = 2f;
				break;
			}
			Brush brush = base.Brush;
			Sprite sprite;
			if (brush == null)
			{
				sprite = null;
			}
			else
			{
				Style style = brush.GetStyle(base.CurrentState);
				if (style == null)
				{
					sprite = null;
				}
				else
				{
					StyleLayer layer = style.GetLayer(0);
					sprite = ((layer != null) ? layer.Sprite : null);
				}
			}
			Sprite sprite2 = sprite;
			if (sprite2 != null)
			{
				base.SuggestedWidth = base.SuggestedHeight / (float)sprite2.Height * (float)sprite2.Width;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00013CFE File Offset: 0x00011EFE
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00013D06 File Offset: 0x00011F06
		[Editor(false)]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (this._type != value)
				{
					this._type = value;
					base.OnPropertyChanged(value, "Type");
				}
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00013D24 File Offset: 0x00011F24
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00013D2C File Offset: 0x00011F2C
		[Editor(false)]
		public string AdditionalParameters
		{
			get
			{
				return this._additionalParameters;
			}
			set
			{
				if (this._additionalParameters != value)
				{
					this._additionalParameters = value;
					base.OnPropertyChanged<string>(value, "AdditionalParameters");
				}
			}
		}

		// Token: 0x040002E6 RID: 742
		private bool _determinedVisual;

		// Token: 0x040002E7 RID: 743
		private int _type = -1;

		// Token: 0x040002E8 RID: 744
		private string _additionalParameters;
	}
}
