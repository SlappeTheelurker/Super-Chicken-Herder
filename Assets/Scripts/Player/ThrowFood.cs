using UnityEngine;
using System.Collections;

public class ThrowFood : MonoBehaviour
{
    public GameObject foodPrefab;
    public float throwAngle;

    private Transform throwPoint;
    // Use this for initialization
    void Start()
    {
        throwPoint = transform.Find("Food Throw Point");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject food = Instantiate(foodPrefab, throwPoint.position, transform.rotation);
            food.GetComponent<Rigidbody>().AddForce(Vector3.forward * GetComponentInChildren<ThrowArcMesh>().velocity * 50);
        }
    }
}
