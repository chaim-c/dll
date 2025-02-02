﻿using System;
using System.ComponentModel;
using SandBox.ViewModelCollection.Map;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200007E RID: 126
	public class MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM : Widget, IGeneratedGauntletMovieRoot
	{
		// Token: 0x06001F01 RID: 7937 RVA: 0x000EBBF2 File Offset: 0x000E9DF2
		public MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM(UIContext context) : base(context)
		{
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000EBBFB File Offset: 0x000E9DFB
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000EBC26 File Offset: 0x000E9E26
		public void SetIds()
		{
			base.Id = "MapEventVisualsScreenWidget";
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000EBC33 File Offset: 0x000E9E33
		public void SetAttributes()
		{
			base.DoNotAcceptEvents = true;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.DoNotAcceptEvents = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000EBC70 File Offset: 0x000E9E70
		public void RefreshBindingWithChildren()
		{
			MapEventVisualsVM datasource_Root = this._datasource_Root;
			this.SetDataSource(null);
			this.SetDataSource(datasource_Root);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000EBC94 File Offset: 0x000E9E94
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
				if (this._datasource_Root_MapEvents != null)
				{
					this._datasource_Root_MapEvents.ListChanged -= this.OnList_datasource_Root_MapEventsChanged;
					for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0.GetChild(i);
						((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
						((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)this._widget_0.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_MapEvents = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000EBDEF File Offset: 0x000E9FEF
		public void SetDataSource(MapEventVisualsVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000EBDF8 File Offset: 0x000E9FF8
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000EBE06 File Offset: 0x000EA006
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000EBE14 File Offset: 0x000EA014
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000EBE22 File Offset: 0x000EA022
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000EBE30 File Offset: 0x000EA030
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000EBE3E File Offset: 0x000EA03E
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000EBE4C File Offset: 0x000EA04C
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000EBE5A File Offset: 0x000EA05A
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000EBE68 File Offset: 0x000EA068
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000EBE76 File Offset: 0x000EA076
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "MapEvents")
			{
				this.RefreshDataSource_datasource_Root_MapEvents(this._datasource_Root.MapEvents);
				return;
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000EBE98 File Offset: 0x000EA098
		public void OnList_datasource_Root_MapEventsChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0.GetChild(i);
					((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0.GetChild(i);
					((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
					this._widget_0.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_MapEvents.Count; j++)
				{
					MapEventVisualItemVM bindingObject = this._datasource_Root_MapEvents[j];
					this._widget_0.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate = new MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate);
				MapEventVisualItemVM mapEventVisualItemVM = this._datasource_Root_MapEvents[e.NewIndex];
				generatedWidgetData.Data = mapEventVisualItemVM;
				mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_0.AddChildAtIndex(mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate, e.NewIndex);
				mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.CreateWidgets();
				mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetIds();
				mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetAttributes();
				mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetDataSource(mapEventVisualItemVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_0.GetChild(e.NewIndex);
				((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_0.GetChild(e.NewIndex);
				((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child4).SetDataSource(null);
				this._widget_0.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000EC034 File Offset: 0x000EA234
		private void RefreshDataSource_datasource_Root(MapEventVisualsVM newDataSource)
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
				if (this._datasource_Root_MapEvents != null)
				{
					this._datasource_Root_MapEvents.ListChanged -= this.OnList_datasource_Root_MapEventsChanged;
					for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0.GetChild(i);
						((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_0.GetChild(i);
						((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
						this._widget_0.RemoveChild(child2);
					}
					this._datasource_Root_MapEvents = null;
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
				this._datasource_Root_MapEvents = this._datasource_Root.MapEvents;
				if (this._datasource_Root_MapEvents != null)
				{
					this._datasource_Root_MapEvents.ListChanged += this.OnList_datasource_Root_MapEventsChanged;
					for (int j = 0; j < this._datasource_Root_MapEvents.Count; j++)
					{
						MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate = new MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate);
						MapEventVisualItemVM mapEventVisualItemVM = this._datasource_Root_MapEvents[j];
						generatedWidgetData.Data = mapEventVisualItemVM;
						mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_0.AddChildAtIndex(mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate, j);
						mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.CreateWidgets();
						mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetIds();
						mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetAttributes();
						mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetDataSource(mapEventVisualItemVM);
					}
				}
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000EC32C File Offset: 0x000EA52C
		private void RefreshDataSource_datasource_Root_MapEvents(MBBindingList<MapEventVisualItemVM> newDataSource)
		{
			if (this._datasource_Root_MapEvents != null)
			{
				this._datasource_Root_MapEvents.ListChanged -= this.OnList_datasource_Root_MapEventsChanged;
				for (int i = this._widget_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0.GetChild(i);
					((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0.GetChild(i);
					((MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
					this._widget_0.RemoveChild(child2);
				}
				this._datasource_Root_MapEvents = null;
			}
			this._datasource_Root_MapEvents = newDataSource;
			this._datasource_Root_MapEvents = this._datasource_Root.MapEvents;
			if (this._datasource_Root_MapEvents != null)
			{
				this._datasource_Root_MapEvents.ListChanged += this.OnList_datasource_Root_MapEventsChanged;
				for (int j = 0; j < this._datasource_Root_MapEvents.Count; j++)
				{
					MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate = new MapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate);
					MapEventVisualItemVM mapEventVisualItemVM = this._datasource_Root_MapEvents[j];
					generatedWidgetData.Data = mapEventVisualItemVM;
					mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_0.AddChildAtIndex(mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate, j);
					mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.CreateWidgets();
					mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetIds();
					mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetAttributes();
					mapEventVisuals__SandBox_ViewModelCollection_Map_MapEventVisualsVM_Dependency_1_ItemTemplate.SetDataSource(mapEventVisualItemVM);
				}
			}
		}

		// Token: 0x04000678 RID: 1656
		private Widget _widget;

		// Token: 0x04000679 RID: 1657
		private Widget _widget_0;

		// Token: 0x0400067A RID: 1658
		private MapEventVisualsVM _datasource_Root;

		// Token: 0x0400067B RID: 1659
		private MBBindingList<MapEventVisualItemVM> _datasource_Root_MapEvents;
	}
}
