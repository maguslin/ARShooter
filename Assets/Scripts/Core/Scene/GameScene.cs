using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameScene : SceneBase
{
	[SerializeField]
	public Camera effCamera;
	static public GameScene Instance = null;
	private Transform thisT;
	public static float myHeight =0;
	public static float cameraHeight = 60;
	public bool PermissionTango = false;
	public GameObject playerBody;
	void Awake()
	{
		Instance = this;
		thisT = transform;
		Time.timeScale = 1;
		Application.targetFrameRate = 45;
		#if UNITY_EDITOR
		if(!MagusSceneManager.Created)
		{
            MagusSceneManager.Create ();
			GameManager.Create ();
		}
		#endif
		GameManager.Instance.SetGameState(GameState.GamePlay);
		ResourcesManager.Instance.LoadSystemBaseData();
		StartCoroutine(LoadScene());
	}



	IEnumerator StartScene()
	{

		yield return null;//StartCoroutine();
	}
	
	IEnumerator LoadScene()
	{
		if(!GameManager.Instance.resetGame)
		{
			//等待服务器数据到位
			yield return StartCoroutine(GetGameUserData());
		}
		else
		{
			//reset game!!
		}
        //		GamePlayer.Me.Create ();
        //		GamePlayer.Me.instance.SetPlayer (playerBody.transform);
        //加载基础场景


        //OnSceneLoaded();
        //UIManager.Instance.Open (UIID.CatHandbook);
        //更新下载状态
        MagusSceneManager.Instance.UpdateLoadingState(100, 100);
	}
		
	public void OnSceneLoaded()
	{
		//加载基础怪物群，创建枪支
//		SceneCatLittle catLitter = MapSceneManager.Instance.CreateSceneCatLittle(102,Vector3.zero, Quaternion.identity);
//		SceneCatLittle spade = MapSceneManager.Instance.CreateSceneCatLittle(103,Vector3.zero, Quaternion.identity, false);
//		spade.target = catLitter;
//		GamePlayer.Me.instance.HoldTool (spade.thisT);
//		SceneCat cat = MapSceneManager.Instance.CreateSceneCat(101,Vector3.zero, Quaternion.identity);
		//怪物启动
//		cat.StartWorkRoutine ();
		//请求登录
	//网络启动
	}
	void OnLoginResponse(uint id, object obj, object localArg)
	{
		Debug.Log ("id" + id);
		if (obj == null) {
			Debug.LogError ("no response data!!!");
			return;
		}
//		LBSJPTest lbs = obj as LBSJPTest;
//		Debug.Log ("id" + lbs.data.uid);
//		Debug.Log ("name" + lbs.data.uname);
//		GamePlayer.Me.instance.id = int.Parse( lbs.data.uid);
//		GamePlayer.Me.instance.name =  lbs.data.uname;
//		//登录成功后开启gps
//		TestGPS.Instance.StartGPS ();
	}
	void OnWebError(uint id, string msg)
	{
		Debug.Log(msg);
	}

	void OnGUI()
	{
		
	}

	public override void Unload ()
	{

		//TangoService.Instance.OnDispose();
		MapSceneManager.Instance.CleanAll ();
		GameManager.Instance.EndGame();
		//UIManager.Instance.UnloadAllUI ();
		base.Unload ();
	}
	IEnumerator GetGameUserData()
	{
		//模拟
		yield return new WaitForSeconds(0.2f);

//		_GameUserData = ResourcesManager.Instance.GetFileData(ConstantTable.TempUserGameContentFile);
//		if (_GameUserData == null)
//		{
//			//_GameUserData = ResourcesManager.Instance.GetFileData(ConstTable.DefaultUserContentFile);
//			_GameUserData = ResourcesManager.Instance.GetFileData(ConstantTable.DefaultUserContentFile);
//		}

		yield return null;
	}
	IEnumerator Start()
	{

		yield return new WaitForSeconds (2.5f);
	}
	void Update()
	{
			GameManager.Instance.StartGame ();

	}

}

