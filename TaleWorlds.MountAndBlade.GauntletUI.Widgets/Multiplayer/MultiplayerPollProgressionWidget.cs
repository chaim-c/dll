using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x02000085 RID: 133
	public class MultiplayerPollProgressionWidget : Widget
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x00015832 File Offset: 0x00013A32
		public MultiplayerPollProgressionWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001583B File Offset: 0x00013A3B
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00015844 File Offset: 0x00013A44
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001584C File Offset: 0x00013A4C
		public bool HasOngoingPoll
		{
			get
			{
				return this._hasOngoingPoll;
			}
			set
			{
				if (value != this._hasOngoingPoll)
				{
					this._hasOngoingPoll = value;
					base.OnPropertyChanged(value, "HasOngoingPoll");
					ListPanel pollExtension = this.PollExtension;
					if (pollExtension == null)
					{
						return;
					}
					pollExtension.SetState(value ? "Active" : "Inactive");
				}
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00015889 File Offset: 0x00013A89
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x00015891 File Offset: 0x00013A91
		[Editor(false)]
		public ListPanel PollExtension
		{
			get
			{
				return this._pollExtension;
			}
			set
			{
				if (value != this._pollExtension)
				{
					this._pollExtension = value;
					base.OnPropertyChanged<ListPanel>(value, "PollExtension");
					this._pollExtension.SetState("Inactive");
				}
			}
		}

		// Token: 0x04000339 RID: 825
		private const string _activeState = "Active";

		// Token: 0x0400033A RID: 826
		private const string _inactiveState = "Inactive";

		// Token: 0x0400033B RID: 827
		private bool _hasOngoingPoll;

		// Token: 0x0400033C RID: 828
		private ListPanel _pollExtension;
	}
}
