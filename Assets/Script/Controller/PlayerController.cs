using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace odyssey {
    public class PlayerController : MovableObject {
        private Rigidbody2D rb;
        private Animator anim;

        private LayerMask ground;
        private BoxCollider2D headColider;
        private CircleCollider2D bodyColider;
        private GameObject headTop;


        public float speed;
        public float jumpforce;

        public GameObject dialogPanel;

        private Text cherryNumText;

        private bool crouch;
        private bool isCrouch;
        private bool isHurt;

        private long debugNum = 0;

        public int cherry = 0;

        private bool jumpKeyPressed;

        // Start is called before the first frame update
        void Start() {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            headColider = GetComponent<BoxCollider2D>();
            bodyColider = GetComponent<CircleCollider2D>();
            headTop = GameObject.Find("headTop");
            if (headTop != null) {
                Debug.Log("headTop");
            } else {
                Debug.Log("headTop is null");
            }
          
            ground = LayerMask.GetMask(OdysseyConstant.layerMaskGround);
            cherryNumText = GameObject.Find("CherryNum").GetComponent<Text>();
        }

        // Update is called once per frame
        void FixedUpdate() {
            base.FixedUpdate();
            SwitchAnim();
        }


        private void Update() {
            if (Input.GetButtonDown(OdysseyConstant.buttonJump) && bodyColider.IsTouchingLayers(ground)) {
                jumpKeyPressed = true;
            }
            //if (Input.GetButtonDown("Crouch")) {
            //    crouch = true;
            //} else if (Input.GetButtonUp("Crouch")) {
            //    crouch = false;
            //}
        }

        public override void Move() {
            if (isHurt) {
                return;
            } 
                //anim.SetBool(OdysseyConstant.statsHurt, false);
                //anim.SetBool(OdysseyConstant.statsIdle, true);
            


            float horizontalMove = Input.GetAxis(OdysseyConstant.inputHorizontal);
            float faceDircetion = horizontalMove;
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);

            if (horizontalMove != 0) {
                if (faceDircetion > 0) {
                    faceDircetion = 1;
                } else {
                    faceDircetion = -1;
                }
                transform.localScale = new Vector3(faceDircetion, 1, 1);
            }

            anim.SetFloat("running", Mathf.Abs(faceDircetion));
            //Debug.Log(Mathf.Abs(faceDircetion));

            if (jumpKeyPressed && bodyColider.IsTouchingLayers(ground)) {
                jump();
            }

            //debugNum++;
            //Debug.Log(debugNum+"  "+ horizontalMove+" "+ rb.velocity.x);
        }

        private void jump() {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpKeyPressed = false;
            anim.SetBool("jumping", true);
        }

        void SwitchAnim() {
            //Debug.Log(debugNum++ + "SwitchAnim");
            anim.SetBool("idle", false);
            if (rb.velocity.y < float.Epsilon) {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
            if (bodyColider.IsTouchingLayers(ground)) {
                anim.SetBool("falling", false);
                anim.SetBool("idle", true);
            }
            if (isHurt) {
                if (!anim.GetBool(OdysseyConstant.statsHurt)) {
                    anim.SetBool(OdysseyConstant.statsHurt, true);
                }
                if ( Mathf.Abs(rb.velocity.x) < float.Epsilon) {
                    anim.SetBool(OdysseyConstant.statsIdle, true);
                    anim.SetBool(OdysseyConstant.statsHurt, false);
                    isHurt = false;
                }
            }

            if (crouch && !isCrouch) {
                anim.SetBool("idle", false);
                anim.SetBool("crouch", true);
                isCrouch = true;
                headColider.enabled = false;
            } else if (!crouch && Physics2D.OverlapCircle(headTop.transform.position, 0.1f, ground)) {
                anim.SetBool("crouch", false);
                if (anim.GetFloat("running") < 0.1) {
                    anim.SetBool("idle", true);
                }
                isCrouch = false;
                headColider.enabled = true;
            }
            //else if (rb.velocity.y == 0) {
            //    anim.SetBool("falling", false);
            //    anim.SetBool("idle", true);
            //}

        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Debug.Log("OnTriggerEnter2D");
            if (collision.tag.Equals("Collection")) {
                cherry++;
                cherryNumText.text = cherry + "";
                //Debug.Log("collection "+ cherry);
                Destroy(collision.gameObject);
            }
            if (collision.name.Equals("house")) {
                dialogPanel.SetActive(true);
            }
            if (collision.name.Equals("DeadLine")) {
                Invoke("Restart", 1);
            }
        }

        private void attackEnemy(EnemyController enemy) {
            enemy.underAttacked();
            //step on enemy, then jump
            jump();
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            //step on enemy
            if (collision.gameObject.tag.Equals(OdysseyConstant.tagEnemy)) {
                if (anim.GetBool(OdysseyConstant.statsFalling)) {
                    EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                    attackEnemy(enemy);
                } else {
                    //hurt
                    isHurt = true;
                    if (transform.position.x < collision.gameObject.transform.position.x) {
                        rb.velocity = new Vector2(-10, rb.velocity.y);
                    } else {
                        rb.velocity = new Vector2(10, rb.velocity.y);
                    }
                    anim.SetBool(OdysseyConstant.statsHurt, true);
                    //anim.SetBool(OdysseyConstant.statsIdle,false);

                }
            }
        }

        public void restore() {
            isHurt = false;
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.name.Equals("house")) {
                dialogPanel.SetActive(false);
            }
        }

        private void Restart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}