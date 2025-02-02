using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace SandBox.View.Map
{
	// Token: 0x02000057 RID: 87
	public class PartyVisualManager : CampaignEntityVisualComponent
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00022228 File Offset: 0x00020428
		public static PartyVisualManager Current
		{
			get
			{
				return Campaign.Current.GetEntityComponent<PartyVisualManager>();
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00022234 File Offset: 0x00020434
		protected override void OnInitialize()
		{
			base.OnInitialize();
			foreach (MobileParty mobileParty in MobileParty.All)
			{
				this.AddNewPartyVisualForParty(mobileParty.Party);
			}
			foreach (Settlement settlement in Settlement.All)
			{
				this.AddNewPartyVisualForParty(settlement.Party);
			}
			CampaignEvents.MobilePartyCreated.AddNonSerializedListener(this, new Action<MobileParty>(this.OnMobilePartyCreated));
			CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.OnMobilePartyDestroyed));
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00022308 File Offset: 0x00020508
		private void OnMobilePartyDestroyed(MobileParty mobileParty, PartyBase destroyerParty)
		{
			this._partiesAndVisuals[mobileParty.Party].OnPartyRemoved();
			this._visualsFlattened.Remove(this._partiesAndVisuals[mobileParty.Party]);
			this._partiesAndVisuals.Remove(mobileParty.Party);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0002235A File Offset: 0x0002055A
		private void OnMobilePartyCreated(MobileParty mobileParty)
		{
			this.AddNewPartyVisualForParty(mobileParty.Party);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00022368 File Offset: 0x00020568
		private void AddNewPartyVisualForParty(PartyBase partyBase)
		{
			PartyVisual partyVisual = new PartyVisual(partyBase);
			partyVisual.OnStartup();
			this._partiesAndVisuals.Add(partyBase, partyVisual);
			this._visualsFlattened.Add(partyVisual);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0002239B File Offset: 0x0002059B
		public PartyVisual GetVisualOfParty(PartyBase partyBase)
		{
			return this._partiesAndVisuals[partyBase];
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000223AC File Offset: 0x000205AC
		public void OnFinalized()
		{
			foreach (PartyVisual partyVisual in this._partiesAndVisuals.Values)
			{
				partyVisual.ReleaseResources();
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00022404 File Offset: 0x00020604
		public override void OnTick(float realDt, float dt)
		{
			this._dirtyPartyVisualCount = -1;
			TWParallel.For(0, this._visualsFlattened.Count, delegate(int startInclusive, int endExclusive)
			{
				for (int k = startInclusive; k < endExclusive; k++)
				{
					this._visualsFlattened[k].Tick(dt, ref this._dirtyPartyVisualCount, ref this._dirtyPartiesList);
				}
			}, 16);
			for (int i = 0; i < this._dirtyPartyVisualCount + 1; i++)
			{
				this._dirtyPartiesList[i].ValidateIsDirty(realDt, dt);
			}
			for (int j = this._fadingPartiesFlatten.Count - 1; j >= 0; j--)
			{
				this._fadingPartiesFlatten[j].TickFadingState(realDt, dt);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000224A2 File Offset: 0x000206A2
		internal void RegisterFadingVisual(PartyVisual visual)
		{
			if (!this._fadingPartiesSet.Contains(visual))
			{
				this._fadingPartiesFlatten.Add(visual);
				this._fadingPartiesSet.Add(visual);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000224CC File Offset: 0x000206CC
		internal void UnRegisterFadingVisual(PartyVisual visual)
		{
			if (this._fadingPartiesSet.Contains(visual))
			{
				int index = this._fadingPartiesFlatten.IndexOf(visual);
				this._fadingPartiesFlatten[index] = this._fadingPartiesFlatten[this._fadingPartiesFlatten.Count - 1];
				this._fadingPartiesFlatten.Remove(this._fadingPartiesFlatten[this._fadingPartiesFlatten.Count - 1]);
				this._fadingPartiesSet.Remove(visual);
			}
		}

		// Token: 0x04000234 RID: 564
		private readonly Dictionary<PartyBase, PartyVisual> _partiesAndVisuals = new Dictionary<PartyBase, PartyVisual>();

		// Token: 0x04000235 RID: 565
		private readonly List<PartyVisual> _visualsFlattened = new List<PartyVisual>();

		// Token: 0x04000236 RID: 566
		private int _dirtyPartyVisualCount;

		// Token: 0x04000237 RID: 567
		private PartyVisual[] _dirtyPartiesList = new PartyVisual[2500];

		// Token: 0x04000238 RID: 568
		private readonly List<PartyVisual> _fadingPartiesFlatten = new List<PartyVisual>();

		// Token: 0x04000239 RID: 569
		private readonly HashSet<PartyVisual> _fadingPartiesSet = new HashSet<PartyVisual>();
	}
}
