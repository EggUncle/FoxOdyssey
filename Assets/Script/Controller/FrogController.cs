using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace odyssey {

    public class FrogController : EnemyController {
        public float speed;
        public float jumpforce;

        private float leftX;
        private float rigthX;
        private bool faceLeft = true;

        private Rigidbody2D rigidbody2;
        private Animator animator;
        private CircleCollider2D circleCollider;

        private LayerMask groundLayer;


        // Start is called before the first frame update
        void Start() {
            Transform leftTransform = gameObject.transform.GetChild(0);
            Transform rightTransform = gameObject.transform.GetChild(1);

            leftX = leftTransform.position.x;
            rigthX = rightTransform.position.x;

            Destroy(leftTransform.gameObject);
            Destroy(rightTransform.gameObject);

            rigidbody2 = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            circleCollider = GetComponent<CircleCollider2D>();
            groundLayer = LayerMask.GetMask(OdysseyConstant.layerMaskGround);
        }

        // Update is called once per frame
        void Update() {

        }

        public void FixedUpdate() {
            base.FixedUpdate();
            animControl();
        }
        int i = 0;
        private void moveWithJump() {
            if (circleCollider.IsTouchingLayers(groundLayer)) {
                if (transform.position.x < leftX) {
                    faceLeft = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                } else if (transform.position.x > rigthX) {
                    faceLeft = true;
                    transform.localScale = new Vector3(1, 1, 1);
                }
                int direction = faceLeft ? -1 : 1;
                rigidbody2.velocity = new Vector2(speed * direction, jumpforce);

            }
        }

        private void animControl() {
            if (circleCollider.IsTouchingLayers(groundLayer)) {
                //idle
                animator.SetBool(OdysseyConstant.statsJumping, false);
                animator.SetBool(OdysseyConstant.statsFalling, false);
                animator.SetBool(OdysseyConstant.statsIdle, true);
            } else {
                animator.SetBool(OdysseyConstant.statsIdle, false);
            }

            if (rigidbody2.velocity.y < 0) {
                //falling
                animator.SetBool(OdysseyConstant.statsJumping, false);
                animator.SetBool(OdysseyConstant.statsFalling, true);
            } else if (rigidbody2.velocity.y > 0) {
                //jumping
                animator.SetBool(OdysseyConstant.statsJumping, true);
                animator.SetBool(OdysseyConstant.statsFalling, false);
            }
        }

        public override void Move() {
            //moveWithJump();
            //active move action 
        }

        public override void underAttacked() {
            circleCollider.enabled = false;
            rigidbody2.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger(OdysseyConstant.statsDeath);
        }

        public override void death() {
            Destroy(gameObject);
        }


    }

}