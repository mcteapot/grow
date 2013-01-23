//
// Author:
//   Andreas Suter (andy@edelweissinteractive.com)
//
// Copyright (C) 2011-2012 Edelweiss Interactive (http://edelweissinteractive.com)
//

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animation))]
public class DestroyOnFinish : MonoBehaviour {

	private void Update () {
		if (!animation.isPlaying) {
			Destroy (gameObject);
		}
	}
}
