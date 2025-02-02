using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.View.Screens;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Order
{
	// Token: 0x0200007E RID: 126
	public class OrderFlag
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00024485 File Offset: 0x00022685
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00024490 File Offset: 0x00022690
		private GameEntity Current
		{
			get
			{
				return this._current;
			}
			set
			{
				if (this._current != value)
				{
					this._current = value;
					this._flag.SetVisibilityExcludeParents(false);
					this._gear.SetVisibilityExcludeParents(false);
					this._arrow.SetVisibilityExcludeParents(false);
					this._width.SetVisibilityExcludeParents(false);
					this._attack.SetVisibilityExcludeParents(false);
					this._flagUnavailable.SetVisibilityExcludeParents(false);
					this._current.SetVisibilityExcludeParents(true);
					if (this._current == this._arrow || this._current == this._flagUnavailable)
					{
						this._flag.SetVisibilityExcludeParents(true);
					}
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0002453B File Offset: 0x0002273B
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00024543 File Offset: 0x00022743
		public IOrderable FocusedOrderableObject { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0002454C File Offset: 0x0002274C
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00024554 File Offset: 0x00022754
		public int LatestUpdateFrameNo { get; private set; }

		// Token: 0x06000482 RID: 1154 RVA: 0x00024560 File Offset: 0x00022760
		public OrderFlag(Mission mission, MissionScreen missionScreen)
		{
			this._mission = mission;
			this._missionScreen = missionScreen;
			this._entity = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._flag = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._gear = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._arrow = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._width = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._attack = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._flagUnavailable = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._widthLeft = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._widthRight = GameEntity.CreateEmpty(this._mission.Scene, true);
			this._entity.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._flag.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._gear.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._arrow.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._width.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._attack.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._flagUnavailable.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._widthLeft.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._widthRight.EntityFlags |= EntityFlags.NotAffectedBySeason;
			this._flag.AddComponent(MetaMesh.GetCopy("order_flag_a", true, false));
			MatrixFrame frame = this._flag.GetFrame();
			frame.Scale(new Vec3(10f, 10f, 10f, -1f));
			this._flag.SetFrame(ref frame);
			this._gear.AddComponent(MetaMesh.GetCopy("order_gear", true, false));
			MatrixFrame frame2 = this._gear.GetFrame();
			frame2.Scale(new Vec3(10f, 10f, 10f, -1f));
			this._gear.SetFrame(ref frame2);
			this._arrow.AddComponent(MetaMesh.GetCopy("order_arrow_a", true, false));
			this._widthLeft.AddComponent(MetaMesh.GetCopy("order_arrow_a", true, false));
			this._widthRight.AddComponent(MetaMesh.GetCopy("order_arrow_a", true, false));
			MatrixFrame identity = MatrixFrame.Identity;
			identity.rotation.RotateAboutUp(-1.5707964f);
			this._widthLeft.SetFrame(ref identity);
			identity = MatrixFrame.Identity;
			identity.rotation.RotateAboutUp(1.5707964f);
			this._widthRight.SetFrame(ref identity);
			this._width.AddChild(this._widthLeft, false);
			this._width.AddChild(this._widthRight, false);
			MetaMesh copy = MetaMesh.GetCopy("destroy_icon", true, false);
			copy.RecomputeBoundingBox(true);
			MatrixFrame frame3 = copy.Frame;
			frame3.Scale(new Vec3(0.15f, 0.15f, 0.15f, -1f));
			frame3.Elevate(10f);
			copy.Frame = frame3;
			this._attack.AddMultiMesh(copy, true);
			this._flagUnavailable.AddComponent(MetaMesh.GetCopy("order_unavailable", true, false));
			this._entity.AddChild(this._flag, false);
			this._entity.AddChild(this._gear, false);
			this._entity.AddChild(this._arrow, false);
			this._entity.AddChild(this._width, false);
			this._entity.AddChild(this._attack, false);
			this._entity.AddChild(this._flagUnavailable, false);
			this._flag.SetVisibilityExcludeParents(false);
			this._gear.SetVisibilityExcludeParents(false);
			this._arrow.SetVisibilityExcludeParents(false);
			this._width.SetVisibilityExcludeParents(false);
			this._attack.SetVisibilityExcludeParents(false);
			this._flagUnavailable.SetVisibilityExcludeParents(false);
			this.Current = this._flag;
			BoundingBox boundingBox = this._arrow.GetMetaMesh(0).GetBoundingBox();
			this._arrowLength = boundingBox.max.y - boundingBox.min.y;
			bool flag;
			this.UpdateFrame(out flag, false);
			this._orderablesWithInteractionArea = this._mission.MissionObjects.OfType<IOrderableWithInteractionArea>();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000249FC File Offset: 0x00022BFC
		public void Tick(float dt)
		{
			this.FocusedOrderableObject = null;
			GameEntity collidedEntity = this.GetCollidedEntity();
			if (collidedEntity != null)
			{
				BattleSideEnum side = Mission.Current.PlayerTeam.Side;
				IOrderable orderable = (IOrderable)collidedEntity.GetScriptComponents().First(delegate(ScriptComponentBehavior sc)
				{
					IOrderable orderable2;
					return (orderable2 = (sc as IOrderable)) != null && orderable2.GetOrder(side) > OrderType.None;
				});
				if (orderable.GetOrder(side) != OrderType.None)
				{
					this.FocusedOrderableObject = orderable;
				}
			}
			bool isOnValidGround;
			this.UpdateFrame(out isOnValidGround, collidedEntity != null);
			this.LatestUpdateFrameNo = Utilities.EngineFrameNo;
			if (!this.IsVisible)
			{
				return;
			}
			if (this.FocusedOrderableObject == null)
			{
				this.FocusedOrderableObject = this._orderablesWithInteractionArea.FirstOrDefault((IOrderableWithInteractionArea o) => ((ScriptComponentBehavior)o).GameEntity.IsVisibleIncludeParents() && o.IsPointInsideInteractionArea(this.Position));
				ScriptComponentBehavior scriptComponentBehavior;
				if ((scriptComponentBehavior = (this.FocusedOrderableObject as ScriptComponentBehavior)) != null && scriptComponentBehavior.GameEntity.Scene == null)
				{
					this.FocusedOrderableObject = null;
				}
			}
			this.UpdateCurrentMesh(isOnValidGround);
			if (this.Current == this._flag || this.Current == this._flagUnavailable)
			{
				MatrixFrame frame = this._flag.GetFrame();
				float num = MathF.Sin(MBCommon.GetApplicationTime() * 2f) + 1f;
				num *= 0.25f;
				frame.origin.z = num;
				this._flag.SetFrame(ref frame);
				this._flagUnavailable.SetFrame(ref frame);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00024B64 File Offset: 0x00022D64
		private void UpdateCurrentMesh(bool isOnValidGround)
		{
			if (this.FocusedOrderableObject != null)
			{
				BattleSideEnum side = Mission.Current.PlayerTeam.Side;
				if (this.FocusedOrderableObject.GetOrder(side) == OrderType.AttackEntity)
				{
					this.Current = this._attack;
					return;
				}
				OrderType order = this.FocusedOrderableObject.GetOrder(side);
				if (order == OrderType.Use || order == OrderType.FollowEntity)
				{
					this.Current = this._gear;
					return;
				}
			}
			if (this._isArrowVisible)
			{
				this.Current = this._arrow;
				return;
			}
			if (this._isWidthVisible)
			{
				this.Current = this._width;
				return;
			}
			this.Current = (isOnValidGround ? this._flag : this._flagUnavailable);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00024C0A File Offset: 0x00022E0A
		public void SetArrowVisibility(bool isVisible, Vec2 arrowDirection)
		{
			this._isArrowVisible = isVisible;
			this._arrowDirection = arrowDirection;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00024C1C File Offset: 0x00022E1C
		private void UpdateFrame(out bool isOnValidGround, bool checkForTargetEntity)
		{
			Vec3 position;
			Vec3 vec;
			if (this._missionScreen.GetProjectedMousePositionOnGround(out position, out vec, BodyFlags.BodyOwnerFlora, true))
			{
				WorldPosition worldPosition = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, position, false);
				isOnValidGround = ((!this.IsVisible || checkForTargetEntity) ? Mission.Current.IsOrderPositionAvailable(worldPosition, Mission.Current.PlayerTeam) : OrderFlag.IsPositionOnValidGround(worldPosition));
			}
			else
			{
				isOnValidGround = false;
				position = new Vec3(0f, 0f, -100000f, -1f);
			}
			this.Position = position;
			Vec3 v;
			this._missionScreen.ScreenPointToWorldRay(Vec2.One * 0.5f, out v, out vec);
			float num;
			if (this._missionScreen.LastFollowedAgent != null)
			{
				vec = v - this.Position;
				num = vec.AsVec2.RotationInRadians;
			}
			else
			{
				num = this._missionScreen.CombatCamera.Frame.rotation.f.RotationZ;
			}
			float a = num;
			MatrixFrame frame = this._entity.GetFrame();
			frame.rotation = Mat3.Identity;
			frame.rotation.RotateAboutUp(a);
			this._entity.SetFrame(ref frame);
			if (this._isArrowVisible)
			{
				a = this._arrowDirection.RotationInRadians;
				Mat3 identity = Mat3.Identity;
				identity.RotateAboutUp(a);
				MatrixFrame identity2 = MatrixFrame.Identity;
				identity2.rotation = frame.rotation.TransformToLocal(identity);
				identity2.Advance(-this._arrowLength);
				this._arrow.SetFrame(ref identity2);
			}
			if (this._isWidthVisible)
			{
				this._widthLeft.SetLocalPosition(Vec3.Side * (this._customWidth * 0.5f - 0f));
				this._widthRight.SetLocalPosition(Vec3.Side * (this._customWidth * -0.5f + 0f));
				this._widthLeft.SetLocalPosition(Vec3.Side * (this._customWidth * 0.5f - this._arrowLength));
				this._widthRight.SetLocalPosition(Vec3.Side * (this._customWidth * -0.5f + this._arrowLength));
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00024E52 File Offset: 0x00023052
		public static bool IsPositionOnValidGround(WorldPosition worldPosition)
		{
			return Mission.Current.IsFormationUnitPositionAvailable(ref worldPosition, Mission.Current.PlayerTeam);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00024E6A File Offset: 0x0002306A
		public static bool IsOrderPositionValid(WorldPosition orderPosition)
		{
			return Mission.Current.IsOrderPositionAvailable(orderPosition, Mission.Current.PlayerTeam);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00024E82 File Offset: 0x00023082
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00024E90 File Offset: 0x00023090
		public Vec3 Position
		{
			get
			{
				return this._entity.GlobalPosition;
			}
			private set
			{
				MatrixFrame frame = this._entity.GetFrame();
				frame.origin = value;
				this._entity.SetFrame(ref frame);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00024EBE File Offset: 0x000230BE
		public MatrixFrame Frame
		{
			get
			{
				return this._entity.GetGlobalFrame();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00024ECB File Offset: 0x000230CB
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00024ED8 File Offset: 0x000230D8
		public bool IsVisible
		{
			get
			{
				return this._entity.IsVisibleIncludeParents();
			}
			set
			{
				this._entity.SetVisibilityExcludeParents(value);
				if (!value)
				{
					this.FocusedOrderableObject = null;
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00024EF0 File Offset: 0x000230F0
		private GameEntity GetCollidedEntity()
		{
			Vec2 screenPoint = (Mission.Current.GetMissionBehavior<BattleDeploymentHandler>() != null) ? Input.MousePositionRanged : new Vec2(0.5f, 0.5f);
			Vec3 sourcePoint;
			Vec3 targetPoint;
			this._missionScreen.ScreenPointToWorldRay(screenPoint, out sourcePoint, out targetPoint);
			float num;
			GameEntity parent;
			this._mission.Scene.RayCastForClosestEntityOrTerrain(sourcePoint, targetPoint, out num, out parent, 0.3f, BodyFlags.Disabled | BodyFlags.AILimiter | BodyFlags.Barrier | BodyFlags.Barrier3D | BodyFlags.Ragdoll | BodyFlags.RagdollLimiter | BodyFlags.DoNotCollideWithRaycast | BodyFlags.BodyOwnerFlora);
			while (parent != null)
			{
				if (parent.GetScriptComponents().Any(delegate(ScriptComponentBehavior sc)
				{
					IOrderable orderable;
					return (orderable = (sc as IOrderable)) != null && orderable.GetOrder(Mission.Current.PlayerTeam.Side) > OrderType.None;
				}))
				{
					break;
				}
				parent = parent.Parent;
			}
			return parent;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00024F90 File Offset: 0x00023190
		public void SetWidthVisibility(bool isVisible, float width)
		{
			this._isWidthVisible = isVisible;
			this._customWidth = width;
		}

		// Token: 0x040002C7 RID: 711
		private readonly GameEntity _entity;

		// Token: 0x040002C8 RID: 712
		private readonly GameEntity _flag;

		// Token: 0x040002C9 RID: 713
		private readonly GameEntity _gear;

		// Token: 0x040002CA RID: 714
		private readonly GameEntity _arrow;

		// Token: 0x040002CB RID: 715
		private readonly GameEntity _width;

		// Token: 0x040002CC RID: 716
		private readonly GameEntity _attack;

		// Token: 0x040002CD RID: 717
		private readonly GameEntity _flagUnavailable;

		// Token: 0x040002CE RID: 718
		private readonly GameEntity _widthLeft;

		// Token: 0x040002CF RID: 719
		private readonly GameEntity _widthRight;

		// Token: 0x040002D0 RID: 720
		public bool IsTroop = true;

		// Token: 0x040002D1 RID: 721
		private bool _isWidthVisible;

		// Token: 0x040002D2 RID: 722
		private float _customWidth;

		// Token: 0x040002D3 RID: 723
		private GameEntity _current;

		// Token: 0x040002D5 RID: 725
		private readonly IEnumerable<IOrderableWithInteractionArea> _orderablesWithInteractionArea;

		// Token: 0x040002D6 RID: 726
		private readonly Mission _mission;

		// Token: 0x040002D7 RID: 727
		private readonly MissionScreen _missionScreen;

		// Token: 0x040002D8 RID: 728
		private readonly float _arrowLength;

		// Token: 0x040002D9 RID: 729
		private bool _isArrowVisible;

		// Token: 0x040002DA RID: 730
		private Vec2 _arrowDirection;
	}
}
