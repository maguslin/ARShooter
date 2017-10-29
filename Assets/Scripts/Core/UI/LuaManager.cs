using UnityEngine;
using System.Collections;
using LuaInterface;
//using HotUpdate;
using System;
public class LuaManager : UnityAllSceneSingletonVisible<LuaManager>
{
    public LuaState luaState;
    private LuaLooper loop = null;
//    public AssetBundleManager manager;
    // Use this for initialization
    public override void Awake()
    {
        Initialize();
        StartLooper();
        StartManager();
    }

    void Initialize()
    {

        luaState = new LuaState();
        new LuaResLoader();
#if UNITY_EDITOR
        luaState.AddSearchPath(LuaConst.luaDir);
#endif
#if AB_MODE
        luaState.AddSearchPath(AssetBundleUtility.LocalAssetBundlePath);
        luaState.AddSearchPath(AssetBundleUtility.GetStreamingPath());
#endif
        luaState.OpenLibs(LuaDLL.luaopen_pb);
        //if (LuaConst.openLuaSocket)
        //{
        //    OpenLuaSocket();
        //}

        //if (LuaConst.openZbsDebugger)
        //{
        //    OpenZbsDebugger();
        //}
        luaState.LuaSetTop(0);
        LuaBinder.Bind(luaState);
        luaState.Start();
        LuaCoroutine.Register(luaState, this);
    }

    protected void OpenLuaSocket()
    {
        LuaConst.openLuaSocket = true;

        luaState.BeginPreLoad();
        luaState.RegFunction("socket.core", LuaOpen_Socket_Core);
        luaState.RegFunction("mime.core", LuaOpen_Mime_Core);
        luaState.EndPreLoad();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Socket_Core(IntPtr L)
    {
        return LuaDLL.luaopen_socket_core(L);
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Mime_Core(IntPtr L)
    {
        return LuaDLL.luaopen_mime_core(L);
    }

    public void OpenZbsDebugger(string ip = "localhost")
    {
        if (!LuaConst.openLuaSocket)
        {
            OpenLuaSocket();
        }

        if (!string.IsNullOrEmpty(LuaConst.zbsDir))
        {
            luaState.AddSearchPath(LuaConst.zbsDir);
        }

        luaState.LuaDoString(string.Format("DebugServerIp = '{0}'", ip));
    }

    protected void StartLooper()
    {
        loop = gameObject.AddComponent<LuaLooper>();
        loop.luaState = luaState;
    }

    protected void StartManager()
    {
//        manager = gameObject.AddComponent<AssetBundleManager>();
//        manager.Init(() =>
//        {
//            Debug.Log("AssetBundleManager 初始化完成 ！");
//        });
    }
    public object[] CallLuaFunByName(string strLuaPath, string funcName, params object[] args)
    {
        if (!_GameLuaIsLoaded)
        {
            luaState.DoFile("src/Game");
            _GameLuaIsLoaded = true;
        }
        if ("src/Game" != strLuaPath)
        {
            luaState.Require(strLuaPath);
        }
        LuaFunction func = luaState.GetFunction(funcName);
        return func.Call(args);
    }
    private bool _GameLuaIsLoaded;
    public object[] CallLuaFunByName(string funcName, params object[] args)
    {
        if (!_GameLuaIsLoaded)
        {
            luaState.DoFile("src/Game");
            _GameLuaIsLoaded = true;
        }
        LuaFunction func = luaState.GetFunction(funcName);
        return func.Call(args);
    }

    public override void OnDestroy()
    {
        if(luaState != null)
        {
            luaState.Dispose();
        }
    }

    public void Reset()
    {
        _GameLuaIsLoaded = false;
        if (luaState != null)
        {
            luaState.Dispose();
        }
        if (loop)
        {
            Destroy(loop);
            loop = null;
        }
//        if(manager)
//        {
//            Destroy(manager);
//            manager = null;
//        }

        Initialize();
        StartLooper();
        StartManager();
    }
}
