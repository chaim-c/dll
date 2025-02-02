using System;
using System.Collections.Generic;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000033 RID: 51
	public class VisualDefinition
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000EF2E File Offset: 0x0000D12E
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000EF36 File Offset: 0x0000D136
		public string Name { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000EF3F File Offset: 0x0000D13F
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000EF47 File Offset: 0x0000D147
		public float TransitionDuration { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000EF50 File Offset: 0x0000D150
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000EF58 File Offset: 0x0000D158
		public float DelayOnBegin { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000EF61 File Offset: 0x0000D161
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000EF69 File Offset: 0x0000D169
		public bool EaseIn { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000EF72 File Offset: 0x0000D172
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000EF7A File Offset: 0x0000D17A
		public Dictionary<string, VisualState> VisualStates { get; private set; }

		// Token: 0x0600037D RID: 893 RVA: 0x0000EF83 File Offset: 0x0000D183
		public VisualDefinition(string name, float transitionDuration, float delayOnBegin, bool easeIn)
		{
			this.Name = name;
			this.TransitionDuration = transitionDuration;
			this.DelayOnBegin = delayOnBegin;
			this.EaseIn = easeIn;
			this.VisualStates = new Dictionary<string, VisualState>();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000EFB3 File Offset: 0x0000D1B3
		public void AddVisualState(VisualState visualState)
		{
			this.VisualStates.Add(visualState.State, visualState);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000EFC7 File Offset: 0x0000D1C7
		public VisualState GetVisualState(string state)
		{
			if (this.VisualStates.ContainsKey(state))
			{
				return this.VisualStates[state];
			}
			return null;
		}
	}
}
