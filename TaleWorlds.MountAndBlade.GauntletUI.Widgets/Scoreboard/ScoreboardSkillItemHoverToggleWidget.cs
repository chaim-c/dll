using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Scoreboard
{
	// Token: 0x02000052 RID: 82
	public class ScoreboardSkillItemHoverToggleWidget : HoverToggleWidget
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000E406 File Offset: 0x0000C606
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000E40E File Offset: 0x0000C60E
		public ScoreboardGainedSkillsListPanel SkillsShowWidget { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000E417 File Offset: 0x0000C617
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000E41F File Offset: 0x0000C61F
		public ListPanel GainedSkillsList { get; set; }

		// Token: 0x0600047A RID: 1146 RVA: 0x0000E428 File Offset: 0x0000C628
		public ScoreboardSkillItemHoverToggleWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000E431 File Offset: 0x0000C631
		public List<Widget> GetAllSkillWidgets()
		{
			return this.GainedSkillsList.Children;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000E440 File Offset: 0x0000C640
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.IsOverWidget && !this._isHoverBeginHandled)
			{
				this.SkillsShowWidget.SetCurrentUnit(this);
				this._isHoverBeginHandled = true;
				this._isHoverEndHandled = true;
				return;
			}
			if (!base.IsOverWidget && this._isHoverEndHandled)
			{
				this.SkillsShowWidget.SetCurrentUnit(null);
				this._isHoverEndHandled = false;
				this._isHoverBeginHandled = false;
			}
		}

		// Token: 0x040001F3 RID: 499
		private bool _isHoverEndHandled;

		// Token: 0x040001F4 RID: 500
		private bool _isHoverBeginHandled;
	}
}
