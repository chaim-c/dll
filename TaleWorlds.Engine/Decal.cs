using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000012 RID: 18
	[EngineClass("rglDecal")]
	public sealed class Decal : GameEntityComponent
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002F90 File Offset: 0x00001190
		internal Decal(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F99 File Offset: 0x00001199
		public static Decal CreateDecal(string name = null)
		{
			return EngineApplicationInterface.IDecal.CreateDecal(name);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002FA6 File Offset: 0x000011A6
		public Decal CreateCopy()
		{
			return EngineApplicationInterface.IDecal.CreateCopy(base.Pointer);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FB8 File Offset: 0x000011B8
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002FCA File Offset: 0x000011CA
		public uint GetFactor1()
		{
			return EngineApplicationInterface.IDecal.GetFactor1(base.Pointer);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FDC File Offset: 0x000011DC
		public void SetFactor1Linear(uint linearFactorColor1)
		{
			EngineApplicationInterface.IDecal.SetFactor1Linear(base.Pointer, linearFactorColor1);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002FEF File Offset: 0x000011EF
		public void SetFactor1(uint factorColor1)
		{
			EngineApplicationInterface.IDecal.SetFactor1(base.Pointer, factorColor1);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003002 File Offset: 0x00001202
		public void SetVectorArgument(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IDecal.SetVectorArgument(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003019 File Offset: 0x00001219
		public void SetVectorArgument2(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IDecal.SetVectorArgument2(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003030 File Offset: 0x00001230
		public Material GetMaterial()
		{
			return EngineApplicationInterface.IDecal.GetMaterial(base.Pointer);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003042 File Offset: 0x00001242
		public void SetMaterial(Material material)
		{
			EngineApplicationInterface.IDecal.SetMaterial(base.Pointer, material.Pointer);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000305A File Offset: 0x0000125A
		public void SetFrame(MatrixFrame Frame)
		{
			this.Frame = Frame;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003064 File Offset: 0x00001264
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000308C File Offset: 0x0000128C
		public MatrixFrame Frame
		{
			get
			{
				MatrixFrame result = default(MatrixFrame);
				EngineApplicationInterface.IDecal.GetFrame(base.Pointer, ref result);
				return result;
			}
			set
			{
				EngineApplicationInterface.IDecal.SetFrame(base.Pointer, ref value);
			}
		}
	}
}
