using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000020 RID: 32
	[ApplicationInterfaceBase]
	internal interface ICompositeComponent
	{
		// Token: 0x06000195 RID: 405
		[EngineMethod("create_composite_component", false)]
		CompositeComponent CreateCompositeComponent();

		// Token: 0x06000196 RID: 406
		[EngineMethod("set_material", false)]
		void SetMaterial(UIntPtr compositeComponentPointer, UIntPtr materialPointer);

		// Token: 0x06000197 RID: 407
		[EngineMethod("create_copy", false)]
		CompositeComponent CreateCopy(UIntPtr pointer);

		// Token: 0x06000198 RID: 408
		[EngineMethod("add_component", false)]
		void AddComponent(UIntPtr pointer, UIntPtr componentPointer);

		// Token: 0x06000199 RID: 409
		[EngineMethod("add_prefab_entity", false)]
		void AddPrefabEntity(UIntPtr pointer, UIntPtr scenePointer, string prefabName);

		// Token: 0x0600019A RID: 410
		[EngineMethod("release", false)]
		void Release(UIntPtr compositeComponentPointer);

		// Token: 0x0600019B RID: 411
		[EngineMethod("get_factor_1", false)]
		uint GetFactor1(UIntPtr compositeComponentPointer);

		// Token: 0x0600019C RID: 412
		[EngineMethod("get_factor_2", false)]
		uint GetFactor2(UIntPtr compositeComponentPointer);

		// Token: 0x0600019D RID: 413
		[EngineMethod("set_factor_1", false)]
		void SetFactor1(UIntPtr compositeComponentPointer, uint factorColor1);

		// Token: 0x0600019E RID: 414
		[EngineMethod("set_factor_2", false)]
		void SetFactor2(UIntPtr compositeComponentPointer, uint factorColor2);

		// Token: 0x0600019F RID: 415
		[EngineMethod("set_vector_argument", false)]
		void SetVectorArgument(UIntPtr compositeComponentPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x060001A0 RID: 416
		[EngineMethod("get_frame", false)]
		void GetFrame(UIntPtr compositeComponentPointer, ref MatrixFrame outFrame);

		// Token: 0x060001A1 RID: 417
		[EngineMethod("set_frame", false)]
		void SetFrame(UIntPtr compositeComponentPointer, ref MatrixFrame meshFrame);

		// Token: 0x060001A2 RID: 418
		[EngineMethod("get_vector_user_data", false)]
		Vec3 GetVectorUserData(UIntPtr compositeComponentPointer);

		// Token: 0x060001A3 RID: 419
		[EngineMethod("set_vector_user_data", false)]
		void SetVectorUserData(UIntPtr compositeComponentPointer, ref Vec3 vectorArg);

		// Token: 0x060001A4 RID: 420
		[EngineMethod("get_bounding_box", false)]
		void GetBoundingBox(UIntPtr compositeComponentPointer, ref BoundingBox outBoundingBox);

		// Token: 0x060001A5 RID: 421
		[EngineMethod("set_visibility_mask", false)]
		void SetVisibilityMask(UIntPtr compositeComponentPointer, VisibilityMaskFlags visibilityMask);

		// Token: 0x060001A6 RID: 422
		[EngineMethod("get_first_meta_mesh", false)]
		MetaMesh GetFirstMetaMesh(UIntPtr compositeComponentPointer);

		// Token: 0x060001A7 RID: 423
		[EngineMethod("add_multi_mesh", false)]
		void AddMultiMesh(UIntPtr compositeComponentPointer, string multiMeshName);

		// Token: 0x060001A8 RID: 424
		[EngineMethod("is_visible", false)]
		bool IsVisible(UIntPtr compositeComponentPointer);

		// Token: 0x060001A9 RID: 425
		[EngineMethod("set_visible", false)]
		void SetVisible(UIntPtr compositeComponentPointer, bool visible);
	}
}
