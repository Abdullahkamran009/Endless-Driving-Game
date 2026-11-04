using UnityEngine;

public class ExplodeHandler : MonoBehaviour
{
    [SerializeField]
    GameObject originalObjects;

    [SerializeField]
    GameObject model;

    Rigidbody[] rigidbodies;

    private void Awake()
    {
        rigidbodies = model.GetComponentsInChildren<Rigidbody>(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // Explode(Vector3.forward);
    }

    public void Explode(Vector3 externalForce)
    {
        originalObjects.SetActive(false);

        foreach(Rigidbody rb in rigidbodies)
        {
            rb.transform.parent = null;

            rb.GetComponent<MeshCollider>().enabled = true;

            rb.gameObject.SetActive(true);
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.AddForce(Vector3.up * 200 + externalForce, ForceMode.Force);
            rb.AddTorque(Random.insideUnitSphere * 0.5f, ForceMode.Impulse);

            //Change the tag so other objects can explode after being hit by carpart
            rb.gameObject.tag = "CarPart";
        }
    }


}
