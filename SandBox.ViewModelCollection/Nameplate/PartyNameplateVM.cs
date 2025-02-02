using System;
using SandBox.ViewModelCollection.Missions.NameMarker;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x02000016 RID: 22
	public class PartyNameplateVM : NameplateVM
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00009F65 File Offset: 0x00008165
		public bool GetIsMainParty
		{
			get
			{
				return this._isMainPartyBind || this.IsMainParty;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009F77 File Offset: 0x00008177
		public MobileParty Party { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009F7F File Offset: 0x0000817F
		private IFaction _mainFaction
		{
			get
			{
				return Hero.MainHero.MapFaction;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009F8C File Offset: 0x0000818C
		public PartyNameplateVM(MobileParty party, Camera mapCamera, Action resetCamera, Func<bool> getShouldShowFullName)
		{
			this._resetCamera = resetCamera;
			this._mapCamera = mapCamera;
			this._getShouldShowFullName = getShouldShowFullName;
			this.Party = party;
			this._isMainPartyBind = party.IsMainParty;
			this.MainHeroVisual = (this._isMainPartyBind ? new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(Hero.MainHero.CharacterObject, false)) : null);
			this._isPartyHeroVisualDirty = true;
			this._isPartyBannerDirty = true;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this.RegisterEvents();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A02D File Offset: 0x0000822D
		public override void OnFinalize()
		{
			this.UnregisterEvents();
			base.OnFinalize();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A03B File Offset: 0x0000823B
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.RefreshDynamicProperties(true);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A04C File Offset: 0x0000824C
		private void AddQuestBindFlagsForParty(MobileParty party)
		{
			if (party != MobileParty.MainParty && party != this.Party)
			{
				Hero leaderHero = party.LeaderHero;
				if (((leaderHero != null) ? leaderHero.Issue : null) != null && (this._questsBind & SandBoxUIHelper.IssueQuestFlags.TrackedIssue) == SandBoxUIHelper.IssueQuestFlags.None && ((this._questsBind & SandBoxUIHelper.IssueQuestFlags.AvailableIssue) == SandBoxUIHelper.IssueQuestFlags.None || (this._questsBind & SandBoxUIHelper.IssueQuestFlags.ActiveIssue) == SandBoxUIHelper.IssueQuestFlags.None))
				{
					this._questsBind |= SandBoxUIHelper.GetIssueType(party.LeaderHero.Issue);
				}
				if (((this._questsBind & SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest) == SandBoxUIHelper.IssueQuestFlags.None && (this._questsBind & SandBoxUIHelper.IssueQuestFlags.ActiveIssue) == SandBoxUIHelper.IssueQuestFlags.None) || (this._questsBind & SandBoxUIHelper.IssueQuestFlags.ActiveStoryQuest) == SandBoxUIHelper.IssueQuestFlags.None)
				{
					foreach (QuestBase questBase in SandBoxUIHelper.GetQuestsRelatedToParty(party))
					{
						if (party.LeaderHero != null && questBase.QuestGiver == party.LeaderHero)
						{
							if (questBase.IsSpecialQuest && (this._questsBind & SandBoxUIHelper.IssueQuestFlags.ActiveStoryQuest) == SandBoxUIHelper.IssueQuestFlags.None)
							{
								this._questsBind |= SandBoxUIHelper.IssueQuestFlags.ActiveStoryQuest;
							}
							else if (!questBase.IsSpecialQuest && (this._questsBind & SandBoxUIHelper.IssueQuestFlags.ActiveIssue) == SandBoxUIHelper.IssueQuestFlags.None)
							{
								this._questsBind |= SandBoxUIHelper.IssueQuestFlags.ActiveIssue;
							}
						}
						else if (questBase.IsSpecialQuest && (this._questsBind & SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest) == SandBoxUIHelper.IssueQuestFlags.None)
						{
							this._questsBind |= SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest;
						}
						else if (!questBase.IsSpecialQuest && (this._questsBind & SandBoxUIHelper.IssueQuestFlags.TrackedIssue) == SandBoxUIHelper.IssueQuestFlags.None)
						{
							this._questsBind |= SandBoxUIHelper.IssueQuestFlags.TrackedIssue;
						}
					}
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A1C4 File Offset: 0x000083C4
		public override void RefreshDynamicProperties(bool forceUpdate)
		{
			base.RefreshDynamicProperties(forceUpdate);
			if ((this.IsMainParty && MathF.Abs(Hero.MainHero.Age - this._latestMainHeroAge) >= 1f) || forceUpdate)
			{
				this._latestMainHeroAge = Hero.MainHero.Age;
				this._isPartyHeroVisualDirty = true;
			}
			if (this._isPartyHeroVisualDirty || forceUpdate)
			{
				this._mainHeroVisualBind = (this._isMainPartyBind ? new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(Hero.MainHero.CharacterObject, false)) : null);
				this._isPartyHeroVisualDirty = false;
			}
			if (this._isVisibleOnMapBind || forceUpdate)
			{
				MobileParty party = this.Party;
				IssueBase issueBase;
				if (party == null)
				{
					issueBase = null;
				}
				else
				{
					Hero leaderHero = party.LeaderHero;
					issueBase = ((leaderHero != null) ? leaderHero.Issue : null);
				}
				IssueBase issueBase2 = issueBase;
				this._questsBind = SandBoxUIHelper.IssueQuestFlags.None;
				if (this.Party != MobileParty.MainParty)
				{
					if (issueBase2 != null)
					{
						this._questsBind |= SandBoxUIHelper.GetIssueType(issueBase2);
					}
					foreach (QuestBase questBase in SandBoxUIHelper.GetQuestsRelatedToParty(this.Party))
					{
						if (questBase.QuestGiver != null && questBase.QuestGiver == this.Party.LeaderHero)
						{
							this._questsBind |= (questBase.IsSpecialQuest ? SandBoxUIHelper.IssueQuestFlags.ActiveStoryQuest : SandBoxUIHelper.IssueQuestFlags.ActiveIssue);
						}
						else
						{
							this._questsBind |= (questBase.IsSpecialQuest ? SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest : SandBoxUIHelper.IssueQuestFlags.TrackedIssue);
						}
					}
				}
			}
			this._isInArmyBind = (this.Party.Army != null && this.Party.AttachedTo != null);
			this._isArmyBind = (this.Party.Army != null && this.Party.Army.LeaderParty == this.Party);
			MobileParty party2 = this.Party;
			this._isInSettlementBind = (((party2 != null) ? party2.CurrentSettlement : null) != null);
			if (this._isArmyBind && (this._isVisibleOnMapBind || forceUpdate))
			{
				this.AddQuestBindFlagsForParty(this.Party.Army.LeaderParty);
				foreach (MobileParty party3 in this.Party.Army.LeaderParty.AttachedParties)
				{
					this.AddQuestBindFlagsForParty(party3);
				}
			}
			if (this._isArmyBind || !this._isInArmy || forceUpdate)
			{
				int partyHealthyCount = SandBoxUIHelper.GetPartyHealthyCount(this.Party);
				if (partyHealthyCount != this._latestTotalCount)
				{
					this._latestTotalCount = partyHealthyCount;
					this._countBind = partyHealthyCount.ToString();
				}
				int allWoundedMembersAmount = SandBoxUIHelper.GetAllWoundedMembersAmount(this.Party);
				int allPrisonerMembersAmount = SandBoxUIHelper.GetAllPrisonerMembersAmount(this.Party);
				if (this._latestWoundedAmount != allWoundedMembersAmount || this._latestPrisonerAmount != allPrisonerMembersAmount)
				{
					if (this._latestWoundedAmount != allWoundedMembersAmount)
					{
						this._woundedBind = ((allWoundedMembersAmount == 0) ? "" : SandBoxUIHelper.GetPartyWoundedText(allWoundedMembersAmount));
						this._latestWoundedAmount = allWoundedMembersAmount;
					}
					if (this._latestPrisonerAmount != allPrisonerMembersAmount)
					{
						this._prisonerBind = ((allPrisonerMembersAmount == 0) ? "" : SandBoxUIHelper.GetPartyPrisonerText(allPrisonerMembersAmount));
						this._latestPrisonerAmount = allPrisonerMembersAmount;
					}
					this._extraInfoTextBind = this._woundedBind + this._prisonerBind;
				}
			}
			if (!this.Party.IsMainParty)
			{
				Army army = this.Party.Army;
				if (army == null || !army.LeaderParty.AttachedParties.Contains(MobileParty.MainParty) || !this.Party.Army.LeaderParty.AttachedParties.Contains(this.Party))
				{
					if (FactionManager.IsAtWarAgainstFaction(this.Party.MapFaction, this._mainFaction))
					{
						this._factionColorBind = ((this.Party.Army != null && this.Party.Army.LeaderParty == this.Party) ? PartyNameplateVM.NegativeArmyIndicator : PartyNameplateVM.NegativeIndicator);
						goto IL_491;
					}
					if (FactionManager.IsAlliedWithFaction(this.Party.MapFaction, Hero.MainHero.MapFaction))
					{
						this._factionColorBind = ((this.Party.Army != null && this.Party.Army.LeaderParty == this.Party) ? PartyNameplateVM.PositiveArmyIndicator : PartyNameplateVM.PositiveIndicator);
						goto IL_491;
					}
					this._factionColorBind = ((this.Party.Army != null && this.Party.Army.LeaderParty == this.Party) ? PartyNameplateVM.NeutralArmyIndicator : PartyNameplateVM.NeutralIndicator);
					goto IL_491;
				}
			}
			this._factionColorBind = ((this.Party.Army != null && this.Party.Army.LeaderParty == this.Party) ? PartyNameplateVM.MainPartyArmyIndicator : PartyNameplateVM.MainPartyIndicator);
			IL_491:
			if (this._isPartyBannerDirty || forceUpdate)
			{
				if (this.Party.Party.Banner != null)
				{
					this.PartyBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.Party.Party.Banner), true);
				}
				else
				{
					IFaction mapFaction = this.Party.MapFaction;
					this.PartyBanner = new ImageIdentifierVM(BannerCode.CreateFrom((mapFaction != null) ? mapFaction.Banner : null), true);
				}
				this._isPartyBannerDirty = false;
			}
			if (this._isVisibleOnMapBind && (this._isInArmyBind || this._isInSettlementBind))
			{
				this._isVisibleOnMapBind = false;
			}
			Army army2 = this.Party.Army;
			TextObject textObject;
			if (army2 != null && army2.DoesLeaderPartyAndAttachedPartiesContain(this.Party))
			{
				textObject = this.Party.ArmyName;
			}
			else if (this.Party.LeaderHero != null)
			{
				textObject = this.Party.LeaderHero.Name;
			}
			else
			{
				textObject = this.Party.Name;
			}
			bool isPrisonerBind;
			if (this.IsMainParty && this.Party.LeaderHero == null)
			{
				Hero mainHero = Hero.MainHero;
				isPrisonerBind = (mainHero != null && mainHero.IsAlive);
			}
			else
			{
				isPrisonerBind = false;
			}
			this._isPrisonerBind = isPrisonerBind;
			this._isDisorganizedBind = this.Party.IsDisorganized;
			this._shouldShowFullNameBind = (this._getShouldShowFullName != null && this._getShouldShowFullName());
			if (this._latestNameTextObject != textObject || forceUpdate)
			{
				this._latestNameTextObject = textObject;
				this._fullNameBind = this._latestNameTextObject.ToString();
			}
			if (!this._cachedSpeed.ApproximatelyEqualsTo(this.Party.Speed, 0.01f))
			{
				this._cachedSpeed = this.Party.Speed;
				this._movementSpeedTextBind = this._cachedSpeed.ToString("F1");
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A82C File Offset: 0x00008A2C
		public override void RefreshPosition()
		{
			base.RefreshPosition();
			float z = 0f;
			Campaign.Current.MapSceneWrapper.GetHeightAtPoint(this.Party.VisualPosition2DWithoutError, ref z);
			Vec3 vec = this.Party.VisualPosition2DWithoutError.ToVec3(z);
			Vec3 worldSpacePosition = vec + new Vec3(0f, 0f, 0.8f, -1f);
			if (this._isMainPartyBind)
			{
				this.RefreshMainPartyScreenPosition(vec);
				MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, worldSpacePosition, ref this._latestX, ref this._latestY, ref this._latestW);
				this._headPositionBind = new Vec2(this._latestX, this._latestY);
			}
			else
			{
				this._latestX = 0f;
				this._latestY = 0f;
				this._latestW = 0f;
				MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, vec, ref this._latestX, ref this._latestY, ref this._latestW);
				this._partyPositionBind = new Vec2(this._latestX, this._latestY);
				MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, worldSpacePosition, ref this._latestX, ref this._latestY, ref this._latestW);
				this._headPositionBind = new Vec2(this._latestX, this._latestY);
			}
			base.DistanceToCamera = vec.Distance(this._mapCamera.Position);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A988 File Offset: 0x00008B88
		public override void RefreshTutorialStatus(string newTutorialHighlightElementID)
		{
			base.RefreshTutorialStatus(newTutorialHighlightElementID);
			MobileParty party = this.Party;
			bool flag;
			if (party == null)
			{
				flag = (null != null);
			}
			else
			{
				PartyBase party2 = party.Party;
				flag = (((party2 != null) ? party2.Id : null) != null);
			}
			if (!flag)
			{
				Debug.FailedAssert("Mobile party id is null when refreshing tutorial status", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Nameplate\\PartyNameplateVM.cs", "RefreshTutorialStatus", 344);
				return;
			}
			this._bindIsTargetedByTutorial = (this.Party.Party.Id == newTutorialHighlightElementID);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		private void RefreshMainPartyScreenPosition(Vec3 partyWorldPosition)
		{
			this._latestX = 0f;
			this._latestY = 0f;
			this._latestW = 0f;
			MatrixFrame identity = MatrixFrame.Identity;
			this._mapCamera.GetViewProjMatrix(ref identity);
			Vec3 v = partyWorldPosition;
			v.w = 1f;
			Vec3 vec = v * identity;
			this._isBehindBind = (vec.w < 0f);
			vec.w = MathF.Abs(vec.w);
			vec.x /= vec.w;
			vec.y /= vec.w;
			vec.z /= vec.w;
			vec.w /= vec.w;
			vec *= 0.5f;
			vec.x += 0.5f;
			vec.y += 0.5f;
			vec.y = 1f - vec.y;
			if (this._isBehindBind)
			{
				vec.y = 1f;
			}
			int num = (int)(Screen.RealScreenResolutionWidth * ScreenManager.UsableArea.X);
			int num2 = (int)(Screen.RealScreenResolutionHeight * ScreenManager.UsableArea.Y);
			this._latestX = vec.x * (float)num;
			this._latestY = vec.y * (float)num2;
			this._latestW = (this.IsBehind ? (-vec.w) : vec.w);
			this._isHighBind = (this._mapCamera.Position.Distance(v) >= 110f);
			this._partyPositionBind = new Vec2(this._latestX, this._latestY);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		public void DetermineIsVisibleOnMap()
		{
			this._isVisibleOnMapBind = (this._latestW < 100f && this._latestW > 0f && this._mapCamera.Position.z < 200f);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000ABEC File Offset: 0x00008DEC
		public void OnPlayerCharacterChanged(Hero newPlayer)
		{
			if (this.IsMainParty && this.Party.LeaderHero != newPlayer)
			{
				this._isMainPartyBind = false;
				this._mainHeroVisualBind = new ImageIdentifierVM(ImageIdentifierType.Null);
			}
			else if (this.Party.LeaderHero == newPlayer)
			{
				this._isMainPartyBind = true;
				this._mainHeroVisualBind = new ImageIdentifierVM(CharacterCode.CreateFrom(Hero.MainHero.CharacterObject));
			}
			this._isPartyHeroVisualDirty = true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AC5C File Offset: 0x00008E5C
		private bool IsInsideWindow()
		{
			return this._latestX <= Screen.RealScreenResolutionWidth && this._latestY <= Screen.RealScreenResolutionHeight && this._latestX + 100f >= 0f && this._latestY + 30f >= 0f;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public void RefreshBinding()
		{
			base.Position = this._partyPositionBind;
			this.HeadPosition = this._headPositionBind;
			base.IsVisibleOnMap = this._isVisibleOnMapBind;
			this.IsInSettlement = this._isInSettlementBind;
			this.IsMainParty = this._isMainPartyBind;
			base.FactionColor = this._factionColorBind;
			this.MainHeroVisual = this._mainHeroVisualBind;
			this.IsHigh = this._isHighBind;
			this.Count = this._countBind;
			this.Prisoner = this._prisonerBind;
			this.Wounded = this._woundedBind;
			this.IsBehind = this._isBehindBind;
			this.FullName = this._fullNameBind;
			base.IsTargetedByTutorial = this._bindIsTargetedByTutorial;
			this.ShouldShowFullName = this._shouldShowFullNameBind;
			this.IsInArmy = this._isInArmyBind;
			this.IsArmy = this._isArmyBind;
			this.ExtraInfoText = this._extraInfoTextBind;
			this.IsPrisoner = this._isPrisonerBind;
			this.IsDisorganized = this._isDisorganizedBind;
			this.MovementSpeedText = this._movementSpeedTextBind;
			this.Quests.Clear();
			foreach (SandBoxUIHelper.IssueQuestFlags issueQuestFlags in SandBoxUIHelper.IssueQuestFlagsValues)
			{
				if (issueQuestFlags != SandBoxUIHelper.IssueQuestFlags.None && (this._questsBind & issueQuestFlags) != SandBoxUIHelper.IssueQuestFlags.None)
				{
					this.Quests.Add(new QuestMarkerVM(issueQuestFlags));
				}
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000ADFA File Offset: 0x00008FFA
		public void ExecuteSetCameraPosition()
		{
			if (this.IsMainParty)
			{
				this._resetCamera();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000AE10 File Offset: 0x00009010
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			bool flag = this.Party.HomeSettlement != null && (this.Party.HomeSettlement.IsVillage ? settlement.BoundVillages.Contains(this.Party.HomeSettlement.Village) : (this.Party.HomeSettlement == settlement));
			if ((this.Party.IsCaravan || this.Party.IsVillager) && flag)
			{
				this._isPartyBannerDirty = true;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AE91 File Offset: 0x00009091
		private void OnClanChangeKingdom(Clan arg1, Kingdom arg2, Kingdom arg3, ChangeKingdomAction.ChangeKingdomActionDetail arg4, bool showNotification)
		{
			Hero leaderHero = this.Party.LeaderHero;
			if (((leaderHero != null) ? leaderHero.Clan : null) == arg1)
			{
				this._isPartyBannerDirty = true;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AEB4 File Offset: 0x000090B4
		private void OnClanLeaderChanged(Hero arg1, Hero arg2)
		{
			if (arg2.MapFaction == this.Party.MapFaction)
			{
				this._isPartyBannerDirty = true;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000AED0 File Offset: 0x000090D0
		public void RegisterEvents()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangeKingdom));
			CampaignEvents.OnClanLeaderChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnClanLeaderChanged));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000AF22 File Offset: 0x00009122
		public void UnregisterEvents()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.ClearListeners(this);
			CampaignEvents.OnClanChangedKingdomEvent.ClearListeners(this);
			CampaignEvents.OnClanLeaderChangedEvent.ClearListeners(this);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000AF45 File Offset: 0x00009145
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000AF4D File Offset: 0x0000914D
		public Vec2 HeadPosition
		{
			get
			{
				return this._headPosition;
			}
			set
			{
				if (value != this._headPosition)
				{
					this._headPosition = value;
					base.OnPropertyChangedWithValue(value, "HeadPosition");
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000AF70 File Offset: 0x00009170
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000AF78 File Offset: 0x00009178
		public string Count
		{
			get
			{
				return this._count;
			}
			set
			{
				if (value != this._count)
				{
					this._count = value;
					base.OnPropertyChangedWithValue<string>(value, "Count");
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000AF9B File Offset: 0x0000919B
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000AFA3 File Offset: 0x000091A3
		public string Prisoner
		{
			get
			{
				return this._prisoner;
			}
			set
			{
				if (value != this._prisoner)
				{
					this._prisoner = value;
					base.OnPropertyChangedWithValue<string>(value, "Prisoner");
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000AFC6 File Offset: 0x000091C6
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000AFCE File Offset: 0x000091CE
		public MBBindingList<QuestMarkerVM> Quests
		{
			get
			{
				return this._quests;
			}
			set
			{
				if (value != this._quests)
				{
					this._quests = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestMarkerVM>>(value, "Quests");
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000AFEC File Offset: 0x000091EC
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public string Wounded
		{
			get
			{
				return this._wounded;
			}
			set
			{
				if (value != this._wounded)
				{
					this._wounded = value;
					base.OnPropertyChangedWithValue<string>(value, "Wounded");
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000B017 File Offset: 0x00009217
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000B01F File Offset: 0x0000921F
		public string ExtraInfoText
		{
			get
			{
				return this._extraInfoText;
			}
			set
			{
				if (value != this._extraInfoText)
				{
					this._extraInfoText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExtraInfoText");
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B042 File Offset: 0x00009242
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000B04A File Offset: 0x0000924A
		public string MovementSpeedText
		{
			get
			{
				return this._movementSpeedText;
			}
			set
			{
				if (value != this._movementSpeedText)
				{
					this._movementSpeedText = value;
					base.OnPropertyChangedWithValue<string>(value, "MovementSpeedText");
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B06D File Offset: 0x0000926D
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000B075 File Offset: 0x00009275
		public string FullName
		{
			get
			{
				return this._fullName;
			}
			set
			{
				if (value != this._fullName)
				{
					this._fullName = value;
					base.OnPropertyChangedWithValue<string>(value, "FullName");
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000B098 File Offset: 0x00009298
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000B0A0 File Offset: 0x000092A0
		public bool IsMainParty
		{
			get
			{
				return this._isMainParty;
			}
			set
			{
				if (value != this._isMainParty)
				{
					this._isMainParty = value;
					base.OnPropertyChangedWithValue(value, "IsMainParty");
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B0BE File Offset: 0x000092BE
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000B0C6 File Offset: 0x000092C6
		public bool IsInArmy
		{
			get
			{
				return this._isInArmy;
			}
			set
			{
				if (value != this._isInArmy)
				{
					this._isInArmy = value;
					base.OnPropertyChangedWithValue(value, "IsInArmy");
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000B0E4 File Offset: 0x000092E4
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000B0EC File Offset: 0x000092EC
		public bool IsInSettlement
		{
			get
			{
				return this._isInSettlement;
			}
			set
			{
				if (value != this._isInSettlement)
				{
					this._isInSettlement = value;
					base.OnPropertyChangedWithValue(value, "IsInSettlement");
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000B10A File Offset: 0x0000930A
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000B112 File Offset: 0x00009312
		public bool IsDisorganized
		{
			get
			{
				return this._isDisorganized;
			}
			set
			{
				if (value != this._isDisorganized)
				{
					this._isDisorganized = value;
					base.OnPropertyChangedWithValue(value, "IsDisorganized");
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000B130 File Offset: 0x00009330
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000B138 File Offset: 0x00009338
		public bool IsArmy
		{
			get
			{
				return this._isArmy;
			}
			set
			{
				if (value != this._isArmy)
				{
					this._isArmy = value;
					base.OnPropertyChangedWithValue(value, "IsArmy");
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000B156 File Offset: 0x00009356
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000B15E File Offset: 0x0000935E
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (value != this._isBehind)
				{
					this._isBehind = value;
					base.OnPropertyChangedWithValue(value, "IsBehind");
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000B17C File Offset: 0x0000937C
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000B184 File Offset: 0x00009384
		public bool IsHigh
		{
			get
			{
				return this._isHigh;
			}
			set
			{
				if (value != this._isHigh)
				{
					this._isHigh = value;
					base.OnPropertyChangedWithValue(value, "IsHigh");
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000B1A2 File Offset: 0x000093A2
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000B1AA File Offset: 0x000093AA
		public bool IsPrisoner
		{
			get
			{
				return this._isPrisoner;
			}
			set
			{
				if (value != this._isPrisoner)
				{
					this._isPrisoner = value;
					base.OnPropertyChangedWithValue(value, "IsPrisoner");
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000B1C8 File Offset: 0x000093C8
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000B1DA File Offset: 0x000093DA
		public bool ShouldShowFullName
		{
			get
			{
				return this._shouldShowFullName || this._bindIsTargetedByTutorial;
			}
			set
			{
				if (value != this._shouldShowFullName)
				{
					this._shouldShowFullName = value;
					base.OnPropertyChangedWithValue(value, "ShouldShowFullName");
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000B1F8 File Offset: 0x000093F8
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000B200 File Offset: 0x00009400
		public ImageIdentifierVM MainHeroVisual
		{
			get
			{
				return this._mainHeroVisual;
			}
			set
			{
				if (value != this._mainHeroVisual)
				{
					this._mainHeroVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "MainHeroVisual");
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000B21E File Offset: 0x0000941E
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000B226 File Offset: 0x00009426
		public ImageIdentifierVM PartyBanner
		{
			get
			{
				return this._partyBanner;
			}
			set
			{
				if (value != this._partyBanner)
				{
					this._partyBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "PartyBanner");
				}
			}
		}

		// Token: 0x040000DA RID: 218
		public static string PositiveIndicator = Color.FromUint(4285650500U).ToString();

		// Token: 0x040000DB RID: 219
		public static string PositiveArmyIndicator = Color.FromUint(4288804731U).ToString();

		// Token: 0x040000DC RID: 220
		public static string NegativeIndicator = Color.FromUint(4292232774U).ToString();

		// Token: 0x040000DD RID: 221
		public static string NegativeArmyIndicator = Color.FromUint(4294931829U).ToString();

		// Token: 0x040000DE RID: 222
		public static string NeutralIndicator = Color.FromUint(4291877096U).ToString();

		// Token: 0x040000DF RID: 223
		public static string NeutralArmyIndicator = Color.FromUint(4294573055U).ToString();

		// Token: 0x040000E0 RID: 224
		public static string MainPartyIndicator = Color.FromUint(4287421380U).ToString();

		// Token: 0x040000E1 RID: 225
		public static string MainPartyArmyIndicator = Color.FromUint(4289593317U).ToString();

		// Token: 0x040000E3 RID: 227
		private float _latestX;

		// Token: 0x040000E4 RID: 228
		private float _latestY;

		// Token: 0x040000E5 RID: 229
		private float _latestW;

		// Token: 0x040000E6 RID: 230
		private float _cachedSpeed;

		// Token: 0x040000E7 RID: 231
		private readonly Camera _mapCamera;

		// Token: 0x040000E8 RID: 232
		private readonly Action _resetCamera;

		// Token: 0x040000E9 RID: 233
		private readonly Func<bool> _getShouldShowFullName;

		// Token: 0x040000EA RID: 234
		private int _latestPrisonerAmount = -1;

		// Token: 0x040000EB RID: 235
		private int _latestWoundedAmount = -1;

		// Token: 0x040000EC RID: 236
		private int _latestTotalCount = -1;

		// Token: 0x040000ED RID: 237
		private bool _isPartyBannerDirty;

		// Token: 0x040000EE RID: 238
		private bool _isPartyHeroVisualDirty;

		// Token: 0x040000EF RID: 239
		private float _latestMainHeroAge = -1f;

		// Token: 0x040000F0 RID: 240
		private TextObject _latestNameTextObject;

		// Token: 0x040000F1 RID: 241
		private SandBoxUIHelper.IssueQuestFlags _questsBind;

		// Token: 0x040000F2 RID: 242
		private Vec2 _partyPositionBind;

		// Token: 0x040000F3 RID: 243
		private Vec2 _headPositionBind;

		// Token: 0x040000F4 RID: 244
		private ImageIdentifierVM _mainHeroVisualBind;

		// Token: 0x040000F5 RID: 245
		private bool _isMainPartyBind;

		// Token: 0x040000F6 RID: 246
		private bool _isHighBind;

		// Token: 0x040000F7 RID: 247
		private bool _isBehindBind;

		// Token: 0x040000F8 RID: 248
		private bool _isInArmyBind;

		// Token: 0x040000F9 RID: 249
		private bool _isInSettlementBind;

		// Token: 0x040000FA RID: 250
		private bool _isVisibleOnMapBind;

		// Token: 0x040000FB RID: 251
		private bool _shouldShowFullNameBind;

		// Token: 0x040000FC RID: 252
		private bool _isArmyBind;

		// Token: 0x040000FD RID: 253
		private bool _isPrisonerBind;

		// Token: 0x040000FE RID: 254
		private bool _isDisorganizedBind;

		// Token: 0x040000FF RID: 255
		private string _factionColorBind;

		// Token: 0x04000100 RID: 256
		private string _countBind;

		// Token: 0x04000101 RID: 257
		private string _woundedBind;

		// Token: 0x04000102 RID: 258
		private string _prisonerBind;

		// Token: 0x04000103 RID: 259
		private string _extraInfoTextBind;

		// Token: 0x04000104 RID: 260
		private string _fullNameBind;

		// Token: 0x04000105 RID: 261
		private string _movementSpeedTextBind;

		// Token: 0x04000106 RID: 262
		private string _count;

		// Token: 0x04000107 RID: 263
		private string _wounded;

		// Token: 0x04000108 RID: 264
		private string _prisoner;

		// Token: 0x04000109 RID: 265
		private MBBindingList<QuestMarkerVM> _quests;

		// Token: 0x0400010A RID: 266
		private string _fullName;

		// Token: 0x0400010B RID: 267
		private string _extraInfoText;

		// Token: 0x0400010C RID: 268
		private string _movementSpeedText;

		// Token: 0x0400010D RID: 269
		private bool _isMainParty;

		// Token: 0x0400010E RID: 270
		private bool _isBehind;

		// Token: 0x0400010F RID: 271
		private bool _isHigh;

		// Token: 0x04000110 RID: 272
		private bool _shouldShowFullName;

		// Token: 0x04000111 RID: 273
		private bool _isInArmy;

		// Token: 0x04000112 RID: 274
		private bool _isArmy;

		// Token: 0x04000113 RID: 275
		private bool _isPrisoner;

		// Token: 0x04000114 RID: 276
		private bool _isInSettlement;

		// Token: 0x04000115 RID: 277
		private bool _isDisorganized;

		// Token: 0x04000116 RID: 278
		private ImageIdentifierVM _mainHeroVisual;

		// Token: 0x04000117 RID: 279
		private ImageIdentifierVM _partyBanner;

		// Token: 0x04000118 RID: 280
		private Vec2 _headPosition;
	}
}
