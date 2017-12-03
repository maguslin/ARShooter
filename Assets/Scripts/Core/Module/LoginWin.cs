using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using LuaInterface;

public class LoginWin 
{
	//ui 控件
	private Window m_loginWindow;

	private GTextField m_useid;//用户账号
	private GButton m_buttonLogin;//登录按钮
	private Controller m_Controller;//ui控制器.这个控制器用来变化，表示是否是账户密码登录界面，进度条还是sdk登录
	private GProgressBar m_progressBar;//更新进度
	//private GTextField m_progressText;//更新进度提示
	private GTextField m_serverText;//服务器显示提示
	private GButton m_chooseServerBtn;//选择服务器
	private GComponent m_chooseServerCom;
	private GTextField m_appNumText;
	private GTextField m_resNumText;
	private static LoginWin _Instance;
//	private int addressId;
//	private float m_delayConnectSecond;

//	#if UNITY_EDITOR
//	private readonly string[,] ServerTypeDic = {
//		{"测试服务器", "http://127.0.0.1:8080"}	
//	};
//	#else
//	private readonly string[,] ServerTypeDic = {
//	{"正式服务器", "http://127.0.0.1:8080"}	
//	};
//	#endif
	public static void InitComponent()
	{
		if (_Instance == null)
			_Instance = new LoginWin ();
		_Instance._InitComponent ();
	}
	private void _InitComponent()
	{
		if (m_loginWindow == null)
		{
			m_loginWindow = new Window ();
			UIPackage.AddPackage ("UI/ARShooter");
			m_loginWindow.contentPane = UIPackage.CreateObject ("ARShooter", "ARShooter").asCom;
			if (m_loginWindow.contentPane != null) {
				m_loginWindow.contentPane.height = GRoot.inst.height;
				m_loginWindow.contentPane.width = GRoot.inst.width;
				m_loginWindow.Center ();

				//userid
				//button
				//controller
				m_progressBar  = m_loginWindow.contentPane.GetChild("ProgressBar").asProgress;
				//servertext
				//choosenserverbtn 
				//appnumtext
				//resnumtext
				}
			}
		m_loginWindow.Show ();
		m_loginWindow.sortingOrder = int.MaxValue;
	}
	public static void RemoveComponent()
	{
		if (_Instance != null)
			_Instance._RemoveComponent ();
		_Instance = null;
	}
	private void _RemoveComponent()
	{
		if (m_loginWindow != null)
		{
			m_loginWindow.Hide ();
			m_loginWindow.Dispose ();
			m_loginWindow = null;

			m_Controller = null;
			m_progressBar = null;
		}
	}
}

