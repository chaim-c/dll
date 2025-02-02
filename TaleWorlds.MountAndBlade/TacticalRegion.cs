using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000355 RID: 853
	public class TacticalRegion : MissionObject
	{
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x000BF51B File Offset: 0x000BD71B
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x000BF523 File Offset: 0x000BD723
		public WorldPosition Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000BF52C File Offset: 0x000BD72C
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x000BF534 File Offset: 0x000BD734
		public List<TacticalPosition> LinkedTacticalPositions
		{
			get
			{
				return this._linkedTacticalPositions;
			}
			set
			{
				this._linkedTacticalPositions = value;
			}
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000BF53D File Offset: 0x000BD73D
		protected internal override void OnInit()
		{
			base.OnInit();
			this._position = new WorldPosition(base.GameEntity.GetScenePointer(), base.GameEntity.GlobalPosition);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000BF568 File Offset: 0x000BD768
		public override void AfterMissionStart()
		{
			base.AfterMissionStart();
			this._linkedTacticalPositions = new List<TacticalPosition>();
			this._linkedTacticalPositions = (from c in base.GameEntity.GetChildren()
			where c.HasScriptOfType<TacticalPosition>()
			select c.GetFirstScriptOfType<TacticalPosition>()).ToList<TacticalPosition>();
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000BF5E4 File Offset: 0x000BD7E4
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this._position = new WorldPosition(base.GameEntity.GetScenePointer(), UIntPtr.Zero, base.GameEntity.GlobalPosition, false);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000BF613 File Offset: 0x000BD813
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this._position.SetVec3(UIntPtr.Zero, base.GameEntity.GlobalPosition, false);
			if (this.IsEditorDebugRingVisible)
			{
				MBEditor.HelpersEnabled();
			}
		}

		// Token: 0x040013B7 RID: 5047
		public bool IsEditorDebugRingVisible;

		// Token: 0x040013B8 RID: 5048
		private WorldPosition _position;

		// Token: 0x040013B9 RID: 5049
		public float radius = 1f;

		// Token: 0x040013BA RID: 5050
		private List<TacticalPosition> _linkedTacticalPositions;

		// Token: 0x040013BB RID: 5051
		public TacticalRegion.TacticalRegionTypeEnum tacticalRegionType;

		// Token: 0x02000610 RID: 1552
		public enum TacticalRegionTypeEnum
		{
			// Token: 0x04001F8C RID: 8076
			Forest,
			// Token: 0x04001F8D RID: 8077
			DifficultTerrain,
			// Token: 0x04001F8E RID: 8078
			Opening
		}
	}
}
