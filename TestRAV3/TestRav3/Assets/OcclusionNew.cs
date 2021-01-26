using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;


public class OcclusionNew : MonoBehaviour
{
    [Header("FMOD Event")]
    [EventRef]
    public string SelectAudio;
    EventInstance Audio;

    Transform SlLocation;

    [Header("Occlusion Options")]
    [Range(0f, 1f)]
    public float VolumeValue = 0.5f;
    [Range(0f, 1f)]
    public float LPFCutoff = 0.5f;
    public LayerMask OcclusionLayer = 1;

    void Start()
    {
        Audio = RuntimeManager.CreateInstance(SelectAudio);
        PLAYBACK_STATE PbState;
        Audio.getPlaybackState(out PbState);
        if (PbState != PLAYBACK_STATE.PLAYING)
        {
            Audio.start();
        }
    }

    void Update()
    {
        SlLocation = GameObject.FindObjectOfType<StudioListener>().transform;

        RaycastHit hit;
        RuntimeManager.AttachInstanceToGameObject(Audio, GetComponent<Transform>(), GetComponent<Rigidbody>());
        Physics.Linecast(transform.position, SlLocation.position, out hit, OcclusionLayer);

        if (hit.collider)
        {
            //Debug.Log ("not occluded");
            NotOccluded();
            Debug.DrawLine(transform.position, SlLocation.position, Color.blue);
        }
        else
        {
           // Debug.Log ("occluded");
            Occluded();
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }

    }

    void Occluded()
    {
        Audio.setParameterByName("LPF", LPFCutoff);
        Audio.setParameterByName("Volume", VolumeValue);
    }

    void NotOccluded()
    {
        Audio.setParameterByName("LPF", 22000f);
        Audio.setParameterByName("Volume", 1f);
    }
}