using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundEnemy : Enemy {

    public void Attack() {
        anim.SetTrigger("Attack");
    }

}
