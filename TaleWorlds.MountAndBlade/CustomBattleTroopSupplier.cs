using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000205 RID: 517
	public class CustomBattleTroopSupplier : IMissionTroopSupplier
	{
		// Token: 0x06001C8D RID: 7309 RVA: 0x0006389B File Offset: 0x00061A9B
		public CustomBattleTroopSupplier(CustomBattleCombatant customBattleCombatant, bool isPlayerSide, bool isPlayerGeneral, bool isSallyOut, Func<BasicCharacterObject, bool> customAllocationConditions = null)
		{
			this._customBattleCombatant = customBattleCombatant;
			this._customAllocationConditions = customAllocationConditions;
			this._isPlayerSide = isPlayerSide;
			this._isPlayerGeneral = (isPlayerSide && isPlayerGeneral);
			this._isSallyOut = isSallyOut;
			this.ArrangePriorities();
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000638D8 File Offset: 0x00061AD8
		private void ArrangePriorities()
		{
			this._characters = new PriorityQueue<float, BasicCharacterObject>(new GenericComparer<float>());
			int[] array = new int[8];
			int[] array2 = new int[8];
			int i;
			int j;
			for (i = 0; i < 8; i = j + 1)
			{
				array[i] = this._customBattleCombatant.Characters.Count((BasicCharacterObject character) => character.DefaultFormationClass == (FormationClass)i);
				j = i;
			}
			UnitSpawnPrioritizations unitSpawnPrioritization = this._isPlayerSide ? Game.Current.UnitSpawnPrioritization : UnitSpawnPrioritizations.HighLevel;
			int troopCountTotal = array.Sum();
			float num = 1000f;
			foreach (BasicCharacterObject basicCharacterObject in this._customBattleCombatant.Characters)
			{
				FormationClass formationClass = basicCharacterObject.GetFormationClass();
				float priority;
				if (this._isSallyOut)
				{
					priority = this.GetSallyOutAmbushProbabilityOfTroop(basicCharacterObject, troopCountTotal, ref num);
				}
				else
				{
					priority = this.GetDefaultProbabilityOfTroop(basicCharacterObject, troopCountTotal, unitSpawnPrioritization, ref num, ref array, ref array2);
				}
				array[(int)formationClass]--;
				array2[(int)formationClass]++;
				this._characters.Enqueue(priority, basicCharacterObject);
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00063A24 File Offset: 0x00061C24
		private float GetSallyOutAmbushProbabilityOfTroop(BasicCharacterObject character, int troopCountTotal, ref float heroProbability)
		{
			float num = 0f;
			if (character.IsHero)
			{
				float num2 = heroProbability;
				heroProbability = num2 - 1f;
				num = num2;
			}
			else
			{
				num += (float)character.Level;
				if (character.HasMount())
				{
					num += 100f;
				}
			}
			return num;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00063A6C File Offset: 0x00061C6C
		private float GetDefaultProbabilityOfTroop(BasicCharacterObject character, int troopCountTotal, UnitSpawnPrioritizations unitSpawnPrioritization, ref float heroProbability, ref int[] troopCountByFormationType, ref int[] enqueuedTroopCountByFormationType)
		{
			FormationClass formationClass = character.GetFormationClass();
			float num = (float)troopCountByFormationType[(int)formationClass] / (float)((unitSpawnPrioritization == UnitSpawnPrioritizations.Homogeneous) ? (enqueuedTroopCountByFormationType[(int)formationClass] + 1) : troopCountTotal);
			float num2;
			if (!character.IsHero)
			{
				num2 = num;
			}
			else
			{
				float num3 = heroProbability;
				heroProbability = num3 - 1f;
				num2 = num3;
			}
			float num4 = num2;
			if (!character.IsHero && (unitSpawnPrioritization == UnitSpawnPrioritizations.HighLevel || unitSpawnPrioritization == UnitSpawnPrioritizations.LowLevel))
			{
				num4 += (float)character.Level;
				if (unitSpawnPrioritization == UnitSpawnPrioritizations.LowLevel)
				{
					num4 *= -1f;
				}
			}
			return num4;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00063ADC File Offset: 0x00061CDC
		public IEnumerable<IAgentOriginBase> SupplyTroops(int numberToAllocate)
		{
			List<BasicCharacterObject> list = this.AllocateTroops(numberToAllocate);
			CustomBattleAgentOrigin[] array = new CustomBattleAgentOrigin[list.Count];
			this._numAllocated += list.Count;
			for (int i = 0; i < array.Length; i++)
			{
				UniqueTroopDescriptor uniqueNo = new UniqueTroopDescriptor(Game.Current.NextUniqueTroopSeed);
				array[i] = new CustomBattleAgentOrigin(this._customBattleCombatant, list[i], this, this._isPlayerSide, i, uniqueNo);
			}
			if (array.Length < numberToAllocate)
			{
				this._anyTroopRemainsToBeSupplied = false;
			}
			return array;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00063B5C File Offset: 0x00061D5C
		public IEnumerable<IAgentOriginBase> GetAllTroops()
		{
			CustomBattleAgentOrigin[] array = new CustomBattleAgentOrigin[this._customBattleCombatant.Characters.Count<BasicCharacterObject>()];
			int num = 0;
			foreach (BasicCharacterObject characterObject in this._customBattleCombatant.Characters)
			{
				array[num] = new CustomBattleAgentOrigin(this._customBattleCombatant, characterObject, this, this._isPlayerSide, -1, default(UniqueTroopDescriptor));
				num++;
			}
			return array;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00063BE8 File Offset: 0x00061DE8
		public BasicCharacterObject GetGeneralCharacter()
		{
			return this._customBattleCombatant.General;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00063BF8 File Offset: 0x00061DF8
		private List<BasicCharacterObject> AllocateTroops(int numberToAllocate)
		{
			if (numberToAllocate > this._characters.Count)
			{
				numberToAllocate = this._characters.Count;
			}
			List<BasicCharacterObject> list = new List<BasicCharacterObject>();
			while (numberToAllocate > 0 && this._characters.Count > 0)
			{
				BasicCharacterObject basicCharacterObject = this._characters.DequeueValue();
				if (this._customAllocationConditions == null || this._customAllocationConditions(basicCharacterObject))
				{
					list.Add(basicCharacterObject);
					numberToAllocate--;
				}
			}
			return list;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00063C69 File Offset: 0x00061E69
		public void OnTroopWounded()
		{
			this._numWounded++;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00063C79 File Offset: 0x00061E79
		public void OnTroopKilled()
		{
			this._numKilled++;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00063C89 File Offset: 0x00061E89
		public void OnTroopRouted()
		{
			this._numRouted++;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x00063C99 File Offset: 0x00061E99
		public int NumRemovedTroops
		{
			get
			{
				return this._numWounded + this._numKilled + this._numRouted;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00063CAF File Offset: 0x00061EAF
		public int NumTroopsNotSupplied
		{
			get
			{
				return this._characters.Count - this._numAllocated;
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00063CC3 File Offset: 0x00061EC3
		public int GetNumberOfPlayerControllableTroops()
		{
			return this._numAllocated;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00063CCB File Offset: 0x00061ECB
		public bool AnyTroopRemainsToBeSupplied
		{
			get
			{
				return this._anyTroopRemainsToBeSupplied;
			}
		}

		// Token: 0x04000925 RID: 2341
		private readonly CustomBattleCombatant _customBattleCombatant;

		// Token: 0x04000926 RID: 2342
		private PriorityQueue<float, BasicCharacterObject> _characters;

		// Token: 0x04000927 RID: 2343
		private int _numAllocated;

		// Token: 0x04000928 RID: 2344
		private int _numWounded;

		// Token: 0x04000929 RID: 2345
		private int _numKilled;

		// Token: 0x0400092A RID: 2346
		private int _numRouted;

		// Token: 0x0400092B RID: 2347
		private Func<BasicCharacterObject, bool> _customAllocationConditions;

		// Token: 0x0400092C RID: 2348
		private bool _anyTroopRemainsToBeSupplied = true;

		// Token: 0x0400092D RID: 2349
		private readonly bool _isPlayerSide;

		// Token: 0x0400092E RID: 2350
		private readonly bool _isPlayerGeneral;

		// Token: 0x0400092F RID: 2351
		private readonly bool _isSallyOut;
	}
}
