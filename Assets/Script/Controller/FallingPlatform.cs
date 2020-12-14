using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    public float fallingTime = 2;
    private float restoreTime = 5;

    private TargetJoint2D targetJoint2D;
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rigidbody2D;

    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start() {
        targetJoint2D = GetComponent<TargetJoint2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        initPosition = new Vector3(transform.position.x, transform.position.y);
    }

    void restore() {
        gameObject.SetActive(true);
        targetJoint2D.enabled = true;
        gameObject.transform.position = initPosition;
    }

    void setDisable() {
        gameObject.SetActive(false);
        Invoke(nameof(restore), restoreTime);
    }

    // Update is called once per frame
    void Update() {
        if (rigidbody2D.velocity.y < -5) {
            setDisable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            Invoke(nameof(fall), fallingTime);
        }
        if (collision.gameObject.tag.Equals("StaticGround") || collision.gameObject.tag.Equals("FalingPlatform")) {
            setDisable();
        }
    }

    private void fall() {
        targetJoint2D.enabled = false;
    }

}
