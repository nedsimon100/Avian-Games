using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsFlocking : MonoBehaviour
{
    private Vector3 OptimalDirection;
    //optimaldirection = average position where there is space without colliding with other boid and sum of average direction all boids are moving
    public float speed = 1f;
    public int directCohesionDist = 50;
    // Start is called before the first frame update
    public float seperationDist = 10;
    public float maxSpeed = 10f;
    public Vector3 seperationAve;
    private Rigidbody rb;
    public float playerSpeed = 2;
    public float cohesionMult = 2f;
    public float seperationMult = 0.2f;

    public bool firing = false;
    public float shootSpeed = 20f;
    private BoidController Player;
    private void Start()
    {
        Player = FindObjectOfType<BoidController>();
        rb = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(!firing)
        {
            move();
            
        }
        else
        {
            rb.velocity = transform.forward * shootSpeed;
        }
        Remove();
    }
    public void Remove()
    {
        if ((Player.transform.position - this.transform.position).magnitude > 150)
        {
            Destroy(this.gameObject);
        }
    }
    public void move()
    {
        BoidsFlocking[] boids = FindObjectsOfType<BoidsFlocking>();
        OptimalDirection = Vector3.zero;
        seperationAve = Vector3.zero;
        foreach (BoidsFlocking bo in boids)
        {
            if((Player.transform.position - this.transform.position).magnitude > Player.flockRange && (Player.transform.position - bo.transform.position).magnitude > Player.flockRange && !bo.firing) 
            {
                OptimalDirection += DirectionalCohesion(bo.gameObject);
            }


            if (bo != this)
            {
                //check if each boid is too close to another boid and the direction it should go
                
                if((bo.transform.position - this.transform.position).magnitude < seperationDist)
                {
                    seperationAve += Seperation(bo);
                }
            }
        }
        OptimalDirection /= boids.Length;
        if ((Player.transform.position - this.transform.position).magnitude < Player.flockRange)
        {
            OptimalDirection = DirectionalCohesion(Player.gameObject);
           // speed = playerSpeed;
        }
        
        seperationAve /= boids.Length;
        Vector3 desiredVelocity = (seperationAve * seperationMult) + (OptimalDirection * cohesionMult) * speed;
        rb.velocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
        transform.LookAt(transform.position+rb.velocity);
    }
    public Vector3 Seperation(BoidsFlocking b)
    {
        Vector3 Seperate = (this.transform.position - b.transform.position);
        Seperate.x = (Seperate.x == 0) ? 0 : 1 / (Seperate.x);   //  /seperationDist
        Seperate.y = (Seperate.y == 0) ? 0 : 1 / (Seperate.y);   //  /seperationDist
        Seperate.z = (Seperate.z == 0) ? 0 : 1 / (Seperate.z);   //  /seperationDist
        return Seperate;
    }
    public Vector3 DirectionalCohesion (GameObject b)
    {
        Vector3 directCohesion = (b.transform.position  + (b.transform.forward * directCohesionDist))-this.transform.position;
        return directCohesion;

    }
}
