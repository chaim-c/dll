using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001BC RID: 444
	public static class MBBodyProperties
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x0004FF3F File Offset: 0x0004E13F
		public static int GetNumEditableDeformKeys(int race, bool initialGender, int age)
		{
			return MBAPI.IMBFaceGen.GetNumEditableDeformKeys(race, initialGender, (float)age);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0004FF4F File Offset: 0x0004E14F
		public static void GetParamsFromKey(ref FaceGenerationParams faceGenerationParams, BodyProperties bodyProperties, bool earsAreHidden, bool mouthHidden)
		{
			MBAPI.IMBFaceGen.GetParamsFromKey(ref faceGenerationParams, ref bodyProperties, earsAreHidden, mouthHidden);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0004FF60 File Offset: 0x0004E160
		public static void GetParamsMax(int race, int curGender, int curAge, ref int hairNum, ref int beardNum, ref int faceTextureNum, ref int mouthTextureNum, ref int faceTattooNum, ref int soundNum, ref int eyebrowNum, ref float scale)
		{
			MBAPI.IMBFaceGen.GetParamsMax(race, curGender, (float)curAge, ref hairNum, ref beardNum, ref faceTextureNum, ref mouthTextureNum, ref faceTattooNum, ref soundNum, ref eyebrowNum, ref scale);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0004FF8A File Offset: 0x0004E18A
		public static void GetZeroProbabilities(int race, int curGender, float curAge, ref float tattooZeroProbability)
		{
			MBAPI.IMBFaceGen.GetZeroProbabilities(race, curGender, curAge, ref tattooZeroProbability);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0004FF9A File Offset: 0x0004E19A
		public static void ProduceNumericKeyWithParams(FaceGenerationParams faceGenerationParams, bool earsAreHidden, bool mouthIsHidden, ref BodyProperties bodyProperties)
		{
			MBAPI.IMBFaceGen.ProduceNumericKeyWithParams(ref faceGenerationParams, earsAreHidden, mouthIsHidden, ref bodyProperties);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0004FFAB File Offset: 0x0004E1AB
		public static void TransformFaceKeysToDefaultFace(ref FaceGenerationParams faceGenerationParams)
		{
			MBAPI.IMBFaceGen.TransformFaceKeysToDefaultFace(ref faceGenerationParams);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0004FFB8 File Offset: 0x0004E1B8
		public static void ProduceNumericKeyWithDefaultValues(ref BodyProperties initialBodyProperties, bool earsAreHidden, bool mouthIsHidden, int race, int gender, int age)
		{
			MBAPI.IMBFaceGen.ProduceNumericKeyWithDefaultValues(ref initialBodyProperties, earsAreHidden, mouthIsHidden, race, gender, (float)age);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0004FFD0 File Offset: 0x0004E1D0
		public static BodyProperties GetRandomBodyProperties(int race, bool isFemale, BodyProperties bodyPropertiesMin, BodyProperties bodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tatooTags)
		{
			BodyProperties result = default(BodyProperties);
			MBAPI.IMBFaceGen.GetRandomBodyProperties(race, isFemale ? 1 : 0, ref bodyPropertiesMin, ref bodyPropertiesMax, hairCoverType, seed, hairTags, beardTags, tatooTags, ref result);
			return result;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00050008 File Offset: 0x0004E208
		public static DeformKeyData GetDeformKeyData(int keyNo, int race, int gender, int age)
		{
			DeformKeyData result = default(DeformKeyData);
			MBAPI.IMBFaceGen.GetDeformKeyData(keyNo, ref result, race, gender, (float)age);
			return result;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0005002F File Offset: 0x0004E22F
		public static int GetFaceGenInstancesLength(int race, int gender, int age)
		{
			return MBAPI.IMBFaceGen.GetFaceGenInstancesLength(race, gender, (float)age);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0005003F File Offset: 0x0004E23F
		public static bool EnforceConstraints(ref FaceGenerationParams faceGenerationParams)
		{
			return MBAPI.IMBFaceGen.EnforceConstraints(ref faceGenerationParams);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0005004C File Offset: 0x0004E24C
		public static float GetScaleFromKey(int race, int gender, BodyProperties bodyProperties)
		{
			return MBAPI.IMBFaceGen.GetScaleFromKey(race, gender, ref bodyProperties);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0005005C File Offset: 0x0004E25C
		public static int GetHairColorCount(int race, int curGender, int age)
		{
			return MBAPI.IMBFaceGen.GetHairColorCount(race, curGender, (float)age);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0005006C File Offset: 0x0004E26C
		public static List<uint> GetHairColorGradientPoints(int race, int curGender, int age)
		{
			int hairColorCount = MBBodyProperties.GetHairColorCount(race, curGender, age);
			List<uint> list = new List<uint>();
			Vec3[] array = new Vec3[hairColorCount];
			MBAPI.IMBFaceGen.GetHairColorGradientPoints(race, curGender, (float)age, array);
			foreach (Vec3 vec in array)
			{
				list.Add(MBMath.ColorFromRGBA(vec.x, vec.y, vec.z, 1f));
			}
			return list;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000500DB File Offset: 0x0004E2DB
		public static int GetTatooColorCount(int race, int curGender, int age)
		{
			return MBAPI.IMBFaceGen.GetTatooColorCount(race, curGender, (float)age);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000500EC File Offset: 0x0004E2EC
		public static List<uint> GetTatooColorGradientPoints(int race, int curGender, int age)
		{
			int tatooColorCount = MBBodyProperties.GetTatooColorCount(race, curGender, age);
			List<uint> list = new List<uint>();
			Vec3[] array = new Vec3[tatooColorCount];
			MBAPI.IMBFaceGen.GetTatooColorGradientPoints(race, curGender, (float)age, array);
			foreach (Vec3 vec in array)
			{
				list.Add(MBMath.ColorFromRGBA(vec.x, vec.y, vec.z, 1f));
			}
			return list;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0005015B File Offset: 0x0004E35B
		public static int GetSkinColorCount(int race, int curGender, int age)
		{
			return MBAPI.IMBFaceGen.GetSkinColorCount(race, curGender, (float)age);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0005016B File Offset: 0x0004E36B
		public static BodyMeshMaturityType GetMaturityType(float age)
		{
			return (BodyMeshMaturityType)MBAPI.IMBFaceGen.GetMaturityType(age);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00050178 File Offset: 0x0004E378
		public static string[] GetRaceIds()
		{
			return MBAPI.IMBFaceGen.GetRaceIds().Split(new char[]
			{
				';'
			});
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00050194 File Offset: 0x0004E394
		public static List<uint> GetSkinColorGradientPoints(int race, int curGender, int age)
		{
			int skinColorCount = MBBodyProperties.GetSkinColorCount(race, curGender, age);
			List<uint> list = new List<uint>();
			Vec3[] array = new Vec3[skinColorCount];
			MBAPI.IMBFaceGen.GetSkinColorGradientPoints(race, curGender, (float)age, array);
			foreach (Vec3 vec in array)
			{
				list.Add(MBMath.ColorFromRGBA(vec.x, vec.y, vec.z, 1f));
			}
			return list;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00050204 File Offset: 0x0004E404
		public static List<bool> GetVoiceTypeUsableForPlayerData(int race, int curGender, float age, int voiceTypeCount)
		{
			bool[] array = new bool[voiceTypeCount];
			MBAPI.IMBFaceGen.GetVoiceTypeUsableForPlayerData(race, curGender, age, array);
			return new List<bool>(array);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0005022C File Offset: 0x0004E42C
		public static void SetHair(ref BodyProperties bodyProperties, int hair, int beard, int tattoo)
		{
			FaceGenerationParams faceGenerationParams = FaceGenerationParams.Create();
			MBBodyProperties.GetParamsFromKey(ref faceGenerationParams, bodyProperties, false, false);
			if (hair > -1)
			{
				faceGenerationParams.CurrentHair = hair;
			}
			if (beard > -1)
			{
				faceGenerationParams.CurrentBeard = beard;
			}
			if (tattoo > -1)
			{
				faceGenerationParams.CurrentFaceTattoo = tattoo;
			}
			MBBodyProperties.ProduceNumericKeyWithParams(faceGenerationParams, false, false, ref bodyProperties);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0005027C File Offset: 0x0004E47C
		public static void SetBody(ref BodyProperties bodyProperties, int build, int weight)
		{
			FaceGenerationParams faceGenerationParams = FaceGenerationParams.Create();
			MBBodyProperties.GetParamsFromKey(ref faceGenerationParams, bodyProperties, false, false);
			MBBodyProperties.ProduceNumericKeyWithParams(faceGenerationParams, false, false, ref bodyProperties);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000502B0 File Offset: 0x0004E4B0
		public static void SetPigmentation(ref BodyProperties bodyProperties, int skinColor, int hairColor, int eyeColor)
		{
			FaceGenerationParams faceGenerationParams = FaceGenerationParams.Create();
			MBBodyProperties.GetParamsFromKey(ref faceGenerationParams, bodyProperties, false, false);
			MBBodyProperties.ProduceNumericKeyWithParams(faceGenerationParams, false, false, ref bodyProperties);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000502E8 File Offset: 0x0004E4E8
		public static void GenerateParentKey(BodyProperties childBodyProperties, int race, ref BodyProperties motherBodyProperties, ref BodyProperties fatherBodyProperties)
		{
			FaceGenerationParams faceGenerationParams = FaceGenerationParams.Create();
			FaceGenerationParams faceGenerationParams2 = FaceGenerationParams.Create();
			FaceGenerationParams faceGenerationParams3 = FaceGenerationParams.Create();
			MBBodyProperties.GenerationType[] array = new MBBodyProperties.GenerationType[4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (MBBodyProperties.GenerationType)MBRandom.RandomInt(2);
			}
			MBBodyProperties.GetParamsFromKey(ref faceGenerationParams, childBodyProperties, false, false);
			int faceGenInstancesLength = MBBodyProperties.GetFaceGenInstancesLength(race, faceGenerationParams.CurrentGender, (int)faceGenerationParams.CurrentAge);
			for (int j = 0; j < faceGenInstancesLength; j++)
			{
				DeformKeyData deformKeyData = MBBodyProperties.GetDeformKeyData(j, race, faceGenerationParams.CurrentGender, (int)faceGenerationParams.CurrentAge);
				if (deformKeyData.GroupId >= 0 && deformKeyData.GroupId != 0 && deformKeyData.GroupId != 5 && deformKeyData.GroupId != 6)
				{
					float num = MBRandom.RandomFloat * MathF.Min(faceGenerationParams.KeyWeights[j], 1f - faceGenerationParams.KeyWeights[j]);
					if (array[deformKeyData.GroupId - 1] == MBBodyProperties.GenerationType.FromMother)
					{
						faceGenerationParams3.KeyWeights[j] = faceGenerationParams.KeyWeights[j];
						faceGenerationParams2.KeyWeights[j] = faceGenerationParams.KeyWeights[j] + num;
					}
					else if (array[deformKeyData.GroupId - 1] == MBBodyProperties.GenerationType.FromFather)
					{
						faceGenerationParams2.KeyWeights[j] = faceGenerationParams.KeyWeights[j];
						faceGenerationParams3.KeyWeights[j] = faceGenerationParams.KeyWeights[j] + num;
					}
					else
					{
						faceGenerationParams3.KeyWeights[j] = faceGenerationParams.KeyWeights[j] + num;
						faceGenerationParams2.KeyWeights[j] = faceGenerationParams.KeyWeights[j] - num;
					}
				}
			}
			faceGenerationParams2.CurrentAge = faceGenerationParams.CurrentAge + (float)MBRandom.RandomInt(18, 25);
			float num2;
			faceGenerationParams2.SetRandomParamsExceptKeys(race, 0, (int)faceGenerationParams2.CurrentAge, out num2);
			faceGenerationParams2.CurrentFaceTattoo = 0;
			faceGenerationParams3.CurrentAge = faceGenerationParams.CurrentAge + (float)MBRandom.RandomInt(18, 22);
			float num3;
			faceGenerationParams3.SetRandomParamsExceptKeys(race, 1, (int)faceGenerationParams3.CurrentAge, out num3);
			faceGenerationParams3.CurrentFaceTattoo = 0;
			faceGenerationParams3.HeightMultiplier = faceGenerationParams2.HeightMultiplier * MBRandom.RandomFloatRanged(0.7f, 0.9f);
			if (faceGenerationParams3.CurrentHair == 0)
			{
				faceGenerationParams3.CurrentHair = 1;
			}
			float num4 = MBRandom.RandomFloat * MathF.Min(faceGenerationParams.CurrentSkinColorOffset, 1f - faceGenerationParams.CurrentSkinColorOffset);
			float num5 = MBRandom.RandomFloat * MathF.Min(faceGenerationParams.CurrentHairColorOffset, 1f - faceGenerationParams.CurrentHairColorOffset);
			int num6 = MBRandom.RandomInt(2);
			if (num6 == 1)
			{
				faceGenerationParams2.CurrentSkinColorOffset = faceGenerationParams.CurrentSkinColorOffset + num4;
				faceGenerationParams3.CurrentSkinColorOffset = faceGenerationParams.CurrentSkinColorOffset - num4;
			}
			else
			{
				faceGenerationParams2.CurrentSkinColorOffset = faceGenerationParams.CurrentSkinColorOffset - num4;
				faceGenerationParams3.CurrentSkinColorOffset = faceGenerationParams.CurrentSkinColorOffset + num4;
			}
			if (num6 == 1)
			{
				faceGenerationParams2.CurrentHairColorOffset = faceGenerationParams.CurrentHairColorOffset + num5;
				faceGenerationParams3.CurrentHairColorOffset = faceGenerationParams.CurrentHairColorOffset - num5;
			}
			else
			{
				faceGenerationParams2.CurrentHairColorOffset = faceGenerationParams.CurrentHairColorOffset - num5;
				faceGenerationParams3.CurrentHairColorOffset = faceGenerationParams.CurrentHairColorOffset + num5;
			}
			MBBodyProperties.ProduceNumericKeyWithParams(faceGenerationParams3, false, false, ref motherBodyProperties);
			MBBodyProperties.ProduceNumericKeyWithParams(faceGenerationParams2, false, false, ref fatherBodyProperties);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000505D8 File Offset: 0x0004E7D8
		public static BodyProperties GetBodyPropertiesWithAge(ref BodyProperties bodyProperties, float age)
		{
			FaceGenerationParams faceGenerationParams = default(FaceGenerationParams);
			MBBodyProperties.GetParamsFromKey(ref faceGenerationParams, bodyProperties, false, false);
			faceGenerationParams.CurrentAge = age;
			BodyProperties result = default(BodyProperties);
			MBBodyProperties.ProduceNumericKeyWithParams(faceGenerationParams, false, false, ref result);
			return result;
		}

		// Token: 0x020004BF RID: 1215
		public enum GenerationType
		{
			// Token: 0x04001AC7 RID: 6855
			FromMother,
			// Token: 0x04001AC8 RID: 6856
			FromFather,
			// Token: 0x04001AC9 RID: 6857
			Count
		}
	}
}
