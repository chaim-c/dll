using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.GamepadNavigation
{
	// Token: 0x02000047 RID: 71
	internal static class GamepadNavigationHelper
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00011718 File Offset: 0x0000F918
		internal static void GetRelatedLineOfScope(GamepadNavigationScope scope, Vector2 fromPosition, GamepadNavigationTypes movement, out Vector2 lineBegin, out Vector2 lineEnd, out bool isFromWidget)
		{
			Rectangle discoveryRectangle = scope.GetDiscoveryRectangle();
			if (discoveryRectangle.IsPointInside(fromPosition))
			{
				Widget approximatelyClosestWidgetToPosition = scope.GetApproximatelyClosestWidgetToPosition(fromPosition, movement, false);
				if (approximatelyClosestWidgetToPosition != null)
				{
					isFromWidget = true;
					GamepadNavigationHelper.GetRelatedLineOfWidget(approximatelyClosestWidgetToPosition, movement, out lineBegin, out lineEnd);
					return;
				}
			}
			isFromWidget = false;
			float scale = scope.ParentWidget.EventManager.Context.Scale;
			Vector2 vector = new Vector2(discoveryRectangle.X, discoveryRectangle.Y);
			Vector2 vector2 = new Vector2(discoveryRectangle.X2, discoveryRectangle.Y);
			Vector2 vector3 = new Vector2(discoveryRectangle.X2, discoveryRectangle.Y2);
			Vector2 vector4 = new Vector2(discoveryRectangle.X, discoveryRectangle.Y2);
			if (movement == GamepadNavigationTypes.Up)
			{
				lineBegin = vector4;
				lineEnd = vector3;
				return;
			}
			if (movement == GamepadNavigationTypes.Right)
			{
				lineBegin = vector;
				lineEnd = vector4;
				return;
			}
			if (movement == GamepadNavigationTypes.Down)
			{
				lineBegin = vector;
				lineEnd = vector2;
				return;
			}
			if (movement == GamepadNavigationTypes.Left)
			{
				lineBegin = vector2;
				lineEnd = vector3;
				return;
			}
			lineBegin = Vector2.Zero;
			lineEnd = Vector2.Zero;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00011824 File Offset: 0x0000FA24
		internal static void GetRelatedLineOfWidget(Widget widget, GamepadNavigationTypes movement, out Vector2 lineBegin, out Vector2 lineEnd)
		{
			Vector2 globalPosition = widget.GlobalPosition;
			Vector2 vector = new Vector2(widget.GlobalPosition.X + widget.Size.X, widget.GlobalPosition.Y);
			Vector2 vector2 = widget.GlobalPosition + widget.Size;
			Vector2 vector3 = new Vector2(widget.GlobalPosition.X, widget.GlobalPosition.Y + widget.Size.Y);
			if (movement == GamepadNavigationTypes.Up)
			{
				lineBegin = vector3;
				lineEnd = vector2;
				return;
			}
			if (movement == GamepadNavigationTypes.Right)
			{
				lineBegin = globalPosition;
				lineEnd = vector3;
				return;
			}
			if (movement == GamepadNavigationTypes.Down)
			{
				lineBegin = globalPosition;
				lineEnd = vector;
				return;
			}
			if (movement == GamepadNavigationTypes.Left)
			{
				lineBegin = vector;
				lineEnd = vector2;
				return;
			}
			lineBegin = Vector2.Zero;
			lineEnd = Vector2.Zero;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00011900 File Offset: 0x0000FB00
		internal static float GetDistanceToClosestWidgetEdge(Widget widget, Vector2 point, GamepadNavigationTypes movement, out Vector2 closestPointOnEdge)
		{
			Vector2 globalPosition = widget.GlobalPosition;
			Vector2 size = widget.Size;
			Rectangle rectangle = new Rectangle(globalPosition.X, globalPosition.Y, size.X, size.Y);
			if (movement == GamepadNavigationTypes.Up)
			{
				Vector2 lineBegin = new Vector2(rectangle.X, rectangle.Y2);
				Vector2 lineEnd = new Vector2(rectangle.X2, rectangle.Y2);
				closestPointOnEdge = GamepadNavigationHelper.GetClosestPointOnLineSegment(lineBegin, lineEnd, point);
				return Vector2.Distance(closestPointOnEdge, point);
			}
			if (movement == GamepadNavigationTypes.Right)
			{
				Vector2 lineBegin2 = new Vector2(rectangle.X, rectangle.Y);
				Vector2 lineEnd2 = new Vector2(rectangle.X, rectangle.Y2);
				closestPointOnEdge = GamepadNavigationHelper.GetClosestPointOnLineSegment(lineBegin2, lineEnd2, point);
				return Vector2.Distance(closestPointOnEdge, point);
			}
			if (movement == GamepadNavigationTypes.Down)
			{
				Vector2 lineBegin3 = new Vector2(rectangle.X, rectangle.Y);
				Vector2 lineEnd3 = new Vector2(rectangle.X2, rectangle.Y);
				closestPointOnEdge = GamepadNavigationHelper.GetClosestPointOnLineSegment(lineBegin3, lineEnd3, point);
				return Vector2.Distance(closestPointOnEdge, point);
			}
			if (movement == GamepadNavigationTypes.Left)
			{
				Vector2 lineBegin4 = new Vector2(rectangle.X2, rectangle.Y);
				Vector2 lineEnd4 = new Vector2(rectangle.X2, rectangle.Y2);
				closestPointOnEdge = GamepadNavigationHelper.GetClosestPointOnLineSegment(lineBegin4, lineEnd4, point);
				return Vector2.Distance(closestPointOnEdge, point);
			}
			closestPointOnEdge = globalPosition + size / 2f;
			return Vector2.Distance(closestPointOnEdge, point);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011A78 File Offset: 0x0000FC78
		internal static float GetDistanceToClosestWidgetEdge(Widget widget, Vector2 point, GamepadNavigationTypes movement)
		{
			Vector2 vector;
			return GamepadNavigationHelper.GetDistanceToClosestWidgetEdge(widget, point, movement, out vector);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011A90 File Offset: 0x0000FC90
		internal static Vector2 GetClosestPointOnLineSegment(Vector2 lineBegin, Vector2 lineEnd, Vector2 point)
		{
			Vector2 value = point - lineBegin;
			Vector2 vector = lineEnd - lineBegin;
			float num = vector.LengthSquared();
			float num2 = Vector2.Dot(value, vector) / num;
			if (num2 < 0f)
			{
				return lineBegin;
			}
			if (num2 > 1f)
			{
				return lineEnd;
			}
			return lineBegin + vector * num2;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		internal static GamepadNavigationTypes GetMovementsToReachRectangle(Vector2 fromPosition, Rectangle rect)
		{
			GamepadNavigationTypes gamepadNavigationTypes = GamepadNavigationTypes.None;
			if (fromPosition.X > rect.X + rect.Width)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Left;
			}
			else if (fromPosition.X < rect.X)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Right;
			}
			if (fromPosition.Y > rect.Y + rect.Height)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Up;
			}
			else if (fromPosition.Y < rect.Y)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Down;
			}
			return gamepadNavigationTypes;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00011B4C File Offset: 0x0000FD4C
		internal static Vector2 GetMovementVectorForNavigation(GamepadNavigationTypes navigationMovement)
		{
			return Vector2.Normalize(new Vector2
			{
				X = (float)((navigationMovement == GamepadNavigationTypes.Right) ? 1 : ((navigationMovement == GamepadNavigationTypes.Left) ? -1 : 0)),
				Y = (float)((navigationMovement == GamepadNavigationTypes.Up) ? -1 : ((navigationMovement == GamepadNavigationTypes.Down) ? 1 : 0))
			});
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011B95 File Offset: 0x0000FD95
		internal static GamepadNavigationScope GetClosestChildScopeAtDirection(GamepadNavigationScope parentScope, Vector2 fromPosition, GamepadNavigationTypes movement, bool checkForAutoGain, out float distanceToScope)
		{
			return GamepadNavigationHelper.GetClosestScopeAtDirectionFromList(parentScope.ChildScopes.ToList<GamepadNavigationScope>(), fromPosition, movement, checkForAutoGain, true, out distanceToScope, Array.Empty<GamepadNavigationScope>());
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		internal static GamepadNavigationScope GetClosestScopeAtDirectionFromList(List<GamepadNavigationScope> scopesList, GamepadNavigationScope fromScope, Vector2 fromPosition, GamepadNavigationTypes movement, bool checkForAutoGain, out float distanceToScope)
		{
			distanceToScope = -1f;
			if (fromScope != null)
			{
				Widget lastNavigatedWidget = fromScope.LastNavigatedWidget;
				Rectangle rectangle = fromScope.UseDiscoveryAreaAsScopeEdges ? fromScope.GetDiscoveryRectangle() : fromScope.GetRectangle();
				if (fromScope.NavigateFromScopeEdges || !rectangle.IsPointInside(fromPosition))
				{
					if (lastNavigatedWidget != null)
					{
						fromPosition = lastNavigatedWidget.GlobalPosition + lastNavigatedWidget.Size / 2f;
					}
					if (movement == GamepadNavigationTypes.Up)
					{
						fromPosition.Y = rectangle.Y;
					}
					else if (movement == GamepadNavigationTypes.Right)
					{
						fromPosition.X = rectangle.X2;
					}
					else if (movement == GamepadNavigationTypes.Down)
					{
						fromPosition.Y = rectangle.Y2;
					}
					else if (movement == GamepadNavigationTypes.Left)
					{
						fromPosition.X = rectangle.X;
					}
				}
			}
			return GamepadNavigationHelper.GetClosestScopeAtDirectionFromList(scopesList, fromPosition, movement, checkForAutoGain, false, out distanceToScope, new GamepadNavigationScope[]
			{
				fromScope
			});
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00011C80 File Offset: 0x0000FE80
		internal static GamepadNavigationScope GetClosestScopeFromList(List<GamepadNavigationScope> scopeList, Vector2 fromPosition, bool checkForAutoGain)
		{
			float num = float.MaxValue;
			int num2 = -1;
			if (scopeList.Count > 0)
			{
				GamepadNavigationTypes[] array = new GamepadNavigationTypes[]
				{
					GamepadNavigationTypes.Up,
					GamepadNavigationTypes.Right,
					GamepadNavigationTypes.Down,
					GamepadNavigationTypes.Left
				};
				for (int i = 0; i < scopeList.Count; i++)
				{
					if ((!checkForAutoGain || !scopeList[i].DoNotAutoGainNavigationOnInit) && scopeList[i].IsAvailable())
					{
						if (scopeList[i].GetRectangle().IsPointInside(fromPosition))
						{
							num2 = i;
							break;
						}
						GamepadNavigationTypes movementsToReachMyPosition = scopeList[i].GetMovementsToReachMyPosition(fromPosition);
						foreach (GamepadNavigationTypes gamepadNavigationTypes in array)
						{
							if (movementsToReachMyPosition.HasAnyFlag(gamepadNavigationTypes))
							{
								Vector2 movementVectorForNavigation = GamepadNavigationHelper.GetMovementVectorForNavigation(gamepadNavigationTypes);
								Vector2 lineBegin;
								Vector2 lineEnd;
								bool flag;
								GamepadNavigationHelper.GetRelatedLineOfScope(scopeList[i], fromPosition, gamepadNavigationTypes, out lineBegin, out lineEnd, out flag);
								Vector2 closestPointOnLineSegment = GamepadNavigationHelper.GetClosestPointOnLineSegment(lineBegin, lineEnd, fromPosition);
								Vector2 value = Vector2.Normalize(closestPointOnLineSegment - fromPosition);
								float num3 = flag ? 1f : Vector2.Dot(movementVectorForNavigation, value);
								float num4 = Vector2.Distance(closestPointOnLineSegment, fromPosition) / num3;
								if (num3 > 0.2f && num4 < num)
								{
									num = num4;
									num2 = i;
								}
							}
						}
					}
				}
				if (num2 != -1)
				{
					return scopeList[num2];
				}
			}
			return null;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		internal static GamepadNavigationScope GetClosestScopeAtDirectionFromList(List<GamepadNavigationScope> scopesList, Vector2 fromPosition, GamepadNavigationTypes movement, bool checkForAutoGain, bool checkOnlyOneDirection, out float distanceToScope, params GamepadNavigationScope[] scopesToIgnore)
		{
			distanceToScope = -1f;
			if (scopesList == null || scopesList.Count == 0)
			{
				return null;
			}
			scopesList = scopesList.ToList<GamepadNavigationScope>();
			for (int i = 0; i < scopesToIgnore.Length; i++)
			{
				scopesList.Remove(scopesToIgnore[i]);
				if (scopesToIgnore[i].ParentScope != null)
				{
					scopesList.Remove(scopesToIgnore[i].ParentScope);
				}
			}
			Vector2 movementVectorForNavigation = GamepadNavigationHelper.GetMovementVectorForNavigation(movement);
			Vec2 resolution = Input.Resolution;
			float num = ((movement & GamepadNavigationTypes.Vertical) != GamepadNavigationTypes.None) ? (resolution.Y * 0.85f) : (((movement & GamepadNavigationTypes.Horizontal) != GamepadNavigationTypes.None) ? (resolution.X * 0.85f) : 0f);
			float num2 = float.MaxValue;
			int num3 = -1;
			if (scopesList != null && scopesList.Count > 0)
			{
				for (int j = 0; j < scopesList.Count; j++)
				{
					if ((!checkForAutoGain || !scopesList[j].DoNotAutoGainNavigationOnInit) && scopesList[j].IsAvailable())
					{
						Vector2 lineBegin;
						Vector2 lineEnd;
						bool flag;
						GamepadNavigationHelper.GetRelatedLineOfScope(scopesList[j], fromPosition, movement, out lineBegin, out lineEnd, out flag);
						Vector2 closestPointOnLineSegment = GamepadNavigationHelper.GetClosestPointOnLineSegment(lineBegin, lineEnd, fromPosition);
						Vector2 value = Vector2.Normalize(closestPointOnLineSegment - fromPosition);
						float num4 = flag ? 1f : Vector2.Dot(movementVectorForNavigation, value);
						if (num4 > 0.2f)
						{
							float num5;
							if (checkOnlyOneDirection)
							{
								num5 = GamepadNavigationHelper.GetDirectionalDistanceBetweenTwoPoints(movement, fromPosition, closestPointOnLineSegment);
							}
							else
							{
								num5 = Vector2.Distance(closestPointOnLineSegment, fromPosition) / num4;
							}
							if (num5 < num2 && num5 < num)
							{
								num2 = num5;
								distanceToScope = num5;
								num3 = j;
							}
						}
					}
				}
				if (num3 != -1)
				{
					return scopesList[num3];
				}
			}
			return null;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00011F50 File Offset: 0x00010150
		internal static float GetDirectionalDistanceBetweenTwoPoints(GamepadNavigationTypes movement, Vector2 p1, Vector2 p2)
		{
			if (movement == GamepadNavigationTypes.Right || movement == GamepadNavigationTypes.Left)
			{
				return MathF.Abs(p1.X - p2.X);
			}
			if (movement == GamepadNavigationTypes.Up || movement == GamepadNavigationTypes.Down)
			{
				return MathF.Abs(p1.Y - p2.Y);
			}
			Debug.FailedAssert("Invalid gamepad movement type:" + movement, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\GamepadNavigation\\GamepadNavigationHelper.cs", "GetDirectionalDistanceBetweenTwoPoints", 411);
			return 0f;
		}
	}
}
