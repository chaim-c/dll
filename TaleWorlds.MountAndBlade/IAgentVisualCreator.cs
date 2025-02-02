using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B7 RID: 695
	public interface IAgentVisualCreator
	{
		// Token: 0x0600264C RID: 9804
		IAgentVisual Create(AgentVisualsData data, string name, bool needBatchedVersionForWeaponMeshes, bool forceUseFaceCache);
	}
}
