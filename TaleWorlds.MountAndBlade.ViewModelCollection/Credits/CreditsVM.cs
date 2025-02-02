using System;
using System.IO;
using System.Xml;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Credits
{
	// Token: 0x02000073 RID: 115
	public class CreditsVM : ViewModel
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x0002530D File Offset: 0x0002350D
		public CreditsVM()
		{
			this.ExitKey = InputKeyItemVM.CreateFromHotKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"), false);
			this.ExitText = new TextObject("{=3CsACce8}Exit", null).ToString();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002534C File Offset: 0x0002354C
		private static CreditsItemVM CreateFromFile(string path)
		{
			CreditsItemVM result = null;
			try
			{
				if (File.Exists(path))
				{
					XmlDocument xmlDocument = new XmlDocument();
					using (XmlReader xmlReader = XmlReader.Create(path, new XmlReaderSettings
					{
						IgnoreComments = true
					}))
					{
						xmlDocument.Load(xmlReader);
					}
					XmlNode xmlNode = null;
					for (int i = 0; i < xmlDocument.ChildNodes.Count; i++)
					{
						XmlNode xmlNode2 = xmlDocument.ChildNodes.Item(i);
						if (xmlNode2.NodeType == XmlNodeType.Element && xmlNode2.Name == "Credits")
						{
							xmlNode = xmlNode2;
							break;
						}
					}
					if (xmlNode != null)
					{
						result = CreditsVM.CreateItem(xmlNode);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Print("Could not load Credits xml from " + path + ". Exception: " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				result = null;
			}
			return result;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002543C File Offset: 0x0002363C
		public void FillFromFile(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					XmlDocument xmlDocument = new XmlDocument();
					using (XmlReader xmlReader = XmlReader.Create(path, new XmlReaderSettings
					{
						IgnoreComments = true
					}))
					{
						xmlDocument.Load(xmlReader);
					}
					XmlNode xmlNode = null;
					for (int i = 0; i < xmlDocument.ChildNodes.Count; i++)
					{
						XmlNode xmlNode2 = xmlDocument.ChildNodes.Item(i);
						if (xmlNode2.NodeType == XmlNodeType.Element && xmlNode2.Name == "Credits")
						{
							xmlNode = xmlNode2;
							break;
						}
					}
					if (xmlNode != null)
					{
						CreditsItemVM rootItem = CreditsVM.CreateItem(xmlNode);
						this._rootItem = rootItem;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Print("Could not load Credits xml. Exception: " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00025528 File Offset: 0x00023728
		private static CreditsItemVM CreateItem(XmlNode node)
		{
			CreditsItemVM creditsItemVM = null;
			if (node.Name.ToLower() == "LoadFromFile".ToLower())
			{
				string value = node.Attributes["Name"].Value;
				string str = "";
				if (node.Attributes["PlatformSpecific"] != null && node.Attributes["PlatformSpecific"].Value.ToLower() == "true")
				{
					if (ApplicationPlatform.IsPlatformConsole())
					{
						str = "Console";
					}
					else
					{
						str = "PC";
					}
				}
				if (node.Attributes["ConsoleSpecific"] != null && node.Attributes["ConsoleSpecific"].Value.ToLower() == "true")
				{
					if (ApplicationPlatform.CurrentPlatform == Platform.Durango)
					{
						str = "XBox";
					}
					else if (ApplicationPlatform.CurrentPlatform == Platform.Orbis)
					{
						str = "PlayStation";
					}
					else
					{
						str = "PC";
					}
				}
				creditsItemVM = CreditsVM.CreateFromFile(ModuleHelper.GetModuleFullPath("Native") + "ModuleData/" + value + str + ".xml");
			}
			else
			{
				creditsItemVM = new CreditsItemVM();
				creditsItemVM.Type = node.Name;
				if (node.Attributes["Text"] != null)
				{
					creditsItemVM.Text = new TextObject(node.Attributes["Text"].Value, null).ToString();
				}
				else
				{
					creditsItemVM.Text = "";
				}
				foreach (object obj in node.ChildNodes)
				{
					CreditsItemVM item = CreditsVM.CreateItem((XmlNode)obj);
					creditsItemVM.Items.Add(item);
				}
			}
			return creditsItemVM;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000256FC File Offset: 0x000238FC
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.ExitKey.OnFinalize();
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0002570F File Offset: 0x0002390F
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00025717 File Offset: 0x00023917
		[DataSourceProperty]
		public CreditsItemVM RootItem
		{
			get
			{
				return this._rootItem;
			}
			set
			{
				if (value != this._rootItem)
				{
					this._rootItem = value;
					base.OnPropertyChangedWithValue<CreditsItemVM>(value, "RootItem");
				}
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00025735 File Offset: 0x00023935
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0002573D File Offset: 0x0002393D
		[DataSourceProperty]
		public InputKeyItemVM ExitKey
		{
			get
			{
				return this._exitKey;
			}
			set
			{
				if (value != this._exitKey)
				{
					this._exitKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ExitKey");
				}
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0002575B File Offset: 0x0002395B
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00025763 File Offset: 0x00023963
		[DataSourceProperty]
		public string ExitText
		{
			get
			{
				return this._exitText;
			}
			set
			{
				if (value != this._exitText)
				{
					this._exitText = value;
					base.OnPropertyChangedWithValue<string>(value, "ExitText");
				}
			}
		}

		// Token: 0x04000489 RID: 1161
		public CreditsItemVM _rootItem;

		// Token: 0x0400048A RID: 1162
		private InputKeyItemVM _exitKey;

		// Token: 0x0400048B RID: 1163
		private string _exitText;
	}
}
