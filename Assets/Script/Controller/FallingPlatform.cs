using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    public float fallingTime = 2;

    private TargetJoint2D targetJoint2D;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start() {
        targetJoint2D = GetComponent<TargetJoint2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            Invoke(nameof(fall), fallingTime);
        }
        if (collision.gameObject.tag.Equals("StaticGround")) {
            Destroy(gameObject);
        }
    }

    private void fall() {
        targetJoint2D.enabled = false;
    }

}
