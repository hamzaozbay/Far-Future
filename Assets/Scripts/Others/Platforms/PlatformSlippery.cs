using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSlippery : PlatformHorizontal {

    [Range(0f, 1f)] [SerializeField] private float _slipAmount = .2f;

    private CharacterController2D _controller2D;



    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if (_controller2D == null) _controller2D = GameManager.instance.player.GetComponent<CharacterController2D>();

        if (other.gameObject.CompareTag("Player")) {
            _controller2D.SlipperyOn(_slipAmount);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if (other.gameObject.CompareTag("Player")) {
            _controller2D.SlipperyOff();
        }
    }


}
