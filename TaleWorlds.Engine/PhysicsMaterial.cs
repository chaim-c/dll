using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000074 RID: 116
	[EngineStruct("int", false)]
	public struct PhysicsMaterial
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x00008DF8 File Offset: 0x00006FF8
		internal PhysicsMaterial(int index)
		{
			this = default(PhysicsMaterial);
			this.Index = index;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00008E08 File Offset: 0x00007008
		public bool IsValid
		{
			get
			{
				return this.Index >= 0;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00008E16 File Offset: 0x00007016
		public PhysicsMaterialFlags GetFlags()
		{
			return PhysicsMaterial.GetFlagsAtIndex(this.Index);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00008E23 File Offset: 0x00007023
		public float GetDynamicFriction()
		{
			return PhysicsMaterial.GetDynamicFrictionAtIndex(this.Index);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00008E30 File Offset: 0x00007030
		public float GetStaticFriction()
		{
			return PhysicsMaterial.GetStaticFrictionAtIndex(this.Index);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00008E3D File Offset: 0x0000703D
		public float GetSoftness()
		{
			return PhysicsMaterial.GetSoftnessAtIndex(this.Index);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00008E4A File Offset: 0x0000704A
		public float GetRestitution()
		{
			return PhysicsMaterial.GetRestitutionAtIndex(this.Index);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00008E57 File Offset: 0x00007057
		public string Name
		{
			get
			{
				return PhysicsMaterial.GetNameAtIndex(this.Index);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00008E64 File Offset: 0x00007064
		public bool Equals(PhysicsMaterial m)
		{
			return this.Index == m.Index;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00008E74 File Offset: 0x00007074
		public static int GetMaterialCount()
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetMaterialCount();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00008E80 File Offset: 0x00007080
		public static PhysicsMaterial GetFromName(string id)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetIndexWithName(id);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00008E8D File Offset: 0x0000708D
		public static string GetNameAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetMaterialNameAtIndex(index);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00008E9A File Offset: 0x0000709A
		public static PhysicsMaterialFlags GetFlagsAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetFlagsAtIndex(index);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00008EA7 File Offset: 0x000070A7
		public static float GetRestitutionAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetRestitutionAtIndex(index);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00008EB4 File Offset: 0x000070B4
		public static float GetSoftnessAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetSoftnessAtIndex(index);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00008EC1 File Offset: 0x000070C1
		public static float GetDynamicFrictionAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetDynamicFrictionAtIndex(index);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00008ECE File Offset: 0x000070CE
		public static float GetStaticFrictionAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetStaticFrictionAtIndex(index);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00008EDB File Offset: 0x000070DB
		public static PhysicsMaterial GetFromIndex(int index)
		{
			return new PhysicsMaterial(index);
		}

		// Token: 0x04000159 RID: 345
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly int Index;

		// Token: 0x0400015A RID: 346
		public static readonly PhysicsMaterial InvalidPhysicsMaterial = new PhysicsMaterial(-1);
	}
}
