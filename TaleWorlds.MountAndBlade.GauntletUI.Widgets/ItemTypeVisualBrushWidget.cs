using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000029 RID: 41
	public class ItemTypeVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00007C09 File Offset: 0x00005E09
		public ItemTypeVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007C14 File Offset: 0x00005E14
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized)
			{
				string brushName = null;
				if (!string.IsNullOrEmpty(this.ItemTypeAsString))
				{
					string itemTypeAsString = this.ItemTypeAsString;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(itemTypeAsString);
					if (num <= 2039097040U)
					{
						if (num <= 947870807U)
						{
							if (num <= 784896431U)
							{
								if (num != 677454334U)
								{
									if (num != 784896431U)
									{
										goto IL_27D;
									}
									if (!(itemTypeAsString == "Banner"))
									{
										goto IL_27D;
									}
								}
								else if (!(itemTypeAsString == "Spear"))
								{
									goto IL_27D;
								}
							}
							else if (num != 810547195U)
							{
								if (num != 947870807U)
								{
									goto IL_27D;
								}
								if (!(itemTypeAsString == "Mace"))
								{
									goto IL_27D;
								}
							}
							else if (!(itemTypeAsString == "None"))
							{
								goto IL_27D;
							}
						}
						else if (num <= 1842662042U)
						{
							if (num != 1041399898U)
							{
								if (num != 1842662042U)
								{
									goto IL_27D;
								}
								if (!(itemTypeAsString == "Stone"))
								{
									goto IL_27D;
								}
							}
							else if (!(itemTypeAsString == "Mount"))
							{
								goto IL_27D;
							}
						}
						else if (num != 1894730868U)
						{
							if (num != 2039097040U)
							{
								goto IL_27D;
							}
							if (!(itemTypeAsString == "Shield"))
							{
								goto IL_27D;
							}
						}
						else if (!(itemTypeAsString == "Javelin"))
						{
							goto IL_27D;
						}
					}
					else if (num <= 3440297014U)
					{
						if (num <= 2665595067U)
						{
							if (num != 2233436357U)
							{
								if (num != 2665595067U)
								{
									goto IL_27D;
								}
								if (!(itemTypeAsString == "Axe"))
								{
									goto IL_27D;
								}
							}
							else if (!(itemTypeAsString == "PickUp"))
							{
								goto IL_27D;
							}
						}
						else if (num != 3332997230U)
						{
							if (num != 3440297014U)
							{
								goto IL_27D;
							}
							if (!(itemTypeAsString == "Sword"))
							{
								goto IL_27D;
							}
						}
						else if (!(itemTypeAsString == "ThrowingKnife"))
						{
							goto IL_27D;
						}
					}
					else if (num <= 3687274959U)
					{
						if (num != 3637216139U)
						{
							if (num != 3687274959U)
							{
								goto IL_27D;
							}
							if (!(itemTypeAsString == "Ammo"))
							{
								goto IL_27D;
							}
						}
						else if (!(itemTypeAsString == "Bow"))
						{
							goto IL_27D;
						}
					}
					else if (num != 3778748927U)
					{
						if (num != 4282369777U)
						{
							goto IL_27D;
						}
						if (!(itemTypeAsString == "Crossbow"))
						{
							goto IL_27D;
						}
					}
					else if (!(itemTypeAsString == "ThrowingAxe"))
					{
						goto IL_27D;
					}
					brushName = "Item.Type.Icon." + this.ItemTypeAsString;
					goto IL_2AB;
					IL_27D:
					Debug.FailedAssert("Unidentified item type to show type for: " + this.ItemTypeAsString, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\ItemTypeVisualBrushWidget.cs", "OnLateUpdate", 66);
				}
				else
				{
					brushName = "Item.Type.Icon.None";
				}
				IL_2AB:
				if (!string.IsNullOrEmpty(brushName))
				{
					base.Brush = base.Context.Brushes.SingleOrDefault((Brush b) => b.Name == brushName);
				}
				this._isInitialized = true;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00007F02 File Offset: 0x00006102
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00007F0A File Offset: 0x0000610A
		[Editor(false)]
		public string ItemTypeAsString
		{
			get
			{
				return this._itemTypeAsString;
			}
			set
			{
				if (value != this._itemTypeAsString)
				{
					this._itemTypeAsString = value;
					base.OnPropertyChanged<string>(value, "ItemTypeAsString");
					this._isInitialized = false;
				}
			}
		}

		// Token: 0x040000FF RID: 255
		private const string ItemTypeBrushNameBase = "Item.Type.Icon.";

		// Token: 0x04000100 RID: 256
		private bool _isInitialized;

		// Token: 0x04000101 RID: 257
		private string _itemTypeAsString;
	}
}
