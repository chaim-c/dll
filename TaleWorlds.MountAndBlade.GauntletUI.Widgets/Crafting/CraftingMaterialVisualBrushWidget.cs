using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x02000159 RID: 345
	public class CraftingMaterialVisualBrushWidget : BrushWidget
	{
		// Token: 0x06001233 RID: 4659 RVA: 0x000322BF File Offset: 0x000304BF
		public CraftingMaterialVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000322CF File Offset: 0x000304CF
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._visualDirty)
			{
				this.UpdateVisual();
				this._visualDirty = false;
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000322F0 File Offset: 0x000304F0
		private void UpdateVisual()
		{
			this.RegisterBrushStatesOfWidget();
			string text = this.MaterialType;
			if (this.IsBig)
			{
				text += "Big";
			}
			this.SetState(text);
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00032325 File Offset: 0x00030525
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x0003232D File Offset: 0x0003052D
		public string MaterialType
		{
			get
			{
				return this._materialType;
			}
			set
			{
				if (this._materialType != value)
				{
					this._materialType = value;
					this._visualDirty = true;
				}
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0003234B File Offset: 0x0003054B
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x00032353 File Offset: 0x00030553
		public bool IsBig
		{
			get
			{
				return this._isBig;
			}
			set
			{
				if (this._isBig != value)
				{
					this._isBig = value;
					this._visualDirty = true;
				}
			}
		}

		// Token: 0x0400084F RID: 2127
		private bool _visualDirty = true;

		// Token: 0x04000850 RID: 2128
		private string _materialType;

		// Token: 0x04000851 RID: 2129
		private bool _isBig;
	}
}
