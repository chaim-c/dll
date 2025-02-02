using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E8 RID: 488
	public static class CombatLogManager
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001B18 RID: 6936 RVA: 0x0005DE90 File Offset: 0x0005C090
		// (remove) Token: 0x06001B19 RID: 6937 RVA: 0x0005DEC4 File Offset: 0x0005C0C4
		public static event CombatLogManager.OnPrintCombatLogHandler OnGenerateCombatLog;

		// Token: 0x06001B1A RID: 6938 RVA: 0x0005DEF8 File Offset: 0x0005C0F8
		public static void PrintDebugLogForInfo(Agent attackerAgent, Agent victimAgent, DamageTypes damageType, int speedBonus, int armorAmount, int inflictedDamage, int absorbedByArmor, sbyte collisionBone, float lostHpPercentage)
		{
			TextObject message = TextObject.Empty;
			CombatLogColor logColor = CombatLogColor.White;
			bool isMine = attackerAgent.IsMine;
			bool isMine2 = victimAgent.IsMine;
			GameTexts.SetVariable("AMOUNT", inflictedDamage);
			GameTexts.SetVariable("DAMAGE_TYPE", damageType.ToString().ToLower());
			GameTexts.SetVariable("LOST_HP_PERCENTAGE", lostHpPercentage);
			if (isMine2)
			{
				GameTexts.SetVariable("ATTACKER_NAME", attackerAgent.Name);
				message = GameTexts.FindText("combat_log_player_attacked", null);
				logColor = CombatLogColor.Red;
			}
			else if (isMine)
			{
				GameTexts.SetVariable("VICTIM_NAME", victimAgent.Name);
				message = GameTexts.FindText("combat_log_player_attacker", null);
				logColor = CombatLogColor.Green;
			}
			CombatLogManager.Print(message, logColor);
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "PrintDebugLogForInfo");
			if (armorAmount > 0)
			{
				GameTexts.SetVariable("ABSORBED_AMOUNT", absorbedByArmor);
				GameTexts.SetVariable("ARMOR_AMOUNT", armorAmount);
				mbstringBuilder.AppendLine<string>(GameTexts.FindText("combat_log_damage_absorbed", null).ToString());
			}
			if (victimAgent.IsHuman)
			{
				GameTexts.SetVariable("BONE", collisionBone.ToString());
				mbstringBuilder.AppendLine<string>(GameTexts.FindText("combat_log_hit_bone", null).ToString());
			}
			if (speedBonus != 0)
			{
				GameTexts.SetVariable("SPEED_BONUS", speedBonus);
				mbstringBuilder.AppendLine<string>(GameTexts.FindText("combat_log_speed_bonus", null).ToString());
			}
			CombatLogManager.Print(new TextObject(mbstringBuilder.ToStringAndRelease(), null), CombatLogColor.White);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0005E050 File Offset: 0x0005C250
		private static void Print(TextObject message, CombatLogColor logColor = CombatLogColor.White)
		{
			Debug.Print(message.ToString(), 0, (Debug.DebugColor)logColor, 562949953421312UL);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0005E078 File Offset: 0x0005C278
		public static void GenerateCombatLog(CombatLogData logData)
		{
			CombatLogManager.OnPrintCombatLogHandler onGenerateCombatLog = CombatLogManager.OnGenerateCombatLog;
			if (onGenerateCombatLog != null)
			{
				onGenerateCombatLog(logData);
			}
			foreach (ValueTuple<string, uint> valueTuple in logData.GetLogString())
			{
				InformationManager.DisplayMessage(new InformationMessage(valueTuple.Item1, Color.FromUint(valueTuple.Item2), "Combat"));
			}
		}

		// Token: 0x020004E3 RID: 1251
		// (Invoke) Token: 0x0600379F RID: 14239
		public delegate void OnPrintCombatLogHandler(CombatLogData logData);
	}
}
