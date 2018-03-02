using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    Vector3 startPos;
    public bool open;
    float speed;

    private void Start()
    {
        startPos = this.transform.position;
    }

    public void ToggleDoor()
    {
        if (open)
            speed = 5f;
        else
            speed = -5f;

        open = !open;
    }

    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(startPos, new Vector3(startPos.x, startPos.y - 3.5f, startPos.z), speed * Time.deltaTime);
    }
}
