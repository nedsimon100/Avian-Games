using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsFlocking : MonoBehaviour
{
    public Vector3 OptimalDirection;
    //optimaldirection = average position where there is space without colliding with other boid and sum of average direction all boids are moving
    public int speed = 5;
    public int directCohesionDist = 50;
    // Start is called before the first frame update
    public float seperationDist = 10;
    public float maxSpeed = 10f;
    public Vector3 seperationAve;
    public Rigidbody rb;

    public float cohesionMult = 2f;
    public float seperationMult = 0.2f;

    public GameObject BoidController;
    private void Start()
    {
        BoidController = FindObjectOfType<BoidController>().gameObject;
    }
    void Update()
    {
        move();
    }
    public void move()
    {
        BoidsFlocking[] boids = FindObjectsOfType<BoidsFlocking>();
        OptimalDirection = Vector3.zero;
        seperationAve = Vector3.zero;
        foreach (BoidsFlocking bo in boids)
        {

            OptimalDirection += DirectionalCohesion(bo);
            if (bo != this)
            {
                //check if each boid is too close to another boid and the direction itr should go
                
                if((bo.transform.position - this.transform.position).magnitude < seperationDist)
                {
                    seperationAve += Seperation(bo);
                }
            }
        }
        OptimalDirection /= boids.Length;
        //OptimalDirection -= this.transform.position;
        seperationAve/= boids.Length;
        Vector3 desiredVelocity = seperationAve*seperationMult + (OptimalDirection*cohesionMult / (directCohesionDist)) * speed;
        rb.velocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
        transform.LookAt(transform.position+rb.velocity);
    }
    public Vector3 Seperation(BoidsFlocking b)
    {
        Vector3 Seperate = (this.transform.position - b.transform.position);
        Seperate.x = (Seperate.x == 0) ? 0 : 1 / (Seperate.x/seperationDist);
        Seperate.y = (Seperate.y == 0) ? 0 : 1 / (Seperate.y/seperationDist);
        Seperate.z = (Seperate.z == 0) ? 0 : 1 / (Seperate.z/seperationDist);
        return Seperate;
    }
    public Vector3 DirectionalCohesion(BoidsFlocking b)
    {
        Vector3 directCohesion = (b.transform.position - this.transform.position) + (b.transform.forward * directCohesionDist);
        return directCohesion;

    }
}
