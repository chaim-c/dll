using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.PrefabSystem;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x02000003 RID: 3
	public class GauntletView : WidgetComponent
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002438 File Offset: 0x00000638
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002440 File Offset: 0x00000640
		internal bool AddedToChildren { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002449 File Offset: 0x00000649
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002456 File Offset: 0x00000656
		public object Tag
		{
			get
			{
				return base.Target.Tag;
			}
			set
			{
				base.Target.Tag = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000246C File Offset: 0x0000066C
		public GauntletMovie GauntletMovie { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002475 File Offset: 0x00000675
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000247D File Offset: 0x0000067D
		public ItemTemplateUsageWithData ItemTemplateUsageWithData { get; internal set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002488 File Offset: 0x00000688
		public BindingPath ViewModelPath
		{
			get
			{
				if (this.Parent == null)
				{
					return this._viewModelPath;
				}
				if (this._viewModelPath != null)
				{
					return this.Parent.ViewModelPath.Append(this._viewModelPath);
				}
				return this.Parent.ViewModelPath;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024D4 File Offset: 0x000006D4
		public string ViewModelPathString
		{
			get
			{
				MBStringBuilder mbstringBuilder = default(MBStringBuilder);
				mbstringBuilder.Initialize(16, "ViewModelPathString");
				this.WriteViewModelPathToStringBuilder(ref mbstringBuilder);
				return mbstringBuilder.ToStringAndRelease();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002508 File Offset: 0x00000708
		private void WriteViewModelPathToStringBuilder(ref MBStringBuilder sb)
		{
			if (this.Parent == null)
			{
				if (this._viewModelPath != null)
				{
					sb.Append<string>(this._viewModelPath.Path);
					return;
				}
			}
			else
			{
				this.Parent.WriteViewModelPathToStringBuilder(ref sb);
				if (this._viewModelPath != null)
				{
					sb.Append<string>("\\");
					sb.Append<string>(this._viewModelPath.Path);
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002576 File Offset: 0x00000776
		internal void InitializeViewModelPath(BindingPath path)
		{
			this._viewModelPath = path;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000257F File Offset: 0x0000077F
		public MBReadOnlyList<GauntletView> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002587 File Offset: 0x00000787
		public IEnumerable<GauntletView> AllChildren
		{
			get
			{
				foreach (GauntletView gauntletView in this._children)
				{
					yield return gauntletView;
					foreach (GauntletView gauntletView2 in gauntletView.AllChildren)
					{
						yield return gauntletView2;
					}
					IEnumerator<GauntletView> enumerator2 = null;
					gauntletView = null;
				}
				List<GauntletView>.Enumerator enumerator = default(List<GauntletView>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002597 File Offset: 0x00000797
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000259F File Offset: 0x0000079F
		public GauntletView Parent { get; private set; }

		// Token: 0x0600002A RID: 42 RVA: 0x000025A8 File Offset: 0x000007A8
		internal GauntletView(GauntletMovie gauntletMovie, GauntletView parent, Widget target, int childCount = 64) : base(target)
		{
			this.GauntletMovie = gauntletMovie;
			this.Parent = parent;
			this._children = new MBList<GauntletView>(childCount);
			this._bindDataInfos = new Dictionary<string, ViewBindDataInfo>();
			this._bindCommandInfos = new Dictionary<string, ViewBindCommandInfo>();
			this._items = new List<GauntletView>(childCount);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025FA File Offset: 0x000007FA
		public void AddChild(GauntletView child)
		{
			this._children.Add(child);
			child.AddedToChildren = true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000260F File Offset: 0x0000080F
		public void RemoveChild(GauntletView child)
		{
			base.Target.OnBeforeRemovedChild(child.Target);
			base.Target.RemoveChild(child.Target);
			this._children.Remove(child);
			child.ClearEventHandlersWithChildren();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002648 File Offset: 0x00000848
		public void SwapChildrenAtIndeces(GauntletView child1, GauntletView child2)
		{
			int index = this._children.IndexOf(child1);
			int index2 = this._children.IndexOf(child2);
			base.Target.SwapChildren(this._children[index].Target, this._children[index2].Target);
			GauntletView value = this._children[index];
			this._children[index] = this._children[index2];
			this._children[index2] = value;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026D0 File Offset: 0x000008D0
		public void RefreshBinding()
		{
			object viewModelAtPath = this.GauntletMovie.GetViewModelAtPath(this.ViewModelPath, this.ItemTemplateUsageWithData != null && this.ItemTemplateUsageWithData.ItemTemplateUsage != null);
			this.ClearEventHandlersWithChildren();
			if (viewModelAtPath is IViewModel)
			{
				this._viewModel = (viewModelAtPath as IViewModel);
				if (this._viewModel == null)
				{
					goto IL_1D0;
				}
				this._viewModel.PropertyChanged += this.OnViewModelPropertyChanged;
				this._viewModel.PropertyChangedWithValue += this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithBoolValue += this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithColorValue += this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithDoubleValue += this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithFloatValue += this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithIntValue += this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithUIntValue += this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithVec2Value += this.OnViewModelPropertyChangedWithValue;
				using (Dictionary<string, ViewBindDataInfo>.ValueCollection.Enumerator enumerator = this._bindDataInfos.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ViewBindDataInfo viewBindDataInfo = enumerator.Current;
						object propertyValue = this._viewModel.GetPropertyValue(viewBindDataInfo.Path.LastNode);
						this.SetData(viewBindDataInfo.Property, propertyValue);
					}
					goto IL_1D0;
				}
			}
			if (viewModelAtPath is IMBBindingList)
			{
				this._bindingList = (viewModelAtPath as IMBBindingList);
				if (this._bindingList != null)
				{
					this._bindingList.ListChanged += this.OnViewModelBindingListChanged;
					for (int i = 0; i < this._bindingList.Count; i++)
					{
						this.AddItemToList(i);
					}
				}
			}
			IL_1D0:
			base.Target.EventFire += this.OnEventFired;
			base.Target.PropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.boolPropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.ColorPropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.doublePropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.floatPropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.intPropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.uintPropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.Vec2PropertyChanged += this.OnViewObjectPropertyChanged;
			base.Target.Vector2PropertyChanged += this.OnViewObjectPropertyChanged;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029A4 File Offset: 0x00000BA4
		private void OnEventFired(Widget widget, string commandName, object[] args)
		{
			this.OnCommand(commandName, args);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void RefreshBindingWithChildren()
		{
			this.RefreshBinding();
			for (int i = 0; i < this._children.Count; i++)
			{
				this._children[i].RefreshBindingWithChildren();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029EC File Offset: 0x00000BEC
		private void ReleaseBinding()
		{
			if (this._viewModel != null)
			{
				this._viewModel.PropertyChanged -= this.OnViewModelPropertyChanged;
				this._viewModel.PropertyChangedWithValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithBoolValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithColorValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithDoubleValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithFloatValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithIntValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithUIntValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithVec2Value -= this.OnViewModelPropertyChangedWithValue;
				return;
			}
			if (this._bindingList != null)
			{
				this._bindingList.ListChanged -= this.OnViewModelBindingListChanged;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public void ReleaseBindingWithChildren()
		{
			this.ReleaseBinding();
			for (int i = 0; i < this._children.Count; i++)
			{
				this._children[i].ReleaseBindingWithChildren();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B30 File Offset: 0x00000D30
		internal void ClearEventHandlersWithChildren()
		{
			this.ClearEventHandlers();
			for (int i = 0; i < this._children.Count; i++)
			{
				this._children[i].ClearEventHandlersWithChildren();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B6C File Offset: 0x00000D6C
		private void ClearEventHandlers()
		{
			if (this._viewModel != null)
			{
				this._viewModel.PropertyChanged -= this.OnViewModelPropertyChanged;
				this._viewModel.PropertyChangedWithValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithBoolValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithColorValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithDoubleValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithFloatValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithIntValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithUIntValue -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel.PropertyChangedWithVec2Value -= this.OnViewModelPropertyChangedWithValue;
				this._viewModel = null;
			}
			if (this._bindingList != null)
			{
				this.OnListReset();
				this._bindingList.ListChanged -= this.OnViewModelBindingListChanged;
				this._bindingList = null;
			}
			base.Target.EventFire -= this.OnEventFired;
			base.Target.PropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.boolPropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.ColorPropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.doublePropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.floatPropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.intPropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.uintPropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.Vec2PropertyChanged -= this.OnViewObjectPropertyChanged;
			base.Target.Vector2PropertyChanged -= this.OnViewObjectPropertyChanged;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D6C File Offset: 0x00000F6C
		public void BindData(string property, BindingPath path)
		{
			ViewBindDataInfo viewBindDataInfo = new ViewBindDataInfo(this, property, path);
			if (!this._bindDataInfos.ContainsKey(path.Path))
			{
				this._bindDataInfos.Add(path.Path, viewBindDataInfo);
			}
			else
			{
				this._bindDataInfos[path.Path] = viewBindDataInfo;
			}
			if (this._viewModel != null)
			{
				object propertyValue = this._viewModel.GetPropertyValue(path.LastNode);
				this.SetData(viewBindDataInfo.Property, propertyValue);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public void ClearBinding(string propertyName)
		{
			foreach (KeyValuePair<string, ViewBindDataInfo> keyValuePair in this._bindDataInfos.ToArray<KeyValuePair<string, ViewBindDataInfo>>())
			{
				if (keyValuePair.Value.Property == propertyName)
				{
					this._bindDataInfos.Remove(keyValuePair.Key);
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E3C File Offset: 0x0000103C
		internal void BindCommand(string command, BindingPath path, string parameterValue = null)
		{
			ViewBindCommandInfo value = new ViewBindCommandInfo(this, command, path, parameterValue);
			if (!this._bindCommandInfos.ContainsKey(command))
			{
				this._bindCommandInfos.Add(command, value);
				return;
			}
			this._bindCommandInfos[command] = value;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E7C File Offset: 0x0000107C
		private void OnViewModelBindingListChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				this.OnListReset();
				return;
			case ListChangedType.Sorted:
				this.OnListSorted();
				return;
			case ListChangedType.ItemAdded:
				this.OnItemAddedToList(e.NewIndex);
				return;
			case ListChangedType.ItemBeforeDeleted:
				this.OnBeforeItemRemovedFromList(e.NewIndex);
				break;
			case ListChangedType.ItemDeleted:
				this.OnItemRemovedFromList(e.NewIndex);
				return;
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002EE3 File Offset: 0x000010E3
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002EF7 File Offset: 0x000010F7
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F10 File Offset: 0x00001110
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F29 File Offset: 0x00001129
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F42 File Offset: 0x00001142
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F5B File Offset: 0x0000115B
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F74 File Offset: 0x00001174
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F8D File Offset: 0x0000118D
		private void OnViewModelPropertyChangedWithValue(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.OnPropertyChanged(e.PropertyName, e.Value);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FA8 File Offset: 0x000011A8
		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			object propertyValue = this._viewModel.GetPropertyValue(e.PropertyName);
			this.OnPropertyChanged(e.PropertyName, propertyValue);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002FD4 File Offset: 0x000011D4
		private void OnPropertyChanged(string propertyName, object value)
		{
			if (value is ViewModel || value is IMBBindingList)
			{
				MBStringBuilder mbstringBuilder = default(MBStringBuilder);
				mbstringBuilder.Initialize(16, "OnPropertyChanged");
				this.WriteViewModelPathToStringBuilder(ref mbstringBuilder);
				mbstringBuilder.Append<string>("\\");
				mbstringBuilder.Append<string>(propertyName);
				string path = mbstringBuilder.ToStringAndRelease();
				using (List<GauntletView>.Enumerator enumerator = this.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GauntletView gauntletView = enumerator.Current;
						if (BindingPath.IsRelatedWithPathAsString(path, gauntletView.ViewModelPathString))
						{
							gauntletView.RefreshBindingWithChildren();
						}
					}
					return;
				}
			}
			ViewBindDataInfo viewBindDataInfo;
			if (this._bindDataInfos.Count > 0 && this._bindDataInfos.TryGetValue(propertyName, out viewBindDataInfo))
			{
				this.SetData(viewBindDataInfo.Property, value);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000030AC File Offset: 0x000012AC
		private void OnCommand(string command, object[] args)
		{
			ViewBindCommandInfo viewBindCommandInfo = null;
			if (this._bindCommandInfos.TryGetValue(command, out viewBindCommandInfo))
			{
				object[] array;
				if (viewBindCommandInfo.Parameter != null)
				{
					array = new object[args.Length + 1];
					array[args.Length] = viewBindCommandInfo.Parameter;
				}
				else
				{
					array = new object[args.Length];
				}
				for (int i = 0; i < args.Length; i++)
				{
					object parameter = args[i];
					object obj = this.ConvertCommandParameter(parameter);
					array[i] = obj;
				}
				BindingPath parentPath = viewBindCommandInfo.Path.ParentPath;
				BindingPath bindingPath = this.ViewModelPath;
				if (parentPath != null)
				{
					bindingPath = bindingPath.Append(parentPath);
				}
				BindingPath path = bindingPath.Simplify();
				IViewModel viewModel = this.GauntletMovie.ViewModel;
				string lastNode = viewBindCommandInfo.Path.LastNode;
				ViewModel viewModel2 = viewModel.GetViewModelAtPath(path, viewBindCommandInfo.Owner.ItemTemplateUsageWithData != null && viewBindCommandInfo.Owner.ItemTemplateUsageWithData.ItemTemplateUsage != null) as ViewModel;
				if (viewModel2 != null)
				{
					viewModel2.ExecuteCommand(lastNode, array);
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000031A4 File Offset: 0x000013A4
		private object ConvertCommandParameter(object parameter)
		{
			object result = parameter;
			if (parameter is Widget)
			{
				Widget widget = (Widget)parameter;
				GauntletView gauntletView = this.GauntletMovie.FindViewOf(widget);
				if (gauntletView != null)
				{
					if (gauntletView._viewModel != null)
					{
						result = gauntletView._viewModel;
					}
					else
					{
						result = gauntletView._bindingList;
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000031F0 File Offset: 0x000013F0
		private ViewBindDataInfo GetBindDataInfoOfProperty(string propertyName)
		{
			foreach (ViewBindDataInfo viewBindDataInfo in this._bindDataInfos.Values)
			{
				if (viewBindDataInfo.Property == propertyName)
				{
					return viewBindDataInfo;
				}
			}
			return null;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003258 File Offset: 0x00001458
		private void OnListSorted()
		{
			List<GauntletView> list = new List<GauntletView>(this._items.Capacity);
			for (int i = 0; i < this._bindingList.Count; i++)
			{
				object bindingObject = this._bindingList[i];
				GauntletView item = this._items.Find((GauntletView gauntletView) => gauntletView._viewModel == bindingObject);
				list.Add(item);
			}
			this._items = list;
			for (int j = 0; j < this._items.Count; j++)
			{
				BindingPath path = new BindingPath(j);
				GauntletView gauntletView2 = this._items[j];
				gauntletView2.Target.SetSiblingIndex(j, false);
				gauntletView2.InitializeViewModelPath(path);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003310 File Offset: 0x00001510
		private void OnListReset()
		{
			GauntletView[] array = this._items.ToArray();
			this._items.Clear();
			foreach (GauntletView gauntletView in array)
			{
				base.Target.OnBeforeRemovedChild(gauntletView.Target);
				this._children.Remove(gauntletView);
				base.Target.RemoveChild(gauntletView.Target);
				gauntletView.ClearEventHandlersWithChildren();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000337B File Offset: 0x0000157B
		private void OnItemAddedToList(int index)
		{
			this.AddItemToList(index).RefreshBindingWithChildren();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000338C File Offset: 0x0000158C
		private GauntletView AddItemToList(int index)
		{
			for (int i = index; i < this._items.Count; i++)
			{
				this._items[i]._viewModelPath = new BindingPath(i + 1);
			}
			BindingPath path = new BindingPath(index);
			WidgetCreationData widgetCreationData = new WidgetCreationData(this.GauntletMovie.Context, this.GauntletMovie.WidgetFactory, base.Target);
			widgetCreationData.AddExtensionData(this.GauntletMovie);
			widgetCreationData.AddExtensionData(this);
			bool flag = false;
			GauntletView gauntletView;
			if (this._items.Count == 0 && this.ItemTemplateUsageWithData.FirstItemTemplate != null)
			{
				gauntletView = this.ItemTemplateUsageWithData.FirstItemTemplate.Instantiate(widgetCreationData, this.ItemTemplateUsageWithData.GivenParameters).GetGauntletView();
			}
			else if (this._items.Count == index && this._items.Count > 0 && this.ItemTemplateUsageWithData.LastItemTemplate != null)
			{
				BindingPath viewModelPath = this._items[this._items.Count - 1]._viewModelPath;
				GauntletView gauntletView2 = this.ItemTemplateUsageWithData.DefaultItemTemplate.Instantiate(widgetCreationData, this.ItemTemplateUsageWithData.GivenParameters).GetGauntletView();
				this._items.Insert(this._items.Count, gauntletView2);
				this.RemoveItemFromList(this._items.Count - 2);
				gauntletView2.InitializeViewModelPath(viewModelPath);
				gauntletView2.RefreshBindingWithChildren();
				flag = true;
				gauntletView = this.ItemTemplateUsageWithData.LastItemTemplate.Instantiate(widgetCreationData, this.ItemTemplateUsageWithData.GivenParameters).GetGauntletView();
			}
			else
			{
				gauntletView = this.ItemTemplateUsageWithData.DefaultItemTemplate.Instantiate(widgetCreationData, this.ItemTemplateUsageWithData.GivenParameters).GetGauntletView();
			}
			gauntletView.InitializeViewModelPath(path);
			this._items.Insert(index, gauntletView);
			for (int j = flag ? (index - 1) : index; j < this._items.Count; j++)
			{
				this._items[j].Target.SetSiblingIndex(j, flag);
			}
			return gauntletView;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003597 File Offset: 0x00001797
		private void OnItemRemovedFromList(int index)
		{
			this.RemoveItemFromList(index);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035A0 File Offset: 0x000017A0
		private void RemoveItemFromList(int index)
		{
			GauntletView gauntletView = this._items[index];
			this._items.RemoveAt(index);
			this._children.Remove(gauntletView);
			base.Target.RemoveChild(gauntletView.Target);
			gauntletView.ClearEventHandlersWithChildren();
			for (int i = index; i < this._items.Count; i++)
			{
				this._items[i].Target.SetSiblingIndex(i, false);
			}
			BindingPath viewModelPath = gauntletView._viewModelPath;
			for (int j = index; j < this._items.Count; j++)
			{
				GauntletView gauntletView2 = this._items[j];
				BindingPath viewModelPath2 = gauntletView2._viewModelPath;
				gauntletView2._viewModelPath = viewModelPath;
				viewModelPath = viewModelPath2;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003652 File Offset: 0x00001852
		private void OnBeforeItemRemovedFromList(int index)
		{
			this.PreviewRemoveItemFromList(index);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000365C File Offset: 0x0000185C
		private void PreviewRemoveItemFromList(int index)
		{
			GauntletView gauntletView = this._items[index];
			base.Target.OnBeforeRemovedChild(gauntletView.Target);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003687 File Offset: 0x00001887
		private void SetData(string path, object value)
		{
			WidgetExtensions.SetWidgetAttribute(this.GauntletMovie.Context, base.Target, path, value);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000036A4 File Offset: 0x000018A4
		private void OnViewPropertyChanged(string propertyName, object value)
		{
			if (this._viewModel != null)
			{
				ViewBindDataInfo bindDataInfoOfProperty = this.GetBindDataInfoOfProperty(propertyName);
				if (bindDataInfoOfProperty != null)
				{
					this._viewModel.SetPropertyValue(bindDataInfoOfProperty.Path.LastNode, value);
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000036DC File Offset: 0x000018DC
		public string DisplayName
		{
			get
			{
				string text = "";
				if (base.Target != null)
				{
					text = text + base.Target.Id + "!" + base.Target.Tag.ToString();
				}
				if (this.ViewModelPath != null)
				{
					text = text + "@" + this.ViewModelPath.Path;
				}
				return text;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003744 File Offset: 0x00001944
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, object value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000374E File Offset: 0x0000194E
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, bool value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000375D File Offset: 0x0000195D
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, float value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000376C File Offset: 0x0000196C
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, Color value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000377B File Offset: 0x0000197B
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, double value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000378A File Offset: 0x0000198A
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, int value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003799 File Offset: 0x00001999
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, uint value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000037A8 File Offset: 0x000019A8
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037B7 File Offset: 0x000019B7
		private void OnViewObjectPropertyChanged(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 value)
		{
			this.OnViewPropertyChanged(propertyName, value);
		}

		// Token: 0x0400000C RID: 12
		private BindingPath _viewModelPath;

		// Token: 0x0400000E RID: 14
		private Dictionary<string, ViewBindDataInfo> _bindDataInfos;

		// Token: 0x0400000F RID: 15
		private Dictionary<string, ViewBindCommandInfo> _bindCommandInfos;

		// Token: 0x04000010 RID: 16
		private IViewModel _viewModel;

		// Token: 0x04000011 RID: 17
		private IMBBindingList _bindingList;

		// Token: 0x04000012 RID: 18
		private MBList<GauntletView> _children;

		// Token: 0x04000013 RID: 19
		private List<GauntletView> _items;
	}
}
