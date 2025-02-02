using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000021 RID: 33
	[ApplicationInterfaceBase]
	internal interface IPhysicsShape
	{
		// Token: 0x060001AA RID: 426
		[EngineMethod("get_from_resource", false)]
		PhysicsShape GetFromResource(string bodyName, bool mayReturnNull);

		// Token: 0x060001AB RID: 427
		[EngineMethod("add_preload_queue_with_name", false)]
		void AddPreloadQueueWithName(string bodyName, ref Vec3 scale);

		// Token: 0x060001AC RID: 428
		[EngineMethod("process_preload_queue", false)]
		void ProcessPreloadQueue();

		// Token: 0x060001AD RID: 429
		[EngineMethod("unload_dynamic_bodies", false)]
		void UnloadDynamicBodies();

		// Token: 0x060001AE RID: 430
		[EngineMethod("create_body_copy", false)]
		PhysicsShape CreateBodyCopy(UIntPtr bodyPointer);

		// Token: 0x060001AF RID: 431
		[EngineMethod("get_name", false)]
		string GetName(PhysicsShape shape);

		// Token: 0x060001B0 RID: 432
		[EngineMethod("triangle_mesh_count", false)]
		int TriangleMeshCount(UIntPtr pointer);

		// Token: 0x060001B1 RID: 433
		[EngineMethod("triangle_count_in_triangle_mesh", false)]
		int TriangleCountInTriangleMesh(UIntPtr pointer, int meshIndex);

		// Token: 0x060001B2 RID: 434
		[EngineMethod("get_dominant_material_index_for_mesh_at_index", false)]
		int GetDominantMaterialForTriangleMesh(PhysicsShape shape, int meshIndex);

		// Token: 0x060001B3 RID: 435
		[EngineMethod("get_triangle", false)]
		void GetTriangle(UIntPtr pointer, Vec3[] data, int meshIndex, int triangleIndex);

		// Token: 0x060001B4 RID: 436
		[EngineMethod("sphere_count", false)]
		int SphereCount(UIntPtr pointer);

		// Token: 0x060001B5 RID: 437
		[EngineMethod("get_sphere", false)]
		void GetSphere(UIntPtr shapePointer, ref SphereData data, int sphereIndex);

		// Token: 0x060001B6 RID: 438
		[EngineMethod("get_sphere_with_material", false)]
		void GetSphereWithMaterial(UIntPtr shapePointer, ref SphereData data, ref int materialIndex, int sphereIndex);

		// Token: 0x060001B7 RID: 439
		[EngineMethod("prepare", false)]
		void Prepare(UIntPtr shapePointer);

		// Token: 0x060001B8 RID: 440
		[EngineMethod("capsule_count", false)]
		int CapsuleCount(UIntPtr shapePointer);

		// Token: 0x060001B9 RID: 441
		[EngineMethod("add_capsule", false)]
		void AddCapsule(UIntPtr shapePointer, ref CapsuleData data);

		// Token: 0x060001BA RID: 442
		[EngineMethod("init_description", false)]
		void InitDescription(UIntPtr shapePointer);

		// Token: 0x060001BB RID: 443
		[EngineMethod("add_sphere", false)]
		void AddSphere(UIntPtr shapePointer, ref Vec3 origin, float radius);

		// Token: 0x060001BC RID: 444
		[EngineMethod("set_capsule", false)]
		void SetCapsule(UIntPtr shapePointer, ref CapsuleData data, int index);

		// Token: 0x060001BD RID: 445
		[EngineMethod("get_capsule", false)]
		void GetCapsule(UIntPtr shapePointer, ref CapsuleData data, int index);

		// Token: 0x060001BE RID: 446
		[EngineMethod("get_capsule_with_material", false)]
		void GetCapsuleWithMaterial(UIntPtr shapePointer, ref CapsuleData data, ref int materialIndex, int index);

		// Token: 0x060001BF RID: 447
		[EngineMethod("clear", false)]
		void clear(UIntPtr shapePointer);

		// Token: 0x060001C0 RID: 448
		[EngineMethod("transform", false)]
		void Transform(UIntPtr shapePointer, ref MatrixFrame frame);

		// Token: 0x060001C1 RID: 449
		[EngineMethod("get_bounding_box_center", false)]
		Vec3 GetBoundingBoxCenter(UIntPtr shapePointer);
	}
}
