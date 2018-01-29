using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcubeFx : MonoBehaviour {
	[SerializeField]
	SkinnedMeshRenderer skin;
	[SerializeField]
	AnimationCurve CubeToSphere;

	[SerializeField]
	Color A;
	[SerializeField]
	Color B;

	private void Start (){
	StartCoroutine("OrcubeShape");
	}

	private IEnumerator OrcubeShape(){
		while (true) {
			skin.SetBlendShapeWeight (0, CubeToSphere.Evaluate (Time.time) * 100f);
			skin.material.color = Color.Lerp (A, B, CubeToSphere.Evaluate (Time.time));
			yield return null;
		}
	}
}
