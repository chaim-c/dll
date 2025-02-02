using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x0200008D RID: 141
	public class TestContext
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x00010388 File Offset: 0x0000E588
		public void RunTestAux(string commandLine)
		{
			TestCommonBase.BaseInstance.IsTestEnabled = true;
			Debug.SetTestModeEnabled(true);
			string[] array = commandLine.Split(new char[]
			{
				' '
			});
			if (array.Length < 2)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "/runTest")
				{
					num = i + 1;
				}
			}
			if (num >= array.Length || num == -1)
			{
				return;
			}
			string text = array[num];
			if (text == "OpenSceneOnStartup")
			{
				TestCommonBase.BaseInstance.SceneNameToOpenOnStartup = array[2];
			}
			for (int j = 3; j < array.Length; j++)
			{
				int testRandomSeed;
				int.TryParse(array[j], out testRandomSeed);
				TestCommonBase.BaseInstance.TestRandomSeed = testRandomSeed;
			}
			Debug.Print("commandLine" + commandLine, 0, Debug.DebugColor.White, 17592186044416UL);
			Debug.Print("p" + array.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
			Debug.Print("Looking for test " + text, 0, Debug.DebugColor.Yellow, 17592186044416UL);
			ConstructorInfo asyncRunnerConstructor = this.GetAsyncRunnerConstructor(text);
			object obj = null;
			if (asyncRunnerConstructor != null)
			{
				obj = asyncRunnerConstructor.Invoke(new object[0]);
			}
			this._asyncRunner = (obj as AsyncRunner);
			this._awaitableAsyncRunner = (obj as AwaitableAsyncRunner);
			if (this._asyncRunner != null)
			{
				this._asyncThread = new Thread(delegate()
				{
					this._asyncRunner.Run();
				});
				this._asyncThread.Name = "ManagedAsyncThread";
				this._asyncThread.Start();
			}
			if (this._awaitableAsyncRunner != null)
			{
				this._asyncTask = this._awaitableAsyncRunner.RunAsync();
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00010524 File Offset: 0x0000E724
		private ConstructorInfo GetAsyncRunnerConstructor(string asyncRunner)
		{
			Assembly[] asyncRunnerAssemblies = this.GetAsyncRunnerAssemblies();
			for (int i = 0; i < asyncRunnerAssemblies.Length; i++)
			{
				foreach (Type type in asyncRunnerAssemblies[i].GetTypes())
				{
					if (type.Name == asyncRunner && (typeof(AsyncRunner).IsAssignableFrom(type) || typeof(AwaitableAsyncRunner).IsAssignableFrom(type)))
					{
						ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new Type[0], null);
						if (constructor != null)
						{
							return constructor;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000105C4 File Offset: 0x0000E7C4
		private Assembly[] GetAsyncRunnerAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(AsyncRunner).Assembly;
			foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
				for (int j = 0; j < referencedAssemblies.Length; j++)
				{
					if (referencedAssemblies[j].ToString() == assembly.GetName().ToString())
					{
						list.Add(assembly2);
						break;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00010650 File Offset: 0x0000E850
		public void OnApplicationTick(float dt)
		{
			if (this._asyncTask != null && this._asyncTask.Status == TaskStatus.Faulted)
			{
				string text = "TestRunTaskFailed\n";
				if (this._asyncTask.Exception.InnerException != null)
				{
					text += this._asyncTask.Exception.InnerException.Message;
					text += "\n";
					text += this._asyncTask.Exception.InnerException.StackTrace;
				}
				this._asyncTask = null;
				Debug.Print(text, 5, Debug.DebugColor.White, 17592186044416UL);
				Debug.FailedAssert(text, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\TestContext.cs", "OnApplicationTick", 173);
				Debug.DoDelayedexit(5);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00010709 File Offset: 0x0000E909
		public void TickTest(float dt)
		{
			if (this._asyncThread != null && this._asyncThread.IsAlive && this._asyncRunner != null)
			{
				this._asyncRunner.SyncTick();
			}
			if (this._awaitableAsyncRunner != null)
			{
				this._awaitableAsyncRunner.OnTick(dt);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00010747 File Offset: 0x0000E947
		public void FinalizeContext()
		{
			if (this._asyncThread != null)
			{
				this._asyncThread.Join();
			}
			this._asyncThread = null;
			this._asyncRunner = null;
			this._awaitableAsyncRunner = null;
			this._asyncTask = null;
		}

		// Token: 0x04000175 RID: 373
		private AsyncRunner _asyncRunner;

		// Token: 0x04000176 RID: 374
		private AwaitableAsyncRunner _awaitableAsyncRunner;

		// Token: 0x04000177 RID: 375
		private Thread _asyncThread;

		// Token: 0x04000178 RID: 376
		private Task _asyncTask;
	}
}
