using UnityEngine;
using UnityEngine.InputSystem;

public class ClickAndReact : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckMouseClick();
        }
    }
    private void CheckMouseClick()
    {
    Vector2 mousePos = Mouse.current.position.ReadValue();

    Ray ray = Camera.main.ScreenPointToRay(mousePos);
    Debug.Log("Clicked " + mousePos.ToString() + " ray " + ray.ToString());

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.CompareTag("Target"))
            {
                Debug.Log("You clicked the target!");
                //clickedObject.transform.localScale *= 1.25f;
                clickedObject.GetComponent<Renderer>().material.color = Color.red;
            }
        } else
        {
            Debug.Log("You clicked something else.");
        }
    }
}
