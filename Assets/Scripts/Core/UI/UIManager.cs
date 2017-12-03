using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
public class UIManager : UnityAllSceneSingletonVisible<LuaManager>
{
	protected GComponent uiView;
	public void InitFairyGUI()
	{
		//重命名根节点
		GRoot.inst.displayObject.parent.gameObject.name = "FairyGUI";
		GRoot.inst.displayObject.gameObject.name = "UIROOT";
		Stage.inst.stage.GetRenderCamera ().allowMSAA = true;
		//分辨率适配
		//1280*720 1334*750 1920*1080
		GRoot.inst.SetContentScaleFactor(1334, 750, FairyGUI.UIContentScaler.ScreenMatchMode.MatchHeight);

		//基础ui
		LoginWin.InitComponent();
		//loadingui
		//messagebox
		//transitionwin
	}

	//所有的包
	public  override  void  OnInit()//此处介入quickunity
	{
		

		//这里放公共的包
//		UICfg[] cfgs = new UICfg[] {
//			new UICfg{ pkg = "ARShooter", path = "UI/ARShooter" },
//		};
//
//		ResourcesManager.Instance.LoadUI (cfgs [0].path);//其后根据各自的界面自己加载，然后生成界面
	}

}
