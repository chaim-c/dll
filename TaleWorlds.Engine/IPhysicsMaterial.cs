using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200002D RID: 45
	[ApplicationInterfaceBase]
	internal interface IPhysicsMaterial
	{
		// Token: 0x06000414 RID: 1044
		[EngineMethod("get_index_with_name", false)]
		PhysicsMaterial GetIndexWithName(string materialName);

		// Token: 0x06000415 RID: 1045
		[EngineMethod("get_material_count", false)]
		int GetMaterialCount();

		// Token: 0x06000416 RID: 1046
		[EngineMethod("get_material_name_at_index", false)]
		string GetMaterialNameAtIndex(int index);

		// Token: 0x06000417 RID: 1047
		[EngineMethod("get_material_flags_at_index", false)]
		PhysicsMaterialFlags GetFlagsAtIndex(int index);

		// Token: 0x06000418 RID: 1048
		[EngineMethod("get_restitution_at_index", false)]
		float GetRestitutionAtIndex(int index);

		// Token: 0x06000419 RID: 1049
		[EngineMethod("get_dynamic_friction_at_index", false)]
		float GetDynamicFrictionAtIndex(int index);

		// Token: 0x0600041A RID: 1050
		[EngineMethod("get_static_friction_at_index", false)]
		float GetStaticFrictionAtIndex(int index);

		// Token: 0x0600041B RID: 1051
		[EngineMethod("get_softness_at_index", false)]
		float GetSoftnessAtIndex(int index);
	}
}
