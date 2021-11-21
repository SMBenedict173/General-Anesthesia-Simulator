using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToSpawnPoint : MonoBehaviour
{
    private Transform spawnPoint;
    private bool isHeld;
    // Start is called before the first frame update
    void Start()
    {
        this.spawnPoint = this.gameObject.transform.parent;
        this.isHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This may not be necessary. If not delete to reduce the number of calculations per frame
        if (!this.isHeld)
        {
            this.gameObject.transform.position = spawnPoint.position;
            this.gameObject.transform.rotation = spawnPoint.rotation;
        }
    }

    public void GrabbedByPlayer()
    {
        this.isHeld = true;
    }
    
    public void ReturnToSpawn()
    {
        this.isHeld = false;
        this.gameObject.transform.position = spawnPoint.position;
        this.gameObject.transform.rotation = spawnPoint.rotation;
    }
}
