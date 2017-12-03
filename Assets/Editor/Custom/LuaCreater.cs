using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class LuaCreater  {

	[MenuItem("Assets/Create/Lua Script", false, 80)]
	public static void CreateNewLua()
	{
		ProjectWindowUtil.StartNameEditingIfProjectWindowExists (0,
			ScriptableObject.CreateInstance<MyDoCreateScriptAsset> (),
			GetSelectedPathOrFallback () + "/New Lua.lua",
			null,
			"Assets/Editor/Template/lua.lua");
	}
	public static string GetSelectedPathOrFallback()
	{
		string path = "Assets";
		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets)) 
		{
			path = AssetDatabase.GetAssetPath (obj);
			if (!String.IsNullOrEmpty (path) && File.Exists (path))
			{
				path = Path.GetDirectoryName (path);
				break;
			}
		}
		return path;
	}
}
class MyDoCreateScriptAsset : EndNameEditAction
{
	public override void Action (int instanceId, string pathName, string resourceFile)
	{
		UnityEngine.Object o = CreateScriptAssetFromTemplate (pathName, resourceFile);
		ProjectWindowUtil.ShowCreatedAsset (o);
	}
	internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
	{
			string fullpath = Path.GetFullPath (pathName);
			StreamReader streamReader = new StreamReader (resourceFile);
			string text = streamReader.ReadToEnd ();
			streamReader.Close ();
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension (pathName);
			text = Regex.Replace (text, "#NAME#", fileNameWithoutExtension);
			DateTime dt = DateTime.Now;
			text = Regex.Replace(text, "#TIME#", dt.ToString ());
			text = Regex.Replace(text, "#WRITER#", "Magus");
			bool encoderShouldEmitUTF8Identifer = true;
			bool throwOnInvalidBytes = false;
			UTF8Encoding encoding = new UTF8Encoding (encoderShouldEmitUTF8Identifer, throwOnInvalidBytes);
			bool append = false;
			StreamWriter streamWriter = new StreamWriter (fullpath, append, encoding);	
			streamWriter.Write (text);
			streamWriter.Close ();
			AssetDatabase.ImportAsset (pathName);
			return AssetDatabase.LoadAssetAtPath(pathName,typeof(UnityEngine.Object)); 

	}

}