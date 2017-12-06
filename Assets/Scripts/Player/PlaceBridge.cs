using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBridge : MonoBehaviour {
    public GameObject bridge;
    public float rotationPerTick, positionPerTick, spawnDistancePlayer;
    
    private GameObject bridgeMarker;
    private int ticksToResetPosition;
	// Use this for initialization
	void Start () {
        bridgeMarker = GameObject.Find("Bridge Marker");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            bridgeMarker.SetActive(true);
            if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.E))
            {
                bridgeMarker.transform.Rotate(Vector3.left * rotationPerTick);
                bridgeMarker.transform.Translate(Vector3.up * positionPerTick);
                ticksToResetPosition--;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.Q))
            {
                bridgeMarker.transform.Rotate(Vector3.right * rotationPerTick);
                bridgeMarker.transform.Translate(Vector3.down * positionPerTick);
                ticksToResetPosition++;
            }
        }
        else
        {
            bridgeMarker.SetActive(false);
        }

        //Reset position and rotation
        if (Input.GetMouseButtonUp(1))
        {
            Instantiate(bridge, bridgeMarker.transform.position, bridgeMarker.transform.rotation);
            bridgeMarker.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            bridgeMarker.transform.Translate(Vector3.up * (ticksToResetPosition * positionPerTick));
            ticksToResetPosition = 0;
        }
	}
}
