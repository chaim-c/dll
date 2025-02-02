using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.AuxiliaryKeys
{
	// Token: 0x0200006B RID: 107
	public class AuxiliaryKeyOptionVM : KeyOptionVM
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x000207B3 File Offset: 0x0001E9B3
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x000207BB File Offset: 0x0001E9BB
		public HotKey CurrentHotKey { get; private set; }

		// Token: 0x06000877 RID: 2167 RVA: 0x000207C4 File Offset: 0x0001E9C4
		public AuxiliaryKeyOptionVM(HotKey hotKey, Action<KeyOptionVM> onKeybindRequest, Action<AuxiliaryKeyOptionVM, InputKey> onKeySet) : base(hotKey.GroupId, hotKey.Id, onKeybindRequest)
		{
			this._onKeySet = onKeySet;
			this.CurrentHotKey = hotKey;
			Key key;
			if (!Input.IsGamepadActive)
			{
				key = this.CurrentHotKey.Keys.FirstOrDefault((Key x) => !x.IsControllerInput);
			}
			else
			{
				key = this.CurrentHotKey.Keys.FirstOrDefault((Key x) => x.IsControllerInput);
			}
			base.Key = key;
			if (base.Key == null)
			{
				base.Key = new Key(InputKey.Invalid);
			}
			base.CurrentKey = new Key(base.Key.InputKey);
			this.RefreshValues();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00020898 File Offset: 0x0001EA98
		public override void RefreshValues()
		{
			base.RefreshValues();
			string name = this.CurrentHotKey.Id;
			TextObject textObject;
			if (Module.CurrentModule.GlobalTextManager.TryGetText("str_hotkey_name", this._groupId + "_" + this._id, out textObject))
			{
				name = textObject.ToString();
			}
			base.Name = name;
			string variable = "";
			TextObject textObject2;
			if (Module.CurrentModule.GlobalTextManager.TryGetText("str_hotkey_description", this._groupId + "_" + this._id, out textObject2))
			{
				variable = textObject2.ToString();
			}
			GameTextManager globalTextManager = Module.CurrentModule.GlobalTextManager;
			base.OptionValueText = globalTextManager.FindText("str_game_key_text", base.CurrentKey.ToString().ToLower()).ToString();
			string text = base.OptionValueText;
			foreach (HotKey.Modifiers modifier in new List<HotKey.Modifiers>
			{
				HotKey.Modifiers.Alt,
				HotKey.Modifiers.Shift,
				HotKey.Modifiers.Control
			})
			{
				if (this.CurrentHotKey.HasModifier(modifier))
				{
					MBTextManager.SetTextVariable("KEY", text, false);
					MBTextManager.SetTextVariable("MODIFIER", globalTextManager.FindText("str_game_key_text", "any" + modifier.ToString().ToLower()).ToString(), false);
					text = globalTextManager.FindText("str_hot_key_with_modifier", null).ToString();
				}
			}
			TextObject textObject3 = new TextObject("{=ol0rBSrb}{STR1}{newline}{STR2}", null);
			textObject3.SetTextVariable("STR1", text);
			textObject3.SetTextVariable("STR2", variable);
			textObject3.SetTextVariable("newline", "\n \n");
			base.Description = textObject3.ToString();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00020A70 File Offset: 0x0001EC70
		private void ExecuteKeybindRequest()
		{
			this._onKeybindRequest(this);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00020A7E File Offset: 0x0001EC7E
		public override void Set(InputKey newKey)
		{
			this._onKeySet(this, newKey);
			this.RefreshValues();
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00020A94 File Offset: 0x0001EC94
		public override void Update()
		{
			Key key;
			if (!Input.IsGamepadActive)
			{
				key = this.CurrentHotKey.Keys.FirstOrDefault((Key x) => !x.IsControllerInput);
			}
			else
			{
				key = this.CurrentHotKey.Keys.FirstOrDefault((Key x) => x.IsControllerInput);
			}
			base.Key = key;
			if (base.Key == null)
			{
				base.Key = new Key(InputKey.Invalid);
			}
			base.CurrentKey = new Key(base.Key.InputKey);
			this.RefreshValues();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00020B44 File Offset: 0x0001ED44
		public override void OnDone()
		{
			base.Key.ChangeKey(base.CurrentKey.InputKey);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00020B5C File Offset: 0x0001ED5C
		internal override bool IsChanged()
		{
			return base.CurrentKey != base.Key;
		}

		// Token: 0x040003EE RID: 1006
		private readonly Action<AuxiliaryKeyOptionVM, InputKey> _onKeySet;
	}
}
