using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDissapear : MonoBehaviour {
    public float travelSpeed, dissapearSpeed, textCounter;

    private float opacity, travelDistance;

    // Use this for initialization
    void Start ()
    {
        opacity = 1.5f;
        travelDistance = GetComponent<RectTransform>().transform.localPosition.y;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 p = GetComponent<RectTransform>().transform.localPosition;
        GetComponent<Text>().color = new Color(1, 1, 1, opacity);
        GetComponent<RectTransform>().transform.localPosition = new Vector3(GetComponent<RectTransform>().transform.localPosition.x, travelDistance, GetComponent<RectTransform>().transform.localPosition.z);

        travelDistance += travelSpeed;
        opacity -= dissapearSpeed;

        if (opacity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
