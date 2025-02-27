using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NumberAnimation : MonoBehaviour
{
    public enum MovementDirection
    {
        Up,
        Down
    }

    [FormerlySerializedAs("cubePrefabs")] 
    public GameObject[] numberPrefabs; // Array to hold the six unique cube prefabs
    public Transform spawnPoint; // Spawn point transform
    public float spawnInterval = 1.0f; // Time interval between cube spawns
    public float spaceBetweenCubes = 2.0f; // Minimum space between cubes when spawned
    public float cubeSpeed = 2.0f; // Speed at which cubes move
    public MovementDirection direction = MovementDirection.Up; // Enum to select movement direction

    [Range(0, 20)] // Restrict the start index to a reasonable range
    public int startIndex = 0; // Starting index for spawning numbers

    private int currentNumberIndex; // Tracks the current number to spawn
    private bool hasLooped; // Flag to indicate if the sequence has looped

    private void Start()
    {
        if (numberPrefabs.Length == 0)
        {
            Debug.LogError("Cube prefabs are not assigned!");
            return;
        }

        // Ensure the starting index is within bounds
        startIndex = Mathf.Clamp(startIndex, 0, numberPrefabs.Length - 1);
        currentNumberIndex = startIndex;
        hasLooped = false;

        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            SpawnCube();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCube()
    {
        // Pick the current number prefab
        GameObject cubePrefab = numberPrefabs[currentNumberIndex];

        // Instantiate the cube
        Vector3 spawnPosition = spawnPoint.position;
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        // Pass the selected direction to the cube
        newCube.AddComponent<MovingCube>().Initialize(cubeSpeed, direction);

        // Update the index for the next cube
        currentNumberIndex++;

        // Check if the end of the array is reached
        if (currentNumberIndex >= numberPrefabs.Length)
        {
            if (!hasLooped)
            {
                // Reset to the first element after the first complete loop
                currentNumberIndex = 0;
                hasLooped = true;
            }
            else
            {
                // Continue looping from the beginning after initial reset
                currentNumberIndex = 0;
            }
        }
    }
}

public class MovingCube : MonoBehaviour
{
    private float speed;
    private Vector3 moveDirection;
    private string targetTag;

    public void Initialize(float cubeSpeed, NumberAnimation.MovementDirection direction)
    {
        speed = cubeSpeed;

        // Set movement direction and target tag based on the selected direction
        if (direction == NumberAnimation.MovementDirection.Up)
        {
            moveDirection = Vector3.up;
            targetTag = "Ceiling";
        }
        else
        {
            moveDirection = Vector3.down;
            targetTag = "Floor";
        }
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
    }
}
