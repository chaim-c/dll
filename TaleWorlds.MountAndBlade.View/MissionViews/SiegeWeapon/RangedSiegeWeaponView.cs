using System;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Screens;

namespace TaleWorlds.MountAndBlade.View.MissionViews.SiegeWeapon
{
	// Token: 0x0200007B RID: 123
	public class RangedSiegeWeaponView : UsableMissionObjectComponent
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00023D6C File Offset: 0x00021F6C
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00023D74 File Offset: 0x00021F74
		public RangedSiegeWeapon RangedSiegeWeapon { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00023D7D File Offset: 0x00021F7D
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00023D85 File Offset: 0x00021F85
		public MissionScreen MissionScreen { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00023D8E File Offset: 0x00021F8E
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00023D96 File Offset: 0x00021F96
		public Camera Camera { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00023D9F File Offset: 0x00021F9F
		public GameEntity CameraHolder
		{
			get
			{
				return this.RangedSiegeWeapon.cameraHolder;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00023DAC File Offset: 0x00021FAC
		public Agent PilotAgent
		{
			get
			{
				return this.RangedSiegeWeapon.PilotAgent;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00023DB9 File Offset: 0x00021FB9
		internal void Initialize(RangedSiegeWeapon rangedSiegeWeapon, MissionScreen missionScreen)
		{
			this.RangedSiegeWeapon = rangedSiegeWeapon;
			this.MissionScreen = missionScreen;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00023DC9 File Offset: 0x00021FC9
		protected override void OnAdded(Scene scene)
		{
			base.OnAdded(scene);
			if (this.CameraHolder != null)
			{
				this.CreateCamera();
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00023DE8 File Offset: 0x00021FE8
		protected override void OnMissionReset()
		{
			base.OnMissionReset();
			if (this.CameraHolder != null)
			{
				this._cameraYaw = this._cameraInitialYaw;
				this._cameraPitch = this._cameraInitialPitch;
				this.ApplyCameraRotation();
				this._isInWeaponCameraMode = false;
				this.ResetCamera();
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00023E34 File Offset: 0x00022034
		public override bool IsOnTickRequired()
		{
			return true;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00023E37 File Offset: 0x00022037
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (!GameNetwork.IsReplay)
			{
				this.HandleUserInput(dt);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00023E50 File Offset: 0x00022050
		protected virtual void HandleUserInput(float dt)
		{
			if (this.PilotAgent != null && this.PilotAgent.IsMainAgent && this.CameraHolder != null)
			{
				if (!this._isInWeaponCameraMode)
				{
					this._isInWeaponCameraMode = true;
					this.StartUsingWeaponCamera();
				}
				this.HandleUserCameraRotation(dt);
			}
			if (this._isInWeaponCameraMode && (this.PilotAgent == null || !this.PilotAgent.IsMainAgent))
			{
				this._isInWeaponCameraMode = false;
				this.ResetCamera();
			}
			this.HandleUserAiming(dt);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00023ED0 File Offset: 0x000220D0
		private void CreateCamera()
		{
			this.Camera = Camera.CreateCamera();
			float aspectRatio = Screen.AspectRatio;
			this.Camera.SetFovVertical(1.0471976f, aspectRatio, 0.1f, 1000f);
			this.Camera.Entity = this.CameraHolder;
			MatrixFrame frame = this.CameraHolder.GetFrame();
			Vec3 eulerAngles = frame.rotation.GetEulerAngles();
			this._cameraYaw = eulerAngles.z;
			this._cameraPitch = eulerAngles.x;
			this._cameraRoll = eulerAngles.y;
			this._cameraPositionOffset = frame.origin;
			this._cameraPositionOffset.RotateAboutZ(-this._cameraYaw);
			this._cameraPositionOffset.RotateAboutX(-this._cameraPitch);
			this._cameraPositionOffset.RotateAboutY(-this._cameraRoll);
			this._cameraInitialYaw = this._cameraYaw;
			this._cameraInitialPitch = this._cameraPitch;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00023FB4 File Offset: 0x000221B4
		protected virtual void StartUsingWeaponCamera()
		{
			if (this.CameraHolder != null && this.Camera.Entity != null)
			{
				this.MissionScreen.CustomCamera = this.Camera;
				Agent.Main.IsLookDirectionLocked = true;
				return;
			}
			Debug.FailedAssert("Camera entities are null.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\MissionViews\\SiegeWeapon\\RangedSiegeWeaponView.cs", "StartUsingWeaponCamera", 140);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00024018 File Offset: 0x00022218
		private void ResetCamera()
		{
			if (this.MissionScreen.CustomCamera == this.Camera)
			{
				this.MissionScreen.CustomCamera = null;
				if (Agent.Main != null)
				{
					Agent.Main.IsLookDirectionLocked = false;
					this.MissionScreen.SetExtraCameraParameters(false, 0f);
				}
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0002406C File Offset: 0x0002226C
		protected virtual void HandleUserCameraRotation(float dt)
		{
			float cameraYaw = this._cameraYaw;
			float cameraPitch = this._cameraPitch;
			if (this.MissionScreen.SceneLayer.Input.IsGameKeyDown(10))
			{
				this._cameraYaw = this._cameraInitialYaw;
				this._cameraPitch = this._cameraInitialPitch;
			}
			this._cameraYaw += this.MissionScreen.SceneLayer.Input.GetMouseMoveX() * dt * 0.2f;
			this._cameraPitch += this.MissionScreen.SceneLayer.Input.GetMouseMoveY() * dt * 0.2f;
			this._cameraYaw = MBMath.ClampFloat(this._cameraYaw, 1.5707964f, 4.712389f);
			this._cameraPitch = MBMath.ClampFloat(this._cameraPitch, 1.0471976f, 1.7453294f);
			if (cameraPitch != this._cameraPitch || cameraYaw != this._cameraYaw)
			{
				this.ApplyCameraRotation();
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00024158 File Offset: 0x00022358
		private void ApplyCameraRotation()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			identity.rotation.RotateAboutUp(this._cameraYaw);
			identity.rotation.RotateAboutSide(this._cameraPitch);
			identity.rotation.RotateAboutForward(this._cameraRoll);
			identity.Strafe(this._cameraPositionOffset.x);
			identity.Advance(this._cameraPositionOffset.y);
			identity.Elevate(this._cameraPositionOffset.z);
			this.CameraHolder.SetFrame(ref identity);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000241E8 File Offset: 0x000223E8
		private void HandleUserAiming(float dt)
		{
			bool flag = false;
			float num = 0f;
			float num2 = 0f;
			if (this.PilotAgent != null && this.PilotAgent.IsMainAgent)
			{
				if (this.UsesMouseForAiming)
				{
					InputContext input = this.MissionScreen.SceneLayer.Input;
					float num3 = dt / 0.0006f;
					float num4 = input.GetMouseMoveX() + num3 * input.GetGameKeyAxis("CameraAxisX");
					float num5 = input.GetMouseMoveY() + -num3 * input.GetGameKeyAxis("CameraAxisY");
					if (NativeConfig.InvertMouse)
					{
						num5 *= -1f;
					}
					Vec2 vec = new Vec2(-num4, -num5);
					if (vec.IsNonZero())
					{
						float num6 = vec.Normalize();
						num6 = MathF.Min(5f, MathF.Pow(num6, 1.5f) * 0.025f);
						vec *= num6;
						num = vec.x;
						num2 = vec.y;
					}
				}
				else
				{
					if (this.MissionScreen.SceneLayer.Input.IsGameKeyDown(2))
					{
						num = 1f;
					}
					else if (this.MissionScreen.SceneLayer.Input.IsGameKeyDown(3))
					{
						num = -1f;
					}
					if (this.MissionScreen.SceneLayer.Input.IsGameKeyDown(0))
					{
						num2 = 1f;
					}
					else if (this.MissionScreen.SceneLayer.Input.IsGameKeyDown(1))
					{
						num2 = -1f;
					}
				}
				if (num != 0f)
				{
					flag = true;
				}
				if (num2 != 0f)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.RangedSiegeWeapon.GiveInput(num, num2);
			}
		}

		// Token: 0x040002BF RID: 703
		private float _cameraYaw;

		// Token: 0x040002C0 RID: 704
		private float _cameraPitch;

		// Token: 0x040002C1 RID: 705
		private float _cameraRoll;

		// Token: 0x040002C2 RID: 706
		private float _cameraInitialYaw;

		// Token: 0x040002C3 RID: 707
		private float _cameraInitialPitch;

		// Token: 0x040002C4 RID: 708
		private Vec3 _cameraPositionOffset;

		// Token: 0x040002C5 RID: 709
		private bool _isInWeaponCameraMode;

		// Token: 0x040002C6 RID: 710
		protected bool UsesMouseForAiming;
	}
}
