using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position + offset;
    }
}
