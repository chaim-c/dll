using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.Cinematics
{
	// Token: 0x0200003F RID: 63
	public class HideoutBossFightBehavior : ScriptComponentBehavior
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000DCCC File Offset: 0x0000BECC
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		public int PerturbSeed
		{
			get
			{
				return this._perturbSeed;
			}
			private set
			{
				this._perturbSeed = value;
				this.ReSeedPerturbRng(0);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public void GetPlayerFrames(out MatrixFrame initialFrame, out MatrixFrame targetFrame, float perturbAmount = 0f)
		{
			this.ReSeedPerturbRng(0);
			Vec3 v;
			this.ComputePerturbedSpawnOffset(perturbAmount, out v);
			float localAngle = 3.1415927f;
			float innerRadius = this.InnerRadius;
			Vec3 vec = v - this.WalkDistance * Vec3.Forward;
			this.ComputeSpawnWorldFrame(localAngle, innerRadius, vec, out initialFrame);
			this.ComputeSpawnWorldFrame(3.1415927f, this.InnerRadius, v, out targetFrame);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000DD30 File Offset: 0x0000BF30
		public void GetBossFrames(out MatrixFrame initialFrame, out MatrixFrame targetFrame, float perturbAmount = 0f)
		{
			this.ReSeedPerturbRng(1);
			Vec3 v;
			this.ComputePerturbedSpawnOffset(perturbAmount, out v);
			float localAngle = 0f;
			float innerRadius = this.InnerRadius;
			Vec3 vec = v + this.WalkDistance * Vec3.Forward;
			this.ComputeSpawnWorldFrame(localAngle, innerRadius, vec, out initialFrame);
			this.ComputeSpawnWorldFrame(0f, this.InnerRadius, v, out targetFrame);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		public void GetAllyFrames(out List<MatrixFrame> initialFrames, out List<MatrixFrame> targetFrames, int agentCount = 10, float agentOffsetAngle = 0.15707964f, float perturbAmount = 0f)
		{
			this.ReSeedPerturbRng(2);
			initialFrames = this.ComputeSpawnWorldFrames(agentCount, this.OuterRadius, -this.WalkDistance * Vec3.Forward, 3.1415927f, agentOffsetAngle, perturbAmount).ToList<MatrixFrame>();
			this.ReSeedPerturbRng(2);
			targetFrames = this.ComputeSpawnWorldFrames(agentCount, this.OuterRadius, Vec3.Zero, 3.1415927f, agentOffsetAngle, perturbAmount).ToList<MatrixFrame>();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		public void GetBanditFrames(out List<MatrixFrame> initialFrames, out List<MatrixFrame> targetFrames, int agentCount = 10, float agentOffsetAngle = 0.15707964f, float perturbAmount = 0f)
		{
			this.ReSeedPerturbRng(3);
			initialFrames = this.ComputeSpawnWorldFrames(agentCount, this.OuterRadius, this.WalkDistance * Vec3.Forward, 0f, agentOffsetAngle, perturbAmount).ToList<MatrixFrame>();
			this.ReSeedPerturbRng(3);
			targetFrames = this.ComputeSpawnWorldFrames(agentCount, this.OuterRadius, Vec3.Zero, 0f, agentOffsetAngle, perturbAmount).ToList<MatrixFrame>();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000DE64 File Offset: 0x0000C064
		public void GetAlliesInitialFrame(out MatrixFrame frame)
		{
			float localAngle = 3.1415927f;
			float outerRadius = this.OuterRadius;
			Vec3 vec = -this.WalkDistance * Vec3.Forward;
			this.ComputeSpawnWorldFrame(localAngle, outerRadius, vec, out frame);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000DE98 File Offset: 0x0000C098
		public void GetBanditsInitialFrame(out MatrixFrame frame)
		{
			float localAngle = 0f;
			float outerRadius = this.OuterRadius;
			Vec3 vec = this.WalkDistance * Vec3.Forward;
			this.ComputeSpawnWorldFrame(localAngle, outerRadius, vec, out frame);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public bool IsWorldPointInsideCameraVolume(in Vec3 worldPoint)
		{
			Vec3 vec = base.GameEntity.GetGlobalFrame().TransformToLocal(worldPoint);
			return this.IsLocalPointInsideCameraVolume(vec);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		public bool ClampWorldPointToCameraVolume(in Vec3 worldPoint, out Vec3 clampedPoint)
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			Vec3 vec = globalFrame.TransformToLocal(worldPoint);
			bool flag = this.IsLocalPointInsideCameraVolume(vec);
			if (flag)
			{
				clampedPoint = worldPoint;
				return flag;
			}
			float num = 5f;
			float num2 = this.OuterRadius + this.WalkDistance;
			vec.x = MathF.Clamp(vec.x, -num, num);
			vec.y = MathF.Clamp(vec.y, -num2, num2);
			vec.z = MathF.Clamp(vec.z, 0f, 5f);
			clampedPoint = globalFrame.TransformToParent(vec);
			return flag;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		protected override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "ShowPreview")
			{
				this.UpdatePreview();
				this.TogglePreviewVisibility(this.ShowPreview);
				return;
			}
			if (this.ShowPreview && (variableName == "InnerRadius" || variableName == "OuterRadius" || variableName == "WalkDistance"))
			{
				this.UpdatePreview();
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000E010 File Offset: 0x0000C210
		protected override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (this.ShowPreview)
			{
				MatrixFrame frame = base.GameEntity.GetFrame();
				if (!this._previousEntityFrame.origin.NearlyEquals(frame.origin, 1E-05f) || !this._previousEntityFrame.rotation.NearlyEquals(frame.rotation, 1E-05f))
				{
					this._previousEntityFrame = frame;
					this.UpdatePreview();
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000E085 File Offset: 0x0000C285
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			this.RemovePreview();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000E094 File Offset: 0x0000C294
		private void UpdatePreview()
		{
			if (this._previewEntities == null)
			{
				this.GeneratePreview();
			}
			GameEntity previewEntities = this._previewEntities;
			MatrixFrame matrixFrame = base.GameEntity.GetGlobalFrame();
			previewEntities.SetGlobalFrame(matrixFrame);
			MatrixFrame identity = MatrixFrame.Identity;
			MatrixFrame identity2 = MatrixFrame.Identity;
			this.GetPlayerFrames(out identity, out identity2, 0.25f);
			this._previewPlayer.InitialEntity.SetGlobalFrame(identity);
			this._previewPlayer.TargetEntity.SetGlobalFrame(identity2);
			List<MatrixFrame> list;
			List<MatrixFrame> list2;
			this.GetAllyFrames(out list, out list2, 10, 0.15707964f, 0.25f);
			int num = 0;
			foreach (HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo hideoutBossFightPreviewEntityInfo in this._previewAllies)
			{
				GameEntity initialEntity = hideoutBossFightPreviewEntityInfo.InitialEntity;
				matrixFrame = list[num];
				initialEntity.SetGlobalFrame(matrixFrame);
				GameEntity targetEntity = hideoutBossFightPreviewEntityInfo.TargetEntity;
				matrixFrame = list2[num];
				targetEntity.SetGlobalFrame(matrixFrame);
				num++;
			}
			this.GetBossFrames(out identity, out identity2, 0.25f);
			this._previewBoss.InitialEntity.SetGlobalFrame(identity);
			this._previewBoss.TargetEntity.SetGlobalFrame(identity2);
			List<MatrixFrame> list3;
			List<MatrixFrame> list4;
			this.GetBanditFrames(out list3, out list4, 10, 0.15707964f, 0.25f);
			int num2 = 0;
			foreach (HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo hideoutBossFightPreviewEntityInfo2 in this._previewBandits)
			{
				GameEntity initialEntity2 = hideoutBossFightPreviewEntityInfo2.InitialEntity;
				matrixFrame = list3[num2];
				initialEntity2.SetGlobalFrame(matrixFrame);
				GameEntity targetEntity2 = hideoutBossFightPreviewEntityInfo2.TargetEntity;
				matrixFrame = list4[num2];
				targetEntity2.SetGlobalFrame(matrixFrame);
				num2++;
			}
			MatrixFrame frame = this._previewCamera.GetFrame();
			Vec3 scaleVector = frame.rotation.GetScaleVector();
			Vec3 vec = Vec3.Forward * (this.OuterRadius + this.WalkDistance) + Vec3.Side * 5f + Vec3.Up * 5f;
			Vec3 scaleAmountXYZ = new Vec3(vec.x / scaleVector.x, vec.y / scaleVector.y, vec.z / scaleVector.z, -1f);
			frame.rotation.ApplyScaleLocal(scaleAmountXYZ);
			this._previewCamera.SetFrame(ref frame);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000E304 File Offset: 0x0000C504
		private void GeneratePreview()
		{
			Scene scene = base.GameEntity.Scene;
			this._previewEntities = GameEntity.CreateEmpty(scene, false);
			this._previewEntities.EntityFlags |= EntityFlags.DontSaveToScene;
			MatrixFrame identity = MatrixFrame.Identity;
			this._previewEntities.SetFrame(ref identity);
			MatrixFrame globalFrame = this._previewEntities.GetGlobalFrame();
			GameEntity gameEntity = GameEntity.Instantiate(scene, "hideout_boss_fight_preview_boss", globalFrame);
			this._previewEntities.AddChild(gameEntity, false);
			GameEntity initialEntity;
			GameEntity targetEntity;
			this.ReadPrefabEntity(gameEntity, out initialEntity, out targetEntity);
			this._previewBoss = new HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo(gameEntity, initialEntity, targetEntity);
			GameEntity gameEntity2 = GameEntity.Instantiate(scene, "hideout_boss_fight_preview_player", globalFrame);
			this._previewEntities.AddChild(gameEntity2, false);
			GameEntity initialEntity2;
			GameEntity targetEntity2;
			this.ReadPrefabEntity(gameEntity2, out initialEntity2, out targetEntity2);
			this._previewPlayer = new HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo(gameEntity2, initialEntity2, targetEntity2);
			for (int i = 0; i < 10; i++)
			{
				GameEntity gameEntity3 = GameEntity.Instantiate(scene, "hideout_boss_fight_preview_ally", globalFrame);
				this._previewEntities.AddChild(gameEntity3, false);
				GameEntity initialEntity3;
				GameEntity targetEntity3;
				this.ReadPrefabEntity(gameEntity3, out initialEntity3, out targetEntity3);
				this._previewAllies.Add(new HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo(gameEntity3, initialEntity3, targetEntity3));
			}
			for (int j = 0; j < 10; j++)
			{
				GameEntity gameEntity4 = GameEntity.Instantiate(scene, "hideout_boss_fight_preview_bandit", globalFrame);
				this._previewEntities.AddChild(gameEntity4, false);
				GameEntity initialEntity4;
				GameEntity targetEntity4;
				this.ReadPrefabEntity(gameEntity4, out initialEntity4, out targetEntity4);
				this._previewBandits.Add(new HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo(gameEntity4, initialEntity4, targetEntity4));
			}
			this._previewCamera = GameEntity.Instantiate(scene, "hideout_boss_fight_camera_preview", globalFrame);
			this._previewEntities.AddChild(this._previewCamera, false);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000E490 File Offset: 0x0000C690
		private void RemovePreview()
		{
			if (this._previewEntities != null)
			{
				this._previewEntities.Remove(90);
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000E4AD File Offset: 0x0000C6AD
		private void TogglePreviewVisibility(bool value)
		{
			if (this._previewEntities != null)
			{
				this._previewEntities.SetVisibilityExcludeParents(value);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		private void ReadPrefabEntity(GameEntity entity, out GameEntity initialEntity, out GameEntity targetEntity)
		{
			GameEntity firstChildEntityWithTag = entity.GetFirstChildEntityWithTag("initial_frame");
			if (firstChildEntityWithTag == null)
			{
				Debug.FailedAssert("Prefab entity " + entity.Name + " is not a spawn prefab with an initial frame entity", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Objects\\Cinematics\\HideoutBossFightBehavior.cs", "ReadPrefabEntity", 389);
			}
			GameEntity firstChildEntityWithTag2 = entity.GetFirstChildEntityWithTag("target_frame");
			if (firstChildEntityWithTag2 == null)
			{
				Debug.FailedAssert("Prefab entity " + entity.Name + " is not a spawn prefab with an target frame entity", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Objects\\Cinematics\\HideoutBossFightBehavior.cs", "ReadPrefabEntity", 395);
			}
			initialEntity = firstChildEntityWithTag;
			targetEntity = firstChildEntityWithTag2;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000E55C File Offset: 0x0000C75C
		private void FindRadialPlacementFrame(float angle, float radius, out MatrixFrame frame)
		{
			float f;
			float num;
			MathF.SinCos(angle, out f, out num);
			Vec3 v = num * Vec3.Forward + f * Vec3.Side;
			Vec3 o = radius * v;
			Vec3 vec = ((num > 0f) ? -1f : 1f) * Vec3.Forward;
			Mat3 rot = Mat3.CreateMat3WithForward(vec);
			frame = new MatrixFrame(rot, o);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		private void SnapOnClosestCollider(ref MatrixFrame frameWs)
		{
			Scene scene = base.GameEntity.Scene;
			Vec3 origin = frameWs.origin;
			origin.z += 5f;
			Vec3 targetPoint = origin;
			float num = 500f;
			targetPoint.z -= num;
			float num2;
			if (scene.RayCastForClosestEntityOrTerrain(origin, targetPoint, out num2, 0.01f, BodyFlags.CommonFocusRayCastExcludeFlags))
			{
				frameWs.origin.z = origin.z - num2;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000E63B File Offset: 0x0000C83B
		private void ReSeedPerturbRng(int seedOffset = 0)
		{
			this._perturbRng = new Random(this._perturbSeed + seedOffset);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000E650 File Offset: 0x0000C850
		private void ComputeSpawnWorldFrame(float localAngle, float localRadius, in Vec3 localOffset, out MatrixFrame worldFrame)
		{
			MatrixFrame m;
			this.FindRadialPlacementFrame(localAngle, localRadius, out m);
			m.origin += localOffset;
			worldFrame = base.GameEntity.GetGlobalFrame().TransformToParent(m);
			this.SnapOnClosestCollider(ref worldFrame);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000E6A7 File Offset: 0x0000C8A7
		private IEnumerable<MatrixFrame> ComputeSpawnWorldFrames(int spawnCount, float localRadius, Vec3 localOffset, float localBaseAngle, float localOffsetAngle, float localPerturbAmount = 0f)
		{
			float[] localPlacementAngles = new float[]
			{
				localBaseAngle + localOffsetAngle / 2f,
				localBaseAngle - localOffsetAngle / 2f
			};
			int angleIndex = 0;
			MatrixFrame identity = MatrixFrame.Identity;
			Vec3 zero = Vec3.Zero;
			int num;
			for (int i = 0; i < spawnCount; i = num + 1)
			{
				this.ComputePerturbedSpawnOffset(localPerturbAmount, out zero);
				float localAngle = localPlacementAngles[angleIndex];
				Vec3 vec = zero + localOffset;
				this.ComputeSpawnWorldFrame(localAngle, localRadius, vec, out identity);
				yield return identity;
				localPlacementAngles[angleIndex] += (float)((angleIndex == 0) ? 1 : -1) * localOffsetAngle;
				angleIndex = (angleIndex + 1) % 2;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		private void ComputePerturbedSpawnOffset(float perturbAmount, out Vec3 perturbVector)
		{
			perturbVector = Vec3.Zero;
			perturbAmount = MathF.Abs(perturbAmount);
			if (perturbAmount > 1E-05f)
			{
				float num;
				float num2;
				MathF.SinCos(6.2831855f * this._perturbRng.NextFloat(), out num, out num2);
				perturbVector.x = perturbAmount * num2;
				perturbVector.y = perturbAmount * num;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000E738 File Offset: 0x0000C938
		private bool IsLocalPointInsideCameraVolume(in Vec3 localPoint)
		{
			float num = 5f;
			float num2 = this.OuterRadius + this.WalkDistance;
			return localPoint.x >= -num && localPoint.x <= num && localPoint.y >= -num2 && localPoint.y <= num2 && localPoint.z >= 0f && localPoint.z <= 5f;
		}

		// Token: 0x040000D2 RID: 210
		private const int PreviewPerturbSeed = 0;

		// Token: 0x040000D3 RID: 211
		private const float PreviewPerturbAmount = 0.25f;

		// Token: 0x040000D4 RID: 212
		private const int PreviewTroopCount = 10;

		// Token: 0x040000D5 RID: 213
		private const float PreviewPlacementAngle = 0.15707964f;

		// Token: 0x040000D6 RID: 214
		private const string InitialFrameTag = "initial_frame";

		// Token: 0x040000D7 RID: 215
		private const string TargetFrameTag = "target_frame";

		// Token: 0x040000D8 RID: 216
		private const string BossPreviewPrefab = "hideout_boss_fight_preview_boss";

		// Token: 0x040000D9 RID: 217
		private const string PlayerPreviewPrefab = "hideout_boss_fight_preview_player";

		// Token: 0x040000DA RID: 218
		private const string AllyPreviewPrefab = "hideout_boss_fight_preview_ally";

		// Token: 0x040000DB RID: 219
		private const string BanditPreviewPrefab = "hideout_boss_fight_preview_bandit";

		// Token: 0x040000DC RID: 220
		private const string PreviewCameraPrefab = "hideout_boss_fight_camera_preview";

		// Token: 0x040000DD RID: 221
		public const float MaxCameraHeight = 5f;

		// Token: 0x040000DE RID: 222
		public const float MaxCameraWidth = 10f;

		// Token: 0x040000DF RID: 223
		public float InnerRadius = 2.5f;

		// Token: 0x040000E0 RID: 224
		public float OuterRadius = 6f;

		// Token: 0x040000E1 RID: 225
		public float WalkDistance = 3f;

		// Token: 0x040000E2 RID: 226
		public bool ShowPreview;

		// Token: 0x040000E3 RID: 227
		private int _perturbSeed;

		// Token: 0x040000E4 RID: 228
		private Random _perturbRng = new Random(0);

		// Token: 0x040000E5 RID: 229
		private MatrixFrame _previousEntityFrame = MatrixFrame.Identity;

		// Token: 0x040000E6 RID: 230
		private GameEntity _previewEntities;

		// Token: 0x040000E7 RID: 231
		private List<HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo> _previewAllies = new List<HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo>();

		// Token: 0x040000E8 RID: 232
		private List<HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo> _previewBandits = new List<HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo>();

		// Token: 0x040000E9 RID: 233
		private HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo _previewBoss = HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo.Invalid;

		// Token: 0x040000EA RID: 234
		private HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo _previewPlayer = HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo.Invalid;

		// Token: 0x040000EB RID: 235
		private GameEntity _previewCamera;

		// Token: 0x0200011B RID: 283
		private readonly struct HideoutBossFightPreviewEntityInfo
		{
			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0005235B File Offset: 0x0005055B
			public static HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo Invalid
			{
				get
				{
					return new HideoutBossFightBehavior.HideoutBossFightPreviewEntityInfo(null, null, null);
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00052365 File Offset: 0x00050565
			public bool IsValid
			{
				get
				{
					return this.BaseEntity == null;
				}
			}

			// Token: 0x06000BA4 RID: 2980 RVA: 0x00052373 File Offset: 0x00050573
			public HideoutBossFightPreviewEntityInfo(GameEntity baseEntity, GameEntity initialEntity, GameEntity targetEntity)
			{
				this.BaseEntity = baseEntity;
				this.InitialEntity = initialEntity;
				this.TargetEntity = targetEntity;
			}

			// Token: 0x04000501 RID: 1281
			public readonly GameEntity BaseEntity;

			// Token: 0x04000502 RID: 1282
			public readonly GameEntity InitialEntity;

			// Token: 0x04000503 RID: 1283
			public readonly GameEntity TargetEntity;
		}

		// Token: 0x0200011C RID: 284
		private enum HideoutSeedPerturbOffset
		{
			// Token: 0x04000505 RID: 1285
			Player,
			// Token: 0x04000506 RID: 1286
			Boss,
			// Token: 0x04000507 RID: 1287
			Ally,
			// Token: 0x04000508 RID: 1288
			Bandit
		}
	}
}
