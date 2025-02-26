﻿using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions
{
	// Token: 0x0200001C RID: 28
	[DefaultView]
	public class MissionSettlementPrepareView : MissionView
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00009B5B File Offset: 0x00007D5B
		public override void AfterStart()
		{
			base.AfterStart();
			this.SetOwnerBanner();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00009B6C File Offset: 0x00007D6C
		private void SetOwnerBanner()
		{
			Campaign campaign = Campaign.Current;
			if (campaign != null && campaign.GameMode == CampaignGameMode.Campaign)
			{
				Settlement currentSettlement = Settlement.CurrentSettlement;
				bool flag;
				if (currentSettlement == null)
				{
					flag = (null != null);
				}
				else
				{
					Clan ownerClan = currentSettlement.OwnerClan;
					flag = (((ownerClan != null) ? ownerClan.Banner : null) != null);
				}
				if (flag && base.Mission.Scene != null)
				{
					bool flag2 = false;
					foreach (GameEntity gameEntity in base.Mission.Scene.FindEntitiesWithTag("bd_banner_b"))
					{
						Action<Texture> setAction = delegate(Texture tex)
						{
							Material material = Mesh.GetFromResource("bd_banner_b").GetMaterial();
							uint num = (uint)material.GetShader().GetMaterialShaderFlagMask("use_tableau_blending", true);
							ulong shaderFlags = material.GetShaderFlags();
							material.SetShaderFlags(shaderFlags | (ulong)num);
							material.SetTexture(Material.MBTextureType.DiffuseMap2, tex);
						};
						Settlement.CurrentSettlement.OwnerClan.Banner.GetTableauTextureLarge(setAction);
						flag2 = true;
					}
					if (flag2)
					{
						base.Mission.Scene.SetClothSimulationState(false);
					}
				}
			}
		}
	}
}
