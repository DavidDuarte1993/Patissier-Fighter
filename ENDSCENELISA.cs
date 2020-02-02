using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENDSCENELISA : MonoBehaviour
{
    [SerializeField]
    Transform point = null;
    [SerializeField]
    float movespeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (point == null) return;
        transform.position = Vector3.MoveTowards(transform.position, point.position, movespeed * Time.deltaTime);
    }
}
