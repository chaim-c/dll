using System;
using System.Collections.Generic;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200002D RID: 45
	public class SoundProperties
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000E4FA File Offset: 0x0000C6FA
		public IEnumerable<KeyValuePair<string, AudioProperty>> RegisteredStateSounds
		{
			get
			{
				foreach (KeyValuePair<string, AudioProperty> keyValuePair in this._stateSounds)
				{
					yield return keyValuePair;
				}
				Dictionary<string, AudioProperty>.Enumerator enumerator = default(Dictionary<string, AudioProperty>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000E50A File Offset: 0x0000C70A
		public IEnumerable<KeyValuePair<string, AudioProperty>> RegisteredEventSounds
		{
			get
			{
				foreach (KeyValuePair<string, AudioProperty> keyValuePair in this._eventSounds)
				{
					yield return keyValuePair;
				}
				Dictionary<string, AudioProperty>.Enumerator enumerator = default(Dictionary<string, AudioProperty>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E51A File Offset: 0x0000C71A
		public SoundProperties()
		{
			this._stateSounds = new Dictionary<string, AudioProperty>();
			this._eventSounds = new Dictionary<string, AudioProperty>();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000E538 File Offset: 0x0000C738
		public void AddStateSound(string state, AudioProperty audioProperty)
		{
			this._stateSounds.Add(state, audioProperty);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E547 File Offset: 0x0000C747
		public void AddEventSound(string state, AudioProperty audioProperty)
		{
			if (this._eventSounds.ContainsKey(state))
			{
				this._eventSounds[state] = audioProperty;
				return;
			}
			this._eventSounds.Add(state, audioProperty);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000E574 File Offset: 0x0000C774
		public void FillFrom(SoundProperties soundProperties)
		{
			this._stateSounds = new Dictionary<string, AudioProperty>();
			this._eventSounds = new Dictionary<string, AudioProperty>();
			foreach (KeyValuePair<string, AudioProperty> keyValuePair in soundProperties._stateSounds)
			{
				string key = keyValuePair.Key;
				AudioProperty value = keyValuePair.Value;
				AudioProperty audioProperty = new AudioProperty();
				audioProperty.FillFrom(value);
				this._stateSounds.Add(key, audioProperty);
			}
			foreach (KeyValuePair<string, AudioProperty> keyValuePair2 in soundProperties._eventSounds)
			{
				string key2 = keyValuePair2.Key;
				AudioProperty value2 = keyValuePair2.Value;
				AudioProperty audioProperty2 = new AudioProperty();
				audioProperty2.FillFrom(value2);
				this._eventSounds.Add(key2, audioProperty2);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000E670 File Offset: 0x0000C870
		public AudioProperty GetEventAudioProperty(string eventName)
		{
			if (this._eventSounds.ContainsKey(eventName))
			{
				return this._eventSounds[eventName];
			}
			return null;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000E68E File Offset: 0x0000C88E
		public AudioProperty GetStateAudioProperty(string stateName)
		{
			if (this._stateSounds.ContainsKey(stateName))
			{
				return this._stateSounds[stateName];
			}
			return null;
		}

		// Token: 0x0400018A RID: 394
		private Dictionary<string, AudioProperty> _stateSounds;

		// Token: 0x0400018B RID: 395
		private Dictionary<string, AudioProperty> _eventSounds;
	}
}
