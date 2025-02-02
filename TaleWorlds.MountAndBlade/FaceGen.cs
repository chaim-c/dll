using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000216 RID: 534
	public class FaceGen : IFaceGen
	{
		// Token: 0x06001D58 RID: 7512 RVA: 0x00066E88 File Offset: 0x00065088
		private FaceGen()
		{
			this._raceNamesDictionary = new Dictionary<string, int>();
			this._raceNamesArray = MBAPI.IMBFaceGen.GetRaceIds().Split(new char[]
			{
				';'
			});
			for (int i = 0; i < this._raceNamesArray.Length; i++)
			{
				this._raceNamesDictionary[this._raceNamesArray[i]] = i;
			}
			this._monstersDictionary = new Dictionary<string, Monster>();
			this._monstersArray = new Monster[this._raceNamesArray.Length];
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00066F0B File Offset: 0x0006510B
		public static void CreateInstance()
		{
			FaceGen.SetInstance(new FaceGen());
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00066F18 File Offset: 0x00065118
		public Monster GetMonster(string monsterID)
		{
			Monster @object;
			if (!this._monstersDictionary.TryGetValue(monsterID, out @object))
			{
				@object = Game.Current.ObjectManager.GetObject<Monster>(monsterID);
				this._monstersDictionary[monsterID] = @object;
			}
			return @object;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00066F54 File Offset: 0x00065154
		public Monster GetMonsterWithSuffix(int race, string suffix)
		{
			return this.GetMonster(this._raceNamesArray[race] + suffix);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00066F6C File Offset: 0x0006516C
		public Monster GetBaseMonsterFromRace(int race)
		{
			if (race >= 0 && race < this._monstersArray.Length)
			{
				Monster monster = this._monstersArray[race];
				if (monster == null)
				{
					monster = Game.Current.ObjectManager.GetObject<Monster>(this._raceNamesArray[race]);
					this._monstersArray[race] = monster;
				}
				return monster;
			}
			Debug.FailedAssert("Monster race index is out of bounds: " + race, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\FaceGen.cs", "GetBaseMonsterFromRace", 64);
			return null;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00066FDC File Offset: 0x000651DC
		public BodyProperties GetRandomBodyProperties(int race, bool isFemale, BodyProperties bodyPropertiesMin, BodyProperties bodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tattooTags)
		{
			return MBBodyProperties.GetRandomBodyProperties(race, isFemale, bodyPropertiesMin, bodyPropertiesMax, hairCoverType, seed, hairTags, beardTags, tattooTags);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x00066FFD File Offset: 0x000651FD
		void IFaceGen.GenerateParentBody(BodyProperties childBodyProperties, int race, ref BodyProperties motherBodyProperties, ref BodyProperties fatherBodyProperties)
		{
			MBBodyProperties.GenerateParentKey(childBodyProperties, race, ref motherBodyProperties, ref fatherBodyProperties);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x00067009 File Offset: 0x00065209
		void IFaceGen.SetHair(ref BodyProperties bodyProperties, int hair, int beard, int tattoo)
		{
			MBBodyProperties.SetHair(ref bodyProperties, hair, beard, tattoo);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00067015 File Offset: 0x00065215
		void IFaceGen.SetBody(ref BodyProperties bodyProperties, int build, int weight)
		{
			MBBodyProperties.SetBody(ref bodyProperties, build, weight);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0006701F File Offset: 0x0006521F
		void IFaceGen.SetPigmentation(ref BodyProperties bodyProperties, int skinColor, int hairColor, int eyeColor)
		{
			MBBodyProperties.SetPigmentation(ref bodyProperties, skinColor, hairColor, eyeColor);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0006702B File Offset: 0x0006522B
		public BodyProperties GetBodyPropertiesWithAge(ref BodyProperties bodyProperties, float age)
		{
			return MBBodyProperties.GetBodyPropertiesWithAge(ref bodyProperties, age);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00067034 File Offset: 0x00065234
		public void GetParamsFromBody(ref FaceGenerationParams faceGenerationParams, BodyProperties bodyProperties, bool earsAreHidden, bool mouthIsHidden)
		{
			MBBodyProperties.GetParamsFromKey(ref faceGenerationParams, bodyProperties, earsAreHidden, mouthIsHidden);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00067040 File Offset: 0x00065240
		public BodyMeshMaturityType GetMaturityTypeWithAge(float age)
		{
			return MBBodyProperties.GetMaturityType(age);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00067048 File Offset: 0x00065248
		public int GetRaceCount()
		{
			return this._raceNamesArray.Length;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00067052 File Offset: 0x00065252
		public int GetRaceOrDefault(string raceId)
		{
			return this._raceNamesDictionary[raceId];
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00067060 File Offset: 0x00065260
		public string GetBaseMonsterNameFromRace(int race)
		{
			return this._raceNamesArray[race];
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0006706A File Offset: 0x0006526A
		public string[] GetRaceNames()
		{
			return (string[])this._raceNamesArray.Clone();
		}

		// Token: 0x04000978 RID: 2424
		private readonly Dictionary<string, int> _raceNamesDictionary;

		// Token: 0x04000979 RID: 2425
		private readonly string[] _raceNamesArray;

		// Token: 0x0400097A RID: 2426
		private readonly Dictionary<string, Monster> _monstersDictionary;

		// Token: 0x0400097B RID: 2427
		private readonly Monster[] _monstersArray;
	}
}
