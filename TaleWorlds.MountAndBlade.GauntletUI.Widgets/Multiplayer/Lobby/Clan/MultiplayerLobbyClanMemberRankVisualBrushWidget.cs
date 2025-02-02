using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Clan
{
	// Token: 0x020000AA RID: 170
	public class MultiplayerLobbyClanMemberRankVisualBrushWidget : BrushWidget
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00019B5B File Offset: 0x00017D5B
		public MultiplayerLobbyClanMemberRankVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00019B6C File Offset: 0x00017D6C
		private void UpdateTypeVisual()
		{
			if (this.Type == 0)
			{
				this.SetState("Member");
				return;
			}
			if (this.Type == 1)
			{
				this.SetState("Officer");
				return;
			}
			if (this.Type == 2)
			{
				this.SetState("Leader");
				return;
			}
			Debug.FailedAssert("This member type is not defined in widget", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Multiplayer\\Lobby\\Clan\\MultiplayerLobbyClanMemberRankVisualBrushWidget.cs", "UpdateTypeVisual", 28);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00019BCD File Offset: 0x00017DCD
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00019BD5 File Offset: 0x00017DD5
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
					this.UpdateTypeVisual();
				}
			}
		}

		// Token: 0x04000418 RID: 1048
		private int _type = -1;
	}
}
