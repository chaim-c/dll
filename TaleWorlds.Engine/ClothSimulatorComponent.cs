using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200000F RID: 15
	[EngineClass("rglCloth_simulator_component")]
	public sealed class ClothSimulatorComponent : GameEntityComponent
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002B3D File Offset: 0x00000D3D
		internal ClothSimulatorComponent(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B46 File Offset: 0x00000D46
		public void SetMaxDistanceMultiplier(float multiplier)
		{
			EngineApplicationInterface.IClothSimulatorComponent.SetMaxDistanceMultiplier(base.Pointer, multiplier);
		}
	}
}
