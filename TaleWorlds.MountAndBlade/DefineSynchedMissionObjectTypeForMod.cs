using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E0 RID: 736
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class DefineSynchedMissionObjectTypeForMod : Attribute
	{
		// Token: 0x06002805 RID: 10245 RVA: 0x0009A41F File Offset: 0x0009861F
		public DefineSynchedMissionObjectTypeForMod(Type type)
		{
			this.Type = type;
		}

		// Token: 0x04000F38 RID: 3896
		public readonly Type Type;
	}
}
