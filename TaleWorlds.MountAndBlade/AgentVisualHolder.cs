using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B9 RID: 697
	public class AgentVisualHolder : IAgentVisual
	{
		// Token: 0x0600265A RID: 9818 RVA: 0x00090B43 File Offset: 0x0008ED43
		public AgentVisualHolder(MatrixFrame frame, Equipment equipment, string name, BodyProperties bodyProperties)
		{
			this.SetFrame(ref frame);
			this._equipment = equipment;
			this._characterObjectStringID = name;
			this._bodyProperties = bodyProperties;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00090B69 File Offset: 0x0008ED69
		public void SetAction(ActionIndexCache actionName, float startProgress = 0f, bool forceFaceMorphRestart = true)
		{
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00090B6B File Offset: 0x0008ED6B
		public GameEntity GetEntity()
		{
			return null;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00090B6E File Offset: 0x0008ED6E
		public MBAgentVisuals GetVisuals()
		{
			return null;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00090B71 File Offset: 0x0008ED71
		public void SetFrame(ref MatrixFrame frame)
		{
			this._frame = frame;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x00090B7F File Offset: 0x0008ED7F
		public MatrixFrame GetFrame()
		{
			return this._frame;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x00090B87 File Offset: 0x0008ED87
		public BodyProperties GetBodyProperties()
		{
			return this._bodyProperties;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00090B8F File Offset: 0x0008ED8F
		public void SetBodyProperties(BodyProperties bodyProperties)
		{
			this._bodyProperties = bodyProperties;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x00090B98 File Offset: 0x0008ED98
		public bool GetIsFemale()
		{
			return false;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x00090B9B File Offset: 0x0008ED9B
		public string GetCharacterObjectID()
		{
			return this._characterObjectStringID;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00090BA3 File Offset: 0x0008EDA3
		public void SetCharacterObjectID(string id)
		{
			this._characterObjectStringID = id;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x00090BAC File Offset: 0x0008EDAC
		public Equipment GetEquipment()
		{
			return this._equipment;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x00090BB4 File Offset: 0x0008EDB4
		public void RefreshWithNewEquipment(Equipment equipment)
		{
			this._equipment = equipment;
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00090BBD File Offset: 0x0008EDBD
		public void SetClothingColors(uint color1, uint color2)
		{
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00090BBF File Offset: 0x0008EDBF
		public void GetClothingColors(out uint color1, out uint color2)
		{
			color1 = uint.MaxValue;
			color2 = uint.MaxValue;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00090BC7 File Offset: 0x0008EDC7
		public AgentVisualsData GetCopyAgentVisualsData()
		{
			return null;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x00090BCA File Offset: 0x0008EDCA
		public void Refresh(bool needBatchedVersionForWeaponMeshes, AgentVisualsData data, bool forceUseFaceCache = false)
		{
		}

		// Token: 0x04000E3B RID: 3643
		private MatrixFrame _frame;

		// Token: 0x04000E3C RID: 3644
		private Equipment _equipment;

		// Token: 0x04000E3D RID: 3645
		private string _characterObjectStringID;

		// Token: 0x04000E3E RID: 3646
		private BodyProperties _bodyProperties;
	}
}
