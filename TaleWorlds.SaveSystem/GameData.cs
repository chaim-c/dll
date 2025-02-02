using System;
using System.IO;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class GameData
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026DF File Offset: 0x000008DF
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026E7 File Offset: 0x000008E7
		public byte[] Header { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026F0 File Offset: 0x000008F0
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000026F8 File Offset: 0x000008F8
		public byte[] Strings { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002701 File Offset: 0x00000901
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002709 File Offset: 0x00000909
		public byte[][] ObjectData { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002712 File Offset: 0x00000912
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000271A File Offset: 0x0000091A
		public byte[][] ContainerData { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002724 File Offset: 0x00000924
		public int TotalSize
		{
			get
			{
				int num = this.Header.Length;
				num += this.Strings.Length;
				for (int i = 0; i < this.ObjectData.Length; i++)
				{
					num += this.ObjectData[i].Length;
				}
				for (int j = 0; j < this.ContainerData.Length; j++)
				{
					num += this.ContainerData[j].Length;
				}
				return num;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002786 File Offset: 0x00000986
		public GameData(byte[] header, byte[] strings, byte[][] objectData, byte[][] containerData)
		{
			this.Header = header;
			this.Strings = strings;
			this.ObjectData = objectData;
			this.ContainerData = containerData;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027AC File Offset: 0x000009AC
		public void Inspect()
		{
			Debug.Print(string.Format("Header Size: {0} Strings Size: {1} Object Size: {2} Container Size: {3}", new object[]
			{
				this.Header.Length,
				this.Strings.Length,
				this.ObjectData.Length,
				this.ContainerData.Length
			}), 0, Debug.DebugColor.White, 17592186044416UL);
			float num = (float)this.TotalSize / 1048576f;
			Debug.Print(string.Format("Total size: {0:##.00} MB", num), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002849 File Offset: 0x00000A49
		public static GameData CreateFrom(byte[] readBytes)
		{
			return (GameData)Common.DeserializeObject(readBytes);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002856 File Offset: 0x00000A56
		public byte[] GetData()
		{
			return Common.SerializeObject(this);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002860 File Offset: 0x00000A60
		public static void Write(BinaryWriter writer, GameData gameData)
		{
			writer.Write(gameData.Header.Length);
			writer.Write(gameData.Header);
			writer.Write(gameData.ObjectData.Length);
			foreach (byte[] array2 in gameData.ObjectData)
			{
				writer.Write(array2.Length);
				writer.Write(array2);
			}
			writer.Write(gameData.ContainerData.Length);
			foreach (byte[] array3 in gameData.ContainerData)
			{
				writer.Write(array3.Length);
				writer.Write(array3);
			}
			writer.Write(gameData.Strings.Length);
			writer.Write(gameData.Strings);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002910 File Offset: 0x00000B10
		public static GameData Read(BinaryReader reader)
		{
			int count = reader.ReadInt32();
			byte[] header = reader.ReadBytes(count);
			int num = reader.ReadInt32();
			byte[][] array = new byte[num][];
			for (int i = 0; i < num; i++)
			{
				int count2 = reader.ReadInt32();
				array[i] = reader.ReadBytes(count2);
			}
			int num2 = reader.ReadInt32();
			byte[][] array2 = new byte[num2][];
			for (int j = 0; j < num2; j++)
			{
				int count3 = reader.ReadInt32();
				array2[j] = reader.ReadBytes(count3);
			}
			int count4 = reader.ReadInt32();
			byte[] strings = reader.ReadBytes(count4);
			return new GameData(header, strings, array, array2);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029B4 File Offset: 0x00000BB4
		public bool IsEqualTo(GameData gameData)
		{
			bool flag = this.CompareByteArrays(this.Header, gameData.Header, "Header");
			bool flag2 = this.CompareByteArrays(this.Strings, gameData.Strings, "Strings");
			bool flag3 = this.CompareByteArrays(this.ObjectData, gameData.ObjectData, "ObjectData");
			bool flag4 = this.CompareByteArrays(this.ContainerData, gameData.ContainerData, "ContainerData");
			return flag && flag2 && flag3 && flag4;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A28 File Offset: 0x00000C28
		private bool CompareByteArrays(byte[] arr1, byte[] arr2, string name)
		{
			if (arr1.Length != arr2.Length)
			{
				Debug.FailedAssert(name + " failed length comparison.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\GameData.cs", "CompareByteArrays", 142);
				return false;
			}
			for (int i = 0; i < arr1.Length; i++)
			{
				if (arr1[i] != arr2[i])
				{
					Debug.FailedAssert(string.Format("{0} failed byte comparison at index {1}.", name, i), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\GameData.cs", "CompareByteArrays", 150);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A9C File Offset: 0x00000C9C
		private bool CompareByteArrays(byte[][] arr1, byte[][] arr2, string name)
		{
			if (arr1.Length != arr2.Length)
			{
				Debug.FailedAssert(name + " failed length comparison.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\GameData.cs", "CompareByteArrays", 161);
				return false;
			}
			for (int i = 0; i < arr1.Length; i++)
			{
				if (arr1[i].Length != arr2[i].Length)
				{
					Debug.FailedAssert(name + " failed length comparison.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\GameData.cs", "CompareByteArrays", 168);
					return false;
				}
				for (int j = 0; j < arr1[i].Length; j++)
				{
					if (arr1[i][j] != arr2[i][j])
					{
						Debug.FailedAssert(string.Format("{0} failed byte comparison at index {1}-{2}.", name, i, j), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\GameData.cs", "CompareByteArrays", 176);
					}
				}
			}
			return true;
		}
	}
}
