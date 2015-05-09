using UnityEngine;
using System.Collections;

public class ResultBool : SingletonMonoBehaviourFast<ResultBool> {

	private bool success;

	public void SetSuccessFlag(bool flag) {
		success = flag;
	}

	public bool IsSuccess() {
		return success;
	}
}
