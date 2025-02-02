using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200001E RID: 30
	[ApplicationInterfaceBase]
	internal interface IDecal
	{
		// Token: 0x06000189 RID: 393
		[EngineMethod("get_material", false)]
		Material GetMaterial(UIntPtr decalPointer);

		// Token: 0x0600018A RID: 394
		[EngineMethod("set_material", false)]
		void SetMaterial(UIntPtr decalPointer, UIntPtr materialPointer);

		// Token: 0x0600018B RID: 395
		[EngineMethod("create_decal", false)]
		Decal CreateDecal(string name);

		// Token: 0x0600018C RID: 396
		[EngineMethod("get_factor_1", false)]
		uint GetFactor1(UIntPtr decalPointer);

		// Token: 0x0600018D RID: 397
		[EngineMethod("set_factor_1_linear", false)]
		void SetFactor1Linear(UIntPtr decalPointer, uint linearFactorColor1);

		// Token: 0x0600018E RID: 398
		[EngineMethod("set_factor_1", false)]
		void SetFactor1(UIntPtr decalPointer, uint factorColor1);

		// Token: 0x0600018F RID: 399
		[EngineMethod("set_vector_argument", false)]
		void SetVectorArgument(UIntPtr decalPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x06000190 RID: 400
		[EngineMethod("set_vector_argument_2", false)]
		void SetVectorArgument2(UIntPtr decalPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x06000191 RID: 401
		[EngineMethod("get_global_frame", false)]
		void GetFrame(UIntPtr decalPointer, ref MatrixFrame outFrame);

		// Token: 0x06000192 RID: 402
		[EngineMethod("set_global_frame", false)]
		void SetFrame(UIntPtr decalPointer, ref MatrixFrame decalFrame);

		// Token: 0x06000193 RID: 403
		[EngineMethod("create_copy", false)]
		Decal CreateCopy(UIntPtr pointer);
	}
}
