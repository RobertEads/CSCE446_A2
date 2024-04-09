using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTeleporter : MonoBehaviour
{
    public Transform teleportTransform;
    public LayerMask golfballLayer;
    public string golfballTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & golfballLayer) != 0)
        {
            other.transform.position = teleportTransform.position;
        }

        if (other.gameObject.CompareTag(golfballTag))
        {
            other.transform.position = teleportTransform.position;
        }
    }
}
