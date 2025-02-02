using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x0200000F RID: 15
	public class GauntletQueryManager : GlobalLayer
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00005128 File Offset: 0x00003328
		public void Initialize()
		{
			if (!this._isInitialized)
			{
				this._isInitialized = true;
				this._inquiryQueue = new Queue<Tuple<Type, object, bool, bool>>();
				InformationManager.OnShowInquiry += this.CreateQuery;
				InformationManager.OnShowTextInquiry += this.CreateTextQuery;
				MBInformationManager.OnShowMultiSelectionInquiry += this.CreateMultiSelectionQuery;
				InformationManager.OnHideInquiry += this.CloseQuery;
				InformationManager.IsAnyInquiryActive = (Func<bool>)Delegate.Combine(InformationManager.IsAnyInquiryActive, new Func<bool>(this.OnIsAnyInquiryActiveQuery));
				this._singleQueryPopupVM = new SingleQueryPopUpVM(new Action(this.CloseQuery));
				this._multiSelectionQueryPopUpVM = new MultiSelectionQueryPopUpVM(new Action(this.CloseQuery));
				this._textQueryPopUpVM = new TextQueryPopUpVM(new Action(this.CloseQuery));
				this._gauntletLayer = new GauntletLayer(4500, "GauntletLayer", false);
				this._createQueryActions = new Dictionary<Type, Action<object, bool, bool>>
				{
					{
						typeof(InquiryData),
						delegate(object data, bool pauseState, bool prioritize)
						{
							this.CreateQuery((InquiryData)data, pauseState, prioritize);
						}
					},
					{
						typeof(TextInquiryData),
						delegate(object data, bool pauseState, bool prioritize)
						{
							this.CreateTextQuery((TextInquiryData)data, pauseState, prioritize);
						}
					},
					{
						typeof(MultiSelectionInquiryData),
						delegate(object data, bool pauseState, bool prioritize)
						{
							this.CreateMultiSelectionQuery((MultiSelectionInquiryData)data, pauseState, prioritize);
						}
					}
				};
				base.Layer = this._gauntletLayer;
				this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
				ScreenManager.AddGlobalLayer(this, true);
			}
			ScreenManager.SetSuspendLayer(base.Layer, true);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000052A9 File Offset: 0x000034A9
		private bool OnIsAnyInquiryActiveQuery()
		{
			return GauntletQueryManager._activeDataSource != null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000052B4 File Offset: 0x000034B4
		internal void InitializeKeyVisuals()
		{
			this._singleQueryPopupVM.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._singleQueryPopupVM.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._multiSelectionQueryPopUpVM.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._multiSelectionQueryPopUpVM.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._textQueryPopUpVM.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._textQueryPopUpVM.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000537C File Offset: 0x0000357C
		protected override void OnEarlyTick(float dt)
		{
			base.OnEarlyTick(dt);
			if (GauntletQueryManager._activeDataSource != null)
			{
				if (ScreenManager.FocusedLayer != base.Layer)
				{
					this.SetLayerFocus(true);
				}
				if (GauntletQueryManager._activeDataSource.IsButtonOkShown && GauntletQueryManager._activeDataSource.IsButtonOkEnabled && this._gauntletLayer.Input.IsHotKeyDownAndReleased("Confirm"))
				{
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					GauntletQueryManager._activeDataSource.ExecuteAffirmativeAction();
					return;
				}
				if (GauntletQueryManager._activeDataSource.IsButtonCancelShown && this._gauntletLayer.Input.IsHotKeyDownAndReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					GauntletQueryManager._activeDataSource.ExecuteNegativeAction();
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005429 File Offset: 0x00003629
		protected override void OnLateTick(float dt)
		{
			base.OnLateTick(dt);
			PopUpBaseVM activeDataSource = GauntletQueryManager._activeDataSource;
			if (activeDataSource == null)
			{
				return;
			}
			activeDataSource.OnTick(dt);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005444 File Offset: 0x00003644
		private void CreateQuery(InquiryData data, bool pauseGameActiveState, bool prioritize)
		{
			if (GauntletQueryManager._activeDataSource == null)
			{
				this._singleQueryPopupVM.SetData(data);
				this._movie = this._gauntletLayer.LoadMovie("SingleQueryPopup", this._singleQueryPopupVM);
				GauntletQueryManager._activeDataSource = this._singleQueryPopupVM;
				GauntletQueryManager._activeQueryData = data;
				this.HandleQueryCreated(data.SoundEventPath, pauseGameActiveState);
				return;
			}
			if (data == null)
			{
				Debug.FailedAssert("Trying to create query with null data!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletQueryManager.cs", "CreateQuery", 126);
				return;
			}
			if (GauntletQueryManager.CheckIfQueryDataIsEqual(GauntletQueryManager._activeQueryData, data) || this._inquiryQueue.Any((Tuple<Type, object, bool, bool> x) => GauntletQueryManager.CheckIfQueryDataIsEqual(x.Item2, data)))
			{
				Debug.FailedAssert("Trying to create query but it is already present! Title: " + data.TitleText, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletQueryManager.cs", "CreateQuery", 131);
				return;
			}
			if (prioritize)
			{
				this.QueueAndShowNewData(data, pauseGameActiveState, prioritize);
				return;
			}
			this._inquiryQueue.Enqueue(new Tuple<Type, object, bool, bool>(typeof(InquiryData), data, pauseGameActiveState, prioritize));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005568 File Offset: 0x00003768
		private void CreateTextQuery(TextInquiryData data, bool pauseGameActiveState, bool prioritize)
		{
			if (GauntletQueryManager._activeDataSource == null)
			{
				this._textQueryPopUpVM.SetData(data);
				this._movie = this._gauntletLayer.LoadMovie("TextQueryPopup", this._textQueryPopUpVM);
				GauntletQueryManager._activeDataSource = this._textQueryPopUpVM;
				GauntletQueryManager._activeQueryData = data;
				this.HandleQueryCreated(data.SoundEventPath, pauseGameActiveState);
				return;
			}
			if (data == null)
			{
				Debug.FailedAssert("Trying to create textQuery with null data!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletQueryManager.cs", "CreateTextQuery", 161);
				return;
			}
			if (GauntletQueryManager.CheckIfQueryDataIsEqual(GauntletQueryManager._activeQueryData, data) || this._inquiryQueue.Any((Tuple<Type, object, bool, bool> x) => GauntletQueryManager.CheckIfQueryDataIsEqual(x.Item2, data)))
			{
				Debug.FailedAssert("Trying to create textQuery but it is already present! Title: " + data.TitleText, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletQueryManager.cs", "CreateTextQuery", 166);
				return;
			}
			if (prioritize)
			{
				this.QueueAndShowNewData(data, pauseGameActiveState, prioritize);
				return;
			}
			this._inquiryQueue.Enqueue(new Tuple<Type, object, bool, bool>(typeof(TextInquiryData), data, pauseGameActiveState, prioritize));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005690 File Offset: 0x00003890
		private void CreateMultiSelectionQuery(MultiSelectionInquiryData data, bool pauseGameActiveState, bool prioritize)
		{
			if (GauntletQueryManager._activeDataSource == null)
			{
				this._multiSelectionQueryPopUpVM.SetData(data);
				this._movie = this._gauntletLayer.LoadMovie("MultiSelectionQueryPopup", this._multiSelectionQueryPopUpVM);
				GauntletQueryManager._activeDataSource = this._multiSelectionQueryPopUpVM;
				GauntletQueryManager._activeQueryData = data;
				this.HandleQueryCreated(data.SoundEventPath, pauseGameActiveState);
				return;
			}
			if (data == null)
			{
				Debug.FailedAssert("Trying to create multiSelectionQuery with null data!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletQueryManager.cs", "CreateMultiSelectionQuery", 196);
				return;
			}
			if (GauntletQueryManager.CheckIfQueryDataIsEqual(GauntletQueryManager._activeQueryData, data) || this._inquiryQueue.Any((Tuple<Type, object, bool, bool> x) => GauntletQueryManager.CheckIfQueryDataIsEqual(x.Item2, data)))
			{
				Debug.FailedAssert("Trying to create multiSelectionQuery but it is already present! Title: " + data.TitleText, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletQueryManager.cs", "CreateMultiSelectionQuery", 201);
				return;
			}
			if (prioritize)
			{
				this.QueueAndShowNewData(data, pauseGameActiveState, prioritize);
				return;
			}
			this._inquiryQueue.Enqueue(new Tuple<Type, object, bool, bool>(typeof(MultiSelectionInquiryData), data, pauseGameActiveState, prioritize));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000057B8 File Offset: 0x000039B8
		private void QueueAndShowNewData(object newInquiryData, bool pauseGameActiveState, bool prioritize)
		{
			Queue<Tuple<Type, object, bool, bool>> queue = new Queue<Tuple<Type, object, bool, bool>>();
			queue.Enqueue(new Tuple<Type, object, bool, bool>(newInquiryData.GetType(), newInquiryData, pauseGameActiveState, prioritize));
			queue.Enqueue(new Tuple<Type, object, bool, bool>(GauntletQueryManager._activeQueryData.GetType(), GauntletQueryManager._activeQueryData, this._isLastActiveGameStatePaused, false));
			this._inquiryQueue = GauntletQueryManager.CombineQueues<Tuple<Type, object, bool, bool>>(queue, this._inquiryQueue);
			this.CloseQuery();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005818 File Offset: 0x00003A18
		private void HandleQueryCreated(string soundEventPath, bool pauseGameActiveState)
		{
			InformationManager.HideTooltip();
			GauntletQueryManager._activeDataSource.ForceRefreshKeyVisuals();
			this.SetLayerFocus(true);
			this._isLastActiveGameStatePaused = pauseGameActiveState;
			if (this._isLastActiveGameStatePaused)
			{
				GameStateManager.Current.RegisterActiveStateDisableRequest(this);
				MBCommon.PauseGameEngine();
			}
			if (!string.IsNullOrEmpty(soundEventPath))
			{
				SoundEvent.PlaySound2D(soundEventPath);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000586C File Offset: 0x00003A6C
		private void CloseQuery()
		{
			if (GauntletQueryManager._activeDataSource == null)
			{
				return;
			}
			this.SetLayerFocus(false);
			if (this._isLastActiveGameStatePaused)
			{
				GameStateManager.Current.UnregisterActiveStateDisableRequest(this);
				MBCommon.UnPauseGameEngine();
			}
			if (this._movie != null)
			{
				this._gauntletLayer.ReleaseMovie(this._movie);
				this._movie = null;
			}
			GauntletQueryManager._activeDataSource.OnClearData();
			GauntletQueryManager._activeDataSource = null;
			GauntletQueryManager._activeQueryData = null;
			if (this._inquiryQueue.Count > 0)
			{
				Tuple<Type, object, bool, bool> tuple = this._inquiryQueue.Dequeue();
				Action<object, bool, bool> action;
				if (this._createQueryActions.TryGetValue(tuple.Item1, out action))
				{
					action(tuple.Item2, tuple.Item3, tuple.Item4);
					return;
				}
				Debug.FailedAssert("Invalid data type for query", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletQueryManager.cs", "CloseQuery", 293);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005938 File Offset: 0x00003B38
		private void SetLayerFocus(bool isFocused)
		{
			if (isFocused)
			{
				ScreenManager.SetSuspendLayer(base.Layer, false);
				base.Layer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(base.Layer);
				base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				return;
			}
			base.Layer.InputRestrictions.ResetInputRestrictions();
			ScreenManager.SetSuspendLayer(base.Layer, true);
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000059B4 File Offset: 0x00003BB4
		private static Queue<T> CombineQueues<T>(Queue<T> t1, Queue<T> t2)
		{
			Queue<T> queue = new Queue<T>();
			int count = t1.Count;
			for (int i = 0; i < count; i++)
			{
				queue.Enqueue(t1.Dequeue());
			}
			count = t2.Count;
			for (int j = 0; j < count; j++)
			{
				queue.Enqueue(t2.Dequeue());
			}
			return queue;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005A08 File Offset: 0x00003C08
		private static bool CheckIfQueryDataIsEqual(object queryData1, object queryData2)
		{
			InquiryData inquiryData;
			if ((inquiryData = (queryData1 as InquiryData)) != null)
			{
				return inquiryData.HasSameContentWith(queryData2);
			}
			TextInquiryData textInquiryData;
			if ((textInquiryData = (queryData1 as TextInquiryData)) != null)
			{
				return textInquiryData.HasSameContentWith(queryData2);
			}
			MultiSelectionInquiryData multiSelectionInquiryData;
			return (multiSelectionInquiryData = (queryData1 as MultiSelectionInquiryData)) != null && multiSelectionInquiryData.HasSameContentWith(queryData2);
		}

		// Token: 0x04000056 RID: 86
		private bool _isInitialized;

		// Token: 0x04000057 RID: 87
		private Queue<Tuple<Type, object, bool, bool>> _inquiryQueue;

		// Token: 0x04000058 RID: 88
		private bool _isLastActiveGameStatePaused;

		// Token: 0x04000059 RID: 89
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400005A RID: 90
		private SingleQueryPopUpVM _singleQueryPopupVM;

		// Token: 0x0400005B RID: 91
		private MultiSelectionQueryPopUpVM _multiSelectionQueryPopUpVM;

		// Token: 0x0400005C RID: 92
		private TextQueryPopUpVM _textQueryPopUpVM;

		// Token: 0x0400005D RID: 93
		private static PopUpBaseVM _activeDataSource;

		// Token: 0x0400005E RID: 94
		private static object _activeQueryData;

		// Token: 0x0400005F RID: 95
		private IGauntletMovie _movie;

		// Token: 0x04000060 RID: 96
		private Dictionary<Type, Action<object, bool, bool>> _createQueryActions;
	}
}
