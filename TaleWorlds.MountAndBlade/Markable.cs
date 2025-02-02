using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200033A RID: 826
	public class Markable : ScriptComponentBehavior
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000B031C File Offset: 0x000AE51C
		// (set) Token: 0x06002C9B RID: 11419 RVA: 0x000B0324 File Offset: 0x000AE524
		private bool MarkerActive
		{
			get
			{
				return this._markerActive;
			}
			set
			{
				if (this._markerActive != value)
				{
					this._markerActive = value;
					base.SetScriptComponentToTick(this.GetTickRequirement());
				}
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000B0344 File Offset: 0x000AE544
		protected internal override void OnInit()
		{
			base.OnInit();
			this._marker = GameEntity.Instantiate(Mission.Current.Scene, "highlight_beam", base.GameEntity.GetGlobalFrame());
			this.DeactivateMarker();
			this._destructibleComponent = base.GameEntity.GetFirstScriptOfType<DestructableComponent>();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000B039F File Offset: 0x000AE59F
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (this.MarkerActive)
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000B03B8 File Offset: 0x000AE5B8
		protected internal override void OnTick(float dt)
		{
			if (this.MarkerActive)
			{
				if (this._destructibleComponent != null && this._destructibleComponent.IsDestroyed)
				{
					if (this._markerVisible)
					{
						this.DisableMarkerActivation();
						return;
					}
				}
				else if (this._markerVisible)
				{
					if (Mission.Current.CurrentTime - this._markerEventBeginningTime > this._markerActiveDuration)
					{
						this.DeactivateMarker();
						return;
					}
				}
				else if (!this._markerVisible && Mission.Current.CurrentTime - this._markerEventBeginningTime > this._markerPassiveDuration)
				{
					this.ActivateMarkerFor(this._markerActiveDuration, this._markerPassiveDuration);
				}
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000B044C File Offset: 0x000AE64C
		public void DisableMarkerActivation()
		{
			this.MarkerActive = false;
			this.DeactivateMarker();
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000B045C File Offset: 0x000AE65C
		public void ActivateMarkerFor(float activeSeconds, float passiveSeconds)
		{
			if (this._destructibleComponent == null || !this._destructibleComponent.IsDestroyed)
			{
				this.MarkerActive = true;
				this._markerVisible = true;
				this._markerEventBeginningTime = Mission.Current.CurrentTime;
				this._markerActiveDuration = activeSeconds;
				this._markerPassiveDuration = passiveSeconds;
				this._marker.SetVisibilityExcludeParents(true);
				this._marker.BurstEntityParticle(true);
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000B04C2 File Offset: 0x000AE6C2
		private void DeactivateMarker()
		{
			this._markerVisible = false;
			this._marker.SetVisibilityExcludeParents(false);
			this._markerEventBeginningTime = Mission.Current.CurrentTime;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000B04E7 File Offset: 0x000AE6E7
		public void ResetPassiveDurationTimer()
		{
			if (!this._markerVisible && this.MarkerActive)
			{
				this._markerEventBeginningTime = Mission.Current.CurrentTime;
			}
		}

		// Token: 0x040011FF RID: 4607
		public string MarkerPrefabName = "highlight_beam";

		// Token: 0x04001200 RID: 4608
		private GameEntity _marker;

		// Token: 0x04001201 RID: 4609
		private DestructableComponent _destructibleComponent;

		// Token: 0x04001202 RID: 4610
		private bool _markerActive;

		// Token: 0x04001203 RID: 4611
		private bool _markerVisible;

		// Token: 0x04001204 RID: 4612
		private float _markerEventBeginningTime;

		// Token: 0x04001205 RID: 4613
		private float _markerActiveDuration;

		// Token: 0x04001206 RID: 4614
		private float _markerPassiveDuration;
	}
}
