using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerController : MonoBehaviour {
    public float speed;
    public delegate void metodoDelegado();
    public event metodoDelegado EventoA;
    public event metodoDelegado EventoB;
    public event metodoDelegado collisionWithBObject;

    public int health;
    public Transform tf;
    public Rigidbody rb;

    void Start()
    {
      health = 100;
      speed = 2.0f;
      tf = GetComponent<Transform>();
      rb = GetComponent<Rigidbody>();
    }

    void Update() {
    }

    void FixedUpdate() {
      Vector3 xVector = tf.right.normalized * speed * Time.deltaTime;
      Vector3 zVector = tf.forward.normalized * speed * Time.deltaTime;

     if (Input.GetKey("w"))
        rb.MovePosition(tf.position + zVector);
      if (Input.GetKey("s"))
        rb.MovePosition(tf.position - zVector);
      if (Input.GetKey("d"))
        rb.MovePosition(tf.position + xVector);
      if (Input.GetKey("a"))
        rb.MovePosition(tf.position - xVector);

      // Rotation
      if (Input.GetKey("e")) {
        Quaternion deltaRotation = Quaternion.AngleAxis(speed, tf.up);
        rb.MoveRotation(deltaRotation * tf.rotation);
      }
      if (Input.GetKey("q")) {
        Quaternion deltaRotation = Quaternion.AngleAxis(-speed, tf.up);
        rb.MoveRotation(deltaRotation * tf.rotation);
      }
    }

    void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.name == "DroneA")
       EventoA();
      if (collision.gameObject.name == "DroneB")
        EventoB();
      if (collision.gameObject.tag == "B")
        collisionWithBObject();
    }
}
