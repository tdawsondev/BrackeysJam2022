using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartLauncher : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform firePoint;
    public Vector3 launchDirection;
    public float fireSpeed;
    public bool startFiring;
    private float fireTime;
    private bool firing;
    
    // Start is called before the first frame update
    void Start()
    {
        firing = false;
        fireTime = fireSpeed;
        if (startFiring)
        {
            StartFiring();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firing)
        {
            fireTime -= Time.deltaTime;
            if(fireTime <= 0)
            {
                FireOneShot();
                fireTime = fireSpeed;
            }
        }
    }

    public void FireOneShot()
    {
        Dart dart = Instantiate(dartPrefab, firePoint.position, firePoint.rotation, transform).GetComponent<Dart>();
        dart.Launch(launchDirection);
        
    }
    public void StartFiring()
    {
        fireTime = 0;
        firing = true;
    }
    public void StopFiring()
    {
        firing = false;
    }
}
