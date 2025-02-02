using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001CA RID: 458
	public abstract class MBMissile
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x0005C73E File Offset: 0x0005A93E
		protected MBMissile(Mission mission)
		{
			this._mission = mission;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0005C74D File Offset: 0x0005A94D
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x0005C755 File Offset: 0x0005A955
		public int Index { get; set; }

		// Token: 0x06001A43 RID: 6723 RVA: 0x0005C75E File Offset: 0x0005A95E
		public Vec3 GetPosition()
		{
			return MBAPI.IMBMission.GetPositionOfMissile(this._mission.Pointer, this.Index);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0005C77B File Offset: 0x0005A97B
		public Vec3 GetVelocity()
		{
			return MBAPI.IMBMission.GetVelocityOfMissile(this._mission.Pointer, this.Index);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0005C798 File Offset: 0x0005A998
		public bool GetHasRigidBody()
		{
			return MBAPI.IMBMission.GetMissileHasRigidBody(this._mission.Pointer, this.Index);
		}

		// Token: 0x040007F7 RID: 2039
		private readonly Mission _mission;
	}
}
