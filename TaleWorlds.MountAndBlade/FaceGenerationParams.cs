using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000218 RID: 536
	[EngineStruct("Face_generation_params", false)]
	public struct FaceGenerationParams
	{
		// Token: 0x06001D6B RID: 7531 RVA: 0x00067160 File Offset: 0x00065360
		public static FaceGenerationParams Create()
		{
			FaceGenerationParams result;
			result.Seed = 0;
			result.CurrentBeard = 0;
			result.CurrentHair = 0;
			result.CurrentEyebrow = 0;
			result.IsHairFlipped = false;
			result.CurrentRace = 0;
			result.CurrentGender = 0;
			result.CurrentFaceTexture = 0;
			result.CurrentMouthTexture = 0;
			result.CurrentFaceTattoo = 0;
			result.CurrentVoice = 0;
			result.HairFilter = 0;
			result.BeardFilter = 0;
			result.TattooFilter = 0;
			result.FaceTextureFilter = 0;
			result.TattooZeroProbability = 0f;
			result.KeyWeights = new float[320];
			result.CurrentAge = 0f;
			result.CurrentWeight = 0f;
			result.CurrentBuild = 0f;
			result.CurrentSkinColorOffset = 0f;
			result.CurrentHairColorOffset = 0f;
			result.CurrentEyeColorOffset = 0f;
			result.FaceDirtAmount = 0f;
			result.CurrentFaceTattooColorOffset1 = 0f;
			result.HeightMultiplier = 0f;
			result.VoicePitch = 0f;
			result.UseCache = false;
			result.UseGpuMorph = false;
			result.Padding2 = false;
			return result;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00067294 File Offset: 0x00065494
		public void SetRaceGenderAndAdjustParams(int race, int gender, int curAge)
		{
			this.CurrentGender = gender;
			this.CurrentRace = race;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			float num8 = 0f;
			MBBodyProperties.GetParamsMax(race, gender, curAge, ref num, ref num2, ref num3, ref num4, ref num7, ref num6, ref num5, ref num8);
			this.CurrentHair = MBMath.ClampInt(this.CurrentHair, 0, num - 1);
			this.CurrentBeard = MBMath.ClampInt(this.CurrentBeard, 0, num2 - 1);
			this.CurrentFaceTexture = MBMath.ClampInt(this.CurrentFaceTexture, 0, num3 - 1);
			this.CurrentMouthTexture = MBMath.ClampInt(this.CurrentMouthTexture, 0, num4 - 1);
			this.CurrentFaceTattoo = MBMath.ClampInt(this.CurrentFaceTattoo, 0, num7 - 1);
			this.CurrentVoice = MBMath.ClampInt(this.CurrentVoice, 0, num6 - 1);
			this.VoicePitch = MBMath.ClampFloat(this.VoicePitch, 0f, 1f);
			this.CurrentEyebrow = MBMath.ClampInt(this.CurrentEyebrow, 0, num5 - 1);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00067390 File Offset: 0x00065590
		public void SetRandomParamsExceptKeys(int race, int gender, int minAge, out float scale)
		{
			int maxValue = 0;
			int maxValue2 = 0;
			int maxValue3 = 0;
			int maxValue4 = 0;
			int maxValue5 = 0;
			int maxValue6 = 0;
			int maxValue7 = 0;
			scale = 0f;
			MBBodyProperties.GetParamsMax(race, gender, minAge, ref maxValue, ref maxValue2, ref maxValue3, ref maxValue4, ref maxValue7, ref maxValue6, ref maxValue5, ref scale);
			this.CurrentHair = MBRandom.RandomInt(maxValue);
			this.CurrentBeard = MBRandom.RandomInt(maxValue2);
			this.CurrentFaceTexture = MBRandom.RandomInt(maxValue3);
			this.CurrentMouthTexture = MBRandom.RandomInt(maxValue4);
			this.CurrentFaceTattoo = MBRandom.RandomInt(maxValue7);
			this.CurrentVoice = MBRandom.RandomInt(maxValue6);
			this.VoicePitch = MBRandom.RandomFloat;
			this.CurrentEyebrow = MBRandom.RandomInt(maxValue5);
			this.CurrentSkinColorOffset = MBRandom.RandomFloat;
			this.CurrentHairColorOffset = MBRandom.RandomFloat;
			this.CurrentEyeColorOffset = MBRandom.RandomFloat;
			this.CurrentFaceTattooColorOffset1 = MBRandom.RandomFloat;
			this.HeightMultiplier = MBRandom.RandomFloat;
		}

		// Token: 0x04000988 RID: 2440
		public int Seed;

		// Token: 0x04000989 RID: 2441
		public int CurrentBeard;

		// Token: 0x0400098A RID: 2442
		public int CurrentHair;

		// Token: 0x0400098B RID: 2443
		public int CurrentEyebrow;

		// Token: 0x0400098C RID: 2444
		public int CurrentRace;

		// Token: 0x0400098D RID: 2445
		public int CurrentGender;

		// Token: 0x0400098E RID: 2446
		public int CurrentFaceTexture;

		// Token: 0x0400098F RID: 2447
		public int CurrentMouthTexture;

		// Token: 0x04000990 RID: 2448
		public int CurrentFaceTattoo;

		// Token: 0x04000991 RID: 2449
		public int CurrentVoice;

		// Token: 0x04000992 RID: 2450
		public int HairFilter;

		// Token: 0x04000993 RID: 2451
		public int BeardFilter;

		// Token: 0x04000994 RID: 2452
		public int TattooFilter;

		// Token: 0x04000995 RID: 2453
		public int FaceTextureFilter;

		// Token: 0x04000996 RID: 2454
		public float TattooZeroProbability;

		// Token: 0x04000997 RID: 2455
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 320)]
		public float[] KeyWeights;

		// Token: 0x04000998 RID: 2456
		public float CurrentAge;

		// Token: 0x04000999 RID: 2457
		public float CurrentWeight;

		// Token: 0x0400099A RID: 2458
		public float CurrentBuild;

		// Token: 0x0400099B RID: 2459
		public float CurrentSkinColorOffset;

		// Token: 0x0400099C RID: 2460
		public float CurrentHairColorOffset;

		// Token: 0x0400099D RID: 2461
		public float CurrentEyeColorOffset;

		// Token: 0x0400099E RID: 2462
		public float FaceDirtAmount;

		// Token: 0x0400099F RID: 2463
		[CustomEngineStructMemberData("current_face_tattoo_color_offset_1")]
		public float CurrentFaceTattooColorOffset1;

		// Token: 0x040009A0 RID: 2464
		public float HeightMultiplier;

		// Token: 0x040009A1 RID: 2465
		public float VoicePitch;

		// Token: 0x040009A2 RID: 2466
		[MarshalAs(UnmanagedType.U1)]
		public bool IsHairFlipped;

		// Token: 0x040009A3 RID: 2467
		[MarshalAs(UnmanagedType.U1)]
		public bool UseCache;

		// Token: 0x040009A4 RID: 2468
		[MarshalAs(UnmanagedType.U1)]
		public bool UseGpuMorph;

		// Token: 0x040009A5 RID: 2469
		[MarshalAs(UnmanagedType.U1)]
		public bool Padding2;
	}
}
