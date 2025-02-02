using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Helpers;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Barter
{
	// Token: 0x0200013B RID: 315
	public class BarterVM : ViewModel
	{
		// Token: 0x06001E49 RID: 7753 RVA: 0x0006BFB4 File Offset: 0x0006A1B4
		public BarterVM(BarterData args)
		{
			this._barterData = args;
			if (this._barterData.OtherHero == Hero.MainHero)
			{
				this._otherParty = this._barterData.OffererParty;
				this._otherCharacter = (this._barterData.OffererHero.CharacterObject ?? CampaignUIHelper.GetVisualPartyLeader(this._otherParty));
			}
			else if (this._barterData.OtherHero != null)
			{
				this._otherCharacter = this._barterData.OtherHero.CharacterObject;
				this.LeftMaxGold = this._otherCharacter.HeroObject.Gold;
			}
			else
			{
				this._otherParty = this._barterData.OtherParty;
				this._otherCharacter = CampaignUIHelper.GetVisualPartyLeader(this._otherParty);
				this.LeftMaxGold = this._otherParty.MobileParty.PartyTradeGold;
			}
			this._barter = Campaign.Current.BarterManager;
			this._isPlayerOfferer = (this._barterData.OffererHero == Hero.MainHero);
			this.AutoBalanceHint = new HintViewModel();
			this.LeftFiefList = new MBBindingList<BarterItemVM>();
			this.RightFiefList = new MBBindingList<BarterItemVM>();
			this.LeftPrisonerList = new MBBindingList<BarterItemVM>();
			this.RightPrisonerList = new MBBindingList<BarterItemVM>();
			this.LeftItemList = new MBBindingList<BarterItemVM>();
			this.RightItemList = new MBBindingList<BarterItemVM>();
			this.LeftOtherList = new MBBindingList<BarterItemVM>();
			this.RightOtherList = new MBBindingList<BarterItemVM>();
			this.LeftDiplomaticList = new MBBindingList<BarterItemVM>();
			this.RightDiplomaticList = new MBBindingList<BarterItemVM>();
			this.LeftGoldList = new MBBindingList<BarterItemVM>();
			this.RightGoldList = new MBBindingList<BarterItemVM>();
			this._leftList = new Dictionary<BarterGroup, MBBindingList<BarterItemVM>>();
			this._rightList = new Dictionary<BarterGroup, MBBindingList<BarterItemVM>>();
			this._barterList = new List<Dictionary<BarterGroup, MBBindingList<BarterItemVM>>>();
			this._offerList = new List<MBBindingList<BarterItemVM>>();
			this.LeftOfferList = new MBBindingList<BarterItemVM>();
			this.RightOfferList = new MBBindingList<BarterItemVM>();
			this.InitBarterList(this._barterData);
			this.OnInitialized();
			this.RightMaxGold = Hero.MainHero.Gold;
			this.LeftHero = new HeroVM(this._otherCharacter.HeroObject, false);
			this.RightHero = new HeroVM(Hero.MainHero, false);
			this.SendOffer();
			this.InitializationIsOver = true;
			this.RefreshValues();
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0006C1F0 File Offset: 0x0006A3F0
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.InitializeStaticContent();
			this.LeftNameLbl = this._otherCharacter.Name.ToString();
			this.RightNameLbl = Hero.MainHero.Name.ToString();
			this.LeftFiefList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightFiefList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftPrisonerList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightPrisonerList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftItemList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightItemList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftOtherList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightOtherList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftDiplomaticList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightDiplomaticList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftGoldList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightGoldList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0006C42C File Offset: 0x0006A62C
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.CancelInputKey.OnFinalize();
			this.ResetInputKey.OnFinalize();
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0006C458 File Offset: 0x0006A658
		private void InitBarterList(BarterData args)
		{
			this._leftList.Add(args.GetBarterGroup<FiefBarterGroup>(), this.LeftFiefList);
			this._leftList.Add(args.GetBarterGroup<PrisonerBarterGroup>(), this.LeftPrisonerList);
			this._leftList.Add(args.GetBarterGroup<ItemBarterGroup>(), this.LeftItemList);
			this._leftList.Add(args.GetBarterGroup<OtherBarterGroup>(), this.LeftOtherList);
			this._leftList.Add(args.GetBarterGroup<GoldBarterGroup>(), this.LeftGoldList);
			this._rightList.Add(args.GetBarterGroup<FiefBarterGroup>(), this.RightFiefList);
			this._rightList.Add(args.GetBarterGroup<PrisonerBarterGroup>(), this.RightPrisonerList);
			this._rightList.Add(args.GetBarterGroup<ItemBarterGroup>(), this.RightItemList);
			this._rightList.Add(args.GetBarterGroup<OtherBarterGroup>(), this.RightOtherList);
			this._rightList.Add(args.GetBarterGroup<GoldBarterGroup>(), this.RightGoldList);
			this._barterList.Add(this._leftList);
			this._barterList.Add(this._rightList);
			this._offerList.Add(this.LeftOfferList);
			this._offerList.Add(this.RightOfferList);
			if (this._barterData.ContextInitializer != null)
			{
				foreach (Barterable barterable in this._barterData.GetBarterables())
				{
					if (barterable.IsContextDependent && this._barterData.ContextInitializer(barterable, this._barterData, null))
					{
						this.ChangeBarterableIsOffered(barterable, true);
					}
				}
			}
			foreach (Barterable barterable2 in args.GetBarterables())
			{
				if (!barterable2.IsOffered && !barterable2.IsContextDependent)
				{
					this._barterList[(barterable2.OriginalOwner == Hero.MainHero) ? 1 : 0][barterable2.Group].Add(new BarterItemVM(barterable2, new BarterItemVM.BarterTransferEventDelegate(this.TransferItem), new Action(this.OnOfferedAmountChange), false));
				}
				else
				{
					BarterItemVM barterItemVM = new BarterItemVM(barterable2, new BarterItemVM.BarterTransferEventDelegate(this.TransferItem), new Action(this.OnOfferedAmountChange), barterable2.IsContextDependent);
					this._offerList[(barterable2.OriginalOwner == Hero.MainHero) ? 1 : 0].Add(barterItemVM);
					this.RefreshCompatibility(barterItemVM, true);
				}
			}
			this._barterData.GetBarterables().Find((Barterable t) => t.Group.GetType() == typeof(GoldBarterGroup) && t.OriginalOwner == Hero.MainHero);
			this._barterData.GetBarterables().Find((Barterable t) => (t.Group.GetType() == typeof(GoldBarterGroup) && this._barterData.OffererHero == Hero.MainHero && t.OriginalOwner == this._barterData.OtherHero) || (this._barterData.OtherHero == Hero.MainHero && t.OriginalOwner == this._barterData.OffererHero));
			this.RefreshOfferLabel();
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0006C74C File Offset: 0x0006A94C
		private void ChangeBarterableIsOffered(Barterable barterable, bool newState)
		{
			if (barterable.IsOffered != newState)
			{
				barterable.SetIsOffered(newState);
				this.OnTransferItem(barterable, true);
				foreach (Barterable barter in barterable.LinkedBarterables)
				{
					this.OnTransferItem(barter, true);
				}
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0006C7B8 File Offset: 0x0006A9B8
		public void OnInitialized()
		{
			BarterManager barterManager = Campaign.Current.BarterManager;
			barterManager.Closed = (BarterManager.BarterCloseEventDelegate)Delegate.Combine(barterManager.Closed, new BarterManager.BarterCloseEventDelegate(this.OnClosed));
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0006C7E5 File Offset: 0x0006A9E5
		private void OnClosed()
		{
			BarterManager barterManager = Campaign.Current.BarterManager;
			barterManager.Closed = (BarterManager.BarterCloseEventDelegate)Delegate.Remove(barterManager.Closed, new BarterManager.BarterCloseEventDelegate(this.OnClosed));
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0006C812 File Offset: 0x0006AA12
		public void ExecuteTransferAllLeftFief()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<FiefBarterGroup>());
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0006C82B File Offset: 0x0006AA2B
		public void ExecuteAutoBalance()
		{
			this.AutoBalanceAdd();
			this.AutoBalanceRemove();
			this.AutoBalanceAdd();
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0006C840 File Offset: 0x0006AA40
		private void AutoBalanceRemove()
		{
			if ((int)Campaign.Current.BarterManager.GetOfferValue(this._otherCharacter.HeroObject, this._otherParty, this._barterData.OffererParty, this._barterData.GetOfferedBarterables()) > 0)
			{
				List<ValueTuple<Barterable, int>> newBarterables = BarterHelper.GetAutoBalanceBarterablesToRemove(this._barterData, this.OtherFaction, Clan.PlayerClan.MapFaction, Hero.MainHero).ToList<ValueTuple<Barterable, int>>();
				List<ValueTuple<BarterItemVM, int>> list = new List<ValueTuple<BarterItemVM, int>>();
				this.GetBarterItems(this.RightGoldList, newBarterables, list);
				this.GetBarterItems(this.RightItemList, newBarterables, list);
				this.GetBarterItems(this.RightPrisonerList, newBarterables, list);
				this.GetBarterItems(this.RightFiefList, newBarterables, list);
				foreach (ValueTuple<BarterItemVM, int> valueTuple in list)
				{
					BarterItemVM item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					this.OfferItemRemove(item, item2);
				}
			}
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0006C940 File Offset: 0x0006AB40
		private void AutoBalanceAdd()
		{
			if ((int)Campaign.Current.BarterManager.GetOfferValue(this._otherCharacter.HeroObject, this._otherParty, this._barterData.OffererParty, this._barterData.GetOfferedBarterables()) < 0)
			{
				List<ValueTuple<Barterable, int>> newBarterables = BarterHelper.GetAutoBalanceBarterablesAdd(this._barterData, this.OtherFaction, Clan.PlayerClan.MapFaction, Hero.MainHero, 1f).ToList<ValueTuple<Barterable, int>>();
				List<ValueTuple<BarterItemVM, int>> list = new List<ValueTuple<BarterItemVM, int>>();
				this.GetBarterItems(this.RightGoldList, newBarterables, list);
				this.GetBarterItems(this.RightItemList, newBarterables, list);
				this.GetBarterItems(this.RightPrisonerList, newBarterables, list);
				this.GetBarterItems(this.RightFiefList, newBarterables, list);
				foreach (ValueTuple<BarterItemVM, int> valueTuple in list)
				{
					BarterItemVM item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					if (item2 > 0)
					{
						this.OfferItemAdd(item, item2);
					}
				}
			}
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0006CA48 File Offset: 0x0006AC48
		private void GetBarterItems(MBBindingList<BarterItemVM> itemList, [TupleElementNames(new string[]
		{
			"barterable",
			"count"
		})] List<ValueTuple<Barterable, int>> newBarterables, List<ValueTuple<BarterItemVM, int>> barterItems)
		{
			foreach (BarterItemVM barterItemVM in itemList)
			{
				foreach (ValueTuple<Barterable, int> valueTuple in newBarterables)
				{
					Barterable item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					if (item == barterItemVM.Barterable)
					{
						barterItems.Add(new ValueTuple<BarterItemVM, int>(barterItemVM, item2));
					}
				}
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0006CAE4 File Offset: 0x0006ACE4
		public void ExecuteTransferAllLeftItem()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<ItemBarterGroup>());
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0006CAFD File Offset: 0x0006ACFD
		public void ExecuteTransferAllLeftPrisoner()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<PrisonerBarterGroup>());
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0006CB16 File Offset: 0x0006AD16
		public void ExecuteTransferAllLeftOther()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<OtherBarterGroup>());
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0006CB2F File Offset: 0x0006AD2F
		public void ExecuteTransferAllRightFief()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<FiefBarterGroup>());
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0006CB47 File Offset: 0x0006AD47
		public void ExecuteTransferAllRightItem()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<ItemBarterGroup>());
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0006CB5F File Offset: 0x0006AD5F
		public void ExecuteTransferAllRightPrisoner()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<PrisonerBarterGroup>());
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0006CB77 File Offset: 0x0006AD77
		public void ExecuteTransferAllRightOther()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<OtherBarterGroup>());
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0006CB90 File Offset: 0x0006AD90
		private void ExecuteTransferAll(CharacterObject fromCharacter, BarterGroup barterGroup)
		{
			if (barterGroup != null)
			{
				foreach (BarterItemVM item in new List<BarterItemVM>(from barterItem in this._barterList[(fromCharacter == CharacterObject.PlayerCharacter) ? 1 : 0][barterGroup]
				where !barterItem.Barterable.IsOffered
				select barterItem))
				{
					this.TransferItem(item, true);
				}
				foreach (BarterItemVM barterItemVM in this._barterList[(fromCharacter == CharacterObject.PlayerCharacter) ? 1 : 0][barterGroup])
				{
					barterItemVM.CurrentOfferedAmount = barterItemVM.TotalItemCount;
				}
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0006CC80 File Offset: 0x0006AE80
		private void SendOffer()
		{
			this.IsOfferDisabled = (!this.IsCurrentOfferAcceptable() || (this.LeftOfferList.Count == 0 && this.RightOfferList.Count == 0));
			this.RefreshResultBar();
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0006CCB7 File Offset: 0x0006AEB7
		private bool IsCurrentOfferAcceptable()
		{
			return Campaign.Current.BarterManager.IsOfferAcceptable(this._barterData, this._otherCharacter.HeroObject, this._otherParty);
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0006CCE0 File Offset: 0x0006AEE0
		private IFaction OtherFaction
		{
			get
			{
				if (!this._otherCharacter.IsHero)
				{
					return this._otherParty.MapFaction;
				}
				return this._otherCharacter.HeroObject.Clan;
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0006CD18 File Offset: 0x0006AF18
		private void RefreshResultBar()
		{
			long num = 0L;
			long num2 = 0L;
			IFaction otherFaction = this.OtherFaction;
			foreach (BarterItemVM barterItemVM in this.LeftOfferList)
			{
				int valueForFaction = barterItemVM.Barterable.GetValueForFaction(otherFaction);
				if (valueForFaction < 0)
				{
					num2 += (long)valueForFaction;
				}
				else
				{
					num += (long)valueForFaction;
				}
			}
			foreach (BarterItemVM barterItemVM2 in this.RightOfferList)
			{
				int valueForFaction2 = barterItemVM2.Barterable.GetValueForFaction(otherFaction);
				if (valueForFaction2 < 0)
				{
					num2 += (long)valueForFaction2;
				}
				else
				{
					num += (long)valueForFaction2;
				}
			}
			double num3 = (double)MathF.Max(0f, (float)num);
			double num4 = (double)MathF.Max(1f, (float)(-(float)num2));
			this.ResultBarOtherPercentage = MathF.Round(num3 / num4 * 100.0);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0006CE20 File Offset: 0x0006B020
		private void ExecuteTransferAllGoldLeft()
		{
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0006CE22 File Offset: 0x0006B022
		private void ExecuteTransferAllGoldRight()
		{
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0006CE24 File Offset: 0x0006B024
		public void ExecuteOffer()
		{
			Campaign.Current.BarterManager.ApplyAndFinalizePlayerBarter(this._barterData.OffererHero, this._barterData.OtherHero, this._barterData);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0006CE51 File Offset: 0x0006B051
		public void ExecuteCancel()
		{
			Campaign.Current.BarterManager.CancelAndFinalizePlayerBarter(this._barterData.OffererHero, this._barterData.OtherHero, this._barterData);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0006CE80 File Offset: 0x0006B080
		public void ExecuteReset()
		{
			this.LeftFiefList.Clear();
			this.RightFiefList.Clear();
			this.LeftPrisonerList.Clear();
			this.RightPrisonerList.Clear();
			this.LeftItemList.Clear();
			this.RightItemList.Clear();
			this.LeftOtherList.Clear();
			this.RightOtherList.Clear();
			this.LeftDiplomaticList.Clear();
			this.RightDiplomaticList.Clear();
			this.LeftGoldList.Clear();
			this.RightGoldList.Clear();
			this._leftList.Clear();
			this._rightList.Clear();
			this._barterList.Clear();
			this.LeftOfferList.Clear();
			this.RightOfferList.Clear();
			this._offerList.Clear();
			foreach (Barterable barterable in this._barterData.GetBarterables())
			{
				if (barterable.IsOffered)
				{
					this.ChangeBarterableIsOffered(barterable, false);
				}
			}
			this.InitBarterList(this._barterData);
			this.LeftNameLbl = this._otherCharacter.Name.ToString();
			this.RightNameLbl = Hero.MainHero.Name.ToString();
			this.LeftMaxGold = ((this._otherCharacter.HeroObject != null) ? this._otherCharacter.HeroObject.Gold : this._otherParty.MobileParty.PartyTradeGold);
			this.RightMaxGold = Hero.MainHero.Gold;
			this.SendOffer();
			this.InitializationIsOver = true;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0006D030 File Offset: 0x0006B230
		private void TransferItem(BarterItemVM item, bool offerAll)
		{
			this.ChangeBarterableIsOffered(item.Barterable, !item.IsOffered);
			if (offerAll)
			{
				item.CurrentOfferedAmount = item.TotalItemCount;
			}
			this.SendOffer();
			this.RefreshOfferLabel();
			this.RefreshCompatibility(item, item.IsOffered);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0006D070 File Offset: 0x0006B270
		private void OfferItemAdd(BarterItemVM barterItemVM, int count)
		{
			this.ChangeBarterableIsOffered(barterItemVM.Barterable, true);
			barterItemVM.CurrentOfferedAmount = (int)MathF.Clamp((float)(barterItemVM.CurrentOfferedAmount + count), 0f, (float)barterItemVM.TotalItemCount);
			this.SendOffer();
			this.RefreshOfferLabel();
			this.RefreshCompatibility(barterItemVM, barterItemVM.IsOffered);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0006D0C4 File Offset: 0x0006B2C4
		private void OfferItemRemove(BarterItemVM barterItemVM, int count)
		{
			if (barterItemVM.CurrentOfferedAmount <= count)
			{
				this.ChangeBarterableIsOffered(barterItemVM.Barterable, false);
			}
			else
			{
				barterItemVM.CurrentOfferedAmount = (int)MathF.Clamp((float)(barterItemVM.CurrentOfferedAmount - count), 0f, (float)barterItemVM.TotalItemCount);
			}
			this.SendOffer();
			this.RefreshOfferLabel();
			this.RefreshCompatibility(barterItemVM, barterItemVM.IsOffered);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0006D124 File Offset: 0x0006B324
		public void OnTransferItem(Barterable barter, bool isTransferrable)
		{
			int index = (barter.OriginalOwner == Hero.MainHero) ? 1 : 0;
			if (!this._barterList.IsEmpty<Dictionary<BarterGroup, MBBindingList<BarterItemVM>>>())
			{
				BarterItemVM barterItemVM = this._barterList[index][barter.Group].FirstOrDefault((BarterItemVM i) => i.Barterable == barter);
				if (barterItemVM == null && !this._offerList.IsEmpty<MBBindingList<BarterItemVM>>())
				{
					barterItemVM = this._offerList[index].FirstOrDefault((BarterItemVM i) => i.Barterable == barter);
				}
				if (barterItemVM != null)
				{
					barterItemVM.IsOffered = barter.IsOffered;
					barterItemVM.IsItemTransferrable = isTransferrable;
					if (barterItemVM.IsOffered)
					{
						this._offerList[index].Add(barterItemVM);
						if (barterItemVM.IsMultiple)
						{
							barterItemVM.CurrentOfferedAmount = 1;
							return;
						}
					}
					else
					{
						this._offerList[index].Remove(barterItemVM);
						if (barterItemVM.IsMultiple)
						{
							barterItemVM.CurrentOfferedAmount = 1;
						}
					}
				}
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0006D228 File Offset: 0x0006B428
		private void OnOfferedAmountChange()
		{
			this.SendOffer();
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0006D230 File Offset: 0x0006B430
		private void RefreshOfferLabel()
		{
			if (this.LeftOfferList.Any((BarterItemVM x) => x.Barterable.GetValueForFaction(this.OtherFaction) < 0) || this.RightOfferList.Any((BarterItemVM x) => x.Barterable.GetValueForFaction(this.OtherFaction) < 0))
			{
				this.OfferLbl = GameTexts.FindText("str_offer", null).ToString();
				return;
			}
			this.OfferLbl = GameTexts.FindText("str_gift", null).ToString();
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0006D29C File Offset: 0x0006B49C
		private void RefreshCompatibility(BarterItemVM lastTransferredItem, bool gotOffered)
		{
			Action<BarterItemVM> <>9__0;
			foreach (MBBindingList<BarterItemVM> source in this._leftList.Values)
			{
				List<BarterItemVM> list = source.ToList<BarterItemVM>();
				Action<BarterItemVM> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(BarterItemVM b)
					{
						b.RefreshCompabilityWithItem(lastTransferredItem, gotOffered);
					});
				}
				list.ForEach(action);
			}
			Action<BarterItemVM> <>9__1;
			foreach (MBBindingList<BarterItemVM> source2 in this._rightList.Values)
			{
				List<BarterItemVM> list2 = source2.ToList<BarterItemVM>();
				Action<BarterItemVM> action2;
				if ((action2 = <>9__1) == null)
				{
					action2 = (<>9__1 = delegate(BarterItemVM b)
					{
						b.RefreshCompabilityWithItem(lastTransferredItem, gotOffered);
					});
				}
				list2.ForEach(action2);
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x0006D394 File Offset: 0x0006B594
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0006D39C File Offset: 0x0006B59C
		[DataSourceProperty]
		public string FiefLbl
		{
			get
			{
				return this._fiefLbl;
			}
			set
			{
				if (value != this._fiefLbl)
				{
					this._fiefLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "FiefLbl");
				}
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0006D3BF File Offset: 0x0006B5BF
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x0006D3C7 File Offset: 0x0006B5C7
		[DataSourceProperty]
		public string PrisonerLbl
		{
			get
			{
				return this._prisonerLbl;
			}
			set
			{
				if (value != this._prisonerLbl)
				{
					this._prisonerLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "PrisonerLbl");
				}
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0006D3EA File Offset: 0x0006B5EA
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x0006D3F2 File Offset: 0x0006B5F2
		[DataSourceProperty]
		public string ItemLbl
		{
			get
			{
				return this._itemLbl;
			}
			set
			{
				if (value != this._itemLbl)
				{
					this._itemLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ItemLbl");
				}
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0006D415 File Offset: 0x0006B615
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0006D41D File Offset: 0x0006B61D
		[DataSourceProperty]
		public string OtherLbl
		{
			get
			{
				return this._otherLbl;
			}
			set
			{
				if (value != this._otherLbl)
				{
					this._otherLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "OtherLbl");
				}
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0006D440 File Offset: 0x0006B640
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0006D448 File Offset: 0x0006B648
		[DataSourceProperty]
		public string CancelLbl
		{
			get
			{
				return this._cancelLbl;
			}
			set
			{
				if (value != this._cancelLbl)
				{
					this._cancelLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelLbl");
				}
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0006D46B File Offset: 0x0006B66B
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x0006D473 File Offset: 0x0006B673
		[DataSourceProperty]
		public string ResetLbl
		{
			get
			{
				return this._resetLbl;
			}
			set
			{
				if (value != this._resetLbl)
				{
					this._resetLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetLbl");
				}
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x0006D496 File Offset: 0x0006B696
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x0006D49E File Offset: 0x0006B69E
		[DataSourceProperty]
		public string OfferLbl
		{
			get
			{
				return this._offerLbl;
			}
			set
			{
				if (value != this._offerLbl)
				{
					this._offerLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "OfferLbl");
				}
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0006D4C1 File Offset: 0x0006B6C1
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0006D4C9 File Offset: 0x0006B6C9
		[DataSourceProperty]
		public string DiplomaticLbl
		{
			get
			{
				return this._diplomaticLbl;
			}
			set
			{
				if (value != this._diplomaticLbl)
				{
					this._diplomaticLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DiplomaticLbl");
				}
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0006D4EC File Offset: 0x0006B6EC
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x0006D4F4 File Offset: 0x0006B6F4
		[DataSourceProperty]
		public HintViewModel AutoBalanceHint
		{
			get
			{
				return this._autoBalanceHint;
			}
			set
			{
				if (value != this._autoBalanceHint)
				{
					this._autoBalanceHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AutoBalanceHint");
				}
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0006D512 File Offset: 0x0006B712
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x0006D51A File Offset: 0x0006B71A
		[DataSourceProperty]
		public HeroVM LeftHero
		{
			get
			{
				return this._leftHero;
			}
			set
			{
				if (value != this._leftHero)
				{
					this._leftHero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "LeftHero");
				}
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0006D538 File Offset: 0x0006B738
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x0006D540 File Offset: 0x0006B740
		[DataSourceProperty]
		public HeroVM RightHero
		{
			get
			{
				return this._rightHero;
			}
			set
			{
				if (value != this._rightHero)
				{
					this._rightHero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "RightHero");
				}
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0006D55E File Offset: 0x0006B75E
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x0006D566 File Offset: 0x0006B766
		[DataSourceProperty]
		public bool IsOfferDisabled
		{
			get
			{
				return this._isOfferDisabled;
			}
			set
			{
				if (value != this._isOfferDisabled)
				{
					this._isOfferDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsOfferDisabled");
				}
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0006D584 File Offset: 0x0006B784
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x0006D58C File Offset: 0x0006B78C
		[DataSourceProperty]
		public int LeftMaxGold
		{
			get
			{
				return this._leftMaxGold;
			}
			set
			{
				if (value != this._leftMaxGold)
				{
					this._leftMaxGold = value;
					base.OnPropertyChangedWithValue(value, "LeftMaxGold");
				}
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0006D5AA File Offset: 0x0006B7AA
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0006D5B2 File Offset: 0x0006B7B2
		[DataSourceProperty]
		public int RightMaxGold
		{
			get
			{
				return this._rightMaxGold;
			}
			set
			{
				if (value != this._rightMaxGold)
				{
					this._rightMaxGold = value;
					base.OnPropertyChangedWithValue(value, "RightMaxGold");
				}
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0006D5D0 File Offset: 0x0006B7D0
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0006D5D8 File Offset: 0x0006B7D8
		[DataSourceProperty]
		public string LeftNameLbl
		{
			get
			{
				return this._leftNameLbl;
			}
			set
			{
				if (value != this._leftNameLbl)
				{
					this._leftNameLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "LeftNameLbl");
				}
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0006D5FB File Offset: 0x0006B7FB
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x0006D603 File Offset: 0x0006B803
		[DataSourceProperty]
		public string RightNameLbl
		{
			get
			{
				return this._rightNameLbl;
			}
			set
			{
				if (value != this._rightNameLbl)
				{
					this._rightNameLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "RightNameLbl");
				}
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0006D626 File Offset: 0x0006B826
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0006D62E File Offset: 0x0006B82E
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftFiefList
		{
			get
			{
				return this._leftFiefList;
			}
			set
			{
				if (value != this._leftFiefList)
				{
					this._leftFiefList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftFiefList");
				}
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0006D64C File Offset: 0x0006B84C
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x0006D654 File Offset: 0x0006B854
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightFiefList
		{
			get
			{
				return this._rightFiefList;
			}
			set
			{
				if (value != this._rightFiefList)
				{
					this._rightFiefList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightFiefList");
				}
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0006D672 File Offset: 0x0006B872
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0006D67A File Offset: 0x0006B87A
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftPrisonerList
		{
			get
			{
				return this._leftPrisonerList;
			}
			set
			{
				if (value != this._leftPrisonerList)
				{
					this._leftPrisonerList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftPrisonerList");
				}
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0006D698 File Offset: 0x0006B898
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x0006D6A0 File Offset: 0x0006B8A0
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightPrisonerList
		{
			get
			{
				return this._rightPrisonerList;
			}
			set
			{
				if (value != this._rightPrisonerList)
				{
					this._rightPrisonerList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightPrisonerList");
				}
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0006D6BE File Offset: 0x0006B8BE
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x0006D6C6 File Offset: 0x0006B8C6
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftItemList
		{
			get
			{
				return this._leftItemList;
			}
			set
			{
				if (value != this._leftItemList)
				{
					this._leftItemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftItemList");
				}
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x0006D6E4 File Offset: 0x0006B8E4
		// (set) Token: 0x06001E98 RID: 7832 RVA: 0x0006D6EC File Offset: 0x0006B8EC
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightItemList
		{
			get
			{
				return this._rightItemList;
			}
			set
			{
				if (value != this._rightItemList)
				{
					this._rightItemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightItemList");
				}
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x0006D70A File Offset: 0x0006B90A
		// (set) Token: 0x06001E9A RID: 7834 RVA: 0x0006D712 File Offset: 0x0006B912
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftOtherList
		{
			get
			{
				return this._leftOtherList;
			}
			set
			{
				if (value != this._leftOtherList)
				{
					this._leftOtherList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftOtherList");
				}
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0006D730 File Offset: 0x0006B930
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0006D738 File Offset: 0x0006B938
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightOtherList
		{
			get
			{
				return this._rightOtherList;
			}
			set
			{
				if (value != this._rightOtherList)
				{
					this._rightOtherList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightOtherList");
				}
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0006D756 File Offset: 0x0006B956
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x0006D75E File Offset: 0x0006B95E
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftDiplomaticList
		{
			get
			{
				return this._leftDiplomaticList;
			}
			set
			{
				if (value != this._leftDiplomaticList)
				{
					this._leftDiplomaticList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftDiplomaticList");
				}
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0006D77C File Offset: 0x0006B97C
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0006D784 File Offset: 0x0006B984
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightDiplomaticList
		{
			get
			{
				return this._rightDiplomaticList;
			}
			set
			{
				if (value != this._rightDiplomaticList)
				{
					this._rightDiplomaticList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightDiplomaticList");
				}
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0006D7A2 File Offset: 0x0006B9A2
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x0006D7AA File Offset: 0x0006B9AA
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftOfferList
		{
			get
			{
				return this._leftOfferList;
			}
			set
			{
				if (value != this._leftOfferList)
				{
					this._leftOfferList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftOfferList");
				}
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x0006D7C8 File Offset: 0x0006B9C8
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0006D7D0 File Offset: 0x0006B9D0
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightOfferList
		{
			get
			{
				return this._rightOfferList;
			}
			set
			{
				if (value != this._rightOfferList)
				{
					this._rightOfferList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightOfferList");
				}
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0006D7EE File Offset: 0x0006B9EE
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x0006D7F6 File Offset: 0x0006B9F6
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightGoldList
		{
			get
			{
				return this._rightGoldList;
			}
			set
			{
				if (value != this._rightGoldList)
				{
					this._rightGoldList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightGoldList");
				}
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0006D814 File Offset: 0x0006BA14
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x0006D81C File Offset: 0x0006BA1C
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftGoldList
		{
			get
			{
				return this._leftGoldList;
			}
			set
			{
				if (value != this._leftGoldList)
				{
					this._leftGoldList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftGoldList");
				}
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x0006D83A File Offset: 0x0006BA3A
		// (set) Token: 0x06001EAA RID: 7850 RVA: 0x0006D842 File Offset: 0x0006BA42
		[DataSourceProperty]
		public bool InitializationIsOver
		{
			get
			{
				return this._initializationIsOver;
			}
			set
			{
				this._initializationIsOver = value;
				base.OnPropertyChangedWithValue(value, "InitializationIsOver");
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x0006D857 File Offset: 0x0006BA57
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x0006D85F File Offset: 0x0006BA5F
		[DataSourceProperty]
		public int ResultBarOtherPercentage
		{
			get
			{
				return this._resultBarOtherPercentage;
			}
			set
			{
				this._resultBarOtherPercentage = value;
				base.OnPropertyChangedWithValue(value, "ResultBarOtherPercentage");
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0006D874 File Offset: 0x0006BA74
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x0006D87C File Offset: 0x0006BA7C
		[DataSourceProperty]
		public int ResultBarOffererPercentage
		{
			get
			{
				return this._resultBarOffererPercentage;
			}
			set
			{
				this._resultBarOffererPercentage = value;
				base.OnPropertyChangedWithValue(value, "ResultBarOffererPercentage");
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0006D891 File Offset: 0x0006BA91
		public void SetResetInputKey(HotKey hotkey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0006D8A0 File Offset: 0x0006BAA0
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0006D8AF File Offset: 0x0006BAAF
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0006D8BE File Offset: 0x0006BABE
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x0006D8C6 File Offset: 0x0006BAC6
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0006D8E4 File Offset: 0x0006BAE4
		// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x0006D8EC File Offset: 0x0006BAEC
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0006D90A File Offset: 0x0006BB0A
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x0006D912 File Offset: 0x0006BB12
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0006D930 File Offset: 0x0006BB30
		public void InitializeStaticContent()
		{
			this.FiefLbl = GameTexts.FindText("str_fiefs", null).ToString();
			this.PrisonerLbl = GameTexts.FindText("str_prisoner_tag_name", null).ToString();
			this.ItemLbl = GameTexts.FindText("str_item_tag_name", null).ToString();
			this.OtherLbl = GameTexts.FindText("str_other", null).ToString();
			this.CancelLbl = GameTexts.FindText("str_cancel", null).ToString();
			this.ResetLbl = GameTexts.FindText("str_reset", null).ToString();
			this.DiplomaticLbl = GameTexts.FindText("str_diplomatic_group", null).ToString();
			this.AutoBalanceHint.HintText = new TextObject("{=Ve5jkJqf}Auto Offer", null);
		}

		// Token: 0x04000E48 RID: 3656
		private readonly List<Dictionary<BarterGroup, MBBindingList<BarterItemVM>>> _barterList;

		// Token: 0x04000E49 RID: 3657
		private readonly List<MBBindingList<BarterItemVM>> _offerList;

		// Token: 0x04000E4A RID: 3658
		private readonly Dictionary<BarterGroup, MBBindingList<BarterItemVM>> _leftList;

		// Token: 0x04000E4B RID: 3659
		private readonly Dictionary<BarterGroup, MBBindingList<BarterItemVM>> _rightList;

		// Token: 0x04000E4C RID: 3660
		private readonly bool _isPlayerOfferer;

		// Token: 0x04000E4D RID: 3661
		private readonly BarterManager _barter;

		// Token: 0x04000E4E RID: 3662
		private readonly CharacterObject _otherCharacter;

		// Token: 0x04000E4F RID: 3663
		private readonly PartyBase _otherParty;

		// Token: 0x04000E50 RID: 3664
		private readonly BarterData _barterData;

		// Token: 0x04000E51 RID: 3665
		private string _fiefLbl;

		// Token: 0x04000E52 RID: 3666
		private string _prisonerLbl;

		// Token: 0x04000E53 RID: 3667
		private string _itemLbl;

		// Token: 0x04000E54 RID: 3668
		private string _otherLbl;

		// Token: 0x04000E55 RID: 3669
		private string _cancelLbl;

		// Token: 0x04000E56 RID: 3670
		private string _resetLbl;

		// Token: 0x04000E57 RID: 3671
		private string _offerLbl;

		// Token: 0x04000E58 RID: 3672
		private string _diplomaticLbl;

		// Token: 0x04000E59 RID: 3673
		private HintViewModel _autoBalanceHint;

		// Token: 0x04000E5A RID: 3674
		private HeroVM _leftHero;

		// Token: 0x04000E5B RID: 3675
		private HeroVM _rightHero;

		// Token: 0x04000E5C RID: 3676
		private string _leftNameLbl;

		// Token: 0x04000E5D RID: 3677
		private string _rightNameLbl;

		// Token: 0x04000E5E RID: 3678
		private MBBindingList<BarterItemVM> _leftFiefList;

		// Token: 0x04000E5F RID: 3679
		private MBBindingList<BarterItemVM> _rightFiefList;

		// Token: 0x04000E60 RID: 3680
		private MBBindingList<BarterItemVM> _leftPrisonerList;

		// Token: 0x04000E61 RID: 3681
		private MBBindingList<BarterItemVM> _rightPrisonerList;

		// Token: 0x04000E62 RID: 3682
		private MBBindingList<BarterItemVM> _leftItemList;

		// Token: 0x04000E63 RID: 3683
		private MBBindingList<BarterItemVM> _rightItemList;

		// Token: 0x04000E64 RID: 3684
		private MBBindingList<BarterItemVM> _leftOtherList;

		// Token: 0x04000E65 RID: 3685
		private MBBindingList<BarterItemVM> _rightOtherList;

		// Token: 0x04000E66 RID: 3686
		private MBBindingList<BarterItemVM> _leftDiplomaticList;

		// Token: 0x04000E67 RID: 3687
		private MBBindingList<BarterItemVM> _rightDiplomaticList;

		// Token: 0x04000E68 RID: 3688
		private MBBindingList<BarterItemVM> _leftGoldList;

		// Token: 0x04000E69 RID: 3689
		private MBBindingList<BarterItemVM> _rightGoldList;

		// Token: 0x04000E6A RID: 3690
		private MBBindingList<BarterItemVM> _leftOfferList;

		// Token: 0x04000E6B RID: 3691
		private MBBindingList<BarterItemVM> _rightOfferList;

		// Token: 0x04000E6C RID: 3692
		private int _leftMaxGold;

		// Token: 0x04000E6D RID: 3693
		private int _rightMaxGold;

		// Token: 0x04000E6E RID: 3694
		private bool _initializationIsOver;

		// Token: 0x04000E6F RID: 3695
		private bool _isOfferDisabled;

		// Token: 0x04000E70 RID: 3696
		private int _resultBarOffererPercentage = -1;

		// Token: 0x04000E71 RID: 3697
		private int _resultBarOtherPercentage = -1;

		// Token: 0x04000E72 RID: 3698
		private InputKeyItemVM _resetInputKey;

		// Token: 0x04000E73 RID: 3699
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000E74 RID: 3700
		private InputKeyItemVM _cancelInputKey;
	}
}
