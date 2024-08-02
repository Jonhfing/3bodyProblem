using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Body : MonoBehaviour
{
    public Rigidbody rb;
    // Awake is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    
}
