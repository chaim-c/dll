using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000076 RID: 118
	[EngineStruct("rglPhysics_contact_info", false)]
	public struct PhysicsContactInfo
	{
		// Token: 0x0400015F RID: 351
		public Vec3 Position;

		// Token: 0x04000160 RID: 352
		public Vec3 Normal;

		// Token: 0x04000161 RID: 353
		public float Penetration;

		// Token: 0x04000162 RID: 354
		public Vec3 Impulse;

		// Token: 0x04000163 RID: 355
		[CustomEngineStructMemberData("physics_material0_index")]
		public PhysicsMaterial PhysicsMaterial0;

		// Token: 0x04000164 RID: 356
		[CustomEngineStructMemberData("physics_material1_index")]
		public PhysicsMaterial PhysicsMaterial1;
	}
}
