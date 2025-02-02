using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036D RID: 877
	public class VolumeBox : MissionObject
	{
		// Token: 0x06003066 RID: 12390 RVA: 0x000C7E28 File Offset: 0x000C6028
		protected internal override void OnInit()
		{
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000C7E2A File Offset: 0x000C602A
		public void AddToCheckList(Agent agent)
		{
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000C7E2C File Offset: 0x000C602C
		public void RemoveFromCheckList(Agent agent)
		{
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000C7E2E File Offset: 0x000C602E
		public void SetIsOccupiedDelegate(VolumeBox.VolumeBoxDelegate volumeBoxDelegate)
		{
			this._volumeBoxIsOccupiedDelegate = volumeBoxDelegate;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000C7E38 File Offset: 0x000C6038
		public bool HasAgentsInAttackerSide()
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, globalFrame.origin.AsVec2, globalFrame.rotation.GetScaleVector().AsVec2.Length, false);
			while (proximityMapSearchStruct.LastFoundAgent != null)
			{
				Agent lastFoundAgent = proximityMapSearchStruct.LastFoundAgent;
				if (lastFoundAgent.Team != null && lastFoundAgent.Team.Side == BattleSideEnum.Attacker && this.IsPointIn(lastFoundAgent.Position))
				{
					return true;
				}
				AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
			}
			return false;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000C7ED0 File Offset: 0x000C60D0
		public bool IsPointIn(Vec3 point)
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			Vec3 scaleVector = globalFrame.rotation.GetScaleVector();
			globalFrame.rotation.ApplyScaleLocal(new Vec3(1f / scaleVector.x, 1f / scaleVector.y, 1f / scaleVector.z, -1f));
			point = globalFrame.TransformToLocal(point);
			return MathF.Abs(point.x) <= scaleVector.x / 2f && MathF.Abs(point.y) <= scaleVector.y / 2f && MathF.Abs(point.z) <= scaleVector.z / 2f;
		}

		// Token: 0x0400148A RID: 5258
		private VolumeBox.VolumeBoxDelegate _volumeBoxIsOccupiedDelegate;

		// Token: 0x02000626 RID: 1574
		// (Invoke) Token: 0x06003C50 RID: 15440
		public delegate void VolumeBoxDelegate(VolumeBox volumeBox, List<Agent> agentsInVolume);
	}
}
