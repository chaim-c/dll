using System;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x0200002A RID: 42
	public static class OrderUIHelper
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		internal static OrderType GetOrderOverrideForUI(Formation formation, OrderSetType setType)
		{
			OrderType overridenOrderType = formation.Team.PlayerOrderController.GetOverridenOrderType(formation);
			switch (overridenOrderType)
			{
			case OrderType.Move:
			case OrderType.Charge:
			case OrderType.StandYourGround:
			case OrderType.FollowMe:
			case OrderType.GuardMe:
			case OrderType.Retreat:
			case OrderType.Advance:
			case OrderType.FallBack:
				if (setType == OrderSetType.Movement)
				{
					return overridenOrderType;
				}
				break;
			case OrderType.LookAtEnemy:
			case OrderType.LookAtDirection:
			case OrderType.HoldFire:
			case OrderType.FireAtWill:
			case OrderType.Mount:
			case OrderType.Dismount:
			case OrderType.AIControlOn:
			case OrderType.AIControlOff:
				if (setType == OrderSetType.Toggle)
				{
					return overridenOrderType;
				}
				break;
			case OrderType.ArrangementLine:
			case OrderType.ArrangementCloseOrder:
			case OrderType.ArrangementLoose:
			case OrderType.ArrangementCircular:
			case OrderType.ArrangementSchiltron:
			case OrderType.ArrangementVee:
			case OrderType.ArrangementColumn:
			case OrderType.ArrangementScatter:
				if (setType == OrderSetType.Form)
				{
					return overridenOrderType;
				}
				break;
			}
			return OrderType.None;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E274 File Offset: 0x0000C474
		internal static OrderSubType GetActiveMovementOrderOfFormation(Formation formation)
		{
			OrderType orderType = OrderUIHelper.GetOrderOverrideForUI(formation, OrderSetType.Movement);
			if (orderType == OrderType.None)
			{
				orderType = OrderController.GetActiveMovementOrderOf(formation);
			}
			return OrderUIHelper.GetOrderSubTypeFrom(orderType);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000E29C File Offset: 0x0000C49C
		internal static OrderSubType GetOrderSubTypeFrom(OrderType orderType)
		{
			bool flag = BannerlordConfig.OrderLayoutType == 1;
			switch (orderType)
			{
			case OrderType.Move:
				return OrderSubType.MoveToPosition;
			case OrderType.Charge:
				return OrderSubType.Charge;
			case OrderType.ChargeWithTarget:
				return OrderSubType.Charge;
			case OrderType.StandYourGround:
				return OrderSubType.Stop;
			case OrderType.FollowMe:
				return OrderSubType.FollowMe;
			case OrderType.Retreat:
				return OrderSubType.Retreat;
			case OrderType.Advance:
				return OrderSubType.Advance;
			case OrderType.FallBack:
				return OrderSubType.Fallback;
			case OrderType.LookAtEnemy:
				if (!flag)
				{
					return OrderSubType.ToggleFacing;
				}
				return OrderSubType.FaceEnemy;
			case OrderType.LookAtDirection:
				return OrderSubType.ActivationFaceDirection;
			case OrderType.ArrangementLine:
				return OrderSubType.FormLine;
			case OrderType.ArrangementCloseOrder:
				return OrderSubType.FormClose;
			case OrderType.ArrangementLoose:
				return OrderSubType.FormLoose;
			case OrderType.ArrangementCircular:
				return OrderSubType.FormCircular;
			case OrderType.ArrangementSchiltron:
				return OrderSubType.FormSchiltron;
			case OrderType.ArrangementVee:
				return OrderSubType.FormV;
			case OrderType.ArrangementColumn:
				return OrderSubType.FormColumn;
			case OrderType.ArrangementScatter:
				return OrderSubType.FormScatter;
			case OrderType.HoldFire:
				return OrderSubType.ToggleFire;
			case OrderType.Dismount:
				return OrderSubType.ToggleMount;
			case OrderType.AIControlOn:
				return OrderSubType.ToggleAI;
			}
			return OrderSubType.None;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E38C File Offset: 0x0000C58C
		public static bool CanOrderHaveTarget(OrderSubType orderType)
		{
			switch (orderType)
			{
			case OrderSubType.None:
			case OrderSubType.MoveToPosition:
			case OrderSubType.FollowMe:
			case OrderSubType.Fallback:
			case OrderSubType.Stop:
			case OrderSubType.Retreat:
			case OrderSubType.FormLine:
			case OrderSubType.FormClose:
			case OrderSubType.FormLoose:
			case OrderSubType.FormCircular:
			case OrderSubType.FormSchiltron:
			case OrderSubType.FormV:
			case OrderSubType.FormColumn:
			case OrderSubType.FormScatter:
			case OrderSubType.ToggleStart:
			case OrderSubType.ToggleFacing:
			case OrderSubType.ToggleFire:
			case OrderSubType.ToggleMount:
			case OrderSubType.ToggleAI:
			case OrderSubType.ToggleTransfer:
			case OrderSubType.ToggleEnd:
			case OrderSubType.ActivationFaceDirection:
			case OrderSubType.FaceEnemy:
			case OrderSubType.Return:
				return false;
			case OrderSubType.Charge:
			case OrderSubType.Advance:
				return true;
			default:
				return false;
			}
		}
	}
}
