using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Objects.Usables;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Objects;

namespace SandBox.Objects.AreaMarkers
{
	// Token: 0x02000041 RID: 65
	public class CommonAreaMarker : AreaMarker
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000EA85 File Offset: 0x0000CC85
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000EA8D File Offset: 0x0000CC8D
		public List<MatrixFrame> HiddenSpawnFrames { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000EA96 File Offset: 0x0000CC96
		public override string Tag
		{
			get
			{
				Alley alley = this.GetAlley();
				if (alley == null)
				{
					return null;
				}
				return alley.Tag;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000EAA9 File Offset: 0x0000CCA9
		protected override void OnInit()
		{
			this.HiddenSpawnFrames = new List<MatrixFrame>();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		public override List<UsableMachine> GetUsableMachinesInRange(string excludeTag = null)
		{
			List<UsableMachine> usableMachinesInRange = base.GetUsableMachinesInRange(null);
			for (int i = usableMachinesInRange.Count - 1; i >= 0; i--)
			{
				UsableMachine usableMachine = usableMachinesInRange[i];
				string[] tags = usableMachine.GameEntity.Tags;
				if (usableMachine.GameEntity.HasScriptOfType<Passage>() || (!tags.Contains("npc_common") && !tags.Contains("npc_common_limited") && !tags.Contains("sp_guard") && !tags.Contains("sp_guard_unarmed") && !tags.Contains("sp_notable")))
				{
					usableMachinesInRange.RemoveAt(i);
				}
			}
			List<GameEntity> list = Mission.Current.Scene.FindEntitiesWithTag("sp_common_hidden").ToList<GameEntity>();
			GameEntity gameEntity = null;
			float num = float.MaxValue;
			float num2 = this.AreaRadius * this.AreaRadius;
			for (int j = list.Count - 1; j >= 0; j--)
			{
				float num3 = list[j].GlobalPosition.DistanceSquared(base.GameEntity.GlobalPosition);
				if (num3 < num)
				{
					gameEntity = list[j];
					num = num3;
				}
				if (num3 < num2)
				{
					this.HiddenSpawnFrames.Add(list[j].GetGlobalFrame());
				}
			}
			if (this.HiddenSpawnFrames.IsEmpty<MatrixFrame>() && gameEntity != null)
			{
				this.HiddenSpawnFrames.Add(gameEntity.GetGlobalFrame());
			}
			return usableMachinesInRange;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public Alley GetAlley()
		{
			Alley result = null;
			Settlement settlement = PlayerEncounter.LocationEncounter.Settlement;
			if (settlement != null && ((settlement != null) ? settlement.Alleys : null) != null && this.AreaIndex > 0 && this.AreaIndex <= settlement.Alleys.Count)
			{
				result = settlement.Alleys[this.AreaIndex - 1];
			}
			return result;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000EC74 File Offset: 0x0000CE74
		public override TextObject GetName()
		{
			Alley alley = this.GetAlley();
			if (alley == null)
			{
				return null;
			}
			return alley.Name;
		}

		// Token: 0x040000F2 RID: 242
		public string Type = "";
	}
}
