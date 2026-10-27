using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [Header("Choose speed")]
    [Range(1, 7)][SerializeField] private int speed = 1;
    private bool rwall = false;
    private bool lwall = false;
    Rigidbody rb;

    // Start is called once before the first execution of Update
    void Start()
    {
        //modifies the Rigidbody component inside the object 
        //with this script to add an initial force to the right
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * speed);
    }

    // Update is called once per frame
    void Update()
    {
        //if the object touches the right wall, the rigidbody
        //gets a force to the left, so it moves left. Then vise
        //versa once the other wall is in contact.
        if (rwall)
        {
            rb.AddForce(Vector3.left * speed);
        }
        else if (lwall)
        {
            rb.AddForce(Vector3.right * speed);
        }
        else
        {
            rb.AddForce(Vector3.right * speed);
        }
    }
    
    //uhhhhhh try deleting this see if it still works, then try calling it in update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RightWall"))
        {
            Debug.Log("right");
            rb.AddForce(Vector3.left * speed);
            rwall = !rwall;
        }
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            Debug.Log("left");
            rb.AddForce(Vector3.right * speed);
            lwall = !lwall;
            rwall = !rwall;
        }
    }
}
