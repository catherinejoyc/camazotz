using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public Vector3 openPos;
    Vector3 closedPos;
    Vector3 target;
    public bool open;
    public float speed;

    private void Start()
    {
        closedPos = new Vector3(openPos.x, openPos.y - 3f, openPos.z);
        target = this.transform.position;
    }

    public void ToggleDoor()
    {
        if (open)
            target = closedPos;
        else
            target = openPos;

        open = !open;
    }

    private void Update()
    {
        this.gameObject.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    }
}
