using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Enemy {
    
    private void Update() {
        transform.position += -transform.right * speed * Time.deltaTime;       
    }


}
