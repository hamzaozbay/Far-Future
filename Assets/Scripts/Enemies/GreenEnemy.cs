using System.Collections;
using UnityEngine;

public class GreenEnemy : Enemy {


	private void FixedUpdate() {
		if (canMove && !dead) {
			transform.position += transform.right * speed * Time.deltaTime;
		}
	}

	public void Move() {
		StartCoroutine(MoveRandomTime());
	}

	private IEnumerator MoveRandomTime() {
		yield return new WaitForSeconds(Random.Range(0f, 0.75f));
		canMove = true;

		anim.Play("Walk", 0, 0f);
		RotateToPlayer();
	}
	

}
		


