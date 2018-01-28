using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;


//Use in BuildManager, the status return when CheckBuildPoint() is called
public enum _TileStatus
{
    NoPlatform, 	//no platform at detected
    Available, 		//there's a valid build point
    Unavailable, 	//the build point is invalid (occupied)
    Blocked			//building on the spot will block the only available path
}

//Use in BuildManager, contain all the infomation of the specific select build spot
[System.Serializable]
public class BuildInfo
{
    public Vector3 position = Vector3.zero;		//the position of the build point
   
    //the prefabIDs of the towers available to be build
    public List<int> availableTowerIDList = new List<int>();
}

public class SceneBuildManager : UnitySingleton<SceneBuildManager>
{
    

    private Transform rangeIndicator;
    private Transform rangeIndicatorCone;
    private GameObject rangeIndicatorObj;
    private GameObject rangeIndicatorConeObj;

    public bool LeaveHand = false;
    private int curPathModelIdx = 0;

    private GameObject _BuildIndicator = null;

    public SceneBuildManager()
    {

    }
    public override void Start()
    {

        //_BuildIndicator = ResourcesManager.Instance.LoadGameObject("Prefabs/Scene/GridViewer");
        //_BuildIndicator.SetActive(false);
        //GameManager.Instance.SetGameState(GameState.HomeBuilding);

        //rangeIndicatorObj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Scene/RangeIndicator"));
        //rangeIndicator = rangeIndicatorObj.transform;
        //rangeIndicatorConeObj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Scene/RangeIndicatorCone"));
        //rangeIndicatorCone = rangeIndicatorConeObj.transform;

        //rangeIndicatorObj.SetActive(false);
        //rangeIndicatorConeObj.SetActive(false);

    }




}