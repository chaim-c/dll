using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E1 RID: 737
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	internal sealed class DefineSynchedMissionObjectType : Attribute
	{
		// Token: 0x06002806 RID: 10246 RVA: 0x0009A42E File Offset: 0x0009862E
		public DefineSynchedMissionObjectType(Type type)
		{
			this.Type = type;
		}

		// Token: 0x04000F39 RID: 3897
		public readonly Type Type;
	}
}
