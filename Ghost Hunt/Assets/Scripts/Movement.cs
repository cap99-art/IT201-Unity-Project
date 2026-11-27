using UnityEngine;


public class Movement : MonoBehaviour
{
   [Header("Choose speed")]
   [Range(0, 7)][SerializeField] private float speed = 1; 
    public static float speedMultiplier = 1f; //public so that the clickScript can access and change it
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
        //checks if the right wall is true, so that it can add a force that is opposite to it,
        //and vise versa for checking the left wall's boolean.
       if (rwall)
       {
           rb.AddForce(Vector3.back * speed * speedMultiplier);
       }
       else
       {
           rb.AddForce(Vector3.forward * speed * speedMultiplier);
       }
}
  
    //gets called whenever a gameObject with the tag RightWall or LeftWall is
    //collided with by another object, so that it can send a force the opposite way
    //and set the boolean value to where it make sense.
   private void OnCollisionEnter(Collision collision)
   {
       if (collision.gameObject.CompareTag("RightWall"))
       {
           rb.AddForce(Vector3.back * speed * speedMultiplier);
           rwall = !rwall;
       }
       if (collision.gameObject.CompareTag("LeftWall"))
       {
           rb.AddForce(Vector3.forward * speed * speedMultiplier);
           lwall = !lwall;
           rwall = !rwall;
       }
   }
}
