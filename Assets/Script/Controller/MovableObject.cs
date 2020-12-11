using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace odyssey {
    public abstract class MovableObject : MonoBehaviour {

        public abstract void Move();

        public void FixedUpdate() {
            Move();
        }


    }
}
