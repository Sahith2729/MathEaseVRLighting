using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader; // Reference to SceneLoader
    private string sceneToLoad = "Math Ease"; // Name of the scene

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the XR Rig is tagged as "Player"
        {
            Debug.Log("XR Rig entered trigger. Loading scene: " + sceneToLoad);
            sceneLoader.LoadScene(sceneToLoad); // Use SceneLoader instead of SceneManager
        }
    }
}