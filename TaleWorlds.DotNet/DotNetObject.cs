using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200000C RID: 12
	public class DotNetObject
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028AC File Offset: 0x00000AAC
		internal static int NumberOfAliveDotNetObjects
		{
			get
			{
				return DotNetObject._numberOfAliveDotNetObjects;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028B4 File Offset: 0x00000AB4
		static DotNetObject()
		{
			DotNetObject.DotnetObjectFirstReferences = new List<Dictionary<int, DotNetObject>>();
			for (int i = 0; i < 200; i++)
			{
				DotNetObject.DotnetObjectFirstReferences.Add(new Dictionary<int, DotNetObject>());
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002900 File Offset: 0x00000B00
		protected DotNetObject()
		{
			object locker = DotNetObject.Locker;
			lock (locker)
			{
				DotNetObject._totalCreatedObjectCount++;
				this._objectId = DotNetObject._totalCreatedObjectCount;
				DotNetObject.DotnetObjectFirstReferences[0].Add(this._objectId, this);
				DotNetObject._numberOfAliveDotNetObjects++;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000297C File Offset: 0x00000B7C
		protected override void Finalize()
		{
			try
			{
				object locker = DotNetObject.Locker;
				lock (locker)
				{
					DotNetObject._numberOfAliveDotNetObjects--;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029D4 File Offset: 0x00000BD4
		[LibraryCallback]
		internal static int GetAliveDotNetObjectCount()
		{
			return DotNetObject._numberOfAliveDotNetObjects;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029DC File Offset: 0x00000BDC
		[LibraryCallback]
		internal static void IncreaseReferenceCount(int dotnetObjectId)
		{
			object locker = DotNetObject.Locker;
			lock (locker)
			{
				if (DotNetObject.DotnetObjectReferences.ContainsKey(dotnetObjectId))
				{
					DotNetObjectReferenceCounter value = DotNetObject.DotnetObjectReferences[dotnetObjectId];
					value.ReferenceCount++;
					DotNetObject.DotnetObjectReferences[dotnetObjectId] = value;
				}
				else
				{
					DotNetObject dotNetObjectFromFirstReferences = DotNetObject.GetDotNetObjectFromFirstReferences(dotnetObjectId);
					DotNetObjectReferenceCounter value2 = default(DotNetObjectReferenceCounter);
					value2.ReferenceCount = 1;
					value2.DotNetObject = dotNetObjectFromFirstReferences;
					DotNetObject.DotnetObjectReferences.Add(dotnetObjectId, value2);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A74 File Offset: 0x00000C74
		[LibraryCallback]
		internal static void DecreaseReferenceCount(int dotnetObjectId)
		{
			object locker = DotNetObject.Locker;
			lock (locker)
			{
				DotNetObjectReferenceCounter dotNetObjectReferenceCounter = DotNetObject.DotnetObjectReferences[dotnetObjectId];
				dotNetObjectReferenceCounter.ReferenceCount--;
				if (dotNetObjectReferenceCounter.ReferenceCount == 0)
				{
					DotNetObject.DotnetObjectReferences.Remove(dotnetObjectId);
				}
				else
				{
					DotNetObject.DotnetObjectReferences[dotnetObjectId] = dotNetObjectReferenceCounter;
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal static DotNetObject GetManagedObjectWithId(int dotnetObjectId)
		{
			object locker = DotNetObject.Locker;
			DotNetObject result;
			lock (locker)
			{
				DotNetObjectReferenceCounter dotNetObjectReferenceCounter;
				if (DotNetObject.DotnetObjectReferences.TryGetValue(dotnetObjectId, out dotNetObjectReferenceCounter))
				{
					result = dotNetObjectReferenceCounter.DotNetObject;
				}
				else
				{
					result = DotNetObject.GetDotNetObjectFromFirstReferences(dotnetObjectId);
				}
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B44 File Offset: 0x00000D44
		private static DotNetObject GetDotNetObjectFromFirstReferences(int dotnetObjectId)
		{
			using (List<Dictionary<int, DotNetObject>>.Enumerator enumerator = DotNetObject.DotnetObjectFirstReferences.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DotNetObject result;
					if (enumerator.Current.TryGetValue(dotnetObjectId, out result))
					{
						return result;
					}
				}
			}
			return null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002BA0 File Offset: 0x00000DA0
		internal int GetManagedId()
		{
			return this._objectId;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BA8 File Offset: 0x00000DA8
		[LibraryCallback]
		internal static string GetAliveDotNetObjectNames()
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "GetAliveDotNetObjectNames");
			object locker = DotNetObject.Locker;
			lock (locker)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (DotNetObjectReferenceCounter dotNetObjectReferenceCounter in DotNetObject.DotnetObjectReferences.Values)
				{
					Type type = dotNetObjectReferenceCounter.DotNetObject.GetType();
					if (!dictionary.ContainsKey(type.Name))
					{
						dictionary.Add(type.Name, 1);
					}
					else
					{
						dictionary[type.Name] = dictionary[type.Name] + 1;
					}
				}
				foreach (string text in dictionary.Keys)
				{
					mbstringBuilder.Append<string>(string.Concat(new object[]
					{
						text,
						",",
						dictionary[text],
						"-"
					}));
				}
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D00 File Offset: 0x00000F00
		internal static void HandleDotNetObjects()
		{
			object locker = DotNetObject.Locker;
			lock (locker)
			{
				Dictionary<int, DotNetObject> dictionary = DotNetObject.DotnetObjectFirstReferences[199];
				for (int i = 199; i > 0; i--)
				{
					DotNetObject.DotnetObjectFirstReferences[i] = DotNetObject.DotnetObjectFirstReferences[i - 1];
				}
				dictionary.Clear();
				DotNetObject.DotnetObjectFirstReferences[0] = dictionary;
			}
		}

		// Token: 0x0400001E RID: 30
		private static readonly object Locker = new object();

		// Token: 0x0400001F RID: 31
		private const int DotnetObjectFirstReferencesTickCount = 200;

		// Token: 0x04000020 RID: 32
		private static readonly List<Dictionary<int, DotNetObject>> DotnetObjectFirstReferences;

		// Token: 0x04000021 RID: 33
		private static readonly Dictionary<int, DotNetObjectReferenceCounter> DotnetObjectReferences = new Dictionary<int, DotNetObjectReferenceCounter>();

		// Token: 0x04000022 RID: 34
		private static int _totalCreatedObjectCount;

		// Token: 0x04000023 RID: 35
		private readonly int _objectId;

		// Token: 0x04000024 RID: 36
		private static int _numberOfAliveDotNetObjects;
	}
}
