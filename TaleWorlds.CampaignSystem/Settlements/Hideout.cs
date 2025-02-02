﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Settlements
{
	// Token: 0x02000356 RID: 854
	public class Hideout : SettlementComponent, ISpottable
	{
		// Token: 0x060030C6 RID: 12486 RVA: 0x000CE423 File Offset: 0x000CC623
		internal static void AutoGeneratedStaticCollectObjectsHideout(object o, List<object> collectedObjects)
		{
			((Hideout)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000CE431 File Offset: 0x000CC631
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this._nextPossibleAttackTime, collectedObjects);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000CE44B File Offset: 0x000CC64B
		internal static object AutoGeneratedGetMemberValue_nextPossibleAttackTime(object o)
		{
			return ((Hideout)o)._nextPossibleAttackTime;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000CE45D File Offset: 0x000CC65D
		internal static object AutoGeneratedGetMemberValue_isSpotted(object o)
		{
			return ((Hideout)o)._isSpotted;
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000CE46F File Offset: 0x000CC66F
		public CampaignTime NextPossibleAttackTime
		{
			get
			{
				return this._nextPossibleAttackTime;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000CE477 File Offset: 0x000CC677
		public static MBReadOnlyList<Hideout> All
		{
			get
			{
				return Campaign.Current.AllHideouts;
			}
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000CE483 File Offset: 0x000CC683
		public void UpdateNextPossibleAttackTime()
		{
			this._nextPossibleAttackTime = CampaignTime.Now + CampaignTime.Hours(12f);
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000CE4A0 File Offset: 0x000CC6A0
		public bool IsInfested
		{
			get
			{
				return base.Owner.Settlement.Parties.CountQ((MobileParty x) => x.IsBandit) >= Campaign.Current.Models.BanditDensityModel.NumberOfMinimumBanditPartiesInAHideoutToInfestIt;
			}
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000CE4FA File Offset: 0x000CC6FA
		public IEnumerable<PartyBase> GetDefenderParties(MapEvent.BattleTypes battleType)
		{
			yield return base.Settlement.Party;
			foreach (MobileParty mobileParty in base.Settlement.Parties)
			{
				if (mobileParty.IsBandit || mobileParty.IsBanditBossParty)
				{
					yield return mobileParty.Party;
				}
			}
			List<MobileParty>.Enumerator enumerator = default(List<MobileParty>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000CE50C File Offset: 0x000CC70C
		public PartyBase GetNextDefenderParty(ref int partyIndex, MapEvent.BattleTypes battleType)
		{
			partyIndex++;
			if (partyIndex == 0)
			{
				return base.Settlement.Party;
			}
			for (int i = partyIndex - 1; i < base.Settlement.Parties.Count; i++)
			{
				MobileParty mobileParty = base.Settlement.Parties[i];
				if (mobileParty.IsBandit || mobileParty.IsBanditBossParty)
				{
					partyIndex = i + 1;
					return mobileParty.Party;
				}
			}
			return null;
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000CE57C File Offset: 0x000CC77C
		// (set) Token: 0x060030D1 RID: 12497 RVA: 0x000CE584 File Offset: 0x000CC784
		public string SceneName { get; private set; }

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000CE590 File Offset: 0x000CC790
		public IFaction MapFaction
		{
			get
			{
				foreach (MobileParty mobileParty in base.Settlement.Parties)
				{
					if (mobileParty.IsBandit)
					{
						return mobileParty.ActualClan;
					}
				}
				foreach (Clan clan in Clan.All)
				{
					if (clan.IsBanditFaction)
					{
						return clan;
					}
				}
				return null;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000CE640 File Offset: 0x000CC840
		// (set) Token: 0x060030D4 RID: 12500 RVA: 0x000CE648 File Offset: 0x000CC848
		public bool IsSpotted
		{
			get
			{
				return this._isSpotted;
			}
			set
			{
				this._isSpotted = value;
			}
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000CE651 File Offset: 0x000CC851
		public void SetScene(string sceneName)
		{
			this.SceneName = sceneName;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000CE65A File Offset: 0x000CC85A
		public Hideout()
		{
			this.IsSpotted = false;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000CE669 File Offset: 0x000CC869
		public override void OnPartyEntered(MobileParty mobileParty)
		{
			base.OnPartyEntered(mobileParty);
			this.UpdateOwnership();
			if (mobileParty.MapFaction.IsBanditFaction)
			{
				mobileParty.BanditPartyComponent.SetHomeHideout(base.Owner.Settlement.Hideout);
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000CE6A0 File Offset: 0x000CC8A0
		public override void OnPartyLeft(MobileParty mobileParty)
		{
			this.UpdateOwnership();
			if (base.Owner.Settlement.Parties.Count == 0)
			{
				this.OnHideoutIsEmpty();
			}
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000CE6C5 File Offset: 0x000CC8C5
		public override void OnRelatedPartyRemoved(MobileParty mobileParty)
		{
			if (base.Owner.Settlement.Parties.Count == 0)
			{
				this.OnHideoutIsEmpty();
			}
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000CE6E4 File Offset: 0x000CC8E4
		private void OnHideoutIsEmpty()
		{
			this.IsSpotted = false;
			base.Owner.Settlement.IsVisible = false;
			CampaignEventDispatcher.Instance.OnHideoutDeactivated(base.Settlement);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000CE70E File Offset: 0x000CC90E
		public override void OnInit()
		{
			base.Owner.Settlement.IsVisible = false;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000CE724 File Offset: 0x000CC924
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.BackgroundCropPosition = float.Parse(node.Attributes["background_crop_position"].Value);
			base.BackgroundMeshName = node.Attributes["background_mesh"].Value;
			base.WaitMeshName = node.Attributes["wait_mesh"].Value;
			base.Deserialize(objectManager, node);
			if (node.Attributes["scene_name"] != null)
			{
				this.SceneName = node.Attributes["scene_name"].InnerText;
			}
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000CE7BC File Offset: 0x000CC9BC
		private void UpdateOwnership()
		{
			if (base.Owner.MemberRoster.Count == 0 || base.Owner.Settlement.Parties.All((MobileParty x) => x.Party.Owner != base.Owner.Owner))
			{
				base.Owner.Settlement.Party.SetVisualAsDirty();
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000CE813 File Offset: 0x000CCA13
		protected override void OnInventoryUpdated(ItemRosterElement item, int count)
		{
		}

		// Token: 0x04000FE8 RID: 4072
		[SaveableField(200)]
		private CampaignTime _nextPossibleAttackTime;

		// Token: 0x04000FE9 RID: 4073
		[SaveableField(201)]
		private bool _isSpotted;
	}
}
