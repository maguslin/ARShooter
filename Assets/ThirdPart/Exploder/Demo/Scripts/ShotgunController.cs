using UnityEngine;
using System.Collections;

public class ShotgunController : MonoBehaviour
{
    public AudioClip GunShot = null;
    public AudioClip Reload = null;
    public AudioSource Source = null;
   // public ExploderMouseLook MouseLookCamera = null;
    public Light Flash = null;
    public Animation ReloadAnim;
    public AnimationClip HideAnim;
    public GameObject MuzzleFlash;
	//public GameObject VideoPlane;

	public float[] ZoomFOVLists;
	public bool Zooming;
	public int IndexZoom = 0;
	public Texture2D CrosshairImg, CrosshairZoom;
	public bool HideGunWhileZooming = true;
    private int flashing = 0;
    private float reloadTimeout = float.MaxValue;
    private float nextShotTimeout = 0.0f;
	private TargetType lastTarget;
	[HideInInspector]
	public float fovTemp;
	private Camera camera;
	private CameraAfterEffect cae;
	//private Material videoMaterial;
    /// <summary>
    /// exploder script
    /// </summary>
    public ExploderObject exploder = null;
	void Awake()
	{
		camera = Camera.main;
		cae = camera.GetComponent<CameraAfterEffect> ();
		//videoMaterial = VideoPlane.GetComponent<Renderer> ().material;
	}
	void Start()
	{

		fovTemp =camera.fieldOfView;
		Zooming = false;
	}
    /// <summary>
    /// on activate this game object
    /// </summary>
	/// 
    public void OnActivate()
    {
		ExploderUtils.SetActive(MuzzleFlash, false);
    }
	public void ZoomThermal()
	{
		Zooming = !Zooming;
		if (Zooming) {
			cae.SetVision (CameraType.THERMAL_VISION);
			//videoMaterial.shader = Shader.Find ("Custom/GreenVision");
		} else {
			cae.SetVision (CameraType.NONE);
			//videoMaterial.shader = Shader.Find ("Mobile/Diffuse");
		}
	}
	public void  ZoomGreen ()
	{
		Zooming = !Zooming;
		if (Zooming) {
			cae.SetVision (CameraType.GREEN_VISION);
			//videoMaterial.shader = Shader.Find ("Custom/GreenVision");
		} else {
			cae.SetVision (CameraType.NONE);
			//videoMaterial.shader = Shader.Find ("Mobile/Diffuse");
		}
	}
	void OnGUI ()
	{
		if (Camera.main.enabled) {
			if (!Zooming) {
				if (CrosshairImg) {
					GUI.color = new Color (1, 1, 1, 0.8f);
					GUI.DrawTexture (new Rect ((Screen.width * 0.5f) - (CrosshairImg.width * 0.5f), (Screen.height * 0.5f) - (CrosshairImg.height * 0.5f), CrosshairImg.width, CrosshairImg.height), CrosshairImg);
					GUI.color = Color.white;
				}
			} else {
				if (CrosshairZoom) {
					float scopeSize = (Screen.height * 1.8f);
					GUI.DrawTexture (new Rect ((Screen.width * 0.5f) - (scopeSize * 0.5f), (Screen.height * 0.5f) - (scopeSize * 0.5f), scopeSize, scopeSize), CrosshairZoom);
				}
			}
		}
	}
	public void ZoomDelta (int plus)
	{

		if (plus > 0) {
			if (IndexZoom < ZoomFOVLists.Length - 1) {
				IndexZoom += 1;
			}
		} else {
			if (IndexZoom > 0) {
				IndexZoom -= 1;
			}
		}
	}
	public void ZoomAdjust (int delta)
	{
		
		ZoomDelta (delta);
	}
    void Update()
    {
        GameObject hitObject = null;
        var targetType = ExplodeTargetManager.Instance.TargetType;
		if (HideGunWhileZooming  && Camera.main.enabled) {
//			GameScene.Instance.player.GetComponent<Equips> ().HideWeapons (Zooming);
		}
        // dont shoot when targeting use object
        if (targetType == TargetType.UseObject)
        {
            if (lastTarget != TargetType.UseObject)
            {
//                animation["shotgunHide"].speed = 1;
//                animation.Play("shotgunHide");
            }

            lastTarget = TargetType.UseObject;
        }

        if (lastTarget == TargetType.UseObject)
        {
//            animation["shotgunHide"].speed = -1;
//
//            if (animation["shotgunHide"].time < 0.01f)
//            {
//                animation["shotgunHide"].time = animation["shotgunHide"].length;
//            }
//
//            animation.Play("shotgunHide", AnimationPlayMode.Mix);
        }

        lastTarget = targetType;

        // run raycast against objects in the scene
        var mouseRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        UnityEngine.Debug.DrawRay(mouseRay.origin, mouseRay.direction * 10, Color.red, 0);

        if (targetType == TargetType.DestroyableObject)
        {
			hitObject = ExplodeTargetManager.Instance.TargetObject;
        }
		#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown (1)) 
		#else
		if(Input.touchCount > 2&& Input.touches[0].phase ==TouchPhase.Began && Input.touches[1].phase ==TouchPhase.Began )
		#endif
		{
			ZoomThermal();
		}
		#if UNITY_EDITOR
		else if(  Input.GetMouseButtonDown (1))
		#else
		else if(Input.touchCount > 1&& Input.touches[0].phase ==TouchPhase.Began)
		#endif
		{
			ZoomGreen ();
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			ZoomAdjust(-1);	
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			ZoomAdjust(1);	
		}
		if (Zooming) {
			if (ZoomFOVLists.Length > 0) {
				//MouseSensitiveZoom = ((MouseSensitive * 0.16f) / 10) * ZoomFOVLists [IndexZoom];
				camera.fieldOfView += (ZoomFOVLists [IndexZoom] - camera.fieldOfView) / 10;
			}
		} else {
			camera.fieldOfView += (fovTemp - camera.fieldOfView) / 10;

		}
		#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && nextShotTimeout < 0 && CursorLocking.IsLocked)
		#else
		if (Input.touchCount ==1&& Input.touches[0].phase ==TouchPhase.Stationary  && nextShotTimeout < 0 )
		#endif
        {
            if (targetType != TargetType.UseObject)
            {
                Source.PlayOneShot(GunShot);
                //MouseLookCamera.Kick();//no kick

                // play reload sound after this timeout
                reloadTimeout = 0.3f;

                // turn on flash light for 5 frames
                flashing = 5;
            }

            if (hitObject)
            {
                // get centroid of the hitting object
                var centroid = ExploderUtils.GetCentroid(hitObject);

                // place the exploder object to centroid position
                exploder.transform.position = centroid;
                exploder.ExplodeSelf = false;

                // adjust force vector to be in direction from shotgun
                exploder.ForceVector = mouseRay.direction.normalized;
//                Utils.Log("ForceVec: " + exploder.ForceVector);
                exploder.Force = 10;
                exploder.UseForceVector = true;

                // fragment pieces
                exploder.TargetFragments = 30;

                // set explosion radius to 5 meters
                exploder.Radius = 1.0f;

                // run explosion
                exploder.Explode();
            }

            nextShotTimeout = 0.6f;
        }

        nextShotTimeout -= Time.deltaTime;

        if (flashing > 0)
        {
            Flash.intensity = 1.0f;
            ExploderUtils.SetActive(MuzzleFlash, true);
            flashing--;
        }
        else
        {
            Flash.intensity = 0.0f;
            ExploderUtils.SetActive(MuzzleFlash, false);
        }

        reloadTimeout -= Time.deltaTime;

        if (reloadTimeout < 0.0f)
        {
            reloadTimeout = float.MaxValue;

            // play reload sound
            Source.PlayOneShot(Reload);

            // play reload animation
            ReloadAnim.Play();
        }
    }
}
