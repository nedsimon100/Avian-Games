using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{

    public float torque =0.5f;

    public float flockRange = 20;

    private float Xinp;
    private float Yinp;

    public bool dead = false;
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
    private void shoot()
    {
        if (Input.GetButtonDown("Space"))
        {
            BoidsFlocking[] boids = FindObjectsOfType<BoidsFlocking>();
            foreach (BoidsFlocking bo in boids)
            {


                if ((bo.transform.position - this.transform.position).magnitude < flockRange)
                {
                    bo.firing = true;
                    return;
                }
                
            }
        }
    }
    private void setPos() 
    {
        BoidsFlocking[] boids = FindObjectsOfType<BoidsFlocking>();
        Vector3 AvePos = Vector3.zero;
        bool noBirds = true;
        int boidsInRange = 0;
        foreach (BoidsFlocking bo in boids)
        {

            if ((bo.transform.position - this.transform.position).magnitude < flockRange)
            {
                AvePos += bo.transform.position;
                noBirds = false;
                boidsInRange++;
            }
            
        }
        flockRange = 10 + boidsInRange * 3;
        if (noBirds)
        {
            //Game Over
        }
        else
        {
            AvePos /= boidsInRange;
        }


        this.transform.position = AvePos;
    }

}
