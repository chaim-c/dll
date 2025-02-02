using System;
using System.Collections.Generic;
using System.ComponentModel;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000044 RID: 68
	public class MissionAgentLabelView : MissionView
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0001B39E File Offset: 0x0001959E
		private OrderController PlayerOrderController
		{
			get
			{
				Team playerTeam = base.Mission.PlayerTeam;
				if (playerTeam == null)
				{
					return null;
				}
				return playerTeam.PlayerOrderController;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001B3B6 File Offset: 0x000195B6
		private SiegeWeaponController PlayerSiegeWeaponController
		{
			get
			{
				Team playerTeam = base.Mission.PlayerTeam;
				if (playerTeam == null)
				{
					return null;
				}
				return playerTeam.PlayerOrderController.SiegeWeaponController;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001B3D3 File Offset: 0x000195D3
		public MissionAgentLabelView()
		{
			this._agentMeshes = new Dictionary<Agent, MetaMesh>();
			this._labelMaterials = new Dictionary<Texture, Material>();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001B3F4 File Offset: 0x000195F4
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.Mission.Teams.OnPlayerTeamChanged += this.Mission_OnPlayerTeamChanged;
			base.Mission.OnMainAgentChanged += this.OnMainAgentChanged;
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			base.MissionScreen.OnSpectateAgentFocusIn += this.HandleSpectateAgentFocusIn;
			base.MissionScreen.OnSpectateAgentFocusOut += this.HandleSpectateAgentFocusOut;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001B488 File Offset: 0x00019688
		public override void AfterStart()
		{
			if (this.PlayerOrderController != null)
			{
				this.PlayerOrderController.OnSelectedFormationsChanged += this.OrderController_OnSelectedFormationsChanged;
				base.Mission.PlayerTeam.OnFormationsChanged += this.PlayerTeam_OnFormationsChanged;
			}
			BannerBearerLogic missionBehavior = base.Mission.GetMissionBehavior<BannerBearerLogic>();
			if (missionBehavior != null)
			{
				missionBehavior.OnBannerBearerAgentUpdated += this.BannerBearerLogic_OnBannerBearerAgentUpdated;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001B4F4 File Offset: 0x000196F4
		public override void OnMissionTick(float dt)
		{
			bool flag = this.IsOrderScreenVisible();
			bool flag2 = this.IsSiegeControllerScreenVisible();
			if (!flag && this._wasOrderScreenVisible)
			{
				this.SetHighlightForAgents(false, false, false);
			}
			if (!flag2 && this._wasSiegeControllerScreenVisible)
			{
				this.SetHighlightForAgents(false, true, false);
			}
			if (flag && !this._wasOrderScreenVisible)
			{
				this.SetHighlightForAgents(true, false, false);
			}
			if (flag2 && !this._wasSiegeControllerScreenVisible)
			{
				this.SetHighlightForAgents(true, true, false);
			}
			this._wasOrderScreenVisible = flag;
			this._wasSiegeControllerScreenVisible = flag2;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001B56D File Offset: 0x0001976D
		public override void OnRemoveBehavior()
		{
			this.UnregisterEvents();
			base.OnRemoveBehavior();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001B57B File Offset: 0x0001977B
		public override void OnMissionScreenFinalize()
		{
			this.UnregisterEvents();
			base.OnMissionScreenFinalize();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001B58C File Offset: 0x0001978C
		private void UnregisterEvents()
		{
			if (base.Mission != null)
			{
				base.Mission.Teams.OnPlayerTeamChanged -= this.Mission_OnPlayerTeamChanged;
				base.Mission.OnMainAgentChanged -= this.OnMainAgentChanged;
			}
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			if (base.MissionScreen != null)
			{
				base.MissionScreen.OnSpectateAgentFocusIn -= this.HandleSpectateAgentFocusIn;
				base.MissionScreen.OnSpectateAgentFocusOut -= this.HandleSpectateAgentFocusOut;
			}
			if (this.PlayerOrderController != null)
			{
				this.PlayerOrderController.OnSelectedFormationsChanged -= this.OrderController_OnSelectedFormationsChanged;
				if (base.Mission != null)
				{
					base.Mission.PlayerTeam.OnFormationsChanged -= this.PlayerTeam_OnFormationsChanged;
				}
			}
			BannerBearerLogic missionBehavior = base.Mission.GetMissionBehavior<BannerBearerLogic>();
			if (missionBehavior != null)
			{
				missionBehavior.OnBannerBearerAgentUpdated -= this.BannerBearerLogic_OnBannerBearerAgentUpdated;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001B68E File Offset: 0x0001988E
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			this.RemoveAgentLabel(affectedAgent);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001B697 File Offset: 0x00019897
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			this.InitAgentLabel(agent, banner);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001B6A4 File Offset: 0x000198A4
		public override void OnAssignPlayerAsSergeantOfFormation(Agent agent)
		{
			float friendlyTroopsBannerOpacity = BannerlordConfig.FriendlyTroopsBannerOpacity;
			this._agentMeshes[agent].SetVectorArgument2(30f, 0.4f, 0.44f, 1f * friendlyTroopsBannerOpacity);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001B6DE File Offset: 0x000198DE
		public override void OnClearScene()
		{
			this._agentMeshes.Clear();
			this._labelMaterials.Clear();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001B6F6 File Offset: 0x000198F6
		private void PlayerTeam_OnFormationsChanged(Team team, Formation formation)
		{
			if (this.IsOrderScreenVisible())
			{
				this.DehighlightAllAgents();
				this.SetHighlightForAgents(true, false, false);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001B710 File Offset: 0x00019910
		private void Mission_OnPlayerTeamChanged(Team previousTeam, Team currentTeam)
		{
			this.DehighlightAllAgents();
			this._wasOrderScreenVisible = false;
			if (((previousTeam != null) ? previousTeam.PlayerOrderController : null) != null)
			{
				previousTeam.PlayerOrderController.OnSelectedFormationsChanged -= this.OrderController_OnSelectedFormationsChanged;
				previousTeam.PlayerOrderController.SiegeWeaponController.OnSelectedSiegeWeaponsChanged -= this.PlayerSiegeWeaponController_OnSelectedSiegeWeaponsChanged;
			}
			if (this.PlayerOrderController != null)
			{
				this.PlayerOrderController.OnSelectedFormationsChanged += this.OrderController_OnSelectedFormationsChanged;
				this.PlayerSiegeWeaponController.OnSelectedSiegeWeaponsChanged += this.PlayerSiegeWeaponController_OnSelectedSiegeWeaponsChanged;
			}
			this.SetHighlightForAgents(true, false, true);
			foreach (Agent agent in base.Mission.Agents)
			{
				this.UpdateVisibilityOfAgentMesh(agent);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001B7F8 File Offset: 0x000199F8
		private void OrderController_OnSelectedFormationsChanged()
		{
			this.DehighlightAllAgents();
			if (this.IsOrderScreenVisible())
			{
				this.SetHighlightForAgents(true, false, false);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001B811 File Offset: 0x00019A11
		private void PlayerSiegeWeaponController_OnSelectedSiegeWeaponsChanged()
		{
			this.DehighlightAllAgents();
			this.SetHighlightForAgents(true, true, false);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001B824 File Offset: 0x00019A24
		public void OnAgentListSelectionChanged(bool selectionMode, List<Agent> affectedAgents)
		{
			foreach (Agent key in affectedAgents)
			{
				float num = selectionMode ? 1f : -1f;
				if (this._agentMeshes.ContainsKey(key))
				{
					MetaMesh metaMesh = this._agentMeshes[key];
					float friendlyTroopsBannerOpacity = BannerlordConfig.FriendlyTroopsBannerOpacity;
					metaMesh.SetVectorArgument2(30f, 0.4f, 0.44f, num * friendlyTroopsBannerOpacity);
				}
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		private void BannerBearerLogic_OnBannerBearerAgentUpdated(Agent agent, bool isBannerBearer)
		{
			this.RemoveAgentLabel(agent);
			this.InitAgentLabel(agent, null);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		private void RemoveAgentLabel(Agent agent)
		{
			if (agent.IsHuman && this._agentMeshes.ContainsKey(agent))
			{
				if (agent.AgentVisuals != null)
				{
					agent.AgentVisuals.ReplaceMeshWithMesh(this._agentMeshes[agent], null, BodyMeshTypes.Label);
				}
				this._agentMeshes.Remove(agent);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001B920 File Offset: 0x00019B20
		private void InitAgentLabel(Agent agent, Banner peerBanner = null)
		{
			if (agent.IsHuman)
			{
				Banner banner = peerBanner ?? agent.Origin.Banner;
				if (banner != null)
				{
					MetaMesh copy = MetaMesh.GetCopy("troop_banner_selection", false, true);
					Material tableauMaterial = Material.GetFromResource("agent_label_with_tableau");
					Texture tableauTextureSmall = banner.GetTableauTextureSmall(null);
					if (copy != null && tableauMaterial != null)
					{
						Texture fromResource = Texture.GetFromResource("banner_top_of_head");
						Material tableauMaterial2;
						if (this._labelMaterials.TryGetValue(tableauTextureSmall ?? fromResource, out tableauMaterial2))
						{
							tableauMaterial = tableauMaterial2;
						}
						else
						{
							tableauMaterial = tableauMaterial.CreateCopy();
							Action<Texture> setAction = delegate(Texture tex)
							{
								tableauMaterial.SetTexture(Material.MBTextureType.DiffuseMap, tex);
							};
							tableauTextureSmall = banner.GetTableauTextureSmall(setAction);
							tableauMaterial.SetTexture(Material.MBTextureType.DiffuseMap2, fromResource);
							this._labelMaterials.Add(tableauTextureSmall, tableauMaterial);
						}
						copy.SetMaterial(tableauMaterial);
						copy.SetVectorArgument(0.5f, 0.5f, 0.25f, 0.25f);
						copy.SetVectorArgument2(30f, 0.4f, 0.44f, -1f);
						agent.AgentVisuals.AddMultiMesh(copy, BodyMeshTypes.Label);
						this._agentMeshes.Add(agent, copy);
						this.UpdateVisibilityOfAgentMesh(agent);
						this.UpdateSelectionVisibility(agent, this._agentMeshes[agent], new bool?(false));
					}
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001BA88 File Offset: 0x00019C88
		private void UpdateVisibilityOfAgentMesh(Agent agent)
		{
			if (agent.IsActive() && this._agentMeshes.ContainsKey(agent))
			{
				bool flag = this.IsMeshVisibleForAgent(agent);
				this._agentMeshes[agent].SetVisibilityMask(flag ? VisibilityMaskFlags.Final : ((VisibilityMaskFlags)0U));
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001BACB File Offset: 0x00019CCB
		private bool IsMeshVisibleForAgent(Agent agent)
		{
			return this.IsAllyInAllyTeam(agent) && base.MissionScreen.LastFollowedAgent != agent && BannerlordConfig.FriendlyTroopsBannerOpacity > 0f && !base.MissionScreen.IsPhotoModeEnabled;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001BB00 File Offset: 0x00019D00
		private void OnUpdateOpacityValueOfAgentMesh(Agent agent)
		{
			if (agent.IsActive() && this._agentMeshes.ContainsKey(agent))
			{
				this._agentMeshes[agent].SetVectorArgument2(30f, 0.4f, 0.44f, -BannerlordConfig.FriendlyTroopsBannerOpacity);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001BB40 File Offset: 0x00019D40
		private bool IsAllyInAllyTeam(Agent agent)
		{
			if (((agent != null) ? agent.Team : null) != null && agent != base.Mission.MainAgent)
			{
				Team team = null;
				Team team2;
				if (GameNetwork.IsSessionActive)
				{
					team2 = (GameNetwork.IsMyPeerReady ? GameNetwork.MyPeer.GetComponent<MissionPeer>().Team : null);
				}
				else
				{
					team2 = base.Mission.PlayerTeam;
					team = base.Mission.PlayerAllyTeam;
				}
				return agent.Team == team2 || agent.Team == team;
			}
			return false;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001BBBC File Offset: 0x00019DBC
		private void OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			foreach (Agent agent in this._agentMeshes.Keys)
			{
				this.UpdateVisibilityOfAgentMesh(agent);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001BC14 File Offset: 0x00019E14
		private void HandleSpectateAgentFocusIn(Agent agent)
		{
			this.UpdateVisibilityOfAgentMesh(agent);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001BC1D File Offset: 0x00019E1D
		private void HandleSpectateAgentFocusOut(Agent agent)
		{
			this.UpdateVisibilityOfAgentMesh(agent);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001BC28 File Offset: 0x00019E28
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType optionType)
		{
			if (optionType == ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity)
			{
				foreach (Agent agent in base.Mission.Agents)
				{
					if (agent.IsHuman)
					{
						this.UpdateVisibilityOfAgentMesh(agent);
						if (this.IsMeshVisibleForAgent(agent))
						{
							this.OnUpdateOpacityValueOfAgentMesh(agent);
						}
					}
				}
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		private bool IsAgentListeningToOrders(Agent agent)
		{
			if (this.IsOrderScreenVisible() && agent.Formation != null && this.IsAllyInAllyTeam(agent))
			{
				Func<Agent, bool> <>9__0;
				foreach (Formation formation in this.PlayerOrderController.SelectedFormations)
				{
					Func<Agent, bool> function;
					if ((function = <>9__0) == null)
					{
						function = (<>9__0 = ((Agent unit) => unit == agent));
					}
					if (formation.HasUnitsWithCondition(function))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001BD50 File Offset: 0x00019F50
		private void UpdateSelectionVisibility(Agent agent, MetaMesh mesh, bool? visibility = null)
		{
			if (visibility == null)
			{
				visibility = new bool?(this.IsAgentListeningToOrders(agent));
			}
			float num = visibility.Value ? 1f : -1f;
			if (agent.MissionPeer == null)
			{
				float config = ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity);
				mesh.SetVectorArgument2(30f, 0.4f, 0.44f, num * config);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001BDB2 File Offset: 0x00019FB2
		private bool IsOrderScreenVisible()
		{
			return this.PlayerOrderController != null && base.MissionScreen.OrderFlag != null && base.MissionScreen.OrderFlag.IsVisible;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001BDDB File Offset: 0x00019FDB
		private bool IsSiegeControllerScreenVisible()
		{
			return this.PlayerOrderController != null && base.MissionScreen.OrderFlag != null && base.MissionScreen.OrderFlag.IsVisible;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001BE04 File Offset: 0x0001A004
		private void SetHighlightForAgents(bool highlight, bool useSiegeMachineUsers, bool useAllTeamAgents)
		{
			if (this.PlayerOrderController == null)
			{
				bool flag = base.Mission.PlayerTeam == null;
				Debug.Print(string.Format("PlayerOrderController is null and playerTeamIsNull: {0}", flag), 0, Debug.DebugColor.White, 17179869184UL);
			}
			if (useSiegeMachineUsers)
			{
				using (List<SiegeWeapon>.Enumerator enumerator = this.PlayerSiegeWeaponController.SelectedWeapons.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SiegeWeapon siegeWeapon = enumerator.Current;
						foreach (StandingPoint standingPoint in siegeWeapon.StandingPoints)
						{
							Agent userAgent = standingPoint.UserAgent;
							if (userAgent != null)
							{
								this.UpdateSelectionVisibility(userAgent, this._agentMeshes[userAgent], new bool?(highlight));
							}
						}
					}
					return;
				}
			}
			if (useAllTeamAgents)
			{
				if (this.PlayerOrderController.Owner != null)
				{
					Team team = this.PlayerOrderController.Owner.Team;
					if (team == null)
					{
						Debug.Print("PlayerOrderController.Owner.Team is null, overriding with Mission.Current.PlayerTeam", 0, Debug.DebugColor.White, 17179869184UL);
						team = Mission.Current.PlayerTeam;
					}
					using (List<Agent>.Enumerator enumerator3 = team.ActiveAgents.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							Agent agent2 = enumerator3.Current;
							this.UpdateSelectionVisibility(agent2, this._agentMeshes[agent2], new bool?(highlight));
						}
						return;
					}
				}
				Debug.Print("PlayerOrderController.Owner is null", 0, Debug.DebugColor.White, 17179869184UL);
				return;
			}
			Action<Agent> <>9__0;
			foreach (Formation formation in this.PlayerOrderController.SelectedFormations)
			{
				Action<Agent> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(Agent agent)
					{
						this.UpdateSelectionVisibility(agent, this._agentMeshes[agent], new bool?(highlight));
					});
				}
				formation.ApplyActionOnEachUnit(action, null);
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001C03C File Offset: 0x0001A23C
		private void DehighlightAllAgents()
		{
			foreach (KeyValuePair<Agent, MetaMesh> keyValuePair in this._agentMeshes)
			{
				this.UpdateSelectionVisibility(keyValuePair.Key, keyValuePair.Value, new bool?(false));
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		public override void OnAgentTeamChanged(Team prevTeam, Team newTeam, Agent agent)
		{
			this.UpdateVisibilityOfAgentMesh(agent);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsHuman)
				{
					this.UpdateVisibilityOfAgentMesh(agent);
				}
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001C118 File Offset: 0x0001A318
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsHuman)
				{
					this.UpdateVisibilityOfAgentMesh(agent);
				}
			}
		}

		// Token: 0x04000235 RID: 565
		private const float _highlightedLabelScaleFactor = 30f;

		// Token: 0x04000236 RID: 566
		private const float _labelBannerWidth = 0.4f;

		// Token: 0x04000237 RID: 567
		private const float _labelBlackBorderWidth = 0.44f;

		// Token: 0x04000238 RID: 568
		private readonly Dictionary<Agent, MetaMesh> _agentMeshes;

		// Token: 0x04000239 RID: 569
		private readonly Dictionary<Texture, Material> _labelMaterials;

		// Token: 0x0400023A RID: 570
		private bool _wasOrderScreenVisible;

		// Token: 0x0400023B RID: 571
		private bool _wasSiegeControllerScreenVisible;
	}
}
