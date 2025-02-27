using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnButtonShot : MonoBehaviour
{
    public UnityEvent onDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDestroy()
    {
        if (onDestroyed != null)
        {
            onDestroyed.Invoke();
        }
        //Debug.Log("<color=cyan>DEESTROYEEDD</color>");
    }
}
