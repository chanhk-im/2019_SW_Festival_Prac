using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Vector3 rotation;
    public float rotationVelocity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation = rotation + new Vector3(rotationVelocity, 0, 0) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
