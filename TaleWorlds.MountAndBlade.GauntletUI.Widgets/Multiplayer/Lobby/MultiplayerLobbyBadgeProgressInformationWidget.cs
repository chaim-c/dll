using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000096 RID: 150
	public class MultiplayerLobbyBadgeProgressInformationWidget : Widget
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001753D File Offset: 0x0001573D
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00017545 File Offset: 0x00015745
		public float CenterBadgeSize { get; set; } = 200f;

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001754E File Offset: 0x0001574E
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00017556 File Offset: 0x00015756
		public float OuterBadgeBaseSize { get; set; } = 175f;

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001755F File Offset: 0x0001575F
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00017567 File Offset: 0x00015767
		public float SizeDecayFromCenterPerElement { get; set; } = 25f;

		// Token: 0x06000800 RID: 2048 RVA: 0x00017570 File Offset: 0x00015770
		public MultiplayerLobbyBadgeProgressInformationWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001759A File Offset: 0x0001579A
		private void OnBadgeAdded(Widget parent, Widget child)
		{
			this.ArrangeChildrenSizes();
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000175A4 File Offset: 0x000157A4
		private void ArrangeChildrenSizes()
		{
			this.ActiveBadgesList.IsVisible = (this.ShownBadgeCount > 0);
			int num = this.ShownBadgeCount / 2;
			int num2 = 0;
			using (IEnumerator<Widget> enumerator = this.ActiveBadgesList.AllChildren.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MultiplayerPlayerBadgeVisualWidget multiplayerPlayerBadgeVisualWidget;
					if ((multiplayerPlayerBadgeVisualWidget = (enumerator.Current as MultiplayerPlayerBadgeVisualWidget)) != null)
					{
						float num3 = (float)MathF.Abs(num2 - num);
						float num4 = this.OuterBadgeBaseSize - this.SizeDecayFromCenterPerElement * num3;
						if (num2 == num)
						{
							num4 = this.CenterBadgeSize;
						}
						multiplayerPlayerBadgeVisualWidget.SetForcedSize(num4, num4);
						num2++;
					}
				}
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00017650 File Offset: 0x00015850
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00017658 File Offset: 0x00015858
		[Editor(false)]
		public int ShownBadgeCount
		{
			get
			{
				return this._shownBadgeCount;
			}
			set
			{
				if (value != this._shownBadgeCount)
				{
					this._shownBadgeCount = value;
					base.OnPropertyChanged(value, "ShownBadgeCount");
					this.ArrangeChildrenSizes();
				}
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0001767C File Offset: 0x0001587C
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00017684 File Offset: 0x00015884
		[Editor(false)]
		public ListPanel ActiveBadgesList
		{
			get
			{
				return this._activeBadgesList;
			}
			set
			{
				if (value != this._activeBadgesList)
				{
					if (this._activeBadgesList != null)
					{
						this._activeBadgesList.ItemAddEventHandlers.Remove(new Action<Widget, Widget>(this.OnBadgeAdded));
					}
					this._activeBadgesList = value;
					base.OnPropertyChanged<ListPanel>(value, "ActiveBadgesList");
					if (value != null)
					{
						this._activeBadgesList.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnBadgeAdded));
					}
				}
			}
		}

		// Token: 0x0400039C RID: 924
		private int _shownBadgeCount;

		// Token: 0x0400039D RID: 925
		private ListPanel _activeBadgesList;
	}
}
