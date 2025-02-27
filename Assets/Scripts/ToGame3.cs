using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGame3 : MonoBehaviour
{
    public AG1Manager gameManager;

    void Start()
    {
        // Check if the gameManager is assigned
        if (gameManager != null)
        {
            // Call the ResetGame method of the AG1Manager script
            gameManager.ChangeToGame3Scene();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("AG1Manager is not assigned in the Inspector.");
        }
    }
}
