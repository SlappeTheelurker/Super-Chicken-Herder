using UnityEngine;
using System.Collections;

public class FoodController : MonoBehaviour
{

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Chicken")
        {
            Destroy(gameObject);
        }
    }
}
