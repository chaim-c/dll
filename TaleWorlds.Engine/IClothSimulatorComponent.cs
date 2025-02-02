using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200001F RID: 31
	[ApplicationInterfaceBase]
	internal interface IClothSimulatorComponent
	{
		// Token: 0x06000194 RID: 404
		[EngineMethod("set_maxdistance_multiplier", false)]
		void SetMaxDistanceMultiplier(UIntPtr cloth_pointer, float multiplier);
	}
}
