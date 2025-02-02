using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GameMenu
{
	// Token: 0x02000148 RID: 328
	public class SettlementMenuPartyCharacterListsButtonWidget : ButtonWidget
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00030B59 File Offset: 0x0002ED59
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x00030B61 File Offset: 0x0002ED61
		public Brush PartyListButtonBrush { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00030B6A File Offset: 0x0002ED6A
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x00030B72 File Offset: 0x0002ED72
		public Brush CharacterListButtonBrush { get; set; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00030B7B File Offset: 0x0002ED7B
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x00030B83 File Offset: 0x0002ED83
		public ContainerPageControlWidget CharactersList { get; set; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00030B8C File Offset: 0x0002ED8C
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x00030B94 File Offset: 0x0002ED94
		public ContainerPageControlWidget PartiesList { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00030B9D File Offset: 0x0002ED9D
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x00030BA5 File Offset: 0x0002EDA5
		public int MaxNumOfVisuals { get; set; } = 5;

		// Token: 0x06001186 RID: 4486 RVA: 0x00030BAE File Offset: 0x0002EDAE
		public SettlementMenuPartyCharacterListsButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00030BC0 File Offset: 0x0002EDC0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			base.Brush = (this.ChildPartiesList.IsVisible ? this.PartyListButtonBrush : (this.ChildCharactersList.IsVisible ? this.CharacterListButtonBrush : null));
			if (!this._initialized)
			{
				if (this.CharactersList.IsVisible)
				{
					this.SetCharacterListVisible();
				}
				else if (this.PartiesList.IsVisible)
				{
					this.SetPartyListVisible();
				}
				this._initialized = true;
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00030C3C File Offset: 0x0002EE3C
		protected override void OnClick()
		{
			base.OnClick();
			if (!this.PartiesList.IsVisible && this.CharactersList.IsVisible)
			{
				this.SetPartyListVisible();
				return;
			}
			if (this.PartiesList.IsVisible && !this.CharactersList.IsVisible)
			{
				this.SetCharacterListVisible();
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00030C90 File Offset: 0x0002EE90
		private void SetCharacterListVisible()
		{
			this.CharactersList.IsVisible = true;
			this.PartiesList.IsVisible = false;
			this.ChildPartiesList.IsVisible = true;
			this.ChildCharactersList.IsVisible = false;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00030CC2 File Offset: 0x0002EEC2
		private void SetPartyListVisible()
		{
			this.CharactersList.IsVisible = false;
			this.PartiesList.IsVisible = true;
			this.ChildPartiesList.IsVisible = false;
			this.ChildCharactersList.IsVisible = true;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x00030CFC File Offset: 0x0002EEFC
		public ListPanel ChildCharactersList
		{
			get
			{
				return this._childCharactersList;
			}
			set
			{
				if (value != this._childCharactersList)
				{
					this._childCharactersList = value;
					this._childCharactersList.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnListItemAdded));
				}
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00030D2A File Offset: 0x0002EF2A
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x00030D32 File Offset: 0x0002EF32
		public ListPanel ChildPartiesList
		{
			get
			{
				return this._childPartiesList;
			}
			set
			{
				if (value != this._childPartiesList)
				{
					this._childPartiesList = value;
					this._childPartiesList.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnListItemAdded));
				}
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00030D60 File Offset: 0x0002EF60
		private void OnListItemAdded(Widget parent, Widget child)
		{
			if (parent.ChildCount > this.MaxNumOfVisuals)
			{
				child.IsVisible = false;
			}
		}

		// Token: 0x04000809 RID: 2057
		private bool _initialized;

		// Token: 0x0400080A RID: 2058
		private ListPanel _childCharactersList;

		// Token: 0x0400080B RID: 2059
		private ListPanel _childPartiesList;
	}
}
