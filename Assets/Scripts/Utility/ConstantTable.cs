﻿using UnityEngine;
using System.Collections;

public partial class ConstantTable {

	public static readonly string serverIp = "";//catnap.top//"106.187.100.61";//"106.187.100.61";// 
	public static readonly int port = 8000;
	public static readonly string FILEPATH = Application.persistentDataPath+"\\ARShooter\\";
	public static readonly string DataAssetBoundleFile = "Data.assetbundle";
	public static readonly string ResourcesPath =  Application.dataPath+"/Resources/";
	public static readonly string UserDataPath = ResourcesManager.Instance.WriteablePath + "/playerInfo.txt";
	// Use this for initialization

}
public enum MsgType
{
	SceneBeginLoad,//场景准备加载
	SceneLoaded,//场景加载完毕
	SceneAssetsLoaded,//场景资源加载完毕
	SceneUnLoaded,//场景卸载完毕

	ApplicationPaused,//程序中断了
	ApplicationActived,//程序激活了

	SceneUnitDead,//场景内对象死亡
	SceneWillLoad,//场景将要加载
	ReturnMainScene,//回到主场景

}