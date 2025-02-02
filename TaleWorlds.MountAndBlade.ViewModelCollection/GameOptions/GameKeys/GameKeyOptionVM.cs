using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions.GameKeys
{
	// Token: 0x02000069 RID: 105
	public class GameKeyOptionVM : KeyOptionVM
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0002023C File Offset: 0x0001E43C
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00020244 File Offset: 0x0001E444
		public GameKey CurrentGameKey { get; private set; }

		// Token: 0x06000860 RID: 2144 RVA: 0x00020250 File Offset: 0x0001E450
		public GameKeyOptionVM(GameKey gameKey, Action<KeyOptionVM> onKeybindRequest, Action<GameKeyOptionVM, InputKey> onKeySet) : base(gameKey.GroupId, ((GameKeyDefinition)gameKey.Id).ToString(), onKeybindRequest)
		{
			this._onKeySet = onKeySet;
			this.CurrentGameKey = gameKey;
			base.Key = (Input.IsGamepadActive ? this.CurrentGameKey.ControllerKey : this.CurrentGameKey.KeyboardKey);
			if (base.Key == null)
			{
				base.Key = new Key(InputKey.Invalid);
			}
			base.CurrentKey = new Key(base.Key.InputKey);
			this._initalKey = base.Key.InputKey;
			this.RefreshValues();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000202F8 File Offset: 0x0001E4F8
		public override void RefreshValues()
		{
			base.RefreshValues();
			base.Name = Module.CurrentModule.GlobalTextManager.FindText("str_key_name", this._groupId + "_" + this._id).ToString();
			base.Description = Module.CurrentModule.GlobalTextManager.FindText("str_key_description", this._groupId + "_" + this._id).ToString();
			base.OptionValueText = Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", base.CurrentKey.ToString().ToLower()).ToString();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000203A4 File Offset: 0x0001E5A4
		private void ExecuteKeybindRequest()
		{
			this._onKeybindRequest(this);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000203B2 File Offset: 0x0001E5B2
		public override void Set(InputKey newKey)
		{
			this._onKeySet(this, newKey);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000203C4 File Offset: 0x0001E5C4
		public override void Update()
		{
			base.Key = (Input.IsGamepadActive ? this.CurrentGameKey.ControllerKey : this.CurrentGameKey.KeyboardKey);
			if (base.Key == null)
			{
				base.Key = new Key(InputKey.Invalid);
			}
			base.CurrentKey = new Key(base.Key.InputKey);
			base.OptionValueText = Module.CurrentModule.GlobalTextManager.FindText("str_game_key_text", base.CurrentKey.ToString().ToLower()).ToString();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00020455 File Offset: 0x0001E655
		public override void OnDone()
		{
			Key key = base.Key;
			if (key != null)
			{
				key.ChangeKey(base.CurrentKey.InputKey);
			}
			this._initalKey = base.CurrentKey.InputKey;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00020484 File Offset: 0x0001E684
		internal override bool IsChanged()
		{
			return base.CurrentKey.InputKey != this._initalKey;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002049C File Offset: 0x0001E69C
		public void Revert()
		{
			this.Set(this._initalKey);
			this.Update();
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000204B0 File Offset: 0x0001E6B0
		public void Apply()
		{
			this.OnDone();
			base.CurrentKey = base.Key;
		}

		// Token: 0x040003E5 RID: 997
		private InputKey _initalKey;

		// Token: 0x040003E7 RID: 999
		private readonly Action<GameKeyOptionVM, InputKey> _onKeySet;
	}
}
