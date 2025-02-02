using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031E RID: 798
	[Obsolete]
	public class DestructedPrefabInfoMissionObject : MissionObject
	{
		// Token: 0x040010A0 RID: 4256
		public string DestructedPrefabName;

		// Token: 0x040010A1 RID: 4257
		public Vec3 Translate = new Vec3(0f, 0f, 0f, -1f);

		// Token: 0x040010A2 RID: 4258
		public Vec3 Rotation = new Vec3(0f, 0f, 0f, -1f);

		// Token: 0x040010A3 RID: 4259
		public Vec3 Scale = new Vec3(1f, 1f, 1f, -1f);
	}
}
