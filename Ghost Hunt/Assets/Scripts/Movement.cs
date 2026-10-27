using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Choose speed")]
    [Range(1, 7)][SerializeField] private int speed = 1;
    private bool rwall = false;
    private bool lwall = false;
    Rigidbody rb;

    // Start is called once before the first execution of Update
    void Start()
    {
        //modifies the Rigidbody component inside the object with this script
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rwall)
        {
            rb.AddForce(Vector3.back * speed);
        }
        else if (lwall)
        {
            rb.AddForce(Vector3.forward * speed);
        }
        else
        {
            rb.AddForce(Vector3.forward * speed);
        }
}
    
    //uhhhhhh try deleting this see if it still works, then try calling it in update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            Debug.Log("right");
            rb.AddForce(Vector3.back * speed);
            rwall = !rwall;
        }
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            Debug.Log("left");
            rb.AddForce(Vector3.forward * speed);
            lwall = !lwall;
            rwall = !rwall;
        }
    }
}
