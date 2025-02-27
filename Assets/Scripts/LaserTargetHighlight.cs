using UnityEngine;

public class LaserTargetHighlight : MonoBehaviour
{
    public LineRenderer laserLine; // Assign the laser's LineRenderer
    public Material highlightMaterial; // Material to apply when aiming
    private Material originalMaterial;
    private GameObject lastHitObject;

    void Update()
    {
        if (laserLine == null) return; // Ensure the laser exists

        RaycastHit hit;
        if (Physics.Raycast(laserLine.transform.position, laserLine.transform.forward, out hit, 100f))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Button")) // Check if object has the "Button" tag
            {
                MeshRenderer meshRenderer = hitObject.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    if (lastHitObject != hitObject) // Change material only if it's a new object
                    {
                        ResetMaterial();
                        originalMaterial = meshRenderer.material;
                        meshRenderer.material = highlightMaterial;
                        lastHitObject = hitObject;
                    }
                }
            }
            else
            {
                ResetMaterial(); // Reset if the hit object is not tagged "Button"
            }
        }
        else
        {
            ResetMaterial(); // Reset if nothing is being hit
        }
    }

    void ResetMaterial()
    {
        if (lastHitObject != null)
        {
            MeshRenderer meshRenderer = lastHitObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = originalMaterial; // Restore original material
            }
            lastHitObject = null;
        }
    }
}