using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002FD RID: 765
	public class MPConditionalEffect
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000A0E72 File Offset: 0x0009F072
		public MBReadOnlyList<MPPerkCondition> Conditions { get; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000A0E7A File Offset: 0x0009F07A
		public MBReadOnlyList<MPPerkEffectBase> Effects { get; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000A0E84 File Offset: 0x0009F084
		public MPPerkCondition.PerkEventFlags EventFlags
		{
			get
			{
				MPPerkCondition.PerkEventFlags perkEventFlags = MPPerkCondition.PerkEventFlags.None;
				foreach (MPPerkCondition mpperkCondition in this.Conditions)
				{
					perkEventFlags |= mpperkCondition.EventFlags;
				}
				return perkEventFlags;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x000A0EDC File Offset: 0x0009F0DC
		public bool IsTickRequired
		{
			get
			{
				using (List<MPPerkEffectBase>.Enumerator enumerator = this.Effects.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsTickRequired)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000A0F38 File Offset: 0x0009F138
		public MPConditionalEffect(List<string> gameModes, XmlNode node)
		{
			MBList<MPPerkCondition> mblist = new MBList<MPPerkCondition>();
			MBList<MPPerkEffectBase> mblist2 = new MBList<MPPerkEffectBase>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Conditions")
				{
					using (IEnumerator enumerator2 = xmlNode.ChildNodes.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.NodeType == XmlNodeType.Element)
							{
								mblist.Add(MPPerkCondition.CreateFrom(gameModes, xmlNode2));
							}
						}
						continue;
					}
				}
				if (xmlNode.Name == "Effects")
				{
					foreach (object obj3 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						if (xmlNode3.NodeType == XmlNodeType.Element)
						{
							MPPerkEffect item = MPPerkEffect.CreateFrom(xmlNode3);
							mblist2.Add(item);
						}
					}
				}
			}
			this.Conditions = mblist;
			this.Effects = mblist2;
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000A109C File Offset: 0x0009F29C
		public bool Check(MissionPeer peer)
		{
			using (List<MPPerkCondition>.Enumerator enumerator = this.Conditions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Check(peer))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000A10F8 File Offset: 0x0009F2F8
		public bool Check(Agent agent)
		{
			using (List<MPPerkCondition>.Enumerator enumerator = this.Conditions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Check(agent))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000A1154 File Offset: 0x0009F354
		public void OnEvent(bool isWarmup, MissionPeer peer, MPConditionalEffect.ConditionalEffectContainer container)
		{
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0)
			{
				if (peer == null)
				{
					return;
				}
				Agent controlledAgent = peer.ControlledAgent;
				bool? flag = (controlledAgent != null) ? new bool?(controlledAgent.IsActive()) : null;
				bool flag2 = true;
				if (!(flag.GetValueOrDefault() == flag2 & flag != null))
				{
					return;
				}
			}
			bool flag3 = true;
			foreach (MPPerkCondition mpperkCondition in this.Conditions)
			{
				if (mpperkCondition.IsPeerCondition && !mpperkCondition.Check(peer))
				{
					flag3 = false;
					break;
				}
			}
			if (!flag3)
			{
				if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
				{
					MBReadOnlyList<IFormationUnit> mbreadOnlyList;
					if (peer == null)
					{
						mbreadOnlyList = null;
					}
					else
					{
						Formation controlledFormation = peer.ControlledFormation;
						mbreadOnlyList = ((controlledFormation != null) ? controlledFormation.Arrangement.GetAllUnits() : null);
					}
					MBReadOnlyList<IFormationUnit> mbreadOnlyList2 = mbreadOnlyList;
					if (mbreadOnlyList2 == null)
					{
						return;
					}
					using (List<IFormationUnit>.Enumerator enumerator2 = mbreadOnlyList2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Agent agent;
							if ((agent = (enumerator2.Current as Agent)) != null && agent.IsActive())
							{
								this.UpdateAgentState(isWarmup, container, agent, false);
							}
						}
						return;
					}
				}
				this.UpdateAgentState(isWarmup, container, peer.ControlledAgent, false);
				return;
			}
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
			{
				MBReadOnlyList<IFormationUnit> mbreadOnlyList3;
				if (peer == null)
				{
					mbreadOnlyList3 = null;
				}
				else
				{
					Formation controlledFormation2 = peer.ControlledFormation;
					mbreadOnlyList3 = ((controlledFormation2 != null) ? controlledFormation2.Arrangement.GetAllUnits() : null);
				}
				MBReadOnlyList<IFormationUnit> mbreadOnlyList4 = mbreadOnlyList3;
				if (mbreadOnlyList4 == null)
				{
					return;
				}
				using (List<IFormationUnit>.Enumerator enumerator2 = mbreadOnlyList4.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Agent agent2;
						if ((agent2 = (enumerator2.Current as Agent)) != null && agent2.IsActive())
						{
							bool state = true;
							foreach (MPPerkCondition mpperkCondition2 in this.Conditions)
							{
								if (!mpperkCondition2.IsPeerCondition && !mpperkCondition2.Check(agent2))
								{
									state = false;
									break;
								}
							}
							this.UpdateAgentState(isWarmup, container, agent2, state);
						}
					}
					return;
				}
			}
			bool state2 = true;
			foreach (MPPerkCondition mpperkCondition3 in this.Conditions)
			{
				if (!mpperkCondition3.IsPeerCondition && !mpperkCondition3.Check(peer.ControlledAgent))
				{
					state2 = false;
					break;
				}
			}
			this.UpdateAgentState(isWarmup, container, peer.ControlledAgent, state2);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000A13F4 File Offset: 0x0009F5F4
		public void OnEvent(bool isWarmup, Agent agent, MPConditionalEffect.ConditionalEffectContainer container)
		{
			if (agent != null)
			{
				bool state = true;
				using (List<MPPerkCondition>.Enumerator enumerator = this.Conditions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.Check(agent))
						{
							state = false;
							break;
						}
					}
				}
				this.UpdateAgentState(isWarmup, container, agent, state);
			}
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000A145C File Offset: 0x0009F65C
		public void OnTick(bool isWarmup, MissionPeer peer, int tickCount)
		{
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) <= 0)
			{
				if (peer == null)
				{
					return;
				}
				Agent controlledAgent = peer.ControlledAgent;
				bool? flag = (controlledAgent != null) ? new bool?(controlledAgent.IsActive()) : null;
				bool flag2 = true;
				if (!(flag.GetValueOrDefault() == flag2 & flag != null))
				{
					return;
				}
			}
			bool flag3 = true;
			foreach (MPPerkCondition mpperkCondition in this.Conditions)
			{
				if (mpperkCondition.IsPeerCondition && !mpperkCondition.Check(peer))
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
				{
					MBReadOnlyList<IFormationUnit> mbreadOnlyList;
					if (peer == null)
					{
						mbreadOnlyList = null;
					}
					else
					{
						Formation controlledFormation = peer.ControlledFormation;
						mbreadOnlyList = ((controlledFormation != null) ? controlledFormation.Arrangement.GetAllUnits() : null);
					}
					MBReadOnlyList<IFormationUnit> mbreadOnlyList2 = mbreadOnlyList;
					if (mbreadOnlyList2 == null)
					{
						return;
					}
					using (List<IFormationUnit>.Enumerator enumerator2 = mbreadOnlyList2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Agent agent;
							if ((agent = (enumerator2.Current as Agent)) != null && agent.IsActive())
							{
								bool flag4 = true;
								foreach (MPPerkCondition mpperkCondition2 in this.Conditions)
								{
									if (!mpperkCondition2.IsPeerCondition && !mpperkCondition2.Check(agent))
									{
										flag4 = false;
										break;
									}
								}
								if (flag4)
								{
									foreach (MPPerkEffectBase mpperkEffectBase in this.Effects)
									{
										if ((!isWarmup || !mpperkEffectBase.IsDisabledInWarmup) && mpperkEffectBase.IsTickRequired)
										{
											mpperkEffectBase.OnTick(agent, tickCount);
										}
									}
								}
							}
						}
						return;
					}
				}
				bool flag5 = true;
				foreach (MPPerkCondition mpperkCondition3 in this.Conditions)
				{
					if (!mpperkCondition3.IsPeerCondition && !mpperkCondition3.Check(peer.ControlledAgent))
					{
						flag5 = false;
						break;
					}
				}
				if (flag5)
				{
					foreach (MPPerkEffectBase mpperkEffectBase2 in this.Effects)
					{
						if ((!isWarmup || !mpperkEffectBase2.IsDisabledInWarmup) && mpperkEffectBase2.IsTickRequired)
						{
							mpperkEffectBase2.OnTick(peer.ControlledAgent, tickCount);
						}
					}
				}
			}
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000A171C File Offset: 0x0009F91C
		private void UpdateAgentState(bool isWarmup, MPConditionalEffect.ConditionalEffectContainer container, Agent agent, bool state)
		{
			if (container.GetState(this, agent) != state)
			{
				container.SetState(this, agent, state);
				foreach (MPPerkEffectBase mpperkEffectBase in this.Effects)
				{
					if (!isWarmup || !mpperkEffectBase.IsDisabledInWarmup)
					{
						mpperkEffectBase.OnUpdate(agent, state);
					}
				}
			}
		}

		// Token: 0x020005C3 RID: 1475
		public class ConditionalEffectContainer : List<MPConditionalEffect>
		{
			// Token: 0x06003B23 RID: 15139 RVA: 0x000E7E8C File Offset: 0x000E608C
			public ConditionalEffectContainer()
			{
			}

			// Token: 0x06003B24 RID: 15140 RVA: 0x000E7E94 File Offset: 0x000E6094
			public ConditionalEffectContainer(IEnumerable<MPConditionalEffect> conditionalEffects) : base(conditionalEffects)
			{
			}

			// Token: 0x06003B25 RID: 15141 RVA: 0x000E7EA0 File Offset: 0x000E60A0
			public bool GetState(MPConditionalEffect conditionalEffect, Agent agent)
			{
				ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState> conditionalWeakTable;
				MPConditionalEffect.ConditionalEffectContainer.ConditionState conditionState;
				return this._states != null && this._states.TryGetValue(conditionalEffect, out conditionalWeakTable) && conditionalWeakTable.TryGetValue(agent, out conditionState) && conditionState.IsSatisfied;
			}

			// Token: 0x06003B26 RID: 15142 RVA: 0x000E7ED8 File Offset: 0x000E60D8
			public void SetState(MPConditionalEffect conditionalEffect, Agent agent, bool state)
			{
				if (this._states == null)
				{
					this._states = new Dictionary<MPConditionalEffect, ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>>();
					ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState> conditionalWeakTable = new ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>();
					conditionalWeakTable.Add(agent, new MPConditionalEffect.ConditionalEffectContainer.ConditionState
					{
						IsSatisfied = state
					});
					this._states.Add(conditionalEffect, conditionalWeakTable);
					return;
				}
				ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState> conditionalWeakTable2;
				if (!this._states.TryGetValue(conditionalEffect, out conditionalWeakTable2))
				{
					conditionalWeakTable2 = new ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>();
					conditionalWeakTable2.Add(agent, new MPConditionalEffect.ConditionalEffectContainer.ConditionState
					{
						IsSatisfied = state
					});
					this._states.Add(conditionalEffect, conditionalWeakTable2);
					return;
				}
				MPConditionalEffect.ConditionalEffectContainer.ConditionState conditionState;
				if (!conditionalWeakTable2.TryGetValue(agent, out conditionState))
				{
					conditionalWeakTable2.Add(agent, new MPConditionalEffect.ConditionalEffectContainer.ConditionState
					{
						IsSatisfied = state
					});
					return;
				}
				conditionState.IsSatisfied = state;
			}

			// Token: 0x06003B27 RID: 15143 RVA: 0x000E7F7C File Offset: 0x000E617C
			public void ResetStates()
			{
				this._states = null;
			}

			// Token: 0x04001E77 RID: 7799
			private Dictionary<MPConditionalEffect, ConditionalWeakTable<Agent, MPConditionalEffect.ConditionalEffectContainer.ConditionState>> _states;

			// Token: 0x0200068B RID: 1675
			private class ConditionState
			{
				// Token: 0x17000A43 RID: 2627
				// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000EE1C7 File Offset: 0x000EC3C7
				// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000EE1CF File Offset: 0x000EC3CF
				public bool IsSatisfied { get; set; }
			}
		}
	}
}
