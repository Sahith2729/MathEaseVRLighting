using UnityEngine;

public class NumberPrefabCollider : MonoBehaviour
{
    private MathWhackAMole gameManager;

    private void Start()
    {
        // Find the MathWhackAMole script in the hierarchy  
        gameManager = FindObjectOfType<MathWhackAMole>();
        if (gameManager == null)
        {
            Debug.LogError("MathWhackAMole script not found in the scene!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the HammerCollider tag
        if (collision.gameObject.CompareTag("HammerCollider"))
        {
            CheckAnswer();
        }
    }

    private void CheckAnswer()
    {
        string numberTag = gameObject.tag; // Use the prefab's tag as the answer  
        gameManager.CheckAnswer(numberTag, transform.position); // Call the CheckAnswer function in MathWhackAMole with the position  
    }

    // You can keep OnDrawGizmos for debugging if needed
    private void OnDrawGizmos()
    {
        GameObject hammer = GameObject.FindGameObjectWithTag("HammerCollider");
        if (hammer != null)
        {
            // Draw a line between the prefab and the hammer  
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, hammer.transform.position);
            // Display the distance as text  
            float distance = Vector3.Distance(transform.position, hammer.transform.position);
            Vector3 midPoint = (transform.position + hammer.transform.position) / 2;
            //UnityEditor.Handles.Label(midPoint, $"Distance: {distance:F2}m");
        }
    }
}