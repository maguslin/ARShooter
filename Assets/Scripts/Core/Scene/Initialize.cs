using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Initialize : SceneBase {

	public Text txtCurStatus;
	//public ProcessBar bar;
	int statusCnt;
	float statusStep;

	void Start()
	{
		GameManager.Create ();
		//CatSceneManager.Create ();

		//开始下载
		StartCoroutine(SystemInitialize());
		statusCnt =	Enum.GetNames(Type.GetType("LoadingState")).Length;
		statusStep = 100.0f /statusCnt ;

       // LuaManager.Instance.CallLuaFunByName("Game.initLoadUI");

    }
    public void OnGUI()
    {
        
        if(Input.GetMouseButtonDown(0))//先这么写
       {
            LoginWin.RemoveComponent();
            LuaManager.Instance.CallLuaFunByName("Game.initLoadUI");
            //    MagusSceneManager.Instance.SetNextScene(SceneID.Game);
        }
    }
    IEnumerator SystemInitialize()
	{
		Debug.Log ("Begin to Load System data");
		//if (GameSceneProxy.userNetWork)
		//{
		//    //链接服务器
		//    yield return StartCoroutine(SolarNetworkManager.Instance.InitAndConnectToServer());
		//}
		yield return StartCoroutine(ResourcesManager.Instance.LoadSystemBaseDataAysn());

	}
	//void Update()
	//{
	//	if (ResourcesManager.Instance.SystemLoadingState.state == LoadingState.DownloadingConfig) {
	//		//bar.Value =(int) statusStep;
	//		txtCurStatus.text = "正在加载...";
	//	}
	//	else if(ResourcesManager.Instance.SystemLoadingState.state == LoadingState.DownloadingFile) {
	//		//bar.Value =(int) statusStep*(int)LoadingState.DownloadingFile;
	//		txtCurStatus.text = "下载文件" + ResourcesManager.Instance.SystemLoadingState.filename;
	//	}
	//	else if(ResourcesManager.Instance.SystemLoadingState.state == LoadingState.DownloadingVersionData) {
	//		//bar.Value =(int) statusStep*(int)LoadingState.DownloadingVersionData;
	//		txtCurStatus.text = "下载版本文件";
	//	}
	//	else if(ResourcesManager.Instance.SystemLoadingState.state == LoadingState.InitializingResource) {
	//		//bar.Value =(int) statusStep*(int)LoadingState.InitializingResource;
	//		txtCurStatus.text = "初始化配置";
	//		//TODO:目前没有链接网络，暂时在这里切换场景
	//		MagusSceneManager.Instance.SetNextScene(SceneID.Game);
	//	}
	//	//链接网络
	//}
}
