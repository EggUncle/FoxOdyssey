using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace odyssey {

    public class CheckPointController : MonoBehaviour {

        private static Vector3 currentCheckPointPosition;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag.Equals(OdysseyConstant.player)) {
                currentCheckPointPosition = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            }
        }

        public static Vector3 getCurrentCheckPointPosition() {
            return currentCheckPointPosition;
        }

    }
}
