using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody body;
    public Animator animator;

    bool running = false;
    float heading = 0;
    float targetHeading = 0;
    float tilt = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dT = Time.deltaTime;

        // Jumping
        if (isJump()) {
            body.velocity = new Vector3(body.velocity.x, 25.0f, body.velocity.z);
            animator.SetTrigger("jump");
        }

        animator.SetBool("running", running);
        heading = angleClamp(heading + angleClamp(targetHeading - heading) * 9.0f * dT);
        tilt += (angleClamp(targetHeading - heading) - tilt) * 8.0f * dT;
        body.MoveRotation(Quaternion.AngleAxis(heading * Mathf.Rad2Deg, Vector3.up));
    }

    void FixedUpdate() {
        float dT = Time.fixedDeltaTime;

        // Walking
        float sx = getX();
        float sy = getY();
        float mag = Mathf.Sqrt(sx * sx + sy * sy);
        if (mag > 0.3f) {
            if (mag > 1) {
                sx = sx / mag;
                sy = sy / mag;
            }
            float SPEED = 22;
            float k = 7;
            running = true;
            Vector3 camDir = Camera.main.transform.forward;
            float camAngle = Mathf.Atan2(camDir.x, camDir.z);
            body.velocity += (
                Quaternion.AngleAxis(camAngle * Mathf.Rad2Deg, Vector3.up) * new Vector3(sx * SPEED, 0, sy * SPEED)
                - new Vector3(body.velocity.x, 0, body.velocity.z)
            ) * (k * dT);
            targetHeading = Mathf.Atan2(sx, sy) + camAngle;
        } else {
            float k = 8;
            running = false;
            body.velocity += (new Vector3(
                (- body.velocity.x),
                0,
                (- body.velocity.z)
            ) * (k * dT));
        }
    }

    Quaternion getHeading() {
        return Quaternion.AngleAxis(heading * Mathf.Rad2Deg + 90, Vector3.up);
    }

    float angleClamp(float val) {
        if (val > Mathf.PI) {
            val -= Mathf.PI * 2;
        }
        if (val < -Mathf.PI) {
            val += Mathf.PI * 2;
        }
        return val;
    }

    float getX() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            return -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            return 1;
        }
        return 0;
    }

    float getY() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            return 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            return -1;
        }
        return 0;
    }

    bool isJump() {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
