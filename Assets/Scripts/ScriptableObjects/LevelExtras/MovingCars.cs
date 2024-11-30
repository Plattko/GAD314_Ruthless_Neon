using UnityEngine;

public class MovingCars : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 startPoint = new Vector3(0, 0, 0); // X-Z plane
    public Vector3 endPoint = new Vector3(5, 0, 5);   // X-Z plane
    public float speed = 2f;

    [Header("Prefab Settings")]
    public GameObject[] prefabs; // Assign prefabs in the inspector
    public float spawnInterval = 2f; // Time between spawns
    public Vector3 rotationEulerAngles = Vector3.zero; // Default rotation in Euler angles

    private void Start()
    {
        // Start spawning objects at regular intervals
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        if (prefabs.Length > 0)
        {
            // Randomly select a prefab
            GameObject selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];

            // Convert Euler angles to Quaternion for rotation
            Quaternion rotation = Quaternion.Euler(rotationEulerAngles);

            // Instantiate the prefab with the specified rotation
            GameObject spawnedObject = Instantiate(selectedPrefab, startPoint, rotation);

            // Assign a script to move and destroy the object
            spawnedObject.AddComponent<MovingObject>().Initialize(endPoint, speed);
        }
        else
        {
            Debug.LogError("No prefabs assigned!");
        }
    }
}

public class MovingObject : MonoBehaviour
{
    private Vector3 targetPoint;
    private float moveSpeed;

    public void Initialize(Vector3 target, float speed)
    {
        targetPoint = target;
        moveSpeed = speed;
    }

    private void Update()
    {
        // Move the object towards the target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        // Destroy the object once it reaches the target
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}