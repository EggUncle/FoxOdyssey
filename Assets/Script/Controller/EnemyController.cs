using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace odyssey {
    public abstract class EnemyController : MovableObject {
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public abstract void underAttacked();
        public abstract void death();

    }
}
