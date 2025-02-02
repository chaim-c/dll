﻿using System;
using System.ComponentModel;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200018F RID: 399
	public class SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_5_SPScoreboardSide__DependendPrefab : Widget
	{
		// Token: 0x06007C8B RID: 31883 RVA: 0x003E5437 File Offset: 0x003E3637
		public SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_5_SPScoreboardSide__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x003E5440 File Offset: 0x003E3640
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_0_0 = new ListPanel(base.Context);
			this._widget_0_0.AddChild(this._widget_0_0_0);
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x003E54BA File Offset: 0x003E36BA
		public void SetIds()
		{
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x003E54BC File Offset: 0x003E36BC
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.SuggestedWidth = 630f;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_0_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_0.VerticalAlignment = VerticalAlignment.Top;
			this._widget_0_0_0.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x003E5560 File Offset: 0x003E3760
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
				if (this._datasource_Root_Parties != null)
				{
					this._datasource_Root_Parties.ListChanged -= this.OnList_datasource_Root_PartiesChanged;
					for (int i = this._widget_0_0_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0_0_0.GetChild(i);
						((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child).OnBeforeRemovedChild(child);
						((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)this._widget_0_0_0.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_Parties = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06007C90 RID: 31888 RVA: 0x003E56BB File Offset: 0x003E38BB
		public void SetDataSource(SPScoreboardSideVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06007C91 RID: 31889 RVA: 0x003E56C4 File Offset: 0x003E38C4
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C92 RID: 31890 RVA: 0x003E56D2 File Offset: 0x003E38D2
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x003E56E0 File Offset: 0x003E38E0
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x003E56EE File Offset: 0x003E38EE
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x003E56FC File Offset: 0x003E38FC
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C96 RID: 31894 RVA: 0x003E570A File Offset: 0x003E390A
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x003E5718 File Offset: 0x003E3918
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x003E5726 File Offset: 0x003E3926
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C99 RID: 31897 RVA: 0x003E5734 File Offset: 0x003E3934
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06007C9A RID: 31898 RVA: 0x003E5742 File Offset: 0x003E3942
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Parties")
			{
				this.RefreshDataSource_datasource_Root_Parties(this._datasource_Root.Parties);
				return;
			}
		}

		// Token: 0x06007C9B RID: 31899 RVA: 0x003E5764 File Offset: 0x003E3964
		public void OnList_datasource_Root_PartiesChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_0_0_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0_0_0.GetChild(i);
					((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0_0_0.GetChild(i);
					((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child2).SetDataSource(null);
					this._widget_0_0_0.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_Parties.Count; j++)
				{
					SPScoreboardPartyVM bindingObject = this._datasource_Root_Parties[j];
					this._widget_0_0_0.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate = new SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate);
				SPScoreboardPartyVM spscoreboardPartyVM = this._datasource_Root_Parties[e.NewIndex];
				generatedWidgetData.Data = spscoreboardPartyVM;
				spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_0_0_0.AddChildAtIndex(spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate, e.NewIndex);
				spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.CreateWidgets();
				spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetIds();
				spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetAttributes();
				spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetDataSource(spscoreboardPartyVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_0_0_0.GetChild(e.NewIndex);
				((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_0_0_0.GetChild(e.NewIndex);
				((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child4).SetDataSource(null);
				this._widget_0_0_0.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x003E5900 File Offset: 0x003E3B00
		private void RefreshDataSource_datasource_Root(SPScoreboardSideVM newDataSource)
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
				if (this._datasource_Root_Parties != null)
				{
					this._datasource_Root_Parties.ListChanged -= this.OnList_datasource_Root_PartiesChanged;
					for (int i = this._widget_0_0_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0_0_0.GetChild(i);
						((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_0_0_0.GetChild(i);
						((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child2).SetDataSource(null);
						this._widget_0_0_0.RemoveChild(child2);
					}
					this._datasource_Root_Parties = null;
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
				this._datasource_Root_Parties = this._datasource_Root.Parties;
				if (this._datasource_Root_Parties != null)
				{
					this._datasource_Root_Parties.ListChanged += this.OnList_datasource_Root_PartiesChanged;
					for (int j = 0; j < this._datasource_Root_Parties.Count; j++)
					{
						SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate = new SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate);
						SPScoreboardPartyVM spscoreboardPartyVM = this._datasource_Root_Parties[j];
						generatedWidgetData.Data = spscoreboardPartyVM;
						spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_0_0_0.AddChildAtIndex(spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate, j);
						spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.CreateWidgets();
						spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetIds();
						spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetAttributes();
						spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetDataSource(spscoreboardPartyVM);
					}
				}
			}
		}

		// Token: 0x06007C9D RID: 31901 RVA: 0x003E5BF8 File Offset: 0x003E3DF8
		private void RefreshDataSource_datasource_Root_Parties(MBBindingList<SPScoreboardPartyVM> newDataSource)
		{
			if (this._datasource_Root_Parties != null)
			{
				this._datasource_Root_Parties.ListChanged -= this.OnList_datasource_Root_PartiesChanged;
				for (int i = this._widget_0_0_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0_0_0.GetChild(i);
					((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0_0_0.GetChild(i);
					((SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate)child2).SetDataSource(null);
					this._widget_0_0_0.RemoveChild(child2);
				}
				this._datasource_Root_Parties = null;
			}
			this._datasource_Root_Parties = newDataSource;
			this._datasource_Root_Parties = this._datasource_Root.Parties;
			if (this._datasource_Root_Parties != null)
			{
				this._datasource_Root_Parties.ListChanged += this.OnList_datasource_Root_PartiesChanged;
				for (int j = 0; j < this._datasource_Root_Parties.Count; j++)
				{
					SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate = new SPScoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate);
					SPScoreboardPartyVM spscoreboardPartyVM = this._datasource_Root_Parties[j];
					generatedWidgetData.Data = spscoreboardPartyVM;
					spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_0_0_0.AddChildAtIndex(spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate, j);
					spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.CreateWidgets();
					spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetIds();
					spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetAttributes();
					spscoreboard__SandBox_ViewModelCollection_SPScoreboardVM_Dependency_9_ItemTemplate.SetDataSource(spscoreboardPartyVM);
				}
			}
		}

		// Token: 0x040018D2 RID: 6354
		private Widget _widget;

		// Token: 0x040018D3 RID: 6355
		private Widget _widget_0;

		// Token: 0x040018D4 RID: 6356
		private Widget _widget_0_0;

		// Token: 0x040018D5 RID: 6357
		private ListPanel _widget_0_0_0;

		// Token: 0x040018D6 RID: 6358
		private SPScoreboardSideVM _datasource_Root;

		// Token: 0x040018D7 RID: 6359
		private MBBindingList<SPScoreboardPartyVM> _datasource_Root_Parties;
	}
}
