using UnityEngine;
using System.Collections;

public class HouseController : MonoBehaviour
{
    public int amountOfChickenNeeded;

    private int chickenReturned;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chicken")
        {
            chickenReturned++;
            Destroy(other.gameObject);
        }
    }
}
