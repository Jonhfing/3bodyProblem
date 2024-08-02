using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [field: SerializeField] public GameObject focus{get; set;}
    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = focus.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = focus.transform.position + offset;
    }
}
