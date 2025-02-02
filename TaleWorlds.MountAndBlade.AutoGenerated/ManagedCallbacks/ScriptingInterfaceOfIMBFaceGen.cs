using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000013 RID: 19
	internal class ScriptingInterfaceOfIMBFaceGen : IMBFaceGen
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000A98D File Offset: 0x00008B8D
		public bool EnforceConstraints(ref FaceGenerationParams faceGenerationParams)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_EnforceConstraintsDelegate(ref faceGenerationParams);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A99A File Offset: 0x00008B9A
		public void GetDeformKeyData(int keyNo, ref DeformKeyData deformKeyData, int race, int gender, float age)
		{
			ScriptingInterfaceOfIMBFaceGen.call_GetDeformKeyDataDelegate(keyNo, ref deformKeyData, race, gender, age);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A9AD File Offset: 0x00008BAD
		public int GetFaceGenInstancesLength(int race, int gender, float age)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetFaceGenInstancesLengthDelegate(race, gender, age);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A9BC File Offset: 0x00008BBC
		public int GetHairColorCount(int race, int curGender, float age)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetHairColorCountDelegate(race, curGender, age);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public void GetHairColorGradientPoints(int race, int curGender, float age, Vec3[] colors)
		{
			PinnedArrayData<Vec3> pinnedArrayData = new PinnedArrayData<Vec3>(colors, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIMBFaceGen.call_GetHairColorGradientPointsDelegate(race, curGender, age, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000AA00 File Offset: 0x00008C00
		public int GetMaturityType(float age)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetMaturityTypeDelegate(age);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000AA0D File Offset: 0x00008C0D
		public int GetNumEditableDeformKeys(int race, bool initialGender, float age)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetNumEditableDeformKeysDelegate(race, initialGender, age);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public void GetParamsFromKey(ref FaceGenerationParams faceGenerationParams, ref BodyProperties bodyProperties, bool earsAreHidden, bool mouthHidden)
		{
			ScriptingInterfaceOfIMBFaceGen.call_GetParamsFromKeyDelegate(ref faceGenerationParams, ref bodyProperties, earsAreHidden, mouthHidden);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000AA30 File Offset: 0x00008C30
		public void GetParamsMax(int race, int curGender, float curAge, ref int hairNum, ref int beardNum, ref int faceTextureNum, ref int mouthTextureNum, ref int faceTattooNum, ref int soundNum, ref int eyebrowNum, ref float scale)
		{
			ScriptingInterfaceOfIMBFaceGen.call_GetParamsMaxDelegate(race, curGender, curAge, ref hairNum, ref beardNum, ref faceTextureNum, ref mouthTextureNum, ref faceTattooNum, ref soundNum, ref eyebrowNum, ref scale);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AA5A File Offset: 0x00008C5A
		public string GetRaceIds()
		{
			if (ScriptingInterfaceOfIMBFaceGen.call_GetRaceIdsDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AA70 File Offset: 0x00008C70
		public void GetRandomBodyProperties(int race, int gender, ref BodyProperties bodyPropertiesMin, ref BodyProperties bodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tatooTags, ref BodyProperties outBodyProperties)
		{
			byte[] array = null;
			if (hairTags != null)
			{
				int byteCount = ScriptingInterfaceOfIMBFaceGen._utf8.GetByteCount(hairTags);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBFaceGen._utf8.GetBytes(hairTags, 0, hairTags.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (beardTags != null)
			{
				int byteCount2 = ScriptingInterfaceOfIMBFaceGen._utf8.GetByteCount(beardTags);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIMBFaceGen._utf8.GetBytes(beardTags, 0, beardTags.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			byte[] array3 = null;
			if (tatooTags != null)
			{
				int byteCount3 = ScriptingInterfaceOfIMBFaceGen._utf8.GetByteCount(tatooTags);
				array3 = ((byteCount3 < 1024) ? CallbackStringBufferManager.StringBuffer2 : new byte[byteCount3 + 1]);
				ScriptingInterfaceOfIMBFaceGen._utf8.GetBytes(tatooTags, 0, tatooTags.Length, array3, 0);
				array3[byteCount3] = 0;
			}
			ScriptingInterfaceOfIMBFaceGen.call_GetRandomBodyPropertiesDelegate(race, gender, ref bodyPropertiesMin, ref bodyPropertiesMax, hairCoverType, seed, array, array2, array3, ref outBodyProperties);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AB6F File Offset: 0x00008D6F
		public float GetScaleFromKey(int race, int gender, ref BodyProperties initialBodyProperties)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetScaleFromKeyDelegate(race, gender, ref initialBodyProperties);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AB7E File Offset: 0x00008D7E
		public int GetSkinColorCount(int race, int curGender, float age)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetSkinColorCountDelegate(race, curGender, age);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000AB90 File Offset: 0x00008D90
		public void GetSkinColorGradientPoints(int race, int curGender, float age, Vec3[] colors)
		{
			PinnedArrayData<Vec3> pinnedArrayData = new PinnedArrayData<Vec3>(colors, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIMBFaceGen.call_GetSkinColorGradientPointsDelegate(race, curGender, age, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		public int GetTatooColorCount(int race, int curGender, float age)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetTatooColorCountDelegate(race, curGender, age);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		public void GetTatooColorGradientPoints(int race, int curGender, float age, Vec3[] colors)
		{
			PinnedArrayData<Vec3> pinnedArrayData = new PinnedArrayData<Vec3>(colors, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIMBFaceGen.call_GetTatooColorGradientPointsDelegate(race, curGender, age, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000AC08 File Offset: 0x00008E08
		public int GetVoiceRecordsCount(int race, int curGender, float age)
		{
			return ScriptingInterfaceOfIMBFaceGen.call_GetVoiceRecordsCountDelegate(race, curGender, age);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AC18 File Offset: 0x00008E18
		public void GetVoiceTypeUsableForPlayerData(int race, int curGender, float age, bool[] aiArray)
		{
			PinnedArrayData<bool> pinnedArrayData = new PinnedArrayData<bool>(aiArray, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIMBFaceGen.call_GetVoiceTypeUsableForPlayerDataDelegate(race, curGender, age, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public void GetZeroProbabilities(int race, int curGender, float curAge, ref float tattooZeroProbability)
		{
			ScriptingInterfaceOfIMBFaceGen.call_GetZeroProbabilitiesDelegate(race, curGender, curAge, ref tattooZeroProbability);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000AC5D File Offset: 0x00008E5D
		public void ProduceNumericKeyWithDefaultValues(ref BodyProperties initialBodyProperties, bool earsAreHidden, bool mouthIsHidden, int race, int gender, float age)
		{
			ScriptingInterfaceOfIMBFaceGen.call_ProduceNumericKeyWithDefaultValuesDelegate(ref initialBodyProperties, earsAreHidden, mouthIsHidden, race, gender, age);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000AC72 File Offset: 0x00008E72
		public void ProduceNumericKeyWithParams(ref FaceGenerationParams faceGenerationParams, bool earsAreHidden, bool mouthIsHidden, ref BodyProperties bodyProperties)
		{
			ScriptingInterfaceOfIMBFaceGen.call_ProduceNumericKeyWithParamsDelegate(ref faceGenerationParams, earsAreHidden, mouthIsHidden, ref bodyProperties);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AC83 File Offset: 0x00008E83
		public void TransformFaceKeysToDefaultFace(ref FaceGenerationParams faceGenerationParams)
		{
			ScriptingInterfaceOfIMBFaceGen.call_TransformFaceKeysToDefaultFaceDelegate(ref faceGenerationParams);
		}

		// Token: 0x0400018A RID: 394
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400018B RID: 395
		public static ScriptingInterfaceOfIMBFaceGen.EnforceConstraintsDelegate call_EnforceConstraintsDelegate;

		// Token: 0x0400018C RID: 396
		public static ScriptingInterfaceOfIMBFaceGen.GetDeformKeyDataDelegate call_GetDeformKeyDataDelegate;

		// Token: 0x0400018D RID: 397
		public static ScriptingInterfaceOfIMBFaceGen.GetFaceGenInstancesLengthDelegate call_GetFaceGenInstancesLengthDelegate;

		// Token: 0x0400018E RID: 398
		public static ScriptingInterfaceOfIMBFaceGen.GetHairColorCountDelegate call_GetHairColorCountDelegate;

		// Token: 0x0400018F RID: 399
		public static ScriptingInterfaceOfIMBFaceGen.GetHairColorGradientPointsDelegate call_GetHairColorGradientPointsDelegate;

		// Token: 0x04000190 RID: 400
		public static ScriptingInterfaceOfIMBFaceGen.GetMaturityTypeDelegate call_GetMaturityTypeDelegate;

		// Token: 0x04000191 RID: 401
		public static ScriptingInterfaceOfIMBFaceGen.GetNumEditableDeformKeysDelegate call_GetNumEditableDeformKeysDelegate;

		// Token: 0x04000192 RID: 402
		public static ScriptingInterfaceOfIMBFaceGen.GetParamsFromKeyDelegate call_GetParamsFromKeyDelegate;

		// Token: 0x04000193 RID: 403
		public static ScriptingInterfaceOfIMBFaceGen.GetParamsMaxDelegate call_GetParamsMaxDelegate;

		// Token: 0x04000194 RID: 404
		public static ScriptingInterfaceOfIMBFaceGen.GetRaceIdsDelegate call_GetRaceIdsDelegate;

		// Token: 0x04000195 RID: 405
		public static ScriptingInterfaceOfIMBFaceGen.GetRandomBodyPropertiesDelegate call_GetRandomBodyPropertiesDelegate;

		// Token: 0x04000196 RID: 406
		public static ScriptingInterfaceOfIMBFaceGen.GetScaleFromKeyDelegate call_GetScaleFromKeyDelegate;

		// Token: 0x04000197 RID: 407
		public static ScriptingInterfaceOfIMBFaceGen.GetSkinColorCountDelegate call_GetSkinColorCountDelegate;

		// Token: 0x04000198 RID: 408
		public static ScriptingInterfaceOfIMBFaceGen.GetSkinColorGradientPointsDelegate call_GetSkinColorGradientPointsDelegate;

		// Token: 0x04000199 RID: 409
		public static ScriptingInterfaceOfIMBFaceGen.GetTatooColorCountDelegate call_GetTatooColorCountDelegate;

		// Token: 0x0400019A RID: 410
		public static ScriptingInterfaceOfIMBFaceGen.GetTatooColorGradientPointsDelegate call_GetTatooColorGradientPointsDelegate;

		// Token: 0x0400019B RID: 411
		public static ScriptingInterfaceOfIMBFaceGen.GetVoiceRecordsCountDelegate call_GetVoiceRecordsCountDelegate;

		// Token: 0x0400019C RID: 412
		public static ScriptingInterfaceOfIMBFaceGen.GetVoiceTypeUsableForPlayerDataDelegate call_GetVoiceTypeUsableForPlayerDataDelegate;

		// Token: 0x0400019D RID: 413
		public static ScriptingInterfaceOfIMBFaceGen.GetZeroProbabilitiesDelegate call_GetZeroProbabilitiesDelegate;

		// Token: 0x0400019E RID: 414
		public static ScriptingInterfaceOfIMBFaceGen.ProduceNumericKeyWithDefaultValuesDelegate call_ProduceNumericKeyWithDefaultValuesDelegate;

		// Token: 0x0400019F RID: 415
		public static ScriptingInterfaceOfIMBFaceGen.ProduceNumericKeyWithParamsDelegate call_ProduceNumericKeyWithParamsDelegate;

		// Token: 0x040001A0 RID: 416
		public static ScriptingInterfaceOfIMBFaceGen.TransformFaceKeysToDefaultFaceDelegate call_TransformFaceKeysToDefaultFaceDelegate;

		// Token: 0x020001EB RID: 491
		// (Invoke) Token: 0x06000A51 RID: 2641
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool EnforceConstraintsDelegate(ref FaceGenerationParams faceGenerationParams);

		// Token: 0x020001EC RID: 492
		// (Invoke) Token: 0x06000A55 RID: 2645
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetDeformKeyDataDelegate(int keyNo, ref DeformKeyData deformKeyData, int race, int gender, float age);

		// Token: 0x020001ED RID: 493
		// (Invoke) Token: 0x06000A59 RID: 2649
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFaceGenInstancesLengthDelegate(int race, int gender, float age);

		// Token: 0x020001EE RID: 494
		// (Invoke) Token: 0x06000A5D RID: 2653
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetHairColorCountDelegate(int race, int curGender, float age);

		// Token: 0x020001EF RID: 495
		// (Invoke) Token: 0x06000A61 RID: 2657
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetHairColorGradientPointsDelegate(int race, int curGender, float age, IntPtr colors);

		// Token: 0x020001F0 RID: 496
		// (Invoke) Token: 0x06000A65 RID: 2661
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMaturityTypeDelegate(float age);

		// Token: 0x020001F1 RID: 497
		// (Invoke) Token: 0x06000A69 RID: 2665
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumEditableDeformKeysDelegate(int race, [MarshalAs(UnmanagedType.U1)] bool initialGender, float age);

		// Token: 0x020001F2 RID: 498
		// (Invoke) Token: 0x06000A6D RID: 2669
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetParamsFromKeyDelegate(ref FaceGenerationParams faceGenerationParams, ref BodyProperties bodyProperties, [MarshalAs(UnmanagedType.U1)] bool earsAreHidden, [MarshalAs(UnmanagedType.U1)] bool mouthHidden);

		// Token: 0x020001F3 RID: 499
		// (Invoke) Token: 0x06000A71 RID: 2673
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetParamsMaxDelegate(int race, int curGender, float curAge, ref int hairNum, ref int beardNum, ref int faceTextureNum, ref int mouthTextureNum, ref int faceTattooNum, ref int soundNum, ref int eyebrowNum, ref float scale);

		// Token: 0x020001F4 RID: 500
		// (Invoke) Token: 0x06000A75 RID: 2677
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetRaceIdsDelegate();

		// Token: 0x020001F5 RID: 501
		// (Invoke) Token: 0x06000A79 RID: 2681
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetRandomBodyPropertiesDelegate(int race, int gender, ref BodyProperties bodyPropertiesMin, ref BodyProperties bodyPropertiesMax, int hairCoverType, int seed, byte[] hairTags, byte[] beardTags, byte[] tatooTags, ref BodyProperties outBodyProperties);

		// Token: 0x020001F6 RID: 502
		// (Invoke) Token: 0x06000A7D RID: 2685
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetScaleFromKeyDelegate(int race, int gender, ref BodyProperties initialBodyProperties);

		// Token: 0x020001F7 RID: 503
		// (Invoke) Token: 0x06000A81 RID: 2689
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSkinColorCountDelegate(int race, int curGender, float age);

		// Token: 0x020001F8 RID: 504
		// (Invoke) Token: 0x06000A85 RID: 2693
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetSkinColorGradientPointsDelegate(int race, int curGender, float age, IntPtr colors);

		// Token: 0x020001F9 RID: 505
		// (Invoke) Token: 0x06000A89 RID: 2697
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetTatooColorCountDelegate(int race, int curGender, float age);

		// Token: 0x020001FA RID: 506
		// (Invoke) Token: 0x06000A8D RID: 2701
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetTatooColorGradientPointsDelegate(int race, int curGender, float age, IntPtr colors);

		// Token: 0x020001FB RID: 507
		// (Invoke) Token: 0x06000A91 RID: 2705
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVoiceRecordsCountDelegate(int race, int curGender, float age);

		// Token: 0x020001FC RID: 508
		// (Invoke) Token: 0x06000A95 RID: 2709
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetVoiceTypeUsableForPlayerDataDelegate(int race, int curGender, float age, IntPtr aiArray);

		// Token: 0x020001FD RID: 509
		// (Invoke) Token: 0x06000A99 RID: 2713
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetZeroProbabilitiesDelegate(int race, int curGender, float curAge, ref float tattooZeroProbability);

		// Token: 0x020001FE RID: 510
		// (Invoke) Token: 0x06000A9D RID: 2717
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ProduceNumericKeyWithDefaultValuesDelegate(ref BodyProperties initialBodyProperties, [MarshalAs(UnmanagedType.U1)] bool earsAreHidden, [MarshalAs(UnmanagedType.U1)] bool mouthIsHidden, int race, int gender, float age);

		// Token: 0x020001FF RID: 511
		// (Invoke) Token: 0x06000AA1 RID: 2721
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ProduceNumericKeyWithParamsDelegate(ref FaceGenerationParams faceGenerationParams, [MarshalAs(UnmanagedType.U1)] bool earsAreHidden, [MarshalAs(UnmanagedType.U1)] bool mouthIsHidden, ref BodyProperties bodyProperties);

		// Token: 0x02000200 RID: 512
		// (Invoke) Token: 0x06000AA5 RID: 2725
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TransformFaceKeysToDefaultFaceDelegate(ref FaceGenerationParams faceGenerationParams);
	}
}
