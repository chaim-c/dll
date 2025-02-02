using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000325 RID: 805
	public class Mover : ScriptComponentBehavior
	{
		// Token: 0x06002B2B RID: 11051 RVA: 0x000A732C File Offset: 0x000A552C
		protected internal override void OnEditorTick(float dt)
		{
			if (!base.GameEntity.EntityFlags.HasAnyFlag(EntityFlags.IsHelper))
			{
				if (this._moverGhost == null && this._pathname != "")
				{
					this.CreateOrUpdateMoverGhost();
				}
				if (this._tracker != null && this._tracker.IsValid)
				{
					if (this._moveGhost)
					{
						this._tracker.Advance(this._speed * dt);
						if (this._tracker.TotalDistanceTraveled >= this._tracker.GetPathLength())
						{
							this._tracker.Reset();
						}
					}
					else
					{
						this._tracker.Advance(0f);
					}
					MatrixFrame currentFrame = this._tracker.CurrentFrame;
					this._moverGhost.SetFrame(ref currentFrame);
				}
			}
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000A73F8 File Offset: 0x000A55F8
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			if (variableName == "_pathname")
			{
				this.CreateOrUpdateMoverGhost();
				return;
			}
			if (variableName == "_moveGhost")
			{
				if (!this._moveGhost)
				{
					this._moverGhost.SetVisibilityExcludeParents(false);
					return;
				}
				this._moverGhost.SetVisibilityExcludeParents(true);
			}
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000A7448 File Offset: 0x000A5648
		private void CreateOrUpdateMoverGhost()
		{
			Path pathWithName = base.GameEntity.Scene.GetPathWithName(this._pathname);
			if (pathWithName != null)
			{
				this._tracker = new PathTracker(pathWithName, Vec3.One);
				this._tracker.Reset();
				base.GameEntity.SetLocalPosition(this._tracker.CurrentFrame.origin);
				if (this._moverGhost == null)
				{
					this._moverGhost = GameEntity.CopyFrom(base.GameEntity.Scene, base.GameEntity);
					this._moverGhost.EntityFlags |= (EntityFlags.IsHelper | EntityFlags.DontSaveToScene | EntityFlags.DoNotTick);
					this._moverGhost.SetAlpha(0.2f);
					return;
				}
				this._moverGhost.SetLocalPosition(this._tracker.CurrentFrame.origin);
			}
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000A751C File Offset: 0x000A571C
		protected internal override void OnInit()
		{
			base.OnInit();
			Path pathWithName = base.GameEntity.Scene.GetPathWithName(this._pathname);
			if (pathWithName != null)
			{
				this._tracker = new PathTracker(pathWithName, Vec3.One);
				this._tracker.Reset();
				base.GameEntity.SetLocalPosition(this._tracker.CurrentFrame.origin);
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000A7592 File Offset: 0x000A5792
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.GameEntity.IsVisibleIncludeParents())
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000A75B0 File Offset: 0x000A57B0
		protected internal override void OnTick(float dt)
		{
			if (Mission.Current.Mode == MissionMode.Battle && this._tracker != null && this._tracker.IsValid && this._tracker.TotalDistanceTraveled < this._tracker.GetPathLength())
			{
				this._tracker.Advance(this._speed * dt);
				MatrixFrame currentFrame = this._tracker.CurrentFrame;
				base.GameEntity.SetFrame(ref currentFrame);
			}
		}

		// Token: 0x040010BC RID: 4284
		[EditorVisibleScriptComponentVariable(true)]
		private string _pathname = "";

		// Token: 0x040010BD RID: 4285
		[EditorVisibleScriptComponentVariable(true)]
		private float _speed;

		// Token: 0x040010BE RID: 4286
		[EditorVisibleScriptComponentVariable(true)]
		private bool _moveGhost;

		// Token: 0x040010BF RID: 4287
		private GameEntity _moverGhost;

		// Token: 0x040010C0 RID: 4288
		private PathTracker _tracker;
	}
}
