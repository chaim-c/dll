using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000066 RID: 102
	public class PartyUpgradeRequirementWidget : Widget
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000109F8 File Offset: 0x0000EBF8
		public PartyUpgradeRequirementWidget(UIContext context) : base(context)
		{
			this.NormalColor = new Color(1f, 1f, 1f, 1f);
			this.InsufficientColor = new Color(0.753f, 0.071f, 0.098f, 1f);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00010A54 File Offset: 0x0000EC54
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._requiresRefresh)
			{
				if (this.RequirementId != null)
				{
					string str = this.IsPerkRequirement ? "SPGeneral\\Skills\\gui_skills_icon_" : "StdAssets\\ItemIcons\\";
					string str2 = this.IsPerkRequirement ? "_tiny" : "";
					base.Sprite = base.Context.SpriteData.GetSprite(str + this.RequirementId + str2);
				}
				base.Color = (this.IsSufficient ? this.NormalColor : this.InsufficientColor);
				this._requiresRefresh = false;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		[Editor(false)]
		public string RequirementId
		{
			get
			{
				return this._requirementId;
			}
			set
			{
				if (value != this._requirementId)
				{
					this._requirementId = value;
					base.OnPropertyChanged<string>(value, "RequirementId");
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00010B1A File Offset: 0x0000ED1A
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00010B22 File Offset: 0x0000ED22
		[Editor(false)]
		public bool IsSufficient
		{
			get
			{
				return this._isSufficient;
			}
			set
			{
				if (value != this._isSufficient)
				{
					this._isSufficient = value;
					base.OnPropertyChanged(value, "IsSufficient");
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00010B47 File Offset: 0x0000ED47
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00010B4F File Offset: 0x0000ED4F
		[Editor(false)]
		public bool IsPerkRequirement
		{
			get
			{
				return this._isPerkRequirement;
			}
			set
			{
				if (value != this._isPerkRequirement)
				{
					this._isPerkRequirement = value;
					base.OnPropertyChanged(value, "IsPerkRequirement");
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00010B74 File Offset: 0x0000ED74
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x00010B7C File Offset: 0x0000ED7C
		public Color NormalColor
		{
			get
			{
				return this._normalColor;
			}
			set
			{
				if (value != this._normalColor)
				{
					this._normalColor = value;
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00010B9A File Offset: 0x0000ED9A
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00010BA2 File Offset: 0x0000EDA2
		public Color InsufficientColor
		{
			get
			{
				return this._insufficientColor;
			}
			set
			{
				if (value != this._insufficientColor)
				{
					this._insufficientColor = value;
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x0400025F RID: 607
		private bool _requiresRefresh = true;

		// Token: 0x04000260 RID: 608
		private string _requirementId;

		// Token: 0x04000261 RID: 609
		private bool _isSufficient;

		// Token: 0x04000262 RID: 610
		private bool _isPerkRequirement;

		// Token: 0x04000263 RID: 611
		private Color _normalColor;

		// Token: 0x04000264 RID: 612
		private Color _insufficientColor;
	}
}
