using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000359 RID: 857
	public abstract class UsableMissionObjectComponent
	{
		// Token: 0x06002F0E RID: 12046 RVA: 0x000C0DE8 File Offset: 0x000BEFE8
		protected internal virtual void OnAdded(Scene scene)
		{
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000C0DEA File Offset: 0x000BEFEA
		protected internal virtual void OnRemoved()
		{
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000C0DEC File Offset: 0x000BEFEC
		protected internal virtual void OnFocusGain(Agent userAgent)
		{
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000C0DEE File Offset: 0x000BEFEE
		protected internal virtual void OnFocusLose(Agent userAgent)
		{
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000C0DF0 File Offset: 0x000BEFF0
		public virtual bool IsOnTickRequired()
		{
			return false;
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000C0DF3 File Offset: 0x000BEFF3
		protected internal virtual void OnTick(float dt)
		{
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000C0DF5 File Offset: 0x000BEFF5
		protected internal virtual void OnEditorTick(float dt)
		{
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000C0DF7 File Offset: 0x000BEFF7
		protected internal virtual void OnEditorValidate()
		{
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000C0DF9 File Offset: 0x000BEFF9
		protected internal virtual void OnUse(Agent userAgent)
		{
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000C0DFB File Offset: 0x000BEFFB
		protected internal virtual void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000C0DFD File Offset: 0x000BEFFD
		protected internal virtual void OnMissionReset()
		{
		}
	}
}
