using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour {

    public bool InTrigger;
    public bool open;
    public Transform Pivot;
    float CurAngle;
    public float Angle = 140;
    public bool IsMove;
    bool collisionPlayer;
    public float Speed = 5;

    public void Action()
    {
        if(!collisionPlayer)
            open = !open;
    }

    public void Update()
    {
        if (open)
        {
            if (CurAngle > -Angle)
            {
                Rotation(-Speed * Time.deltaTime);
            }
            else if (IsMove)
            {
                IsMove = false;
                GetComponent<Collider>().isTrigger = false;
            }
        }
        else
        {
            if(CurAngle < 0)
            {
                Rotation(Speed * Time.deltaTime);
            }
            else if (IsMove)
            {
                IsMove = false;
                GetComponent<Collider>().isTrigger = false;
            }
        }

        collisionPlayer = false;
    }

    void Rotation(float value)
    {
        if (!IsMove)
        {
            IsMove = true;
            GetComponent<Collider>().isTrigger = true;
        }
        CurAngle += value;
        Pivot.localEulerAngles = new Vector3(0, CurAngle, 0);
    }

    public void OnCollision(Vector3 posPayer)
    {
        

        //collisionPlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Untagged")
            open = !open;
    }
}