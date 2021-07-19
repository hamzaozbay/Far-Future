using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet1PlatformDissolve : MonoBehaviour {

    public void Disable() {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

}
