using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B8 RID: 696
	public interface IAgentVisual
	{
		// Token: 0x0600264D RID: 9805
		void SetAction(ActionIndexCache actionName, float startProgress = 0f, bool forceFaceMorphRestart = true);

		// Token: 0x0600264E RID: 9806
		MBAgentVisuals GetVisuals();

		// Token: 0x0600264F RID: 9807
		MatrixFrame GetFrame();

		// Token: 0x06002650 RID: 9808
		BodyProperties GetBodyProperties();

		// Token: 0x06002651 RID: 9809
		void SetBodyProperties(BodyProperties bodyProperties);

		// Token: 0x06002652 RID: 9810
		bool GetIsFemale();

		// Token: 0x06002653 RID: 9811
		string GetCharacterObjectID();

		// Token: 0x06002654 RID: 9812
		void SetCharacterObjectID(string id);

		// Token: 0x06002655 RID: 9813
		Equipment GetEquipment();

		// Token: 0x06002656 RID: 9814
		void SetClothingColors(uint color1, uint color2);

		// Token: 0x06002657 RID: 9815
		void GetClothingColors(out uint color1, out uint color2);

		// Token: 0x06002658 RID: 9816
		AgentVisualsData GetCopyAgentVisualsData();

		// Token: 0x06002659 RID: 9817
		void Refresh(bool needBatchedVersionForWeaponMeshes, AgentVisualsData data, bool forceUseFaceCache = false);
	}
}
