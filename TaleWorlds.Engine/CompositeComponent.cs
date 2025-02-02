using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000010 RID: 16
	[EngineClass("rglComposite_component")]
	public sealed class CompositeComponent : GameEntityComponent
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002B59 File Offset: 0x00000D59
		internal CompositeComponent(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002B62 File Offset: 0x00000D62
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B74 File Offset: 0x00000D74
		public static bool IsNull(CompositeComponent component)
		{
			return component == null || component.Pointer == UIntPtr.Zero;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B91 File Offset: 0x00000D91
		public static CompositeComponent CreateCompositeComponent()
		{
			return EngineApplicationInterface.ICompositeComponent.CreateCompositeComponent();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002B9D File Offset: 0x00000D9D
		public CompositeComponent CreateCopy()
		{
			return EngineApplicationInterface.ICompositeComponent.CreateCopy(base.Pointer);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BAF File Offset: 0x00000DAF
		public void AddComponent(GameEntityComponent component)
		{
			EngineApplicationInterface.ICompositeComponent.AddComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BC7 File Offset: 0x00000DC7
		public void AddPrefabEntity(string prefabName, Scene scene)
		{
			EngineApplicationInterface.ICompositeComponent.AddPrefabEntity(base.Pointer, scene.Pointer, prefabName);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public void Dispose()
		{
			if (this.IsValid)
			{
				this.Release();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BF6 File Offset: 0x00000DF6
		private void Release()
		{
			EngineApplicationInterface.ICompositeComponent.Release(base.Pointer);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C08 File Offset: 0x00000E08
		~CompositeComponent()
		{
			this.Dispose();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C34 File Offset: 0x00000E34
		public uint GetFactor1()
		{
			return EngineApplicationInterface.ICompositeComponent.GetFactor1(base.Pointer);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C46 File Offset: 0x00000E46
		public uint GetFactor2()
		{
			return EngineApplicationInterface.ICompositeComponent.GetFactor2(base.Pointer);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C58 File Offset: 0x00000E58
		public void SetFactor1(uint factorColor1)
		{
			EngineApplicationInterface.ICompositeComponent.SetFactor1(base.Pointer, factorColor1);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C6B File Offset: 0x00000E6B
		public void SetFactor2(uint factorColor2)
		{
			EngineApplicationInterface.ICompositeComponent.SetFactor2(base.Pointer, factorColor2);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002C7E File Offset: 0x00000E7E
		public void SetVectorArgument(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.ICompositeComponent.SetVectorArgument(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C95 File Offset: 0x00000E95
		public void SetMaterial(Material material)
		{
			EngineApplicationInterface.ICompositeComponent.SetMaterial(base.Pointer, material.Pointer);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002CB0 File Offset: 0x00000EB0
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public MatrixFrame Frame
		{
			get
			{
				MatrixFrame result = default(MatrixFrame);
				EngineApplicationInterface.ICompositeComponent.GetFrame(base.Pointer, ref result);
				return result;
			}
			set
			{
				EngineApplicationInterface.ICompositeComponent.SetFrame(base.Pointer, ref value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002CEC File Offset: 0x00000EEC
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002CFE File Offset: 0x00000EFE
		public Vec3 VectorUserData
		{
			get
			{
				return EngineApplicationInterface.ICompositeComponent.GetVectorUserData(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ICompositeComponent.SetVectorUserData(base.Pointer, ref value);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002D12 File Offset: 0x00000F12
		public void SetVisibilityMask(VisibilityMaskFlags visibilityMask)
		{
			EngineApplicationInterface.ICompositeComponent.SetVisibilityMask(base.Pointer, visibilityMask);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D25 File Offset: 0x00000F25
		public override MetaMesh GetFirstMetaMesh()
		{
			return EngineApplicationInterface.ICompositeComponent.GetFirstMetaMesh(base.Pointer);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D37 File Offset: 0x00000F37
		public void AddMultiMesh(string MultiMeshName)
		{
			EngineApplicationInterface.ICompositeComponent.AddMultiMesh(base.Pointer, MultiMeshName);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D4A File Offset: 0x00000F4A
		public void SetVisible(bool visible)
		{
			EngineApplicationInterface.ICompositeComponent.SetVisible(base.Pointer, visible);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D5D File Offset: 0x00000F5D
		public bool GetVisible()
		{
			return EngineApplicationInterface.ICompositeComponent.IsVisible(base.Pointer);
		}
	}
}
