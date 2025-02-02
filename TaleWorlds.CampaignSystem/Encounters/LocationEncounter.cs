﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Encounters
{
	// Token: 0x02000293 RID: 659
	public class LocationEncounter
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0009AB25 File Offset: 0x00098D25
		public Settlement Settlement { get; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x0009AB2D File Offset: 0x00098D2D
		// (set) Token: 0x0600245C RID: 9308 RVA: 0x0009AB35 File Offset: 0x00098D35
		public List<AccompanyingCharacter> CharactersAccompanyingPlayer { get; private set; }

		// Token: 0x0600245D RID: 9309 RVA: 0x0009AB3E File Offset: 0x00098D3E
		protected LocationEncounter(Settlement settlement)
		{
			this.Settlement = settlement;
			this.CharactersAccompanyingPlayer = new List<AccompanyingCharacter>();
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0009AB58 File Offset: 0x00098D58
		public void AddAccompanyingCharacter(LocationCharacter locationCharacter, bool isFollowing = false)
		{
			if (!this.CharactersAccompanyingPlayer.Any((AccompanyingCharacter x) => x.LocationCharacter.Character == locationCharacter.Character))
			{
				AccompanyingCharacter item = new AccompanyingCharacter(locationCharacter, isFollowing);
				this.CharactersAccompanyingPlayer.Add(item);
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0009ABA4 File Offset: 0x00098DA4
		public AccompanyingCharacter GetAccompanyingCharacter(LocationCharacter locationCharacter)
		{
			return this.CharactersAccompanyingPlayer.Find((AccompanyingCharacter x) => x.LocationCharacter == locationCharacter);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0009ABD8 File Offset: 0x00098DD8
		public AccompanyingCharacter GetAccompanyingCharacter(CharacterObject character)
		{
			return this.CharactersAccompanyingPlayer.Find(delegate(AccompanyingCharacter x)
			{
				LocationCharacter locationCharacter = x.LocationCharacter;
				return ((locationCharacter != null) ? locationCharacter.Character : null) == character;
			});
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0009AC0C File Offset: 0x00098E0C
		public void RemoveAccompanyingCharacter(LocationCharacter locationCharacter)
		{
			if (this.CharactersAccompanyingPlayer.Any((AccompanyingCharacter x) => x.LocationCharacter == locationCharacter))
			{
				AccompanyingCharacter item = this.CharactersAccompanyingPlayer.Find((AccompanyingCharacter x) => x.LocationCharacter == locationCharacter);
				this.CharactersAccompanyingPlayer.Remove(item);
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0009AC64 File Offset: 0x00098E64
		public void RemoveAccompanyingCharacter(Hero hero)
		{
			for (int i = this.CharactersAccompanyingPlayer.Count - 1; i >= 0; i--)
			{
				if (this.CharactersAccompanyingPlayer[i].LocationCharacter.Character.IsHero && this.CharactersAccompanyingPlayer[i].LocationCharacter.Character.HeroObject == hero)
				{
					this.CharactersAccompanyingPlayer.Remove(this.CharactersAccompanyingPlayer[i]);
					return;
				}
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0009ACDD File Offset: 0x00098EDD
		public void RemoveAllAccompanyingCharacters()
		{
			this.CharactersAccompanyingPlayer.Clear();
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0009ACEA File Offset: 0x00098EEA
		public void OnCharacterLocationChanged(LocationCharacter locationCharacter, Location fromLocation, Location toLocation)
		{
			if ((fromLocation == CampaignMission.Current.Location && toLocation == null) || (fromLocation == null && toLocation == CampaignMission.Current.Location))
			{
				CampaignMission.Current.OnCharacterLocationChanged(locationCharacter, fromLocation, toLocation);
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0009AD19 File Offset: 0x00098F19
		public virtual bool IsWorkshopLocation(Location location)
		{
			return false;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0009AD1C File Offset: 0x00098F1C
		public virtual bool IsTavern(Location location)
		{
			return false;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0009AD1F File Offset: 0x00098F1F
		public virtual IMission CreateAndOpenMissionController(Location nextLocation, Location previousLocation = null, CharacterObject talkToChar = null, string playerSpecialSpawnTag = null)
		{
			return null;
		}

		// Token: 0x04000AFD RID: 2813
		public bool IsInsideOfASettlement;
	}
}
