using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000377 RID: 887
	public static class SkinVoiceManager
	{
		// Token: 0x060030BF RID: 12479 RVA: 0x000C9FED File Offset: 0x000C81ED
		public static int GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassName(string className)
		{
			return MBAPI.IMBVoiceManager.GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassName(className);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000C9FFA File Offset: 0x000C81FA
		public static void GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassName(string className, int[] definitionIndices)
		{
			MBAPI.IMBVoiceManager.GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassName(className, definitionIndices);
		}

		// Token: 0x0200062A RID: 1578
		public enum CombatVoiceNetworkPredictionType
		{
			// Token: 0x04002006 RID: 8198
			Prediction,
			// Token: 0x04002007 RID: 8199
			OwnerPrediction,
			// Token: 0x04002008 RID: 8200
			NoPrediction
		}

		// Token: 0x0200062B RID: 1579
		public struct SkinVoiceType
		{
			// Token: 0x170009FA RID: 2554
			// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000EA0BF File Offset: 0x000E82BF
			// (set) Token: 0x06003C5B RID: 15451 RVA: 0x000EA0C7 File Offset: 0x000E82C7
			public string TypeID { get; private set; }

			// Token: 0x170009FB RID: 2555
			// (get) Token: 0x06003C5C RID: 15452 RVA: 0x000EA0D0 File Offset: 0x000E82D0
			// (set) Token: 0x06003C5D RID: 15453 RVA: 0x000EA0D8 File Offset: 0x000E82D8
			public int Index { get; private set; }

			// Token: 0x06003C5E RID: 15454 RVA: 0x000EA0E1 File Offset: 0x000E82E1
			public SkinVoiceType(string typeID)
			{
				this.TypeID = typeID;
				this.Index = MBAPI.IMBVoiceManager.GetVoiceTypeIndex(typeID);
			}

			// Token: 0x06003C5F RID: 15455 RVA: 0x000EA0FB File Offset: 0x000E82FB
			public TextObject GetName()
			{
				return GameTexts.FindText("str_taunt_name", this.TypeID);
			}
		}

		// Token: 0x0200062C RID: 1580
		public static class VoiceType
		{
			// Token: 0x0400200B RID: 8203
			public static readonly SkinVoiceManager.SkinVoiceType Grunt = new SkinVoiceManager.SkinVoiceType("Grunt");

			// Token: 0x0400200C RID: 8204
			public static readonly SkinVoiceManager.SkinVoiceType Jump = new SkinVoiceManager.SkinVoiceType("Jump");

			// Token: 0x0400200D RID: 8205
			public static readonly SkinVoiceManager.SkinVoiceType Yell = new SkinVoiceManager.SkinVoiceType("Yell");

			// Token: 0x0400200E RID: 8206
			public static readonly SkinVoiceManager.SkinVoiceType Pain = new SkinVoiceManager.SkinVoiceType("Pain");

			// Token: 0x0400200F RID: 8207
			public static readonly SkinVoiceManager.SkinVoiceType Death = new SkinVoiceManager.SkinVoiceType("Death");

			// Token: 0x04002010 RID: 8208
			public static readonly SkinVoiceManager.SkinVoiceType Stun = new SkinVoiceManager.SkinVoiceType("Stun");

			// Token: 0x04002011 RID: 8209
			public static readonly SkinVoiceManager.SkinVoiceType Fear = new SkinVoiceManager.SkinVoiceType("Fear");

			// Token: 0x04002012 RID: 8210
			public static readonly SkinVoiceManager.SkinVoiceType Climb = new SkinVoiceManager.SkinVoiceType("Climb");

			// Token: 0x04002013 RID: 8211
			public static readonly SkinVoiceManager.SkinVoiceType Focus = new SkinVoiceManager.SkinVoiceType("Focus");

			// Token: 0x04002014 RID: 8212
			public static readonly SkinVoiceManager.SkinVoiceType Debacle = new SkinVoiceManager.SkinVoiceType("Debacle");

			// Token: 0x04002015 RID: 8213
			public static readonly SkinVoiceManager.SkinVoiceType Victory = new SkinVoiceManager.SkinVoiceType("Victory");

			// Token: 0x04002016 RID: 8214
			public static readonly SkinVoiceManager.SkinVoiceType HorseStop = new SkinVoiceManager.SkinVoiceType("HorseStop");

			// Token: 0x04002017 RID: 8215
			public static readonly SkinVoiceManager.SkinVoiceType HorseRally = new SkinVoiceManager.SkinVoiceType("HorseRally");

			// Token: 0x04002018 RID: 8216
			public static readonly SkinVoiceManager.SkinVoiceType Infantry = new SkinVoiceManager.SkinVoiceType("Infantry");

			// Token: 0x04002019 RID: 8217
			public static readonly SkinVoiceManager.SkinVoiceType Cavalry = new SkinVoiceManager.SkinVoiceType("Cavalry");

			// Token: 0x0400201A RID: 8218
			public static readonly SkinVoiceManager.SkinVoiceType Archers = new SkinVoiceManager.SkinVoiceType("Archers");

			// Token: 0x0400201B RID: 8219
			public static readonly SkinVoiceManager.SkinVoiceType HorseArchers = new SkinVoiceManager.SkinVoiceType("HorseArchers");

			// Token: 0x0400201C RID: 8220
			public static readonly SkinVoiceManager.SkinVoiceType Everyone = new SkinVoiceManager.SkinVoiceType("Everyone");

			// Token: 0x0400201D RID: 8221
			public static readonly SkinVoiceManager.SkinVoiceType MixedFormation = new SkinVoiceManager.SkinVoiceType("Mixed");

			// Token: 0x0400201E RID: 8222
			public static readonly SkinVoiceManager.SkinVoiceType Move = new SkinVoiceManager.SkinVoiceType("Move");

			// Token: 0x0400201F RID: 8223
			public static readonly SkinVoiceManager.SkinVoiceType Follow = new SkinVoiceManager.SkinVoiceType("Follow");

			// Token: 0x04002020 RID: 8224
			public static readonly SkinVoiceManager.SkinVoiceType Charge = new SkinVoiceManager.SkinVoiceType("Charge");

			// Token: 0x04002021 RID: 8225
			public static readonly SkinVoiceManager.SkinVoiceType Advance = new SkinVoiceManager.SkinVoiceType("Advance");

			// Token: 0x04002022 RID: 8226
			public static readonly SkinVoiceManager.SkinVoiceType FallBack = new SkinVoiceManager.SkinVoiceType("FallBack");

			// Token: 0x04002023 RID: 8227
			public static readonly SkinVoiceManager.SkinVoiceType Stop = new SkinVoiceManager.SkinVoiceType("Stop");

			// Token: 0x04002024 RID: 8228
			public static readonly SkinVoiceManager.SkinVoiceType Retreat = new SkinVoiceManager.SkinVoiceType("Retreat");

			// Token: 0x04002025 RID: 8229
			public static readonly SkinVoiceManager.SkinVoiceType Mount = new SkinVoiceManager.SkinVoiceType("Mount");

			// Token: 0x04002026 RID: 8230
			public static readonly SkinVoiceManager.SkinVoiceType Dismount = new SkinVoiceManager.SkinVoiceType("Dismount");

			// Token: 0x04002027 RID: 8231
			public static readonly SkinVoiceManager.SkinVoiceType FireAtWill = new SkinVoiceManager.SkinVoiceType("FireAtWill");

			// Token: 0x04002028 RID: 8232
			public static readonly SkinVoiceManager.SkinVoiceType HoldFire = new SkinVoiceManager.SkinVoiceType("HoldFire");

			// Token: 0x04002029 RID: 8233
			public static readonly SkinVoiceManager.SkinVoiceType PickSpears = new SkinVoiceManager.SkinVoiceType("PickSpears");

			// Token: 0x0400202A RID: 8234
			public static readonly SkinVoiceManager.SkinVoiceType PickDefault = new SkinVoiceManager.SkinVoiceType("PickDefault");

			// Token: 0x0400202B RID: 8235
			public static readonly SkinVoiceManager.SkinVoiceType FaceEnemy = new SkinVoiceManager.SkinVoiceType("FaceEnemy");

			// Token: 0x0400202C RID: 8236
			public static readonly SkinVoiceManager.SkinVoiceType FaceDirection = new SkinVoiceManager.SkinVoiceType("FaceDirection");

			// Token: 0x0400202D RID: 8237
			public static readonly SkinVoiceManager.SkinVoiceType UseSiegeWeapon = new SkinVoiceManager.SkinVoiceType("UseSiegeWeapon");

			// Token: 0x0400202E RID: 8238
			public static readonly SkinVoiceManager.SkinVoiceType UseLadders = new SkinVoiceManager.SkinVoiceType("UseLadders");

			// Token: 0x0400202F RID: 8239
			public static readonly SkinVoiceManager.SkinVoiceType AttackGate = new SkinVoiceManager.SkinVoiceType("AttackGate");

			// Token: 0x04002030 RID: 8240
			public static readonly SkinVoiceManager.SkinVoiceType CommandDelegate = new SkinVoiceManager.SkinVoiceType("CommandDelegate");

			// Token: 0x04002031 RID: 8241
			public static readonly SkinVoiceManager.SkinVoiceType CommandUndelegate = new SkinVoiceManager.SkinVoiceType("CommandUndelegate");

			// Token: 0x04002032 RID: 8242
			public static readonly SkinVoiceManager.SkinVoiceType FormLine = new SkinVoiceManager.SkinVoiceType("FormLine");

			// Token: 0x04002033 RID: 8243
			public static readonly SkinVoiceManager.SkinVoiceType FormShieldWall = new SkinVoiceManager.SkinVoiceType("FormShieldWall");

			// Token: 0x04002034 RID: 8244
			public static readonly SkinVoiceManager.SkinVoiceType FormLoose = new SkinVoiceManager.SkinVoiceType("FormLoose");

			// Token: 0x04002035 RID: 8245
			public static readonly SkinVoiceManager.SkinVoiceType FormCircle = new SkinVoiceManager.SkinVoiceType("FormCircle");

			// Token: 0x04002036 RID: 8246
			public static readonly SkinVoiceManager.SkinVoiceType FormSquare = new SkinVoiceManager.SkinVoiceType("FormSquare");

			// Token: 0x04002037 RID: 8247
			public static readonly SkinVoiceManager.SkinVoiceType FormSkein = new SkinVoiceManager.SkinVoiceType("FormSkein");

			// Token: 0x04002038 RID: 8248
			public static readonly SkinVoiceManager.SkinVoiceType FormColumn = new SkinVoiceManager.SkinVoiceType("FormColumn");

			// Token: 0x04002039 RID: 8249
			public static readonly SkinVoiceManager.SkinVoiceType FormScatter = new SkinVoiceManager.SkinVoiceType("FormScatter");

			// Token: 0x0400203A RID: 8250
			public static readonly SkinVoiceManager.SkinVoiceType[] MpBarks = new SkinVoiceManager.SkinVoiceType[]
			{
				new SkinVoiceManager.SkinVoiceType("MpDefend"),
				new SkinVoiceManager.SkinVoiceType("MpAttack"),
				new SkinVoiceManager.SkinVoiceType("MpHelp"),
				new SkinVoiceManager.SkinVoiceType("MpSpot"),
				new SkinVoiceManager.SkinVoiceType("MpThanks"),
				new SkinVoiceManager.SkinVoiceType("MpSorry"),
				new SkinVoiceManager.SkinVoiceType("MpAffirmative"),
				new SkinVoiceManager.SkinVoiceType("MpNegative"),
				new SkinVoiceManager.SkinVoiceType("MpRegroup")
			};

			// Token: 0x0400203B RID: 8251
			public static readonly SkinVoiceManager.SkinVoiceType MpDefend = SkinVoiceManager.VoiceType.MpBarks[0];

			// Token: 0x0400203C RID: 8252
			public static readonly SkinVoiceManager.SkinVoiceType MpAttack = SkinVoiceManager.VoiceType.MpBarks[1];

			// Token: 0x0400203D RID: 8253
			public static readonly SkinVoiceManager.SkinVoiceType MpHelp = SkinVoiceManager.VoiceType.MpBarks[2];

			// Token: 0x0400203E RID: 8254
			public static readonly SkinVoiceManager.SkinVoiceType MpSpot = SkinVoiceManager.VoiceType.MpBarks[3];

			// Token: 0x0400203F RID: 8255
			public static readonly SkinVoiceManager.SkinVoiceType MpThanks = SkinVoiceManager.VoiceType.MpBarks[4];

			// Token: 0x04002040 RID: 8256
			public static readonly SkinVoiceManager.SkinVoiceType MpSorry = SkinVoiceManager.VoiceType.MpBarks[5];

			// Token: 0x04002041 RID: 8257
			public static readonly SkinVoiceManager.SkinVoiceType MpAffirmative = SkinVoiceManager.VoiceType.MpBarks[6];

			// Token: 0x04002042 RID: 8258
			public static readonly SkinVoiceManager.SkinVoiceType MpNegative = SkinVoiceManager.VoiceType.MpBarks[7];

			// Token: 0x04002043 RID: 8259
			public static readonly SkinVoiceManager.SkinVoiceType MpRegroup = SkinVoiceManager.VoiceType.MpBarks[8];

			// Token: 0x04002044 RID: 8260
			public static readonly SkinVoiceManager.SkinVoiceType Idle = new SkinVoiceManager.SkinVoiceType("Idle");

			// Token: 0x04002045 RID: 8261
			public static readonly SkinVoiceManager.SkinVoiceType Neigh = new SkinVoiceManager.SkinVoiceType("Neigh");

			// Token: 0x04002046 RID: 8262
			public static readonly SkinVoiceManager.SkinVoiceType Collide = new SkinVoiceManager.SkinVoiceType("Collide");
		}
	}
}
