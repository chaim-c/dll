using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E5 RID: 485
	public class BodyGenerator
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0005D938 File Offset: 0x0005BB38
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0005D940 File Offset: 0x0005BB40
		public BasicCharacterObject Character { get; private set; }

		// Token: 0x06001AFD RID: 6909 RVA: 0x0005D94C File Offset: 0x0005BB4C
		public BodyGenerator(BasicCharacterObject troop)
		{
			this.Character = troop;
			MBDebug.Print("FaceGen set character> character face key: " + troop.GetBodyProperties(troop.Equipment, -1), 0, Debug.DebugColor.White, 17592186044416UL);
			this.Race = this.Character.Race;
			this.IsFemale = this.Character.IsFemale;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0005D9B8 File Offset: 0x0005BBB8
		public FaceGenerationParams InitBodyGenerator(bool isDressed)
		{
			this.CurrentBodyProperties = this.Character.GetBodyProperties(this.Character.Equipment, -1);
			FaceGenerationParams faceGenerationParams = FaceGenerationParams.Create();
			faceGenerationParams.CurrentRace = this.Character.Race;
			faceGenerationParams.CurrentGender = (this.Character.IsFemale ? 1 : 0);
			faceGenerationParams.CurrentAge = this.Character.Age;
			MBBodyProperties.GetParamsFromKey(ref faceGenerationParams, this.CurrentBodyProperties, isDressed && this.Character.Equipment.EarsAreHidden, isDressed && this.Character.Equipment.MouthIsHidden);
			faceGenerationParams.SetRaceGenderAndAdjustParams(faceGenerationParams.CurrentRace, faceGenerationParams.CurrentGender, (int)faceGenerationParams.CurrentAge);
			return faceGenerationParams;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0005DA78 File Offset: 0x0005BC78
		public void RefreshFace(FaceGenerationParams faceGenerationParams, bool hasEquipment)
		{
			MBBodyProperties.ProduceNumericKeyWithParams(faceGenerationParams, hasEquipment && this.Character.Equipment.EarsAreHidden, hasEquipment && this.Character.Equipment.MouthIsHidden, ref this.CurrentBodyProperties);
			this.Race = faceGenerationParams.CurrentRace;
			this.IsFemale = (faceGenerationParams.CurrentGender == 1);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0005DAD8 File Offset: 0x0005BCD8
		public void SaveCurrentCharacter()
		{
			this.Character.UpdatePlayerCharacterBodyProperties(this.CurrentBodyProperties, this.Race, this.IsFemale);
		}

		// Token: 0x0400088D RID: 2189
		public const string FaceGenTeethAnimationName = "facegen_teeth";

		// Token: 0x0400088E RID: 2190
		public BodyProperties CurrentBodyProperties;

		// Token: 0x0400088F RID: 2191
		public BodyProperties BodyPropertiesMin;

		// Token: 0x04000890 RID: 2192
		public BodyProperties BodyPropertiesMax;

		// Token: 0x04000891 RID: 2193
		public int Race;

		// Token: 0x04000892 RID: 2194
		public bool IsFemale;
	}
}
