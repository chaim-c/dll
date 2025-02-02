using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000354 RID: 852
	public class TacticalPosition : MissionObject
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000BF280 File Offset: 0x000BD480
		// (set) Token: 0x06002EB5 RID: 11957 RVA: 0x000BF288 File Offset: 0x000BD488
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

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000BF291 File Offset: 0x000BD491
		// (set) Token: 0x06002EB7 RID: 11959 RVA: 0x000BF299 File Offset: 0x000BD499
		public Vec2 Direction
		{
			get
			{
				return this._direction;
			}
			set
			{
				this._direction = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x000BF2A2 File Offset: 0x000BD4A2
		public float Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000BF2AA File Offset: 0x000BD4AA
		public float Slope
		{
			get
			{
				return this._slope;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x000BF2B2 File Offset: 0x000BD4B2
		public bool IsInsurmountable
		{
			get
			{
				return this._isInsurmountable;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000BF2BA File Offset: 0x000BD4BA
		public bool IsOuterEdge
		{
			get
			{
				return this._isOuterEdge;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000BF2C2 File Offset: 0x000BD4C2
		// (set) Token: 0x06002EBD RID: 11965 RVA: 0x000BF2CA File Offset: 0x000BD4CA
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

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x000BF2D3 File Offset: 0x000BD4D3
		public TacticalPosition.TacticalPositionTypeEnum TacticalPositionType
		{
			get
			{
				return this._tacticalPositionType;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000BF2DB File Offset: 0x000BD4DB
		public TacticalRegion.TacticalRegionTypeEnum TacticalRegionMembership
		{
			get
			{
				return this._tacticalRegionMembership;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x000BF2E3 File Offset: 0x000BD4E3
		public FormationAI.BehaviorSide TacticalPositionSide
		{
			get
			{
				return this._tacticalPositionSide;
			}
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000BF2EB File Offset: 0x000BD4EB
		public TacticalPosition()
		{
			this._width = 1f;
			this._slope = 0f;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000BF310 File Offset: 0x000BD510
		public TacticalPosition(WorldPosition position, Vec2 direction, float width, float slope = 0f, bool isInsurmountable = false, TacticalPosition.TacticalPositionTypeEnum tacticalPositionType = TacticalPosition.TacticalPositionTypeEnum.Regional, TacticalRegion.TacticalRegionTypeEnum tacticalRegionMembership = TacticalRegion.TacticalRegionTypeEnum.Opening)
		{
			this._position = position;
			this._direction = direction;
			this._width = width;
			this._slope = slope;
			this._isInsurmountable = isInsurmountable;
			this._tacticalPositionType = tacticalPositionType;
			this._tacticalRegionMembership = tacticalRegionMembership;
			this._tacticalPositionSide = FormationAI.BehaviorSide.BehaviorSideNotSet;
			this._isOuterEdge = false;
			this._linkedTacticalPositions = new List<TacticalPosition>();
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000BF378 File Offset: 0x000BD578
		protected internal override void OnInit()
		{
			base.OnInit();
			this._position = new WorldPosition(base.GameEntity.GetScenePointer(), base.GameEntity.GlobalPosition);
			this._direction = base.GameEntity.GetGlobalFrame().rotation.f.AsVec2.Normalized();
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000BF3D8 File Offset: 0x000BD5D8
		public override void AfterMissionStart()
		{
			base.AfterMissionStart();
			this._linkedTacticalPositions = (from c in base.GameEntity.GetChildren()
			where c.HasScriptOfType<TacticalPosition>()
			select c.GetFirstScriptOfType<TacticalPosition>()).ToList<TacticalPosition>();
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000BF449 File Offset: 0x000BD649
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.ApplyChangesFromEditor();
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000BF457 File Offset: 0x000BD657
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.ApplyChangesFromEditor();
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000BF468 File Offset: 0x000BD668
		private void ApplyChangesFromEditor()
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			if (this._width > 0f && this._width != this._oldWidth)
			{
				globalFrame.rotation.MakeUnit();
				globalFrame.rotation.ApplyScaleLocal(new Vec3(this._width, 1f, 1f, -1f));
				base.GameEntity.SetGlobalFrame(globalFrame);
				base.GameEntity.UpdateTriadFrameForEditorForAllChildren();
				this._oldWidth = this._width;
			}
			this._direction = globalFrame.rotation.f.AsVec2.Normalized();
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000BF512 File Offset: 0x000BD712
		public void SetWidth(float width)
		{
			this._width = width;
		}

		// Token: 0x040013AC RID: 5036
		private WorldPosition _position;

		// Token: 0x040013AD RID: 5037
		private Vec2 _direction;

		// Token: 0x040013AE RID: 5038
		private float _oldWidth;

		// Token: 0x040013AF RID: 5039
		[EditableScriptComponentVariable(true)]
		private float _width;

		// Token: 0x040013B0 RID: 5040
		[EditableScriptComponentVariable(true)]
		private float _slope;

		// Token: 0x040013B1 RID: 5041
		[EditableScriptComponentVariable(true)]
		private bool _isInsurmountable;

		// Token: 0x040013B2 RID: 5042
		[EditableScriptComponentVariable(true)]
		private bool _isOuterEdge;

		// Token: 0x040013B3 RID: 5043
		private List<TacticalPosition> _linkedTacticalPositions;

		// Token: 0x040013B4 RID: 5044
		[EditableScriptComponentVariable(true)]
		private TacticalPosition.TacticalPositionTypeEnum _tacticalPositionType;

		// Token: 0x040013B5 RID: 5045
		[EditableScriptComponentVariable(true)]
		private TacticalRegion.TacticalRegionTypeEnum _tacticalRegionMembership;

		// Token: 0x040013B6 RID: 5046
		[EditableScriptComponentVariable(true)]
		private FormationAI.BehaviorSide _tacticalPositionSide = FormationAI.BehaviorSide.BehaviorSideNotSet;

		// Token: 0x0200060E RID: 1550
		public enum TacticalPositionTypeEnum
		{
			// Token: 0x04001F83 RID: 8067
			Regional,
			// Token: 0x04001F84 RID: 8068
			HighGround,
			// Token: 0x04001F85 RID: 8069
			ChokePoint,
			// Token: 0x04001F86 RID: 8070
			Cliff,
			// Token: 0x04001F87 RID: 8071
			SpecialMissionPosition
		}
	}
}
