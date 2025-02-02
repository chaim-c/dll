using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000034 RID: 52
	public class RadioContainerWidget : Widget
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x00009A87 File Offset: 0x00007C87
		public RadioContainerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009A90 File Offset: 0x00007C90
		private void ContainerOnPropertyChanged(PropertyOwnerObject owner, string propertyName, int value)
		{
			if (propertyName == "IntValue")
			{
				this.SelectedIndex = this.Container.IntValue;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009AB0 File Offset: 0x00007CB0
		private void ContainerOnEventFire(Widget owner, string eventName, object[] arguments)
		{
			if (eventName == "ItemAdd" || eventName == "ItemRemove")
			{
				this.Container.IntValue = this.SelectedIndex;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009AE0 File Offset: 0x00007CE0
		private void ContainerUpdated(Container newContainer)
		{
			if (this.Container != null)
			{
				this.Container.intPropertyChanged -= this.ContainerOnPropertyChanged;
				this.Container.EventFire -= this.ContainerOnEventFire;
			}
			if (newContainer != null)
			{
				newContainer.intPropertyChanged += this.ContainerOnPropertyChanged;
				newContainer.EventFire += this.ContainerOnEventFire;
				newContainer.IntValue = this.SelectedIndex;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00009B56 File Offset: 0x00007D56
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00009B5E File Offset: 0x00007D5E
		[Editor(false)]
		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				if (this._selectedIndex != value)
				{
					this._selectedIndex = value;
					base.OnPropertyChanged(value, "SelectedIndex");
					if (this.Container != null)
					{
						this.Container.IntValue = this._selectedIndex;
					}
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00009B95 File Offset: 0x00007D95
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00009B9D File Offset: 0x00007D9D
		[Editor(false)]
		public Container Container
		{
			get
			{
				return this._container;
			}
			set
			{
				if (this._container != value)
				{
					this.ContainerUpdated(value);
					this._container = value;
					base.OnPropertyChanged<Container>(value, "Container");
				}
			}
		}

		// Token: 0x04000132 RID: 306
		private int _selectedIndex;

		// Token: 0x04000133 RID: 307
		private Container _container;
	}
}
