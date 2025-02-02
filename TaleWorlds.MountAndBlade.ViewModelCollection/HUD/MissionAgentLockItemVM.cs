using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000041 RID: 65
	public class MissionAgentLockItemVM : ViewModel
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000176D1 File Offset: 0x000158D1
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000176D9 File Offset: 0x000158D9
		public Agent TrackedAgent { get; private set; }

		// Token: 0x06000595 RID: 1429 RVA: 0x000176E2 File Offset: 0x000158E2
		public MissionAgentLockItemVM(Agent agent, MissionAgentLockItemVM.LockStates initialLockState)
		{
			this.TrackedAgent = agent;
			this.LockState = (int)initialLockState;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000176FF File Offset: 0x000158FF
		public void SetLockState(MissionAgentLockItemVM.LockStates lockState)
		{
			this.LockState = (int)lockState;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017708 File Offset: 0x00015908
		public void UpdatePosition(Vec2 position)
		{
			this.Position = position;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00017711 File Offset: 0x00015911
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00017719 File Offset: 0x00015919
		[DataSourceProperty]
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (value != this._position)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001773C File Offset: 0x0001593C
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00017744 File Offset: 0x00015944
		[DataSourceProperty]
		public int LockState
		{
			get
			{
				return this._lockState;
			}
			set
			{
				if (value != this._lockState)
				{
					this._lockState = value;
					base.OnPropertyChangedWithValue(value, "LockState");
				}
			}
		}

		// Token: 0x040002AC RID: 684
		private Vec2 _position;

		// Token: 0x040002AD RID: 685
		private int _lockState = -1;

		// Token: 0x020000C9 RID: 201
		public enum LockStates
		{
			// Token: 0x040005E1 RID: 1505
			Possible,
			// Token: 0x040005E2 RID: 1506
			Active
		}
	}
}
