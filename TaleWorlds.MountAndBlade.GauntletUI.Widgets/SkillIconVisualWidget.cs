using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200003D RID: 61
	public class SkillIconVisualWidget : Widget
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public SkillIconVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._requiresRefresh)
			{
				string text = "SPGeneral\\Skills\\gui_skills_icon_" + this.SkillId.ToLower();
				if (this.UseSmallestVariation && base.Context.SpriteData.GetSprite(text + "_tiny") != null)
				{
					base.Sprite = base.Context.SpriteData.GetSprite(text + "_tiny");
				}
				else if (this.UseSmallVariation && base.Context.SpriteData.GetSprite(text + "_small") != null)
				{
					base.Sprite = base.Context.SpriteData.GetSprite(text + "_small");
				}
				else if (base.Context.SpriteData.GetSprite(text) != null)
				{
					base.Sprite = base.Context.SpriteData.GetSprite(text);
				}
				this._requiresRefresh = false;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000ACDE File Offset: 0x00008EDE
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000ACE6 File Offset: 0x00008EE6
		[Editor(false)]
		public string SkillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				if (this._skillId != value)
				{
					this._skillId = value;
					base.OnPropertyChanged<string>(value, "SkillId");
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000AD10 File Offset: 0x00008F10
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000AD18 File Offset: 0x00008F18
		[Editor(false)]
		public bool UseSmallVariation
		{
			get
			{
				return this._useSmallVariation;
			}
			set
			{
				if (this._useSmallVariation != value)
				{
					this._useSmallVariation = value;
					base.OnPropertyChanged(value, "UseSmallVariation");
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000AD3D File Offset: 0x00008F3D
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000AD45 File Offset: 0x00008F45
		[Editor(false)]
		public bool UseSmallestVariation
		{
			get
			{
				return this._useSmallestVariation;
			}
			set
			{
				if (this._useSmallestVariation != value)
				{
					this._useSmallestVariation = value;
					base.OnPropertyChanged(value, "UseSmallestVariation");
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x04000167 RID: 359
		private bool _requiresRefresh = true;

		// Token: 0x04000168 RID: 360
		private string _skillId;

		// Token: 0x04000169 RID: 361
		private bool _useSmallVariation;

		// Token: 0x0400016A RID: 362
		private bool _useSmallestVariation;
	}
}
