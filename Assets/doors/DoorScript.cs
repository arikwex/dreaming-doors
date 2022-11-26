using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator animator;

    public void openDoor() {
        animator.SetBool("open", true);
    }

    public void closeDoor() {
        animator.SetBool("open", false);
    }
}
