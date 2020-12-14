using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace odyssey {

    public class ElasticPaltformController : MonoBehaviour {

        public float elasticForce = 20;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.tag.Equals("Player")) {
                Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();

                if (rigidbody2D.velocity.y < 0) {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, elasticForce);
                }

            }


        }
    }

}
