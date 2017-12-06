using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed, lookSensitivity;
    
    private MeshRenderer throwArc;
    private float rotY;

    // Use this for initialization
    private void Start ()
    {
        throwArc = transform.Find("Throw Mesh").GetComponent<MeshRenderer>();

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        //Movement
        float hMoveAxis = Input.GetAxis("Horizontal");
        float vMoveAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hMoveAxis, 0, vMoveAxis) * speed * Time.deltaTime;
        transform.Translate(movement);

        //Rotate
        rotY += Input.GetAxis("Mouse X");
        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.rotation = rotation;

        //Activate throw arc
        if (Input.GetMouseButton(0))
        {
            throwArc.enabled = true;
        }
        else
        {
            throwArc.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
