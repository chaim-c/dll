using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003D0 RID: 976
	public class RetrainOutlawPartyMembersBehavior : CampaignBehaviorBase, IRetrainOutlawPartyMembersCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x06003C0D RID: 15373 RVA: 0x0012039C File Offset: 0x0011E59C
		private int GetRetrainedNumberInternal(CharacterObject character)
		{
			int result;
			if (!this._retrainTable.TryGetValue(character, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x001203BC File Offset: 0x0011E5BC
		private int SetRetrainedNumberInternal(CharacterObject character, int numberRetrained)
		{
			this._retrainTable[character] = numberRetrained;
			return numberRetrained;
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x001203D9 File Offset: 0x0011E5D9
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.DailyTick));
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x001203F4 File Offset: 0x0011E5F4
		private void DailyTick()
		{
			if (MBRandom.RandomFloat > 0.5f)
			{
				int num = MBRandom.RandomInt(MobileParty.MainParty.MemberRoster.Count);
				bool flag = false;
				int num2 = 0;
				while (num2 < MobileParty.MainParty.MemberRoster.Count && !flag)
				{
					int index = (num2 + num) % MobileParty.MainParty.MemberRoster.Count;
					CharacterObject characterAtIndex = MobileParty.MainParty.MemberRoster.GetCharacterAtIndex(index);
					if (characterAtIndex.Occupation == Occupation.Bandit)
					{
						int elementNumber = MobileParty.MainParty.MemberRoster.GetElementNumber(index);
						int num3 = this.GetRetrainedNumberInternal(characterAtIndex);
						if (num3 < elementNumber && !flag)
						{
							num3++;
							this.SetRetrainedNumberInternal(characterAtIndex, num3);
						}
						else if (num3 > elementNumber)
						{
							this.SetRetrainedNumberInternal(characterAtIndex, elementNumber);
						}
					}
					num2++;
				}
			}
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x001204C1 File Offset: 0x0011E6C1
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<CharacterObject, int>>("_retrainTable", ref this._retrainTable);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x001204D8 File Offset: 0x0011E6D8
		public int GetRetrainedNumber(CharacterObject character)
		{
			if (character.Occupation == Occupation.Bandit)
			{
				int retrainedNumberInternal = this.GetRetrainedNumberInternal(character);
				int troopCount = MobileParty.MainParty.MemberRoster.GetTroopCount(character);
				return MathF.Min(retrainedNumberInternal, troopCount);
			}
			return 0;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x0012050F File Offset: 0x0011E70F
		public void SetRetrainedNumber(CharacterObject character, int number)
		{
			this.SetRetrainedNumberInternal(character, number);
		}

		// Token: 0x040011F7 RID: 4599
		private Dictionary<CharacterObject, int> _retrainTable = new Dictionary<CharacterObject, int>();
	}
}
