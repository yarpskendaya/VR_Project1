using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour {

    // Array which stores the Food gameObjects
    [SerializeField]    GameObject[] Food;
    // Array which stores the Meteor gameobject
    [SerializeField]    GameObject Meteor;

    // Array which stores the spwaned Food and Meteor objects in the scene
    GameObject[] spawnedFood;
    GameObject[] spwanedMeteor;

    // Counters to keep track of the amount of spawned objects
    int FoodCounter;
    int MeteorCounter;

    // Boolean to switch between spawning 
    bool foodSpawn;

	// Use this for initialization
	void Start ()
    {
        // Gravity settings to pull objects to the planet of the scene's skybox
        Vector3 gravity = new Vector3(-20, 0, 0);
        Physics.gravity = gravity;

        // Initialisation of food and meteor arrays
        spawnedFood = new GameObject[3];
        spwanedMeteor = new GameObject[3];

        // Initialisation of food and meteor counters
        FoodCounter = 0;
        MeteorCounter = 0;

        // Initialisation of food / meteor boolean
        foodSpawn = true;
	}

    // Method which calls the appropiate spawning methods for meteors or food items, depending on the foodSpawn bool
    public void SpawnObject ()
    {
        if (foodSpawn)
            SpawnFood();
        else
            SpawnMeteor();
    }

    // Method to regulate the amount of spawned food items, by calling the PlaceFood method using the FoodCounter
    public void SpawnFood ()
    {
        switch (FoodCounter)
        {
            case 1:
                if (spawnedFood[1] == null)
                {
                    PlaceFood(FoodCounter);
                    FoodCounter = 2;
                }
                else
                {
                    Destroy(spawnedFood[FoodCounter]);
                    PlaceFood(FoodCounter);
                    FoodCounter = 2;
                }
                break;
            case 2:
                if (spawnedFood[2] == null)
                {
                    PlaceFood(FoodCounter);
                    FoodCounter = 0;
                }
                else
                {
                    Destroy(spawnedFood[FoodCounter]);
                    PlaceFood(FoodCounter);
                    FoodCounter = 0;
                }
                break;
            default:
                if (spawnedFood[0] == null)
                {
                    PlaceFood(FoodCounter);
                    FoodCounter = 1;
                }
                else
                {
                    Destroy(spawnedFood[FoodCounter]);
                    PlaceFood(FoodCounter);
                    FoodCounter = 1;
                }
                break;
        }
    }

    // Method to take a random food object form the Food array, and place it in the active scene
    void PlaceFood (int counter)
    {
        int ranNum = Random.Range(0, 7);
        spawnedFood[counter] = (GameObject)Instantiate(Food[ranNum]);
        spawnedFood[counter].transform.position = new Vector3(0f, -50f, -30f);

    }

    // Method to regulate the amount of spawned meteors , by calling the PlaceMeteor method using the MeteorCounter
    public void SpawnMeteor()
    {
        switch (MeteorCounter)
        {
            case 1:
                if (spwanedMeteor[1] == null)
                {
                    PlaceMeteor(MeteorCounter);
                    MeteorCounter = 2;
                }
                else
                {
                    Destroy(spwanedMeteor[MeteorCounter]);
                    PlaceMeteor(MeteorCounter);
                    MeteorCounter = 2;
                }
                break;
            case 2:
                if (spwanedMeteor[2] == null)
                {
                    PlaceMeteor(MeteorCounter);
                    MeteorCounter = 0;
                }
                else
                {
                    Destroy(spwanedMeteor[MeteorCounter]);
                    PlaceMeteor(MeteorCounter);
                    MeteorCounter = 0;
                }
                break;
            default:
                if (spwanedMeteor[0] == null)
                {
                    PlaceMeteor(MeteorCounter);
                    MeteorCounter = 1;
                }
                else
                {
                    Destroy(spwanedMeteor[MeteorCounter]);
                    PlaceMeteor(MeteorCounter);
                    MeteorCounter = 1;
                }
                break;
        }
    }

    // Method which spawns a meteor in the active scene
    void PlaceMeteor (int counter)
    {
        spwanedMeteor[counter] = Instantiate(Meteor);
        spwanedMeteor[counter].transform.position = new Vector3(0f, -50f, -30f);
    }

    // Method to chage the status of the foodSpawn bool, dictating which gameObject you spawn in the active scene using the in-game Spawner menu
    public void ChangeObject ()
    {
        if (foodSpawn)
            foodSpawn = false;
        else
            foodSpawn = true;
    }
}
