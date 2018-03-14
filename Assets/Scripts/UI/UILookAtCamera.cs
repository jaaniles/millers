using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour {
        public Transform WorldSpaceCanvasTransform;

        [SerializeField]
        private bool lockCanvasRotateToYAxis;

        private void Update() {
			Quaternion transform = WorldSpaceCanvasTransform.rotation;
			WorldSpaceCanvasTransform.LookAt(Camera.main.transform);

			if (lockCanvasRotateToYAxis) {
				Quaternion t = WorldSpaceCanvasTransform.rotation;
				t.x = 0;
				t.z = 0;
				WorldSpaceCanvasTransform.rotation = t;
			}
    }
}