using CommandLine;
using ET;
using ET.Client;
using QFramework;
using QFramework.Example;
using System;
using UnityEngine;


[Event(SceneType.Test)]
public class AppStartInitFinish_StartGame : AEvent<Scene, AppStartInitFinish>
{
    protected async override ETTask Run(Scene scene, AppStartInitFinish a)
    {
        //Qframework
        UIKit.OpenPanel<UITestPanel>();
        await ETTask.CompletedTask;
    }
}

public class GameInit:MonoBehaviour
{

    private async void Awake()
    {
        await StartAsync().GetAwaiter();
    }

    private async ETTask StartAsync()
    {
        DontDestroyOnLoad(gameObject);

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            ET.Log.Error(e.ExceptionObject.ToString());
        };

        // 命令行参数
        string[] args = "".Split(" ");
        Parser.Default.ParseArguments<Options>(args)
            .WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
            .WithParsed((o) => World.Instance.AddSingleton(o));
        Options.Instance.StartConfig = $"StartConfig/Localhost";

        World.Instance.AddSingleton<ET.Logger>().Log = new UnityLogger();
        ETTask.ExceptionHandler += ET.Log.Error;

        World.Instance.AddSingleton<TimeInfo>();
        World.Instance.AddSingleton<FiberManager>();

        await World.Instance.AddSingleton<ResourcesComponent>().CreatePackageAsync("DefaultPackage", true);

        CodeLoader codeLoader = World.Instance.AddSingleton<CodeLoader>();
        await codeLoader.DownloadAsync();

        codeLoader.Start();

    }

    private void Update()
    {
        TimeInfo.Instance.Update();
        FiberManager.Instance.Update();
    }

    private void LateUpdate()
    {
        FiberManager.Instance.LateUpdate();
    }

    private void OnApplicationQuit()
    {
        World.Instance.Dispose();
    }
}

