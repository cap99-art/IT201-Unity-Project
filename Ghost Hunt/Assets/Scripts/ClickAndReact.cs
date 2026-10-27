using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickAndReact : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f;
    private int score = 0;
    [SerializeField] private GameObject ghost1;
    [SerializeField] private GameObject ghost2;
    private GameObject[] spawnedObjects = new GameObject[10];

    // Start is called once before the first execution of Update
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

    //checks for mouse clicking and does something
    //depending on which object the casted ray touches
    private void CheckMouseClick()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.Log("Clicked " + mousePos.ToString() + " ray " + ray.ToString());

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            GameObject clickedObject = hit.collider.gameObject;

            //click an object with the "Spawner" tag to spawn ghosts
            if (clickedObject.CompareTag("Spawner"))
            {
                Debug.Log("Ghosts spawned!");
                SpawnGhosts();
            }

            //Types of ghosts with differet scores, once clicked it will add
            //to the score, delete the ghost, and say this in the logs
            if (clickedObject.CompareTag("Slow"))
            {
                Debug.Log("You killed a ghost!");
                Destroy(clickedObject);
                score++;
                Debug.Log("Score = " + score);
            }
            else if (clickedObject.CompareTag("Fast"))
            {
                Debug.Log("You killed a ghost!");
                Destroy(clickedObject);
                score+= 2;
                Debug.Log("Score = " + score);
            }
        }
        else
        {
            Debug.Log("You clicked something else.");
        }
    }

    private void SpawnGhosts()
    {
        for (int i = 0; i < 10; i++)
        {
            //creates a spawn position with the z-axis incrementally
            //changing with each loop so the ghosts end up forming columns
            Vector3 spawnPos = new Vector3(0f, 0f, (i * 1f));

            //randomly chooses which ghost to spawn on each loop
            GameObject chosenObject = (UnityEngine.Random.value > 0.5f) ? ghost1 : ghost2;

            //spawns a ghost
            spawnedObjects[i] = Instantiate(chosenObject, spawnPos, Quaternion.identity);
        }
    }
}
