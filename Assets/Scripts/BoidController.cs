using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{

    public float torque =0.5f;

    public float flockRange = 20;

    private float Xinp;
    private float Yinp;


    void Update()
    {

        GetInputs();
        setPos();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBird();
    }
    private void GetInputs()
    {
        Xinp = Input.GetAxisRaw("Horizontal");
        Yinp = Input.GetAxisRaw("Vertical");
    }
    private void MoveBird()
    {
        this.transform.Rotate(0, 0, -torque * Xinp);
        this.transform.Rotate(torque * Yinp, 0, 0);
        

    }

    private void setPos() 
    {
        BoidsFlocking[] boids = FindObjectsOfType<BoidsFlocking>();
        Vector3 AvePos = Vector3.zero;
        foreach (BoidsFlocking bo in boids)
        {
            if (bo != this)
            {
                //check if each boid is too close to another boid and the direction itr should go

                if ((bo.transform.position - this.transform.position).magnitude < flockRange)
                {
                    AvePos += bo.transform.position;
                }
            }
        }
        AvePos /= boids.Length;

        this.transform.position = AvePos;
    }

}
