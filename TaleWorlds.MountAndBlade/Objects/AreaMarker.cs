using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x02000387 RID: 903
	public class AreaMarker : MissionObject, ITrackableBase
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000CC278 File Offset: 0x000CA478
		public virtual string Tag
		{
			get
			{
				return "area_marker_" + this.AreaIndex;
			}
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000CC28F File Offset: 0x000CA48F
		protected internal override void OnEditorTick(float dt)
		{
			if (this.CheckToggle)
			{
				MBEditor.HelpersEnabled();
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000CC29F File Offset: 0x000CA49F
		protected internal override void OnEditorInit()
		{
			this.CheckToggle = false;
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000CC2A8 File Offset: 0x000CA4A8
		public bool IsPositionInRange(Vec3 position)
		{
			return position.DistanceSquared(base.GameEntity.GlobalPosition) <= this.AreaRadius * this.AreaRadius;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000CC2D0 File Offset: 0x000CA4D0
		public virtual List<UsableMachine> GetUsableMachinesInRange(string excludeTag = null)
		{
			float distanceSquared = this.AreaRadius * this.AreaRadius;
			return (from x in Mission.Current.ActiveMissionObjects.FindAllWithType<UsableMachine>()
			where !x.IsDeactivated && x.GameEntity.GlobalPosition.DistanceSquared(this.GameEntity.GlobalPosition) <= distanceSquared && !x.GameEntity.HasTag(excludeTag)
			select x).ToList<UsableMachine>();
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000CC32C File Offset: 0x000CA52C
		public virtual List<UsableMachine> GetUsableMachinesWithTagInRange(string tag)
		{
			float distanceSquared = this.AreaRadius * this.AreaRadius;
			return (from x in Mission.Current.ActiveMissionObjects.FindAllWithType<UsableMachine>()
			where x.GameEntity.GlobalPosition.DistanceSquared(this.GameEntity.GlobalPosition) <= distanceSquared && x.GameEntity.HasTag(tag)
			select x).ToList<UsableMachine>();
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000CC388 File Offset: 0x000CA588
		public virtual List<GameEntity> GetGameEntitiesWithTagInRange(string tag)
		{
			float distanceSquared = this.AreaRadius * this.AreaRadius;
			return (from x in Mission.Current.Scene.FindEntitiesWithTag(tag)
			where x.GlobalPosition.DistanceSquared(this.GameEntity.GlobalPosition) <= distanceSquared && x.HasTag(tag)
			select x).ToList<GameEntity>();
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000CC3E7 File Offset: 0x000CA5E7
		public virtual TextObject GetName()
		{
			return new TextObject(base.GameEntity.Name, null);
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000CC3FA File Offset: 0x000CA5FA
		public virtual Vec3 GetPosition()
		{
			return base.GameEntity.GlobalPosition;
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000CC408 File Offset: 0x000CA608
		public float GetTrackDistanceToMainAgent()
		{
			if (Agent.Main == null)
			{
				return -1f;
			}
			return this.GetPosition().Distance(Agent.Main.Position);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000CC43A File Offset: 0x000CA63A
		public bool CheckTracked(BasicCharacterObject basicCharacter)
		{
			return false;
		}

		// Token: 0x0400152B RID: 5419
		public float AreaRadius = 3f;

		// Token: 0x0400152C RID: 5420
		public int AreaIndex;

		// Token: 0x0400152D RID: 5421
		public bool CheckToggle;
	}
}
