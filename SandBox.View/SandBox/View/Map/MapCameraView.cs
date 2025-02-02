using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.View.Map
{
	// Token: 0x02000038 RID: 56
	public class MapCameraView : MapView
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0001232D File Offset: 0x0001052D
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00012335 File Offset: 0x00010535
		protected virtual MapCameraView.CameraFollowMode CurrentCameraFollowMode { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0001233E File Offset: 0x0001053E
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00012346 File Offset: 0x00010546
		public virtual float CameraFastMoveMultiplier { get; protected set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0001234F File Offset: 0x0001054F
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00012357 File Offset: 0x00010557
		protected virtual float CameraBearing { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00012360 File Offset: 0x00010560
		protected virtual float MaximumCameraHeight
		{
			get
			{
				return Math.Max(this._customMaximumCameraHeight, Campaign.MapMaximumHeight);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00012372 File Offset: 0x00010572
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0001237A File Offset: 0x0001057A
		protected virtual float CameraBearingVelocity { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00012383 File Offset: 0x00010583
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0001238B File Offset: 0x0001058B
		public virtual float CameraDistance { get; protected set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00012394 File Offset: 0x00010594
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0001239C File Offset: 0x0001059C
		protected virtual float TargetCameraDistance { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000123A5 File Offset: 0x000105A5
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000123AD File Offset: 0x000105AD
		protected virtual float AdditionalElevation { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000123B6 File Offset: 0x000105B6
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000123BE File Offset: 0x000105BE
		public virtual bool CameraAnimationInProgress { get; protected set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000123C7 File Offset: 0x000105C7
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000123CF File Offset: 0x000105CF
		public virtual bool ProcessCameraInput { get; protected set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000123D8 File Offset: 0x000105D8
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000123E0 File Offset: 0x000105E0
		public virtual Camera Camera { get; protected set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000123E9 File Offset: 0x000105E9
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000123F1 File Offset: 0x000105F1
		public virtual MatrixFrame CameraFrame
		{
			get
			{
				return this._cameraFrame;
			}
			protected set
			{
				this._cameraFrame = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000123FA File Offset: 0x000105FA
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00012402 File Offset: 0x00010602
		protected virtual Vec3 IdealCameraTarget { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0001240B File Offset: 0x0001060B
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00012412 File Offset: 0x00010612
		private static MapCameraView Instance { get; set; }

		// Token: 0x060001DD RID: 477 RVA: 0x0001241C File Offset: 0x0001061C
		public MapCameraView()
		{
			this.Camera = Camera.CreateCamera();
			this.Camera.SetViewVolume(true, -0.1f, 0.1f, -0.07f, 0.07f, 0.2f, 300f);
			this.Camera.Position = new Vec3(0f, 0f, 10f, -1f);
			this.CameraBearing = 0f;
			this._cameraElevation = 1f;
			this.CameraDistance = 2.5f;
			this.ProcessCameraInput = true;
			this.CameraFastMoveMultiplier = 4f;
			this._cameraFrame = MatrixFrame.Identity;
			this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.FollowParty;
			this._mapScene = ((MapScene)Campaign.Current.MapSceneWrapper).Scene;
			MapCameraView.Instance = this;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000124ED File Offset: 0x000106ED
		public virtual void OnActivate(bool leftButtonDraggingMode, Vec3 clickedPosition)
		{
			this.SetCameraMode(MapCameraView.CameraFollowMode.FollowParty);
			this.CameraBearingVelocity = 0f;
			this.UpdateMapCamera(leftButtonDraggingMode, clickedPosition);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0001250C File Offset: 0x0001070C
		public virtual void Initialize()
		{
			if (MobileParty.MainParty != null && PartyBase.MainParty.IsValid)
			{
				float num = 0f;
				this._mapScene.GetHeightAtPoint(MobileParty.MainParty.Position2D, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref num);
				this.IdealCameraTarget = new Vec3(MobileParty.MainParty.Position2D, num + 1f, -1f);
			}
			this._cameraTarget = this.IdealCameraTarget;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0001257C File Offset: 0x0001077C
		protected internal override void OnFinalize()
		{
			base.OnFinalize();
			MapCameraView.Instance = null;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001258A File Offset: 0x0001078A
		public virtual void SetCameraMode(MapCameraView.CameraFollowMode cameraMode)
		{
			this.CurrentCameraFollowMode = cameraMode;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00012593 File Offset: 0x00010793
		public virtual void ResetCamera(bool resetDistance, bool teleportToMainParty)
		{
			if (teleportToMainParty)
			{
				this.TeleportCameraToMainParty();
			}
			if (resetDistance)
			{
				this.TargetCameraDistance = 15f;
				this.CameraDistance = 15f;
			}
			this.CameraBearing = 0f;
			this._cameraElevation = 1f;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000125D0 File Offset: 0x000107D0
		public virtual void TeleportCameraToMainParty()
		{
			this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.FollowParty;
			Campaign.Current.CameraFollowParty = MobileParty.MainParty.Party;
			this.IdealCameraTarget = this.GetCameraTargetForParty(Campaign.Current.CameraFollowParty);
			this._lastUsedIdealCameraTarget = this.IdealCameraTarget.AsVec2;
			this._cameraTarget = this.IdealCameraTarget;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00012630 File Offset: 0x00010830
		public virtual void FastMoveCameraToMainParty()
		{
			this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.FollowParty;
			Campaign.Current.CameraFollowParty = MobileParty.MainParty.Party;
			this.IdealCameraTarget = this.GetCameraTargetForParty(Campaign.Current.CameraFollowParty);
			this._doFastCameraMovementToTarget = true;
			this.TargetCameraDistance = 15f;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00012680 File Offset: 0x00010880
		public virtual void FastMoveCameraToPosition(Vec2 target, bool isInMenu)
		{
			if (!isInMenu)
			{
				this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.MoveToPosition;
				this.IdealCameraTarget = this.GetCameraTargetForPosition(target);
				this._doFastCameraMovementToTarget = true;
				this.TargetCameraDistance = 15f;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000126AB File Offset: 0x000108AB
		public virtual bool IsCameraLockedToPlayerParty()
		{
			return this.CurrentCameraFollowMode == MapCameraView.CameraFollowMode.FollowParty && Campaign.Current.CameraFollowParty == MobileParty.MainParty.Party;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000126CE File Offset: 0x000108CE
		public virtual void StartCameraAnimation(Vec2 targetPosition, float animationStopDuration)
		{
			this.CameraAnimationInProgress = true;
			this._cameraAnimationTarget = targetPosition;
			this._cameraAnimationStopDuration = animationStopDuration;
			Campaign.Current.SetTimeSpeed(0);
			Campaign.Current.SetTimeControlModeLock(true);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000126FB File Offset: 0x000108FB
		public virtual void SiegeEngineClick(MatrixFrame siegeEngineFrame)
		{
			if (this.TargetCameraDistance > 18f)
			{
				this.TargetCameraDistance = 18f;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00012715 File Offset: 0x00010915
		public virtual void OnExit()
		{
			this.ProcessCameraInput = true;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0001271E File Offset: 0x0001091E
		public virtual void OnEscapeMenuToggled(bool isOpened)
		{
			this.ProcessCameraInput = !isOpened;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001272C File Offset: 0x0001092C
		public virtual void HandleMouse(bool rightMouseButtonPressed, float verticalCameraInput, float mouseMoveY, float dt)
		{
			float num = 0.3f / 700f;
			float num2 = -(700f - MathF.Min(700f, MathF.Max(50f, this.CameraDistance))) * num;
			float maxValue = MathF.Max(num2 + 1E-05f, 1.5550884f - this.CalculateCameraElevation(this.CameraDistance));
			if (rightMouseButtonPressed)
			{
				this.AdditionalElevation = MBMath.ClampFloat(this.AdditionalElevation + mouseMoveY * 0.0015f, num2, maxValue);
			}
			if (verticalCameraInput != 0f)
			{
				this.AdditionalElevation = MBMath.ClampFloat(this.AdditionalElevation - verticalCameraInput * dt, num2, maxValue);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000127C6 File Offset: 0x000109C6
		public virtual void HandleLeftMouseButtonClick(bool isMouseActive)
		{
			if (isMouseActive)
			{
				this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.FollowParty;
				Campaign.Current.CameraFollowParty = PartyBase.MainParty;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000127E1 File Offset: 0x000109E1
		public virtual void OnSetMapSiegeOverlayState(bool isActive, bool isMapSiegeOverlayViewNull)
		{
			if (isActive && isMapSiegeOverlayViewNull && PlayerSiege.PlayerSiegeEvent != null)
			{
				this.TargetCameraDistance = 13f;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000127FA File Offset: 0x000109FA
		public virtual void OnRefreshMapSiegeOverlayRequired(bool isMapSiegeOverlayViewNull)
		{
			if (PlayerSiege.PlayerSiegeEvent != null && isMapSiegeOverlayViewNull)
			{
				this.TargetCameraDistance = 13f;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00012814 File Offset: 0x00010A14
		public virtual void OnBeforeTick(in MapCameraView.InputInformation inputInformation)
		{
			float num = MathF.Min(1f, MathF.Max(0f, 1f - this.CameraFrame.rotation.f.z)) + 0.15f;
			this._mapScene.SetDepthOfFieldParameters(0.05f, num * 1000f, true);
			this._mapScene.SetDepthOfFieldFocus(0.05f);
			MobileParty mainParty = MobileParty.MainParty;
			if (inputInformation.IsMainPartyValid && this.CameraAnimationInProgress)
			{
				Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
				if (this._cameraAnimationStopDuration > 0f)
				{
					if (this._cameraAnimationTarget.DistanceSquared(this._cameraTarget.AsVec2) < 0.0001f)
					{
						this._cameraAnimationStopDuration = MathF.Max(this._cameraAnimationStopDuration - inputInformation.Dt, 0f);
					}
					else
					{
						float terrainHeight = this._mapScene.GetTerrainHeight(this._cameraAnimationTarget, true);
						this.IdealCameraTarget = this._cameraAnimationTarget.ToVec3(terrainHeight + 1f);
					}
				}
				else if (MobileParty.MainParty.Position2D.DistanceSquared(this._cameraTarget.AsVec2) < 0.0001f)
				{
					this.CameraAnimationInProgress = false;
					Campaign.Current.SetTimeControlModeLock(false);
				}
				else
				{
					this.IdealCameraTarget = MobileParty.MainParty.GetPosition() + Vec3.Up;
				}
			}
			bool flag = this.CameraAnimationInProgress;
			if (this.ProcessCameraInput && !this.CameraAnimationInProgress && inputInformation.IsMapReady)
			{
				flag = this.GetMapCameraInput(inputInformation);
			}
			if (flag)
			{
				Vec3 v = this.IdealCameraTarget - this._cameraTarget;
				Vec3 v2 = 10f * v * inputInformation.Dt;
				float num2 = MathF.Sqrt(MathF.Max(this.CameraDistance, 20f)) * 0.15f;
				float num3 = this._doFastCameraMovementToTarget ? (num2 * 5f) : num2;
				if (v2.LengthSquared > num3 * num3)
				{
					v2 = v2.NormalizedCopy() * num3;
				}
				if (v2.LengthSquared < num2 * num2)
				{
					this._doFastCameraMovementToTarget = false;
				}
				this._cameraTarget += v2;
			}
			else
			{
				this._cameraTarget = this.IdealCameraTarget;
				this._doFastCameraMovementToTarget = false;
			}
			if (inputInformation.IsMainPartyValid)
			{
				if (inputInformation.CameraFollowModeKeyPressed)
				{
					this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.FollowParty;
				}
				if (!inputInformation.IsInMenu && !inputInformation.MiddleMouseButtonDown && (MobileParty.MainParty == null || MobileParty.MainParty.Army == null || MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty) && (inputInformation.PartyMoveRightKey || inputInformation.PartyMoveLeftKey || inputInformation.PartyMoveUpKey || inputInformation.PartyMoveDownKey))
				{
					float num4 = 0f;
					float num5 = 0f;
					float num6;
					float num7;
					MathF.SinCos(this.CameraBearing, out num6, out num7);
					float num8;
					float num9;
					MathF.SinCos(this.CameraBearing + 1.5707964f, out num8, out num9);
					float num10 = 0.5f;
					if (inputInformation.PartyMoveUpKey)
					{
						num5 += num7 * num10;
						num4 += num6 * num10;
						mainParty.Ai.ForceAiNoPathMode = true;
					}
					if (inputInformation.PartyMoveDownKey)
					{
						num5 -= num7 * num10;
						num4 -= num6 * num10;
						mainParty.Ai.ForceAiNoPathMode = true;
					}
					if (inputInformation.PartyMoveLeftKey)
					{
						num5 -= num9 * num10;
						num4 -= num8 * num10;
						mainParty.Ai.ForceAiNoPathMode = true;
					}
					if (inputInformation.PartyMoveRightKey)
					{
						num5 += num9 * num10;
						num4 += num8 * num10;
						mainParty.Ai.ForceAiNoPathMode = true;
					}
					this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.FollowParty;
					mainParty.Ai.SetMoveGoToPoint(mainParty.Position2D + new Vec2(num4, num5));
					Campaign.Current.TimeControlMode = CampaignTimeControlMode.StoppablePlay;
				}
				else if (mainParty.Ai.ForceAiNoPathMode)
				{
					mainParty.Ai.SetMoveGoToPoint(mainParty.Position2D);
				}
			}
			this.UpdateMapCamera(inputInformation.LeftButtonDraggingMode, inputInformation.ClickedPosition);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00012C1C File Offset: 0x00010E1C
		protected virtual void UpdateMapCamera(bool _leftButtonDraggingMode, Vec3 _clickedPosition)
		{
			this._lastUsedIdealCameraTarget = this.IdealCameraTarget.AsVec2;
			MatrixFrame cameraFrame = this.ComputeMapCamera(ref this._cameraTarget, this.CameraBearing, this._cameraElevation, this.CameraDistance, ref this._lastUsedIdealCameraTarget);
			bool flag = !cameraFrame.origin.NearlyEquals(this._cameraFrame.origin, 1E-05f);
			bool flag2 = !cameraFrame.rotation.NearlyEquals(this._cameraFrame.rotation, 1E-05f);
			if (flag2 || flag)
			{
				Game.Current.EventManager.TriggerEvent<MainMapCameraMoveEvent>(new MainMapCameraMoveEvent(flag2, flag));
			}
			this._cameraFrame = cameraFrame;
			float num = 0f;
			this._mapScene.GetHeightAtPoint(this._cameraFrame.origin.AsVec2, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref num);
			num += 0.5f;
			if (this._cameraFrame.origin.z < num)
			{
				if (_leftButtonDraggingMode)
				{
					Vec3 v = _clickedPosition - Vec3.DotProduct(_clickedPosition - this._cameraFrame.origin, this._cameraFrame.rotation.s) * this._cameraFrame.rotation.s;
					Vec3 vec = Vec3.CrossProduct((v - this._cameraFrame.origin).NormalizedCopy(), (v - (this._cameraFrame.origin + new Vec3(0f, 0f, num - this._cameraFrame.origin.z, -1f))).NormalizedCopy());
					float a = vec.Normalize();
					this._cameraFrame.origin.z = num;
					this._cameraFrame.rotation.u = this._cameraFrame.rotation.u.RotateAboutAnArbitraryVector(vec, a);
					this._cameraFrame.rotation.f = Vec3.CrossProduct(this._cameraFrame.rotation.u, this._cameraFrame.rotation.s).NormalizedCopy();
					this._cameraFrame.rotation.s = Vec3.CrossProduct(this._cameraFrame.rotation.f, this._cameraFrame.rotation.u);
					Vec3 vec2 = -Vec3.Up;
					Vec3 v2 = -this._cameraFrame.rotation.u;
					Vec3 idealCameraTarget = this.IdealCameraTarget;
					float f;
					if (MBMath.GetRayPlaneIntersectionPoint(vec2, idealCameraTarget, this._cameraFrame.origin, v2, out f))
					{
						this.IdealCameraTarget = this._cameraFrame.origin + v2 * f;
						this._cameraTarget = this.IdealCameraTarget;
					}
					this._cameraElevation = -new Vec2(this._cameraFrame.rotation.f.AsVec2.Length, this._cameraFrame.rotation.f.z).RotationInRadians;
					this.CameraDistance = (this._cameraFrame.origin - this.IdealCameraTarget).Length - 2f;
					this.TargetCameraDistance = this.CameraDistance;
					this.AdditionalElevation = this._cameraElevation - this.CalculateCameraElevation(this.CameraDistance);
					this._lastUsedIdealCameraTarget = this.IdealCameraTarget.AsVec2;
					this.ComputeMapCamera(ref this._cameraTarget, this.CameraBearing, this._cameraElevation, this.CameraDistance, ref this._lastUsedIdealCameraTarget);
				}
				else
				{
					float num2 = 0.47123894f;
					int num3 = 0;
					do
					{
						this._cameraElevation += ((this._cameraFrame.origin.z < num) ? num2 : (-num2));
						this.AdditionalElevation = this._cameraElevation - this.CalculateCameraElevation(this.CameraDistance);
						float num4 = 700f;
						float num5 = 0.3f / num4;
						float a2 = 50f;
						float num6 = -(num4 - MathF.Min(num4, MathF.Max(a2, this.CameraDistance))) * num5;
						float maxValue = MathF.Max(num6 + 1E-05f, 1.5550884f - this.CalculateCameraElevation(this.CameraDistance));
						this.AdditionalElevation = MBMath.ClampFloat(this.AdditionalElevation, num6, maxValue);
						this._cameraElevation = this.AdditionalElevation + this.CalculateCameraElevation(this.CameraDistance);
						Vec2 zero = Vec2.Zero;
						this._cameraFrame = this.ComputeMapCamera(ref this._cameraTarget, this.CameraBearing, this._cameraElevation, this.CameraDistance, ref zero);
						this._mapScene.GetHeightAtPoint(this._cameraFrame.origin.AsVec2, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref num);
						num += 0.5f;
						if (num2 > 0.0001f)
						{
							num2 *= 0.5f;
						}
						else
						{
							num3++;
						}
					}
					while (num2 > 0.0001f || (this._cameraFrame.origin.z < num && num3 < 5));
					if (this._cameraFrame.origin.z < num)
					{
						this._cameraFrame.origin.z = num;
						Vec3 vec3 = -Vec3.Up;
						Vec3 v3 = -this._cameraFrame.rotation.u;
						Vec3 idealCameraTarget2 = this.IdealCameraTarget;
						float f2;
						if (MBMath.GetRayPlaneIntersectionPoint(vec3, idealCameraTarget2, this._cameraFrame.origin, v3, out f2))
						{
							this.IdealCameraTarget = this._cameraFrame.origin + v3 * f2;
							this._cameraTarget = this.IdealCameraTarget;
						}
						this.CameraDistance = (this._cameraFrame.origin - this.IdealCameraTarget).Length - 2f;
						this._lastUsedIdealCameraTarget = this.IdealCameraTarget.AsVec2;
						this.ComputeMapCamera(ref this._cameraTarget, this.CameraBearing, this._cameraElevation, this.CameraDistance, ref this._lastUsedIdealCameraTarget);
						this.TargetCameraDistance = MathF.Max(this.TargetCameraDistance, this.CameraDistance);
					}
				}
			}
			this.Camera.Frame = this._cameraFrame;
			this.Camera.SetFovVertical(0.6981317f, Screen.AspectRatio, 0.01f, this.MaximumCameraHeight * 4f);
			this._mapScene.SetDepthOfFieldFocus(0f);
			this._mapScene.SetDepthOfFieldParameters(0f, 0f, false);
			MatrixFrame identity = MatrixFrame.Identity;
			identity.rotation = this._cameraFrame.rotation;
			identity.origin = this._cameraTarget;
			this._mapScene.GetHeightAtPoint(identity.origin.AsVec2, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref identity.origin.z);
			identity.origin = MBMath.Lerp(identity.origin, this._cameraFrame.origin, 0.075f, 1E-05f);
			PathFaceRecord faceIndex = Campaign.Current.MapSceneWrapper.GetFaceIndex(identity.origin.AsVec2);
			if (faceIndex.IsValid())
			{
				TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(faceIndex);
				MBMapScene.TickAmbientSounds(this._mapScene, (int)faceTerrainType);
			}
			SoundManager.SetListenerFrame(identity);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001335C File Offset: 0x0001155C
		protected virtual Vec3 GetCameraTargetForPosition(Vec2 targetPosition)
		{
			float terrainHeight = this._mapScene.GetTerrainHeight(targetPosition, true);
			return new Vec3(targetPosition, terrainHeight + 1f, -1f);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001338C File Offset: 0x0001158C
		protected virtual Vec3 GetCameraTargetForParty(PartyBase party)
		{
			Vec2 targetPosition;
			if (party.IsMobile && party.MobileParty.CurrentSettlement != null)
			{
				targetPosition = party.MobileParty.CurrentSettlement.Position2D;
			}
			else if (party.IsMobile && party.MobileParty.BesiegedSettlement != null)
			{
				if (PlayerSiege.PlayerSiegeEvent != null)
				{
					Vec2 asVec = party.MobileParty.BesiegedSettlement.Town.BesiegerCampPositions1.First<MatrixFrame>().origin.AsVec2;
					targetPosition = Vec2.Lerp(party.MobileParty.BesiegedSettlement.GatePosition, asVec, 0.75f);
				}
				else
				{
					targetPosition = party.MobileParty.BesiegedSettlement.GatePosition;
				}
			}
			else
			{
				targetPosition = party.Position2D;
			}
			return this.GetCameraTargetForPosition(targetPosition);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00013444 File Offset: 0x00011644
		protected virtual bool GetMapCameraInput(MapCameraView.InputInformation inputInformation)
		{
			bool flag = false;
			bool result = !inputInformation.LeftButtonDraggingMode;
			if (inputInformation.IsControlDown && inputInformation.CheatModeEnabled)
			{
				flag = true;
				if (inputInformation.DeltaMouseScroll > 0.01f)
				{
					this.CameraFastMoveMultiplier *= 1.25f;
				}
				else if (inputInformation.DeltaMouseScroll < -0.01f)
				{
					this.CameraFastMoveMultiplier *= 0.8f;
				}
				this.CameraFastMoveMultiplier = MBMath.ClampFloat(this.CameraFastMoveMultiplier, 1f, 37.252903f);
			}
			Vec2 vec = Vec2.Zero;
			if (!inputInformation.LeftMouseButtonPressed && inputInformation.LeftMouseButtonDown && !inputInformation.LeftMouseButtonReleased && inputInformation.MousePositionPixel.DistanceSquared(inputInformation.ClickedPositionPixel) > 300f && !inputInformation.IsInMenu)
			{
				if (!inputInformation.LeftButtonDraggingMode)
				{
					this.IdealCameraTarget = this._cameraTarget;
					this._lastUsedIdealCameraTarget = this.IdealCameraTarget.AsVec2;
				}
				Vec3 v = (inputInformation.WorldMouseFar - inputInformation.WorldMouseNear).NormalizedCopy();
				Vec3 vec2 = -Vec3.Up;
				float f;
				if (MBMath.GetRayPlaneIntersectionPoint(vec2, inputInformation.ClickedPosition, inputInformation.WorldMouseNear, v, out f))
				{
					this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.Free;
					Vec3 vec3 = inputInformation.WorldMouseNear + v * f;
					vec = inputInformation.ClickedPosition.AsVec2 - vec3.AsVec2;
				}
			}
			if (inputInformation.MiddleMouseButtonDown)
			{
				this.TargetCameraDistance += 0.01f * (this.CameraDistance + 20f) * inputInformation.MouseSensitivity * inputInformation.MouseMoveY;
			}
			if (inputInformation.RotateLeftKeyDown)
			{
				this.CameraBearingVelocity = inputInformation.Dt * 2f;
			}
			else if (inputInformation.RotateRightKeyDown)
			{
				this.CameraBearingVelocity = inputInformation.Dt * -2f;
			}
			this.CameraBearingVelocity += inputInformation.HorizontalCameraInput * 1.75f * inputInformation.Dt;
			if (inputInformation.RightMouseButtonDown)
			{
				this.CameraBearingVelocity += 0.01f * inputInformation.MouseSensitivity * inputInformation.MouseMoveX;
			}
			float num = 0.1f;
			if (!inputInformation.IsMouseActive)
			{
				num *= inputInformation.Dt * 10f;
			}
			if (!flag)
			{
				this.TargetCameraDistance -= inputInformation.MapZoomIn * num * (this.CameraDistance + 20f);
				this.TargetCameraDistance += inputInformation.MapZoomOut * num * (this.CameraDistance + 20f);
			}
			PartyBase cameraFollowParty = Campaign.Current.CameraFollowParty;
			this.TargetCameraDistance = MBMath.ClampFloat(this.TargetCameraDistance, 2.5f, (cameraFollowParty != null && cameraFollowParty.IsMobile && (cameraFollowParty.MobileParty.BesiegedSettlement != null || (cameraFollowParty.MobileParty.CurrentSettlement != null && cameraFollowParty.MobileParty.CurrentSettlement.IsUnderSiege))) ? 30f : this.MaximumCameraHeight);
			float num2 = this.TargetCameraDistance - this.CameraDistance;
			float num3 = MathF.Abs(num2);
			float cameraDistance = (num3 > 0.001f) ? (this.CameraDistance + num2 * inputInformation.Dt * 8f) : this.TargetCameraDistance;
			if (this.CurrentCameraFollowMode == MapCameraView.CameraFollowMode.Free && !inputInformation.RightMouseButtonDown && !inputInformation.LeftMouseButtonDown && num3 >= 0.001f && (inputInformation.WorldMouseFar - this.CameraFrame.origin).NormalizedCopy().z < -0.2f && inputInformation.RayCastForClosestEntityOrTerrainCondition)
			{
				MatrixFrame matrixFrame = this.ComputeMapCamera(ref this._cameraTarget, this.CameraBearing + this.CameraBearingVelocity, MathF.Min(this.CalculateCameraElevation(cameraDistance) + this.AdditionalElevation, 1.5550884f), cameraDistance, ref this._lastUsedIdealCameraTarget);
				Vec3 vec4 = -Vec3.Up;
				Vec3 v2 = (inputInformation.WorldMouseFar - this.CameraFrame.origin).NormalizedCopy();
				MatrixFrame matrixFrame2 = this.CameraFrame;
				Vec3 v3 = matrixFrame.rotation.TransformToParent(matrixFrame2.rotation.TransformToLocal(v2));
				float f2;
				if (MBMath.GetRayPlaneIntersectionPoint(vec4, inputInformation.ProjectedPosition, matrixFrame.origin, v3, out f2))
				{
					vec = inputInformation.ProjectedPosition.AsVec2 - (matrixFrame.origin + v3 * f2).AsVec2;
					result = false;
				}
			}
			if (inputInformation.RX != 0f || inputInformation.RY != 0f || vec.IsNonZero())
			{
				float f3 = 0.001f * (this.CameraDistance * 0.55f + 15f);
				Vec2 v4 = Vec2.FromRotation(-this.CameraBearing);
				if ((this.IdealCameraTarget.AsVec2 - this._lastUsedIdealCameraTarget).LengthSquared > 0.010000001f)
				{
					this.IdealCameraTarget = new Vec3(this._lastUsedIdealCameraTarget.x, this._lastUsedIdealCameraTarget.y, this.IdealCameraTarget.z, -1f);
					this._cameraTarget = this.IdealCameraTarget;
				}
				if (!vec.IsNonZero())
				{
					this.IdealCameraTarget = this._cameraTarget;
				}
				Vec2 vec5 = inputInformation.Dt * 500f * inputInformation.RX * v4.RightVec() * f3 + inputInformation.Dt * 500f * inputInformation.RY * v4 * f3;
				this.IdealCameraTarget = new Vec3(this.IdealCameraTarget.x + vec.x + vec5.x, this.IdealCameraTarget.y + vec.y + vec5.y, this.IdealCameraTarget.z, -1f);
				if (vec.IsNonZero())
				{
					this._cameraTarget = this.IdealCameraTarget;
				}
				this._cameraTarget.AsVec2 = this._cameraTarget.AsVec2 + vec5;
				if (inputInformation.RX != 0f || inputInformation.RY != 0f)
				{
					this.CurrentCameraFollowMode = MapCameraView.CameraFollowMode.Free;
				}
			}
			this.CameraBearing += this.CameraBearingVelocity;
			this.CameraBearingVelocity = 0f;
			this.CameraDistance = cameraDistance;
			this._cameraElevation = MathF.Min(this.CalculateCameraElevation(cameraDistance) + this.AdditionalElevation, 1.5550884f);
			if (this.CurrentCameraFollowMode == MapCameraView.CameraFollowMode.FollowParty && cameraFollowParty != null && cameraFollowParty.IsValid)
			{
				Vec2 vec6;
				if (cameraFollowParty.IsMobile && cameraFollowParty.MobileParty.CurrentSettlement != null)
				{
					vec6 = ((cameraFollowParty.MobileParty.CurrentSettlement.SiegeEvent != null) ? cameraFollowParty.MobileParty.CurrentSettlement.GatePosition : cameraFollowParty.MobileParty.CurrentSettlement.Position2D);
				}
				else if (cameraFollowParty.IsMobile && cameraFollowParty.MobileParty.BesiegedSettlement != null)
				{
					if (PlayerSiege.PlayerSiegeEvent != null)
					{
						MatrixFrame matrixFrame2 = cameraFollowParty.MobileParty.BesiegedSettlement.Town.BesiegerCampPositions1.First<MatrixFrame>();
						Vec2 asVec = matrixFrame2.origin.AsVec2;
						vec6 = Vec2.Lerp(cameraFollowParty.MobileParty.BesiegedSettlement.GatePosition, asVec, 0.75f);
					}
					else
					{
						vec6 = cameraFollowParty.MobileParty.BesiegedSettlement.GatePosition;
					}
				}
				else
				{
					vec6 = cameraFollowParty.Position2D;
				}
				float terrainHeight = this._mapScene.GetTerrainHeight(vec6, true);
				this.IdealCameraTarget = new Vec3(vec6.x, vec6.y, terrainHeight + 1f, -1f);
			}
			return result;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00013BEC File Offset: 0x00011DEC
		protected virtual MatrixFrame ComputeMapCamera(ref Vec3 cameraTarget, float cameraBearing, float cameraElevation, float cameraDistance, ref Vec2 lastUsedIdealCameraTarget)
		{
			Vec2 asVec = cameraTarget.AsVec2;
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = cameraTarget;
			identity.rotation.RotateAboutSide(1.5707964f);
			identity.rotation.RotateAboutForward(-cameraBearing);
			identity.rotation.RotateAboutSide(-cameraElevation);
			identity.origin += identity.rotation.u * (cameraDistance + 2f);
			Vec2 vec = (Campaign.MapMinimumPosition + Campaign.MapMaximumPosition) * 0.5f;
			float num = Campaign.MapMaximumPosition.y - vec.y;
			float num2 = Campaign.MapMaximumPosition.x - vec.x;
			asVec.x = MBMath.ClampFloat(asVec.x, vec.x - num2, vec.x + num2);
			asVec.y = MBMath.ClampFloat(asVec.y, vec.y - num, vec.y + num);
			lastUsedIdealCameraTarget.x = MBMath.ClampFloat(lastUsedIdealCameraTarget.x, vec.x - num2, vec.x + num2);
			lastUsedIdealCameraTarget.y = MBMath.ClampFloat(lastUsedIdealCameraTarget.y, vec.y - num, vec.y + num);
			identity.origin.x = identity.origin.x + (asVec.x - cameraTarget.x);
			identity.origin.y = identity.origin.y + (asVec.y - cameraTarget.y);
			return identity;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00013D77 File Offset: 0x00011F77
		protected virtual float CalculateCameraElevation(float cameraDistance)
		{
			return cameraDistance * 0.5f * 0.015f + 0.35f;
		}

		// Token: 0x040000FE RID: 254
		private const float VerticalHalfViewAngle = 0.34906584f;

		// Token: 0x040000FF RID: 255
		private Vec3 _cameraTarget;

		// Token: 0x04000100 RID: 256
		private bool _doFastCameraMovementToTarget;

		// Token: 0x04000101 RID: 257
		private float _cameraElevation;

		// Token: 0x04000102 RID: 258
		private Vec2 _lastUsedIdealCameraTarget;

		// Token: 0x04000103 RID: 259
		private Vec2 _cameraAnimationTarget;

		// Token: 0x04000104 RID: 260
		private float _cameraAnimationStopDuration;

		// Token: 0x04000105 RID: 261
		private readonly Scene _mapScene;

		// Token: 0x04000109 RID: 265
		protected float _customMaximumCameraHeight;

		// Token: 0x04000111 RID: 273
		private MatrixFrame _cameraFrame;

		// Token: 0x02000078 RID: 120
		public enum CameraFollowMode
		{
			// Token: 0x040002C1 RID: 705
			Free,
			// Token: 0x040002C2 RID: 706
			FollowParty,
			// Token: 0x040002C3 RID: 707
			MoveToPosition
		}

		// Token: 0x02000079 RID: 121
		public struct InputInformation
		{
			// Token: 0x040002C4 RID: 708
			public bool IsMainPartyValid;

			// Token: 0x040002C5 RID: 709
			public bool IsMapReady;

			// Token: 0x040002C6 RID: 710
			public bool IsControlDown;

			// Token: 0x040002C7 RID: 711
			public bool IsMouseActive;

			// Token: 0x040002C8 RID: 712
			public bool CheatModeEnabled;

			// Token: 0x040002C9 RID: 713
			public bool LeftMouseButtonPressed;

			// Token: 0x040002CA RID: 714
			public bool LeftMouseButtonDown;

			// Token: 0x040002CB RID: 715
			public bool LeftMouseButtonReleased;

			// Token: 0x040002CC RID: 716
			public bool MiddleMouseButtonDown;

			// Token: 0x040002CD RID: 717
			public bool RightMouseButtonDown;

			// Token: 0x040002CE RID: 718
			public bool RotateLeftKeyDown;

			// Token: 0x040002CF RID: 719
			public bool RotateRightKeyDown;

			// Token: 0x040002D0 RID: 720
			public bool PartyMoveUpKey;

			// Token: 0x040002D1 RID: 721
			public bool PartyMoveDownKey;

			// Token: 0x040002D2 RID: 722
			public bool PartyMoveLeftKey;

			// Token: 0x040002D3 RID: 723
			public bool PartyMoveRightKey;

			// Token: 0x040002D4 RID: 724
			public bool CameraFollowModeKeyPressed;

			// Token: 0x040002D5 RID: 725
			public bool LeftButtonDraggingMode;

			// Token: 0x040002D6 RID: 726
			public bool IsInMenu;

			// Token: 0x040002D7 RID: 727
			public bool RayCastForClosestEntityOrTerrainCondition;

			// Token: 0x040002D8 RID: 728
			public float MapZoomIn;

			// Token: 0x040002D9 RID: 729
			public float MapZoomOut;

			// Token: 0x040002DA RID: 730
			public float DeltaMouseScroll;

			// Token: 0x040002DB RID: 731
			public float MouseSensitivity;

			// Token: 0x040002DC RID: 732
			public float MouseMoveX;

			// Token: 0x040002DD RID: 733
			public float MouseMoveY;

			// Token: 0x040002DE RID: 734
			public float HorizontalCameraInput;

			// Token: 0x040002DF RID: 735
			public float RX;

			// Token: 0x040002E0 RID: 736
			public float RY;

			// Token: 0x040002E1 RID: 737
			public float RS;

			// Token: 0x040002E2 RID: 738
			public float Dt;

			// Token: 0x040002E3 RID: 739
			public Vec2 MousePositionPixel;

			// Token: 0x040002E4 RID: 740
			public Vec2 ClickedPositionPixel;

			// Token: 0x040002E5 RID: 741
			public Vec3 ClickedPosition;

			// Token: 0x040002E6 RID: 742
			public Vec3 ProjectedPosition;

			// Token: 0x040002E7 RID: 743
			public Vec3 WorldMouseNear;

			// Token: 0x040002E8 RID: 744
			public Vec3 WorldMouseFar;
		}
	}
}
