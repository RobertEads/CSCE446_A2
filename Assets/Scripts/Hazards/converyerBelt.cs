using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class converyerBelt : MonoBehaviour
{
    private Renderer myMaterial;

    [SerializeField] private List<Material> materials = new List<Material>();

    public bool reverseDirection = false;
    public int pushStrength = 5;

    void Start()
    {
        myMaterial = GetComponent<Renderer>();
        Invoke("change_direction", 5f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("effectableObject"))
        {
            if (!reverseDirection) { other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(pushStrength, 0f, 0f)); }
            if (reverseDirection) { other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-pushStrength, 0f, 0f)); }

        }
    }

    private void change_direction()
    {
        reverseDirection = !reverseDirection;
        if(reverseDirection) { myMaterial.material = materials[1]; }
        else { myMaterial.material = materials[0]; }
        Invoke("change_direction", 5f);
    }
}
