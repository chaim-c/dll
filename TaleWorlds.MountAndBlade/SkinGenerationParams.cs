using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000217 RID: 535
	[EngineStruct("Skin_generation_params", false)]
	public struct SkinGenerationParams
	{
		// Token: 0x06001D69 RID: 7529 RVA: 0x0006707C File Offset: 0x0006527C
		public static SkinGenerationParams Create()
		{
			SkinGenerationParams result;
			result._skinMeshesVisibilityMask = 481;
			result._underwearType = Equipment.UnderwearTypes.FullUnderwear;
			result._bodyMeshType = 0;
			result._hairCoverType = 0;
			result._beardCoverType = 0;
			result._prepareImmediately = false;
			result._bodyDeformType = -1;
			result._faceDirtAmount = 0f;
			result._gender = 0;
			result._race = 0;
			result._useTranslucency = false;
			result._useTesselation = false;
			return result;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000670F4 File Offset: 0x000652F4
		public SkinGenerationParams(int skinMeshesVisibilityMask, Equipment.UnderwearTypes underwearType, int bodyMeshType, int hairCoverType, int beardCoverType, int bodyDeformType, bool prepareImmediately, float faceDirtAmount, int gender, int race, bool useTranslucency, bool useTesselation)
		{
			this._skinMeshesVisibilityMask = skinMeshesVisibilityMask;
			this._underwearType = underwearType;
			this._bodyMeshType = bodyMeshType;
			this._hairCoverType = hairCoverType;
			this._beardCoverType = beardCoverType;
			this._bodyDeformType = bodyDeformType;
			this._prepareImmediately = prepareImmediately;
			this._faceDirtAmount = faceDirtAmount;
			this._gender = gender;
			this._race = race;
			this._useTranslucency = useTranslucency;
			this._useTesselation = useTesselation;
		}

		// Token: 0x0400097C RID: 2428
		public int _skinMeshesVisibilityMask;

		// Token: 0x0400097D RID: 2429
		public Equipment.UnderwearTypes _underwearType;

		// Token: 0x0400097E RID: 2430
		public int _bodyMeshType;

		// Token: 0x0400097F RID: 2431
		public int _hairCoverType;

		// Token: 0x04000980 RID: 2432
		public int _beardCoverType;

		// Token: 0x04000981 RID: 2433
		public int _bodyDeformType;

		// Token: 0x04000982 RID: 2434
		[MarshalAs(UnmanagedType.U1)]
		public bool _prepareImmediately;

		// Token: 0x04000983 RID: 2435
		[MarshalAs(UnmanagedType.U1)]
		public bool _useTranslucency;

		// Token: 0x04000984 RID: 2436
		[MarshalAs(UnmanagedType.U1)]
		public bool _useTesselation;

		// Token: 0x04000985 RID: 2437
		public float _faceDirtAmount;

		// Token: 0x04000986 RID: 2438
		public int _gender;

		// Token: 0x04000987 RID: 2439
		public int _race;
	}
}
