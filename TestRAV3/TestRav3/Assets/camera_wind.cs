using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_wind : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string HighWind;
    FMOD.Studio.EventInstance windInst;
    float cameraheight;

    public GameObject ExampleCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        ExampleCamera = GameObject.FindGameObjectWithTag("MainCamera");
        windInst = FMODUnity.RuntimeManager.CreateInstance(HighWind);
        windInst.start();
    }

    // Update is called once per frame
    void Update()
    {
        cameraheight = ExampleCamera.transform.position.y;
        windInst.setParameterByName("cameraheight", cameraheight);
    }
}
