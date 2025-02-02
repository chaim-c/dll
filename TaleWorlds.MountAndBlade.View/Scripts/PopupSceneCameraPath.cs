using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x02000039 RID: 57
	public class PopupSceneCameraPath : ScriptComponentBehavior
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00017C79 File Offset: 0x00015E79
		protected override void OnInit()
		{
			base.OnInit();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00017C8D File Offset: 0x00015E8D
		protected override void OnEditorInit()
		{
			base.OnEditorInit();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00017C98 File Offset: 0x00015E98
		public void Initialize()
		{
			if (this.SkeletonName != "" && (base.GameEntity.Skeleton == null || base.GameEntity.Skeleton.GetName() != this.SkeletonName))
			{
				base.GameEntity.CreateSimpleSkeleton(this.SkeletonName);
			}
			else if (this.SkeletonName == "" && base.GameEntity.Skeleton != null)
			{
				base.GameEntity.RemoveSkeleton();
			}
			if (this.LookAtEntity != "")
			{
				this._lookAtEntity = base.GameEntity.Scene.GetFirstEntityWithName(this.LookAtEntity);
			}
			this._transitionState[0].path = ((this.InitialPath == "") ? null : base.GameEntity.Scene.GetPathWithName(this.InitialPath));
			this._transitionState[0].animationName = this.InitialAnimationClip;
			this._transitionState[0].startTime = this.InitialPathStartTime;
			this._transitionState[0].duration = this.InitialPathDuration;
			this._transitionState[0].interpolation = this.InitialInterpolation;
			this._transitionState[0].fadeCamera = this.InitialFadeOut;
			this._transitionState[0].soundEvent = this.InitialSound;
			this._transitionState[1].path = ((this.PositivePath == "") ? null : base.GameEntity.Scene.GetPathWithName(this.PositivePath));
			this._transitionState[1].animationName = this.PositiveAnimationClip;
			this._transitionState[1].startTime = this.PositivePathStartTime;
			this._transitionState[1].duration = this.PositivePathDuration;
			this._transitionState[1].interpolation = this.PositiveInterpolation;
			this._transitionState[1].fadeCamera = this.PositiveFadeOut;
			this._transitionState[1].soundEvent = this.PositiveSound;
			this._transitionState[2].path = ((this.NegativePath == "") ? null : base.GameEntity.Scene.GetPathWithName(this.NegativePath));
			this._transitionState[2].animationName = this.NegativeAnimationClip;
			this._transitionState[2].startTime = this.NegativePathStartTime;
			this._transitionState[2].duration = this.NegativePathDuration;
			this._transitionState[2].interpolation = this.NegativeInterpolation;
			this._transitionState[2].fadeCamera = this.NegativeFadeOut;
			this._transitionState[2].soundEvent = this.NegativeSound;
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = base.GameEntity.GlobalPosition;
			SoundManager.SetListenerFrame(identity);
			List<GameEntity> list = new List<GameEntity>();
			base.Scene.GetAllEntitiesWithScriptComponent<PopupSceneSkeletonAnimationScript>(ref list);
			list.ForEach(delegate(GameEntity e)
			{
				this._skeletonAnims.Add(e.GetFirstScriptOfType<PopupSceneSkeletonAnimationScript>());
			});
			this._skeletonAnims.ForEach(delegate(PopupSceneSkeletonAnimationScript s)
			{
				s.Initialize();
			});
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00018018 File Offset: 0x00016218
		private void SetState(int state)
		{
			if (base.GameEntity.Skeleton != null && !string.IsNullOrEmpty(this._transitionState[state].animationName))
			{
				base.GameEntity.Skeleton.SetAnimationAtChannel(this._transitionState[state].animationName, 0, 1f, -1f, 0f);
			}
			this._currentState = state;
			this._transitionState[state].alpha = 0f;
			if (this._transitionState[state].path != null)
			{
				this._transitionState[state].totalDistance = this._transitionState[state].path.GetTotalLength();
			}
			if (this._transitionState[state].soundEvent != "")
			{
				SoundEvent activeSoundEvent = this._activeSoundEvent;
				if (activeSoundEvent != null)
				{
					activeSoundEvent.Stop();
				}
				this._activeSoundEvent = SoundEvent.CreateEventFromString(this._transitionState[state].soundEvent, null);
				if (this._isReady)
				{
					SoundEvent activeSoundEvent2 = this._activeSoundEvent;
					if (activeSoundEvent2 != null)
					{
						activeSoundEvent2.Play();
					}
				}
			}
			this.UpdateCamera(0f, ref this._transitionState[state]);
			this._skeletonAnims.ForEach(delegate(PopupSceneSkeletonAnimationScript s)
			{
				s.SetState(state);
			});
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000181B0 File Offset: 0x000163B0
		public void SetInitialState()
		{
			this.SetState(0);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000181B9 File Offset: 0x000163B9
		public void SetPositiveState()
		{
			this.SetState(1);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000181C2 File Offset: 0x000163C2
		public void SetNegativeState()
		{
			this.SetState(2);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000181CB File Offset: 0x000163CB
		public void SetIsReady(bool isReady)
		{
			if (this._isReady != isReady)
			{
				if (isReady)
				{
					SoundEvent activeSoundEvent = this._activeSoundEvent;
					if (activeSoundEvent != null && !activeSoundEvent.IsPlaying())
					{
						this._activeSoundEvent.Play();
					}
				}
				this._isReady = isReady;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00018203 File Offset: 0x00016403
		public float GetCameraFade()
		{
			return this._cameraFadeValue;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001820C File Offset: 0x0001640C
		public void Destroy()
		{
			SoundEvent activeSoundEvent = this._activeSoundEvent;
			if (activeSoundEvent != null)
			{
				activeSoundEvent.Stop();
			}
			for (int i = 0; i < 3; i++)
			{
				this._transitionState[i].path = null;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00018248 File Offset: 0x00016448
		private float InQuadBlend(float t)
		{
			return t * t;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001824D File Offset: 0x0001644D
		private float OutQuadBlend(float t)
		{
			return t * (2f - t);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00018258 File Offset: 0x00016458
		private float InOutQuadBlend(float t)
		{
			if (t >= 0.5f)
			{
				return -1f + (4f - 2f * t) * t;
			}
			return 2f * t * t;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00018284 File Offset: 0x00016484
		private MatrixFrame CreateLookAt(Vec3 position, Vec3 target, Vec3 upVector)
		{
			Vec3 vec = target - position;
			vec.Normalize();
			Vec3 vec2 = Vec3.CrossProduct(vec, upVector);
			vec2.Normalize();
			Vec3 vec3 = Vec3.CrossProduct(vec2, vec);
			float x = vec2.x;
			float y = vec2.y;
			float z = vec2.z;
			float <<EMPTY_NAME>> = 0f;
			float x2 = vec3.x;
			float y2 = vec3.y;
			float z2 = vec3.z;
			float 2 = 0f;
			float 3 = -vec.x;
			float 4 = -vec.y;
			float 5 = -vec.z;
			float 6 = 0f;
			float x3 = position.x;
			float y3 = position.y;
			float z3 = position.z;
			float 7 = 1f;
			return new MatrixFrame(x, y, z, <<EMPTY_NAME>>, x2, y2, z2, 2, 3, 4, 5, 6, x3, y3, z3, 7);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00018357 File Offset: 0x00016557
		private float Clamp(float x, float a, float b)
		{
			if (x < a)
			{
				return a;
			}
			if (x <= b)
			{
				return x;
			}
			return b;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00018366 File Offset: 0x00016566
		private float SmoothStep(float edge0, float edge1, float x)
		{
			x = this.Clamp((x - edge0) / (edge1 - edge0), 0f, 1f);
			return x * x * (3f - 2f * x);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00018394 File Offset: 0x00016594
		private void UpdateCamera(float dt, ref PopupSceneCameraPath.PathAnimationState state)
		{
			GameEntity gameEntity = base.GameEntity.Scene.FindEntityWithTag("camera_instance");
			if (gameEntity == null)
			{
				return;
			}
			state.alpha += dt;
			if (state.alpha > state.startTime + state.duration)
			{
				state.alpha = state.startTime + state.duration;
			}
			float num = this.SmoothStep(state.startTime, state.startTime + state.duration, state.alpha);
			switch (state.interpolation)
			{
			case PopupSceneCameraPath.InterpolationType.EaseIn:
				num = this.InQuadBlend(num);
				break;
			case PopupSceneCameraPath.InterpolationType.EaseOut:
				num = this.OutQuadBlend(num);
				break;
			case PopupSceneCameraPath.InterpolationType.EaseInOut:
				num = this.InOutQuadBlend(num);
				break;
			}
			state.easedAlpha = num;
			if (state.fadeCamera)
			{
				this._cameraFadeValue = num;
			}
			if (base.GameEntity.Skeleton != null && !string.IsNullOrEmpty(state.animationName))
			{
				MatrixFrame matrixFrame = base.GameEntity.Skeleton.GetBoneEntitialFrame((sbyte)this.BoneIndex);
				matrixFrame = this._localFrameIdentity.TransformToParent(matrixFrame);
				MatrixFrame listenerFrame = default(MatrixFrame);
				listenerFrame.rotation = matrixFrame.rotation;
				listenerFrame.rotation.u = -matrixFrame.rotation.s;
				listenerFrame.rotation.f = -matrixFrame.rotation.u;
				listenerFrame.rotation.s = matrixFrame.rotation.f;
				listenerFrame.origin = matrixFrame.origin + this.AttachmentOffset;
				gameEntity.SetFrame(ref listenerFrame);
				SoundManager.SetListenerFrame(listenerFrame);
				return;
			}
			if (state.path != null)
			{
				float distance = num * state.totalDistance;
				Vec3 origin = state.path.GetFrameForDistance(distance).origin;
				MatrixFrame matrixFrame2 = MatrixFrame.Identity;
				if (this._lookAtEntity != null)
				{
					matrixFrame2 = this.CreateLookAt(origin, this._lookAtEntity.GetGlobalFrame().origin, Vec3.Up);
				}
				else
				{
					matrixFrame2.origin = origin;
				}
				gameEntity.SetGlobalFrame(matrixFrame2);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000185AF File Offset: 0x000167AF
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000185B9 File Offset: 0x000167B9
		protected override void OnTick(float dt)
		{
			this.UpdateCamera(dt, ref this._transitionState[this._currentState]);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000185D3 File Offset: 0x000167D3
		protected override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.OnTick(dt);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000185E4 File Offset: 0x000167E4
		protected override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			this.Initialize();
			if (variableName == "TestInitial")
			{
				this.SetState(0);
			}
			if (variableName == "TestPositive")
			{
				this.SetState(1);
			}
			if (variableName == "TestNegative")
			{
				this.SetState(2);
			}
		}

		// Token: 0x040001B5 RID: 437
		public string LookAtEntity = "";

		// Token: 0x040001B6 RID: 438
		public string SkeletonName = "";

		// Token: 0x040001B7 RID: 439
		public int BoneIndex;

		// Token: 0x040001B8 RID: 440
		public Vec3 AttachmentOffset = new Vec3(0f, 0f, 0f, -1f);

		// Token: 0x040001B9 RID: 441
		public string InitialPath = "";

		// Token: 0x040001BA RID: 442
		public string InitialAnimationClip = "";

		// Token: 0x040001BB RID: 443
		public string InitialSound = "event:/mission/siege/siegetower/doorland";

		// Token: 0x040001BC RID: 444
		public float InitialPathStartTime;

		// Token: 0x040001BD RID: 445
		public float InitialPathDuration = 1f;

		// Token: 0x040001BE RID: 446
		public PopupSceneCameraPath.InterpolationType InitialInterpolation;

		// Token: 0x040001BF RID: 447
		public bool InitialFadeOut;

		// Token: 0x040001C0 RID: 448
		public string PositivePath = "";

		// Token: 0x040001C1 RID: 449
		public string PositiveAnimationClip = "";

		// Token: 0x040001C2 RID: 450
		public string PositiveSound = "";

		// Token: 0x040001C3 RID: 451
		public float PositivePathStartTime;

		// Token: 0x040001C4 RID: 452
		public float PositivePathDuration = 1f;

		// Token: 0x040001C5 RID: 453
		public PopupSceneCameraPath.InterpolationType PositiveInterpolation;

		// Token: 0x040001C6 RID: 454
		public bool PositiveFadeOut;

		// Token: 0x040001C7 RID: 455
		public string NegativePath = "";

		// Token: 0x040001C8 RID: 456
		public string NegativeAnimationClip = "";

		// Token: 0x040001C9 RID: 457
		public string NegativeSound = "";

		// Token: 0x040001CA RID: 458
		public float NegativePathStartTime;

		// Token: 0x040001CB RID: 459
		public float NegativePathDuration = 1f;

		// Token: 0x040001CC RID: 460
		public PopupSceneCameraPath.InterpolationType NegativeInterpolation;

		// Token: 0x040001CD RID: 461
		public bool NegativeFadeOut;

		// Token: 0x040001CE RID: 462
		private bool _isReady;

		// Token: 0x040001CF RID: 463
		public SimpleButton TestInitial;

		// Token: 0x040001D0 RID: 464
		public SimpleButton TestPositive;

		// Token: 0x040001D1 RID: 465
		public SimpleButton TestNegative;

		// Token: 0x040001D2 RID: 466
		private MatrixFrame _localFrameIdentity = MatrixFrame.Identity;

		// Token: 0x040001D3 RID: 467
		private GameEntity _lookAtEntity;

		// Token: 0x040001D4 RID: 468
		private int _currentState;

		// Token: 0x040001D5 RID: 469
		private float _cameraFadeValue;

		// Token: 0x040001D6 RID: 470
		private List<PopupSceneSkeletonAnimationScript> _skeletonAnims = new List<PopupSceneSkeletonAnimationScript>();

		// Token: 0x040001D7 RID: 471
		private SoundEvent _activeSoundEvent;

		// Token: 0x040001D8 RID: 472
		private readonly PopupSceneCameraPath.PathAnimationState[] _transitionState = new PopupSceneCameraPath.PathAnimationState[3];

		// Token: 0x020000A1 RID: 161
		public enum InterpolationType
		{
			// Token: 0x04000354 RID: 852
			Linear,
			// Token: 0x04000355 RID: 853
			EaseIn,
			// Token: 0x04000356 RID: 854
			EaseOut,
			// Token: 0x04000357 RID: 855
			EaseInOut
		}

		// Token: 0x020000A2 RID: 162
		public struct PathAnimationState
		{
			// Token: 0x04000358 RID: 856
			public Path path;

			// Token: 0x04000359 RID: 857
			public string animationName;

			// Token: 0x0400035A RID: 858
			public float totalDistance;

			// Token: 0x0400035B RID: 859
			public float startTime;

			// Token: 0x0400035C RID: 860
			public float duration;

			// Token: 0x0400035D RID: 861
			public float alpha;

			// Token: 0x0400035E RID: 862
			public float easedAlpha;

			// Token: 0x0400035F RID: 863
			public bool fadeCamera;

			// Token: 0x04000360 RID: 864
			public PopupSceneCameraPath.InterpolationType interpolation;

			// Token: 0x04000361 RID: 865
			public string soundEvent;
		}
	}
}
