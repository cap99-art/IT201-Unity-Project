using System.ComponentModel.Design.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ClickScript : MonoBehaviour
{
    public static int score = 0; //these are public  
    public static int ghostsDestroyed = 0; //so that they can be displayed
    public static int round = 1; //by the 
    public static int level = 1; //UIManager
    private GameObject[] spawnedObjects = new GameObject[17];

    [Header("Text labels")]
    [SerializeField] private TMP_Text SpawnText;
    [SerializeField] private TMP_Text SceneText;
    [SerializeField] private TMP_Text SceneText2;
    
    [Header("Ghosts")]
    [SerializeField] private GameObject ghost1;
    [SerializeField] private GameObject ghost2;
    [SerializeField] private GameObject ghost3;

    [Header("Other")]
    [SerializeField] private float rayDistance = 100f;

    //called once before Update
    void Start()
    {
        //starts the first round so that the rest can be payed for
        SpawnGhosts();
        Debug.Log("Round 1 started!");
    }

    //called once per frame
    void Update()
    {
        //checks if the left mouse button was clicked in every frame
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

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 2f);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            GameObject clickedObject = hit.collider.gameObject;

            //click an object with the "Spawner" tag to spawn ghosts as a new round
            //only if the player destroyed more than 12 ghosts.
            if (clickedObject.CompareTag("Spawner"))
            {
                if (ghostsDestroyed >= 12)
                {
                    SpawnGhosts();
                    round++;
                    ghostsDestroyed = 0;
                    return;
                }
                return;
            }
            //click an object with the "LockedScene" tag to change the
            //tag to "NextScene" only if the score is more than 20.
            if (clickedObject.CompareTag("LockedScene"))
            {
                if (score >= 20 && level == 1)
                {
                    score -= 20;
                    clickedObject.tag = "NextScene";
                    SceneText.text = "LEVEL 2 \n--------->";
                    return;
                }
                else if (score >= 5 && level == 2)
                {
                    score -= 5;
                    clickedObject.tag = "NextScene";
                    SceneText2.text = "LEVEL 3 \n--------->";
                    return;
                }
            }
            //click an object with the "NextScene" tag to move the position
            //of the main camera to the next area tracked by the level variable
            if (clickedObject.CompareTag("NextScene"))
            {
                GameObject cam = GameObject.FindWithTag("MainCamera");
                
                if (level == 1)
                {
                    level++;
                    cam.transform.position = new Vector3(83.17f, 4.82f, 128.6f);
                }
                else if (level == 2)
                {
                    level++;
                    cam.transform.position = new Vector3(83.17f, 4.82f, 200.6f);
                }
            }
            //click an object with the "PreviousScene" tag to move the position
            //of the main camera to the previous area tracked by the level variable
            if (clickedObject.CompareTag("PreviousScene"))
            {
                GameObject cam = GameObject.FindWithTag("MainCamera");
                if (level == 2)
                {
                    level -= 1;
                    cam.transform.position = new Vector3(82.5f, 4.82f, 72.81f);
                }
                else if (level == 3)
                {
                    level -= 2;
                }
            }

            //Types of ghosts with different scores. Once an object with
            //the stated tag is clicked, it will add the stats and delete the ghost
            if (clickedObject.CompareTag("Slow"))
            {
                Debug.Log("You killed a ghost!");
                Destroy(clickedObject);
                score ++;
                ghostsDestroyed++;
            }
            else if (clickedObject.CompareTag("Fast"))
            {
                Debug.Log("You killed a ghost!");
                Destroy(clickedObject);
                score += 3;
                ghostsDestroyed++;
            }
            if (clickedObject.CompareTag("Idle"))
            {
                Debug.Log("You killed a ghost!");
                Destroy(clickedObject);
                score+= 2;
                ghostsDestroyed++;
            }
            //if any object with these tags are clicked, the current score
            //either gets subtracted by 1, or if already 0, it equals to
            //0 so that there are no negative scores.
            //Outputs the current score in the logs.
            if (clickedObject.CompareTag("Props") || clickedObject.CompareTag("RightWall") || clickedObject.CompareTag("LeftWall"))
            {
                score -= 1;
                if (score < 0)
                {
                    score = 0;
                }
                Debug.Log("You missed! -1 point");
            }
        }
    }

    private void SpawnGhosts()
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            //clear all previous round ghosts
            if (spawnedObjects[i] != null)
            {
                Destroy(spawnedObjects[i]);
            }

            if (i == 1) //for an idle ghost
            {
                Vector3 IdlespawnPos = new Vector3(Random.Range(55f,25f), 3.31f, Random.Range(52,96));
                spawnedObjects[i] = Instantiate(ghost3, IdlespawnPos, Quaternion.Euler(0f, 100f, 0f));
            }
            if (i == 2) //for a second idle ghost
            {
                Vector3 IdlespawnPos2 = new Vector3(Random.Range(55f, 25f), 3.31f, Random.Range(52, 96));
                spawnedObjects[i] = Instantiate(ghost3, IdlespawnPos2, Quaternion.Euler(0f, 100f, 0f));
            }

            //creates a spawn position with the z-axis incrementally
            //changing with each loop so the ghosts end up forming columns
            Vector3 spawnPos = new Vector3(
            i * 2f + 40.9f + Random.Range(-0.5f, 0.5f), //x-axis
            0.27f, //y-axis
            58.79f + Random.Range(-0.5f, 0.5f) //z-axis
            );

            //Randomly choose between ghost types
            GameObject chosenGhost = (Random.Range(0, 2) == 0) ? ghost1 : ghost2;
            spawnedObjects[i] = Instantiate(chosenGhost, spawnPos, Quaternion.identity);
        }
    }

    
}
