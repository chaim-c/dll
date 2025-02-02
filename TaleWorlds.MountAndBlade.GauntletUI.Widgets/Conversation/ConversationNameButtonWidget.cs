using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Conversation
{
	// Token: 0x02000162 RID: 354
	public class ConversationNameButtonWidget : ButtonWidget
	{
		// Token: 0x0600128A RID: 4746 RVA: 0x00032B61 File Offset: 0x00030D61
		public ConversationNameButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00032B6A File Offset: 0x00030D6A
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
			this.RelationBarContainer.IsVisible = this.IsRelationEnabled;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00032B83 File Offset: 0x00030D83
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
			this.RelationBarContainer.IsVisible = false;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00032B97 File Offset: 0x00030D97
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x00032B9F File Offset: 0x00030D9F
		[Editor(false)]
		public bool IsRelationEnabled
		{
			get
			{
				return this._isRelationEnabled;
			}
			set
			{
				if (value != this._isRelationEnabled)
				{
					this._isRelationEnabled = value;
					base.OnPropertyChanged(value, "IsRelationEnabled");
				}
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00032BBD File Offset: 0x00030DBD
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x00032BC5 File Offset: 0x00030DC5
		[Editor(false)]
		public Widget RelationBarContainer
		{
			get
			{
				return this._relationBarContainer;
			}
			set
			{
				if (value != this._relationBarContainer)
				{
					this._relationBarContainer = value;
					base.OnPropertyChanged<Widget>(value, "RelationBarContainer");
				}
			}
		}

		// Token: 0x04000873 RID: 2163
		private bool _isRelationEnabled;

		// Token: 0x04000874 RID: 2164
		private Widget _relationBarContainer;
	}
}
