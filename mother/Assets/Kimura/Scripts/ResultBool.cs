using UnityEngine;
using System.Collections;

public class ResultBool : SingletonMonoBehaviourFast<ResultBool> {

	private bool success;

	protected override void Awake() {
		CheckInstance ();

	}

	// ゲームのクリアフラグ(true : 成功, false : 失敗)
	public void SetSuccessFlag(bool flag) {
		success = flag;
	}

	// フラグの取得
	public bool IsSuccess() {
		return success;
	}
}
