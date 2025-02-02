using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002BA RID: 698
	public class PeerVisualsHolder
	{
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x00090BCC File Offset: 0x0008EDCC
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x00090BD4 File Offset: 0x0008EDD4
		public MissionPeer Peer { get; private set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x00090BDD File Offset: 0x0008EDDD
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x00090BE5 File Offset: 0x0008EDE5
		public int VisualsIndex { get; private set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x00090BEE File Offset: 0x0008EDEE
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x00090BF6 File Offset: 0x0008EDF6
		public IAgentVisual AgentVisuals { get; private set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x00090BFF File Offset: 0x0008EDFF
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x00090C07 File Offset: 0x0008EE07
		public IAgentVisual MountAgentVisuals { get; private set; }

		// Token: 0x06002673 RID: 9843 RVA: 0x00090C10 File Offset: 0x0008EE10
		public PeerVisualsHolder(MissionPeer peer, int index, IAgentVisual agentVisuals, IAgentVisual mountVisuals)
		{
			this.Peer = peer;
			this.VisualsIndex = index;
			this.AgentVisuals = agentVisuals;
			this.MountAgentVisuals = mountVisuals;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00090C35 File Offset: 0x0008EE35
		public void SetMountVisuals(IAgentVisual mountAgentVisuals)
		{
			this.MountAgentVisuals = mountAgentVisuals;
		}
	}
}
