using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View.MissionViews.SiegeWeapon
{
	// Token: 0x0200007C RID: 124
	[DefaultView]
	public class RangedSiegeWeaponViewController : MissionView
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x00024388 File Offset: 0x00022588
		public override void OnObjectUsed(Agent userAgent, UsableMissionObject usedObject)
		{
			base.OnObjectUsed(userAgent, usedObject);
			if (userAgent.IsMainAgent && usedObject is StandingPoint)
			{
				UsableMachine usableMachineFromPoint = this.GetUsableMachineFromPoint(usedObject as StandingPoint);
				if (usableMachineFromPoint is RangedSiegeWeapon)
				{
					RangedSiegeWeapon rangedSiegeWeapon = usableMachineFromPoint as RangedSiegeWeapon;
					if (rangedSiegeWeapon.GetComponent<RangedSiegeWeaponView>() == null)
					{
						this.AddRangedSiegeWeaponView(rangedSiegeWeapon);
					}
				}
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000243D8 File Offset: 0x000225D8
		private UsableMachine GetUsableMachineFromPoint(StandingPoint standingPoint)
		{
			GameEntity gameEntity = standingPoint.GameEntity;
			while (gameEntity != null && !gameEntity.HasScriptOfType<UsableMachine>())
			{
				gameEntity = gameEntity.Parent;
			}
			if (gameEntity != null)
			{
				UsableMachine firstScriptOfType = gameEntity.GetFirstScriptOfType<UsableMachine>();
				if (firstScriptOfType != null)
				{
					return firstScriptOfType;
				}
			}
			return null;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0002441C File Offset: 0x0002261C
		private void AddRangedSiegeWeaponView(RangedSiegeWeapon rangedSiegeWeapon)
		{
			RangedSiegeWeaponView rangedSiegeWeaponView;
			if (rangedSiegeWeapon is Trebuchet)
			{
				rangedSiegeWeaponView = new TrebuchetView();
			}
			else if (rangedSiegeWeapon is Mangonel)
			{
				rangedSiegeWeaponView = new MangonelView();
			}
			else if (rangedSiegeWeapon is Ballista)
			{
				rangedSiegeWeaponView = new BallistaView();
			}
			else
			{
				rangedSiegeWeaponView = new RangedSiegeWeaponView();
			}
			rangedSiegeWeaponView.Initialize(rangedSiegeWeapon, base.MissionScreen);
			rangedSiegeWeapon.AddComponent(rangedSiegeWeaponView);
		}
	}
}
