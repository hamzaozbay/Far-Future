using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Enemy {

    private void Update() {
        if(canMove) {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
    }

}
