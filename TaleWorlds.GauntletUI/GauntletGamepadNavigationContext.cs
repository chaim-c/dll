using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;

// Token: 0x02000006 RID: 6
public class GauntletGamepadNavigationContext : IGamepadNavigationContext
{
	// Token: 0x06000029 RID: 41 RVA: 0x0000208B File Offset: 0x0000028B
	private GauntletGamepadNavigationContext()
	{
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002093 File Offset: 0x00000293
	public GauntletGamepadNavigationContext(Func<Vector2, bool> onGetIsBlockedAtPosition, Func<int> onGetLastScreenOrder, Func<bool> onGetIsAvailableForGamepadNavigation)
	{
		this.OnGetIsBlockedAtPosition = onGetIsBlockedAtPosition;
		this.OnGetLastScreenOrder = onGetLastScreenOrder;
		this.OnGetIsAvailableForGamepadNavigation = onGetIsAvailableForGamepadNavigation;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000020B0 File Offset: 0x000002B0
	void IGamepadNavigationContext.OnFinalize()
	{
		GauntletGamepadNavigationManager.Instance.OnContextFinalized(this);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000020BD File Offset: 0x000002BD
	bool IGamepadNavigationContext.GetIsBlockedAtPosition(Vector2 position)
	{
		Func<Vector2, bool> onGetIsBlockedAtPosition = this.OnGetIsBlockedAtPosition;
		return onGetIsBlockedAtPosition == null || onGetIsBlockedAtPosition(position);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000020D1 File Offset: 0x000002D1
	int IGamepadNavigationContext.GetLastScreenOrder()
	{
		Func<int> onGetLastScreenOrder = this.OnGetLastScreenOrder;
		if (onGetLastScreenOrder == null)
		{
			return -1;
		}
		return onGetLastScreenOrder();
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000020E4 File Offset: 0x000002E4
	bool IGamepadNavigationContext.IsAvailableForNavigation()
	{
		Func<bool> onGetIsAvailableForGamepadNavigation = this.OnGetIsAvailableForGamepadNavigation;
		return onGetIsAvailableForGamepadNavigation != null && onGetIsAvailableForGamepadNavigation();
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000020F7 File Offset: 0x000002F7
	void IGamepadNavigationContext.OnWidgetUsedNavigationMovementsUpdated(Widget widget)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.OnWidgetUsedNavigationMovementsUpdated(widget);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002109 File Offset: 0x00000309
	void IGamepadNavigationContext.OnGainNavigation()
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.OnContextGainedNavigation(this);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000211B File Offset: 0x0000031B
	void IGamepadNavigationContext.GainNavigationAfterFrames(int frameCount, Func<bool> predicate)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.SetContextNavigationGainAfterFrames(this, frameCount, predicate);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000212F File Offset: 0x0000032F
	void IGamepadNavigationContext.GainNavigationAfterTime(float seconds, Func<bool> predicate)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.SetContextNavigationGainAfterTime(this, seconds, predicate);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002143 File Offset: 0x00000343
	void IGamepadNavigationContext.OnWidgetNavigationStatusChanged(Widget widget)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.OnWidgetNavigationStatusChanged(this, widget);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002156 File Offset: 0x00000356
	void IGamepadNavigationContext.OnWidgetNavigationIndexUpdated(Widget widget)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.OnWidgetNavigationIndexUpdated(this, widget);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002169 File Offset: 0x00000369
	void IGamepadNavigationContext.AddNavigationScope(GamepadNavigationScope scope, bool initialize)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.AddNavigationScope(this, scope, initialize);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0000217D File Offset: 0x0000037D
	void IGamepadNavigationContext.RemoveNavigationScope(GamepadNavigationScope scope)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.RemoveNavigationScope(this, scope);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002190 File Offset: 0x00000390
	void IGamepadNavigationContext.AddForcedScopeCollection(GamepadNavigationForcedScopeCollection collection)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.AddForcedScopeCollection(collection);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000021A2 File Offset: 0x000003A2
	void IGamepadNavigationContext.RemoveForcedScopeCollection(GamepadNavigationForcedScopeCollection collection)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.RemoveForcedScopeCollection(collection);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000021B4 File Offset: 0x000003B4
	bool IGamepadNavigationContext.HasNavigationScope(GamepadNavigationScope scope)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		return instance != null && instance.HasNavigationScope(this, scope);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000021C8 File Offset: 0x000003C8
	bool IGamepadNavigationContext.HasNavigationScope(Func<GamepadNavigationScope, bool> predicate)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		return instance != null && instance.HasNavigationScope(this, predicate);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000021DC File Offset: 0x000003DC
	void IGamepadNavigationContext.OnMovieLoaded(string movieName)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.OnMovieLoaded(this, movieName);
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000021EF File Offset: 0x000003EF
	void IGamepadNavigationContext.OnMovieReleased(string movieName)
	{
		GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.OnMovieReleased(this, movieName);
	}

	// Token: 0x04000001 RID: 1
	public readonly Func<Vector2, bool> OnGetIsBlockedAtPosition;

	// Token: 0x04000002 RID: 2
	public readonly Func<int> OnGetLastScreenOrder;

	// Token: 0x04000003 RID: 3
	public readonly Func<bool> OnGetIsAvailableForGamepadNavigation;
}
