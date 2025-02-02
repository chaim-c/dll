using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B5 RID: 437
	[ScriptingInterfaceBase]
	internal interface IMBFaceGen
	{
		// Token: 0x060017AA RID: 6058
		[EngineMethod("get_num_editable_deform_keys", false)]
		int GetNumEditableDeformKeys(int race, bool initialGender, float age);

		// Token: 0x060017AB RID: 6059
		[EngineMethod("get_params_from_key", false)]
		void GetParamsFromKey(ref FaceGenerationParams faceGenerationParams, ref BodyProperties bodyProperties, bool earsAreHidden, bool mouthHidden);

		// Token: 0x060017AC RID: 6060
		[EngineMethod("get_params_max", false)]
		void GetParamsMax(int race, int curGender, float curAge, ref int hairNum, ref int beardNum, ref int faceTextureNum, ref int mouthTextureNum, ref int faceTattooNum, ref int soundNum, ref int eyebrowNum, ref float scale);

		// Token: 0x060017AD RID: 6061
		[EngineMethod("get_zero_probabilities", false)]
		void GetZeroProbabilities(int race, int curGender, float curAge, ref float tattooZeroProbability);

		// Token: 0x060017AE RID: 6062
		[EngineMethod("produce_numeric_key_with_params", false)]
		void ProduceNumericKeyWithParams(ref FaceGenerationParams faceGenerationParams, bool earsAreHidden, bool mouthIsHidden, ref BodyProperties bodyProperties);

		// Token: 0x060017AF RID: 6063
		[EngineMethod("produce_numeric_key_with_default_values", false)]
		void ProduceNumericKeyWithDefaultValues(ref BodyProperties initialBodyProperties, bool earsAreHidden, bool mouthIsHidden, int race, int gender, float age);

		// Token: 0x060017B0 RID: 6064
		[EngineMethod("transform_face_keys_to_default_face", false)]
		void TransformFaceKeysToDefaultFace(ref FaceGenerationParams faceGenerationParams);

		// Token: 0x060017B1 RID: 6065
		[EngineMethod("get_random_body_properties", false)]
		void GetRandomBodyProperties(int race, int gender, ref BodyProperties bodyPropertiesMin, ref BodyProperties bodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tatooTags, ref BodyProperties outBodyProperties);

		// Token: 0x060017B2 RID: 6066
		[EngineMethod("enforce_constraints", false)]
		bool EnforceConstraints(ref FaceGenerationParams faceGenerationParams);

		// Token: 0x060017B3 RID: 6067
		[EngineMethod("get_deform_key_data", false)]
		void GetDeformKeyData(int keyNo, ref DeformKeyData deformKeyData, int race, int gender, float age);

		// Token: 0x060017B4 RID: 6068
		[EngineMethod("get_face_gen_instances_length", false)]
		int GetFaceGenInstancesLength(int race, int gender, float age);

		// Token: 0x060017B5 RID: 6069
		[EngineMethod("get_scale", false)]
		float GetScaleFromKey(int race, int gender, ref BodyProperties initialBodyProperties);

		// Token: 0x060017B6 RID: 6070
		[EngineMethod("get_voice_records_count", false)]
		int GetVoiceRecordsCount(int race, int curGender, float age);

		// Token: 0x060017B7 RID: 6071
		[EngineMethod("get_hair_color_count", false)]
		int GetHairColorCount(int race, int curGender, float age);

		// Token: 0x060017B8 RID: 6072
		[EngineMethod("get_hair_color_gradient_points", false)]
		void GetHairColorGradientPoints(int race, int curGender, float age, Vec3[] colors);

		// Token: 0x060017B9 RID: 6073
		[EngineMethod("get_tatoo_color_count", false)]
		int GetTatooColorCount(int race, int curGender, float age);

		// Token: 0x060017BA RID: 6074
		[EngineMethod("get_tatoo_color_gradient_points", false)]
		void GetTatooColorGradientPoints(int race, int curGender, float age, Vec3[] colors);

		// Token: 0x060017BB RID: 6075
		[EngineMethod("get_skin_color_count", false)]
		int GetSkinColorCount(int race, int curGender, float age);

		// Token: 0x060017BC RID: 6076
		[EngineMethod("get_maturity_type", false)]
		int GetMaturityType(float age);

		// Token: 0x060017BD RID: 6077
		[EngineMethod("get_voice_type_usable_for_player_data", false)]
		void GetVoiceTypeUsableForPlayerData(int race, int curGender, float age, bool[] aiArray);

		// Token: 0x060017BE RID: 6078
		[EngineMethod("get_skin_color_gradient_points", false)]
		void GetSkinColorGradientPoints(int race, int curGender, float age, Vec3[] colors);

		// Token: 0x060017BF RID: 6079
		[EngineMethod("get_race_ids", false)]
		string GetRaceIds();
	}
}
