using System;
using System.Collections.Generic;
using SandBox.Objects.AnimationPoints;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.Cinematics
{
	// Token: 0x02000040 RID: 64
	public class SkeletonAnimatedCamera : ScriptComponentBehavior
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E818 File Offset: 0x0000CA18
		private void CreateVisualizer()
		{
			if (this.SkeletonName != "" && this.AnimationName != "")
			{
				base.GameEntity.CreateSimpleSkeleton(this.SkeletonName);
				base.GameEntity.Skeleton.SetAnimationAtChannel(this.AnimationName, 0, 1f, -1f, 0f);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E880 File Offset: 0x0000CA80
		protected override void OnInit()
		{
			base.OnInit();
			this.CreateVisualizer();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000E88E File Offset: 0x0000CA8E
		protected override void OnEditorInit()
		{
			base.OnEditorInit();
			this.OnInit();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000E89C File Offset: 0x0000CA9C
		protected override void OnTick(float dt)
		{
			GameEntity gameEntity = base.GameEntity.Scene.FindEntityWithTag("camera_instance");
			if (gameEntity != null && base.GameEntity.Skeleton != null)
			{
				MatrixFrame matrixFrame = base.GameEntity.Skeleton.GetBoneEntitialFrame((sbyte)this.BoneIndex);
				matrixFrame = base.GameEntity.GetGlobalFrame().TransformToParent(matrixFrame);
				MatrixFrame listenerFrame = default(MatrixFrame);
				listenerFrame.rotation = matrixFrame.rotation;
				listenerFrame.rotation.u = -matrixFrame.rotation.s;
				listenerFrame.rotation.f = -matrixFrame.rotation.u;
				listenerFrame.rotation.s = matrixFrame.rotation.f;
				listenerFrame.origin = matrixFrame.origin + this.AttachmentOffset;
				gameEntity.SetGlobalFrame(listenerFrame);
				SoundManager.SetListenerFrame(listenerFrame);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000E998 File Offset: 0x0000CB98
		protected override void OnEditorTick(float dt)
		{
			this.OnTick(dt);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		protected override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "SkeletonName" || variableName == "AnimationName")
			{
				this.CreateVisualizer();
			}
			if (variableName == "Restart")
			{
				List<GameEntity> list = new List<GameEntity>();
				base.GameEntity.Scene.GetAllEntitiesWithScriptComponent<AnimationPoint>(ref list);
				foreach (GameEntity gameEntity in list)
				{
					gameEntity.GetFirstScriptOfType<AnimationPoint>().RequestResync();
				}
				this.CreateVisualizer();
			}
		}

		// Token: 0x040000EC RID: 236
		public string SkeletonName = "human_skeleton";

		// Token: 0x040000ED RID: 237
		public int BoneIndex;

		// Token: 0x040000EE RID: 238
		public Vec3 AttachmentOffset = new Vec3(0f, 0f, 0f, -1f);

		// Token: 0x040000EF RID: 239
		public string AnimationName = "";

		// Token: 0x040000F0 RID: 240
		public SimpleButton Restart;
	}
}
