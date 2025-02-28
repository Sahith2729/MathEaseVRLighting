using UnityEngine;

namespace BNG {
    public class SceneTransition : MonoBehaviour
    {
        [Tooltip("Enter the scene name to load")]
        [SerializeField] private string sceneToLoad;

        [Tooltip("Assign the SceneLoader from the scene")]
        [SerializeField] private SceneLoader sceneLoader;

        // Method to be called for scene transition
        public void LoadAssignedScene()
        {
            if (sceneLoader != null && !string.IsNullOrEmpty(sceneToLoad))
            {
                sceneLoader.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("SceneLoader is not assigned or Scene name is empty!");
            }
        }
    }
}
