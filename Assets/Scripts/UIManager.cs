using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private BodyManager bm;
    // Start is called before the first frame update
    void Start()
    {
        bm = FindObjectOfType<BodyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause(){
        Time.timeScale = 1-Time.timeScale;
    }
    public void SetConfig(int config){
        bm.Init(config);
    }
}
