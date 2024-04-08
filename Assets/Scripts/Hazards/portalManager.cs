using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalManager : MonoBehaviour
{
    [SerializeField] private GameObject portalExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("effectableObject"))
        {
            other.gameObject.transform.position = portalExit.transform.position;
            other.gameObject.transform.rotation = Quaternion.Euler(other.gameObject.transform.rotation.eulerAngles.x, other.gameObject.transform.rotation.eulerAngles.y + 180, other.gameObject.transform.rotation.eulerAngles.z);

            Vector3 currentVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            Vector3 newVelocity = new Vector3((-currentVelocity.x) * 2f, 0f, (-currentVelocity.z) * 2f);
            other.gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
        }
    }
}
