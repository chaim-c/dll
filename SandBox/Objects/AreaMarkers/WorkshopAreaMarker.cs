using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Objects;

namespace SandBox.Objects.AreaMarkers
{
	// Token: 0x02000042 RID: 66
	public class WorkshopAreaMarker : AreaMarker
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000EC9A File Offset: 0x0000CE9A
		public override string Tag
		{
			get
			{
				Workshop workshop = this.GetWorkshop();
				if (workshop == null)
				{
					return null;
				}
				return workshop.Tag;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		public Workshop GetWorkshop()
		{
			Workshop result = null;
			Settlement settlement = PlayerEncounter.LocationEncounter.Settlement;
			if (settlement != null && settlement.IsTown && settlement.Town.Workshops.Length > this.AreaIndex && this.AreaIndex > 0)
			{
				result = settlement.Town.Workshops[this.AreaIndex];
			}
			return result;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000ED08 File Offset: 0x0000CF08
		protected override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (MBEditor.HelpersEnabled() && this.CheckToggle)
			{
				float distanceSquared = this.AreaRadius * this.AreaRadius;
				List<GameEntity> list = new List<GameEntity>();
				base.Scene.GetEntities(ref list);
				foreach (GameEntity gameEntity in list)
				{
					gameEntity.HasTag("shop_prop");
				}
				foreach (UsableMachine usableMachine in (from x in list.FindAllWithType<UsableMachine>()
				where x.GameEntity.GlobalPosition.DistanceSquared(this.GameEntity.GlobalPosition) <= distanceSquared
				select x).ToList<UsableMachine>())
				{
				}
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		public WorkshopType GetWorkshopType()
		{
			Workshop workshop = this.GetWorkshop();
			if (workshop == null)
			{
				return null;
			}
			return workshop.WorkshopType;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000EE0F File Offset: 0x0000D00F
		public override TextObject GetName()
		{
			Workshop workshop = this.GetWorkshop();
			if (workshop == null)
			{
				return null;
			}
			return workshop.Name;
		}
	}
}
