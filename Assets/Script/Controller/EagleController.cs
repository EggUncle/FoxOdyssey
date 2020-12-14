using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace odyssey {
    public class EagleController : EnemyController {

        private float topY;
        private float bottomY;

        public float speed = 100;

        private bool moveUp;
        private Rigidbody2D rigidbody2;
        private Animator animator;
        private CircleCollider2D circleCollider;

        public override void death() {
            Destroy(gameObject);
        }

        public override void Move() {
            if (transform.position.y > topY) {
                moveUp = false;
            } else if (transform.position.y < bottomY) {
                moveUp = true;
            }
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, speed * Time.fixedDeltaTime * (moveUp ? 1 : -1));
        }

        public override void underAttacked() {
            circleCollider.enabled = false;
            rigidbody2.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger(OdysseyConstant.statsDeath);
        }

        // Start is called before the first frame update
        void Start() {
            Transform topTransform = gameObject.transform.GetChild(0);
            Transform bottomTransform = gameObject.transform.GetChild(1);

            topY = topTransform.position.y;
            bottomY = bottomTransform.position.y;

            Destroy(topTransform.gameObject);
            Destroy(bottomTransform.gameObject);

            rigidbody2 = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            circleCollider = GetComponent<CircleCollider2D>();

        }

        // Update is called once per frame
        void Update() {

        }

    }
}
