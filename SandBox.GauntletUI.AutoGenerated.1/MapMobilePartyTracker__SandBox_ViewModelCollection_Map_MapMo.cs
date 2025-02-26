﻿using System;
using System.ComponentModel;
using SandBox.ViewModelCollection.Map;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200013D RID: 317
	public class MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM : Widget, IGeneratedGauntletMovieRoot
	{
		// Token: 0x06005A81 RID: 23169 RVA: 0x002CBEA7 File Offset: 0x002CA0A7
		public MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM(UIContext context) : base(context)
		{
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x002CBEB0 File Offset: 0x002CA0B0
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x002CBEDB File Offset: 0x002CA0DB
		public void SetIds()
		{
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x002CBEDD File Offset: 0x002CA0DD
		public void SetAttributes()
		{
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.DoNotAcceptEvents = true;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.DoNotAcceptEvents = true;
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x002CBF18 File Offset: 0x002CA118
		public void RefreshBindingWithChildren()
		{
			MapMobilePartyTrackerVM datasource_Root = this._datasource_Root;
			this.SetDataSource(null);
			this.SetDataSource(datasource_Root);
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x002CBF3C File Offset: 0x002CA13C
		public void DestroyDataSource()
		{
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				if (this._datasource_Root_Trackers != null)
				{
					this._datasource_Root_Trackers.ListChanged -= this.OnList_datasource_Root_TrackersChanged;
					for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0.GetChild(i);
						((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
						((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)this._widget_0.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_Trackers = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x002CC097 File Offset: 0x002CA297
		public void SetDataSource(MapMobilePartyTrackerVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x002CC0A0 File Offset: 0x002CA2A0
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x002CC0AE File Offset: 0x002CA2AE
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x002CC0BC File Offset: 0x002CA2BC
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x002CC0CA File Offset: 0x002CA2CA
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x002CC0D8 File Offset: 0x002CA2D8
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x002CC0E6 File Offset: 0x002CA2E6
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x002CC0F4 File Offset: 0x002CA2F4
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x002CC102 File Offset: 0x002CA302
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x002CC110 File Offset: 0x002CA310
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x002CC11E File Offset: 0x002CA31E
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Trackers")
			{
				this.RefreshDataSource_datasource_Root_Trackers(this._datasource_Root.Trackers);
				return;
			}
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x002CC140 File Offset: 0x002CA340
		public void OnList_datasource_Root_TrackersChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0.GetChild(i);
					((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0.GetChild(i);
					((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
					this._widget_0.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_Trackers.Count; j++)
				{
					MobilePartyTrackItemVM bindingObject = this._datasource_Root_Trackers[j];
					this._widget_0.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate = new MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate);
				MobilePartyTrackItemVM mobilePartyTrackItemVM = this._datasource_Root_Trackers[e.NewIndex];
				generatedWidgetData.Data = mobilePartyTrackItemVM;
				mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_0.AddChildAtIndex(mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate, e.NewIndex);
				mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.CreateWidgets();
				mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetIds();
				mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetAttributes();
				mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetDataSource(mobilePartyTrackItemVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_0.GetChild(e.NewIndex);
				((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_0.GetChild(e.NewIndex);
				((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child4).SetDataSource(null);
				this._widget_0.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x002CC2DC File Offset: 0x002CA4DC
		private void RefreshDataSource_datasource_Root(MapMobilePartyTrackerVM newDataSource)
		{
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				if (this._datasource_Root_Trackers != null)
				{
					this._datasource_Root_Trackers.ListChanged -= this.OnList_datasource_Root_TrackersChanged;
					for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0.GetChild(i);
						((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_0.GetChild(i);
						((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
						this._widget_0.RemoveChild(child2);
					}
					this._datasource_Root_Trackers = null;
				}
				this._datasource_Root = null;
			}
			this._datasource_Root = newDataSource;
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._datasource_Root_Trackers = this._datasource_Root.Trackers;
				if (this._datasource_Root_Trackers != null)
				{
					this._datasource_Root_Trackers.ListChanged += this.OnList_datasource_Root_TrackersChanged;
					for (int j = 0; j < this._datasource_Root_Trackers.Count; j++)
					{
						MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate = new MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate);
						MobilePartyTrackItemVM mobilePartyTrackItemVM = this._datasource_Root_Trackers[j];
						generatedWidgetData.Data = mobilePartyTrackItemVM;
						mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_0.AddChildAtIndex(mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate, j);
						mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.CreateWidgets();
						mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetIds();
						mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetAttributes();
						mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetDataSource(mobilePartyTrackItemVM);
					}
				}
			}
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x002CC5D4 File Offset: 0x002CA7D4
		private void RefreshDataSource_datasource_Root_Trackers(MBBindingList<MobilePartyTrackItemVM> newDataSource)
		{
			if (this._datasource_Root_Trackers != null)
			{
				this._datasource_Root_Trackers.ListChanged -= this.OnList_datasource_Root_TrackersChanged;
				for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0.GetChild(i);
					((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0.GetChild(i);
					((MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
					this._widget_0.RemoveChild(child2);
				}
				this._datasource_Root_Trackers = null;
			}
			this._datasource_Root_Trackers = newDataSource;
			this._datasource_Root_Trackers = this._datasource_Root.Trackers;
			if (this._datasource_Root_Trackers != null)
			{
				this._datasource_Root_Trackers.ListChanged += this.OnList_datasource_Root_TrackersChanged;
				for (int j = 0; j < this._datasource_Root_Trackers.Count; j++)
				{
					MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate = new MapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate);
					MobilePartyTrackItemVM mobilePartyTrackItemVM = this._datasource_Root_Trackers[j];
					generatedWidgetData.Data = mobilePartyTrackItemVM;
					mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_0.AddChildAtIndex(mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate, j);
					mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.CreateWidgets();
					mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetIds();
					mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetAttributes();
					mapMobilePartyTracker__SandBox_ViewModelCollection_Map_MapMobilePartyTrackerVM_Dependency_1_ItemTemplate.SetDataSource(mobilePartyTrackItemVM);
				}
			}
		}

		// Token: 0x04001288 RID: 4744
		private Widget _widget;

		// Token: 0x04001289 RID: 4745
		private Widget _widget_0;

		// Token: 0x0400128A RID: 4746
		private MapMobilePartyTrackerVM _datasource_Root;

		// Token: 0x0400128B RID: 4747
		private MBBindingList<MobilePartyTrackItemVM> _datasource_Root_Trackers;
	}
}
