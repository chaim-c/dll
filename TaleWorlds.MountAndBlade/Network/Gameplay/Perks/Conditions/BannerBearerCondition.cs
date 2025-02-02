using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Network.Gameplay.Perks.Conditions
{
	// Token: 0x020003A0 RID: 928
	public class BannerBearerCondition : MPPerkCondition
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000D0159 File Offset: 0x000CE359
		public override MPPerkCondition.PerkEventFlags EventFlags
		{
			get
			{
				return MPPerkCondition.PerkEventFlags.AliveBotCountChange | MPPerkCondition.PerkEventFlags.BannerPickUp | MPPerkCondition.PerkEventFlags.BannerDrop | MPPerkCondition.PerkEventFlags.SpawnEnd;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x000D0160 File Offset: 0x000CE360
		public override bool IsPeerCondition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000D0163 File Offset: 0x000CE363
		protected BannerBearerCondition()
		{
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000D016B File Offset: 0x000CE36B
		protected override void Deserialize(XmlNode node)
		{
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000D0170 File Offset: 0x000CE370
		public override bool Check(MissionPeer peer)
		{
			Formation formation = (peer != null) ? peer.ControlledFormation : null;
			if (formation != null && MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
			{
				using (List<IFormationUnit>.Enumerator enumerator = formation.Arrangement.GetAllUnits().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent;
						if ((agent = (enumerator.Current as Agent)) != null && agent.IsActive())
						{
							MissionWeapon missionWeapon = agent.Equipment[EquipmentIndex.ExtraWeaponSlot];
							if (!missionWeapon.IsEmpty && missionWeapon.Item.ItemType == ItemObject.ItemTypeEnum.Banner && new Banner(formation.BannerCode, peer.Team.Color, peer.Team.Color2).Serialize() == missionWeapon.Banner.Serialize())
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000D0260 File Offset: 0x000CE460
		public override bool Check(Agent agent)
		{
			agent = ((agent != null && agent.IsMount) ? agent.RiderAgent : agent);
			MissionPeer peer = ((agent != null) ? agent.MissionPeer : null) ?? ((agent != null) ? agent.OwningAgentMissionPeer : null);
			return this.Check(peer);
		}

		// Token: 0x040015AF RID: 5551
		protected static string StringType = "BannerBearer";
	}
}
