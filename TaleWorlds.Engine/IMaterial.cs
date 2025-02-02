using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200001B RID: 27
	[ApplicationInterfaceBase]
	internal interface IMaterial
	{
		// Token: 0x06000123 RID: 291
		[EngineMethod("create_copy", false)]
		Material CreateCopy(UIntPtr materialPointer);

		// Token: 0x06000124 RID: 292
		[EngineMethod("get_from_resource", false)]
		Material GetFromResource(string materialName);

		// Token: 0x06000125 RID: 293
		[EngineMethod("get_default_material", false)]
		Material GetDefaultMaterial();

		// Token: 0x06000126 RID: 294
		[EngineMethod("get_outline_material", false)]
		Material GetOutlineMaterial(UIntPtr materialPointer);

		// Token: 0x06000127 RID: 295
		[EngineMethod("get_name", false)]
		string GetName(UIntPtr materialPointer);

		// Token: 0x06000128 RID: 296
		[EngineMethod("set_name", false)]
		void SetName(UIntPtr materialPointer, string name);

		// Token: 0x06000129 RID: 297
		[EngineMethod("get_alpha_blend_mode", false)]
		int GetAlphaBlendMode(UIntPtr materialPointer);

		// Token: 0x0600012A RID: 298
		[EngineMethod("set_alpha_blend_mode", false)]
		void SetAlphaBlendMode(UIntPtr materialPointer, int alphaBlendMode);

		// Token: 0x0600012B RID: 299
		[EngineMethod("release", false)]
		void Release(UIntPtr materialPointer);

		// Token: 0x0600012C RID: 300
		[EngineMethod("set_shader", false)]
		void SetShader(UIntPtr materialPointer, UIntPtr shaderPointer);

		// Token: 0x0600012D RID: 301
		[EngineMethod("get_shader", false)]
		Shader GetShader(UIntPtr materialPointer);

		// Token: 0x0600012E RID: 302
		[EngineMethod("get_shader_flags", false)]
		ulong GetShaderFlags(UIntPtr materialPointer);

		// Token: 0x0600012F RID: 303
		[EngineMethod("set_shader_flags", false)]
		void SetShaderFlags(UIntPtr materialPointer, ulong shaderFlags);

		// Token: 0x06000130 RID: 304
		[EngineMethod("set_mesh_vector_argument", false)]
		void SetMeshVectorArgument(UIntPtr materialPointer, float x, float y, float z, float w);

		// Token: 0x06000131 RID: 305
		[EngineMethod("set_texture", false)]
		void SetTexture(UIntPtr materialPointer, int textureType, UIntPtr texturePointer);

		// Token: 0x06000132 RID: 306
		[EngineMethod("set_texture_at_slot", false)]
		void SetTextureAtSlot(UIntPtr materialPointer, int textureSlotIndex, UIntPtr texturePointer);

		// Token: 0x06000133 RID: 307
		[EngineMethod("get_texture", false)]
		Texture GetTexture(UIntPtr materialPointer, int textureType);

		// Token: 0x06000134 RID: 308
		[EngineMethod("set_alpha_test_value", false)]
		void SetAlphaTestValue(UIntPtr materialPointer, float alphaTestValue);

		// Token: 0x06000135 RID: 309
		[EngineMethod("get_alpha_test_value", false)]
		float GetAlphaTestValue(UIntPtr materialPointer);

		// Token: 0x06000136 RID: 310
		[EngineMethod("get_flags", false)]
		uint GetFlags(UIntPtr materialPointer);

		// Token: 0x06000137 RID: 311
		[EngineMethod("set_flags", false)]
		void SetFlags(UIntPtr materialPointer, uint flags);

		// Token: 0x06000138 RID: 312
		[EngineMethod("add_material_shader_flag", false)]
		void AddMaterialShaderFlag(UIntPtr materialPointer, string flagName, bool showErrors);

		// Token: 0x06000139 RID: 313
		[EngineMethod("set_area_map_scale", false)]
		void SetAreaMapScale(UIntPtr materialPointer, float scale);

		// Token: 0x0600013A RID: 314
		[EngineMethod("set_enable_skinning", false)]
		void SetEnableSkinning(UIntPtr materialPointer, bool enable);

		// Token: 0x0600013B RID: 315
		[EngineMethod("using_skinning", false)]
		bool UsingSkinning(UIntPtr materialPointer);
	}
}
