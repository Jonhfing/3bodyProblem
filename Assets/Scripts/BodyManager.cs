using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public enum ConfigName {Test};

public class BodyManager : MonoBehaviour
{
    // the managed bodies
    [SerializeField] private Body[] bodies;

    // For gravity
    // intensity is used to scale the force without changing the bodies' masses
    [SerializeField] private float intensity = 3; 

    // Structure to store one set of initial conditions (positions and velocities of each body)
    public struct StartConfig{
        //stores all starting positions of bodies for this configuation
        public Vector3[] startingPositions;
        
        //stores all starting velocities of bodies for this configuration 
        public Vector3[] startingVelocities;

        //constructor : you declare the starting xz coordinates of then xz velocities of each body
        //no need to declare for y, as we work in 2 dimensions (for now?)
        public StartConfig(
            float x1, float z1, float x2, float z2, float x3, float z3,
            float xv1, float zv1, float xv2, float zv2, float xv3, float zv3)
        {
                this.startingPositions = new []{new Vector3(x1,0,z1), new Vector3(x2,0,z2), new Vector3(x3,0,z3)};
                this.startingVelocities = new []{new Vector3(xv1,0,zv1), new Vector3(xv2,0,zv2), new Vector3(xv3,0,zv3)};
            }
    }

    // Base start config that I use for testing
    private static StartConfig test = new StartConfig(14.5f,6, -5,0, -12.5f,10,  -2,0, 0,0, 10,0);
    
    //figure eight
    private static float p1 = 0.3471168881f;
    private static float p2 = 0.5327249454f;
    private static StartConfig figureEight = new StartConfig(-10,0, 10,0, 0,0,  2*p1,2*p2, 2*p1,2*p2, -4*p1, -4*p2);
    private static StartConfig pythagorian = new StartConfig(0,40, -30,0, 0,0, 0,0, 0,0, 0,0); // needs 3:4:5 mass ratio, disabled for now
    private static StartConfig isoscele = new StartConfig(-10,0, 10,0, 0,10*Mathf.Sqrt(3),  -1,2*Mathf.Sqrt(3), -2*Mathf.Sqrt(3),-1, 2,0);

    // List of all possible starting configurations
    private StartConfig[] startConfigs = new []{test, figureEight, isoscele};

    // Wait for time seconds
    // Used to avoid residual collisions when reseting a scene where the stars are grouped together
    public IEnumerator Wait(float time)
    {
        Debug.Log("entering wait for "+time+" seconds   ");
        yield return new WaitForSeconds(time);
    }

    // Start is called before the first frame update
    
    void Start()
    {
        Init(2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dist;
        Vector3 pull;
        // for each body, calculate the pull exerted by every other body, then add the sum to its velocity. Finally, scale it by the intensity of the force, defined in the BodyManager class
        for(int i = 0; i<bodies.Length; i++){
            pull = Vector3.zero;
            for(int j = 0;j<bodies.Length; j++)
                if(i!=j){
                    dist = bodies[j].transform.position - bodies[i].transform.position;
                    pull += dist.normalized * (bodies[i].rb.mass*bodies[j].rb.mass/(Mathf.Pow(dist.magnitude, 2))) * intensity;
                }
            bodies[i].rb.AddForce(pull, ForceMode.Acceleration);
        }
    }

    // to reset the simulation with preregistered parameters
    public void Init(int configNb){
        Time.timeScale = 0;
        for(int i = 0; i<bodies.Length; i++){
            bodies[i].transform.position = startConfigs[configNb].startingPositions[i];
            bodies[i].rb.velocity = startConfigs[configNb].startingVelocities[i];
            //Debug.Log("Body "+i+" starting velocity : "+bodies[i].rb.velocity+" planned velocity : "+startConfigs[configNb].startingVelocities[i]);
        }
        Time.timeScale = 1;
    }
}
