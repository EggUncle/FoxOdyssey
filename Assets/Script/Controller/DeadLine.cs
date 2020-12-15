using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace odyssey {

    public class DeadLine : MonoBehaviour {

        private int restartTime = 1;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void trigDeadLine() {
            GameManager.resetGameWithCheckPoint();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag.Equals(OdysseyConstant.player)) {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.GameFailed();

                Invoke(nameof(trigDeadLine), restartTime);
            }
        }

    }
}
