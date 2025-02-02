using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TaleWorlds.Library
{
	// Token: 0x02000038 RID: 56
	public static class InformationManager
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001B8 RID: 440 RVA: 0x00006D00 File Offset: 0x00004F00
		// (remove) Token: 0x060001B9 RID: 441 RVA: 0x00006D34 File Offset: 0x00004F34
		public static event Action<InformationMessage> DisplayMessageInternal;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001BA RID: 442 RVA: 0x00006D68 File Offset: 0x00004F68
		// (remove) Token: 0x060001BB RID: 443 RVA: 0x00006D9C File Offset: 0x00004F9C
		public static event Action ClearAllMessagesInternal;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001BC RID: 444 RVA: 0x00006DD0 File Offset: 0x00004FD0
		// (remove) Token: 0x060001BD RID: 445 RVA: 0x00006E04 File Offset: 0x00005004
		public static event Action<string> OnAddSystemNotification;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001BE RID: 446 RVA: 0x00006E38 File Offset: 0x00005038
		// (remove) Token: 0x060001BF RID: 447 RVA: 0x00006E6C File Offset: 0x0000506C
		public static event Action<Type, object[]> OnShowTooltip;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001C0 RID: 448 RVA: 0x00006EA0 File Offset: 0x000050A0
		// (remove) Token: 0x060001C1 RID: 449 RVA: 0x00006ED4 File Offset: 0x000050D4
		public static event Action OnHideTooltip;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001C2 RID: 450 RVA: 0x00006F08 File Offset: 0x00005108
		// (remove) Token: 0x060001C3 RID: 451 RVA: 0x00006F3C File Offset: 0x0000513C
		public static event Action<InquiryData, bool, bool> OnShowInquiry;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060001C4 RID: 452 RVA: 0x00006F70 File Offset: 0x00005170
		// (remove) Token: 0x060001C5 RID: 453 RVA: 0x00006FA4 File Offset: 0x000051A4
		public static event Action<TextInquiryData, bool, bool> OnShowTextInquiry;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060001C6 RID: 454 RVA: 0x00006FD8 File Offset: 0x000051D8
		// (remove) Token: 0x060001C7 RID: 455 RVA: 0x0000700C File Offset: 0x0000520C
		public static event Action OnHideInquiry;

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000703F File Offset: 0x0000523F
		[TupleElementNames(new string[]
		{
			"tooltipType",
			"onRefreshData",
			"movieName"
		})]
		public static IReadOnlyDictionary<Type, ValueTuple<Type, object, string>> RegisteredTypes
		{
			[return: TupleElementNames(new string[]
			{
				"tooltipType",
				"onRefreshData",
				"movieName"
			})]
			get
			{
				return InformationManager._registeredTypes;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007046 File Offset: 0x00005246
		public static void DisplayMessage(InformationMessage message)
		{
			Action<InformationMessage> displayMessageInternal = InformationManager.DisplayMessageInternal;
			if (displayMessageInternal == null)
			{
				return;
			}
			displayMessageInternal(message);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007058 File Offset: 0x00005258
		public static void ClearAllMessages()
		{
			Action clearAllMessagesInternal = InformationManager.ClearAllMessagesInternal;
			if (clearAllMessagesInternal == null)
			{
				return;
			}
			clearAllMessagesInternal();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007069 File Offset: 0x00005269
		public static void AddSystemNotification(string message)
		{
			Action<string> onAddSystemNotification = InformationManager.OnAddSystemNotification;
			if (onAddSystemNotification == null)
			{
				return;
			}
			onAddSystemNotification(message);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000707B File Offset: 0x0000527B
		public static void ShowTooltip(Type type, params object[] args)
		{
			Action<Type, object[]> onShowTooltip = InformationManager.OnShowTooltip;
			if (onShowTooltip == null)
			{
				return;
			}
			onShowTooltip(type, args);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000708E File Offset: 0x0000528E
		public static void HideTooltip()
		{
			Action onHideTooltip = InformationManager.OnHideTooltip;
			if (onHideTooltip == null)
			{
				return;
			}
			onHideTooltip();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000709F File Offset: 0x0000529F
		public static void ShowInquiry(InquiryData data, bool pauseGameActiveState = false, bool prioritize = false)
		{
			Action<InquiryData, bool, bool> onShowInquiry = InformationManager.OnShowInquiry;
			if (onShowInquiry == null)
			{
				return;
			}
			onShowInquiry(data, pauseGameActiveState, prioritize);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000070B3 File Offset: 0x000052B3
		public static void ShowTextInquiry(TextInquiryData textData, bool pauseGameActiveState = false, bool prioritize = false)
		{
			Action<TextInquiryData, bool, bool> onShowTextInquiry = InformationManager.OnShowTextInquiry;
			if (onShowTextInquiry == null)
			{
				return;
			}
			onShowTextInquiry(textData, pauseGameActiveState, prioritize);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000070C7 File Offset: 0x000052C7
		public static void HideInquiry()
		{
			Action onHideInquiry = InformationManager.OnHideInquiry;
			if (onHideInquiry == null)
			{
				return;
			}
			onHideInquiry();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000070D8 File Offset: 0x000052D8
		public static void RegisterIsAnyTooltipActiveCallback(Func<bool> callback)
		{
			InformationManager._isAnyTooltipActiveCallbacks.Add(callback);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000070E5 File Offset: 0x000052E5
		public static void UnregisterIsAnyTooltipActiveCallback(Func<bool> callback)
		{
			InformationManager._isAnyTooltipActiveCallbacks.Remove(callback);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000070F3 File Offset: 0x000052F3
		public static void RegisterIsAnyTooltipExtendedCallback(Func<bool> callback)
		{
			InformationManager._isAnyTooltipExtendedCallbacks.Add(callback);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007100 File Offset: 0x00005300
		public static void UnregisterIsAnyTooltipExtendedCallback(Func<bool> callback)
		{
			InformationManager._isAnyTooltipExtendedCallbacks.Remove(callback);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007110 File Offset: 0x00005310
		public static bool GetIsAnyTooltipActive()
		{
			for (int i = 0; i < InformationManager._isAnyTooltipActiveCallbacks.Count; i++)
			{
				if (InformationManager._isAnyTooltipActiveCallbacks[i]())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007148 File Offset: 0x00005348
		public static bool GetIsAnyTooltipExtended()
		{
			for (int i = 0; i < InformationManager._isAnyTooltipExtendedCallbacks.Count; i++)
			{
				if (InformationManager._isAnyTooltipExtendedCallbacks[i]())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000717F File Offset: 0x0000537F
		public static bool GetIsAnyTooltipActiveAndExtended()
		{
			return InformationManager.GetIsAnyTooltipActive() && InformationManager.GetIsAnyTooltipExtended();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007190 File Offset: 0x00005390
		public static void RegisterTooltip<TRegistered, TTooltip>(Action<TTooltip, object[]> onRefreshData, string movieName) where TTooltip : TooltipBaseVM
		{
			Type typeFromHandle = typeof(TRegistered);
			Type typeFromHandle2 = typeof(TTooltip);
			InformationManager._registeredTypes[typeFromHandle] = new ValueTuple<Type, object, string>(typeFromHandle2, onRefreshData, movieName);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000071C8 File Offset: 0x000053C8
		public static void UnregisterTooltip<TRegistered>()
		{
			Type typeFromHandle = typeof(TRegistered);
			if (InformationManager._registeredTypes.ContainsKey(typeFromHandle))
			{
				InformationManager._registeredTypes.Remove(typeFromHandle);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000071F9 File Offset: 0x000053F9
		public static void Clear()
		{
			InformationManager.DisplayMessageInternal = null;
			InformationManager.OnShowInquiry = null;
			InformationManager.OnShowTextInquiry = null;
			InformationManager.OnHideInquiry = null;
			InformationManager.IsAnyInquiryActive = null;
			InformationManager.OnShowTooltip = null;
			InformationManager.OnHideTooltip = null;
		}

		// Token: 0x04000086 RID: 134
		public static Func<bool> IsAnyInquiryActive;

		// Token: 0x0400008F RID: 143
		[TupleElementNames(new string[]
		{
			"tooltipType",
			"onRefreshData",
			"movieName"
		})]
		private static Dictionary<Type, ValueTuple<Type, object, string>> _registeredTypes = new Dictionary<Type, ValueTuple<Type, object, string>>();

		// Token: 0x04000090 RID: 144
		private static List<Func<bool>> _isAnyTooltipActiveCallbacks = new List<Func<bool>>();

		// Token: 0x04000091 RID: 145
		private static List<Func<bool>> _isAnyTooltipExtendedCallbacks = new List<Func<bool>>();
	}
}
