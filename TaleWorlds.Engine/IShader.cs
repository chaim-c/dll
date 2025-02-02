using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000019 RID: 25
	[ApplicationInterfaceBase]
	internal interface IShader
	{
		// Token: 0x06000105 RID: 261
		[EngineMethod("get_from_resource", false)]
		Shader GetFromResource(string shaderName);

		// Token: 0x06000106 RID: 262
		[EngineMethod("get_name", false)]
		string GetName(UIntPtr shaderPointer);

		// Token: 0x06000107 RID: 263
		[EngineMethod("release", false)]
		void Release(UIntPtr shaderPointer);

		// Token: 0x06000108 RID: 264
		[EngineMethod("get_material_shader_flag_mask", false)]
		ulong GetMaterialShaderFlagMask(UIntPtr shaderPointer, string flagName, bool showError);
	}
}
