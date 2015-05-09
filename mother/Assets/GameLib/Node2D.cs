/*
*   @author Kyuzen
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// スプライトにくっ付けると色々出来る。。 線形変化するFadeの処理や、全てのNode2Dクラスから2分岐探索などが行えるように(未実装)
/// </summary>
public class Node2D : MonoBehaviour
{
	// 線形変化する値
	LinearValue m_LinearValueAlpha = new LinearValue();
	LinearValue m_LinearValueX = new LinearValue();
	LinearValue m_LinearValueY = new LinearValue();
	LinearValue m_LinearValueScaleX = new LinearValue();
	LinearValue m_LinearValueScaleY = new LinearValue();
	
	public int ID { set { m_ID = value; } get { return m_ID;} }
	int m_ID;
	
	public Color Color { set { m_Color = value; MarkAsChanged(); } get { return m_Color; } }
	public float Alpha { set { if (!Mathf.Approximately(m_Color.a, value)) { m_Color.a = value; MarkAsChanged(); } } get { return m_Color.a; } }
	Color m_Color = Color.white;
	
	/// <summary>
	/// 更新が必要かどうか。
	/// </summary>
	protected bool hasChanged = true;
	
	/// <summary>
	/// ステータス変更。
	/// </summary>
	public void MarkAsChanged()
	{
		hasChanged = true;
	}
	
	/// <summary>
	/// 親ノード
	/// </summary>
	Node2D m_NodeParent;
	
	/// <summary>
	/// 子ノード
	/// </summary>
	public List<Node2D> NodeChildren { get { return m_NodeChildren; } }
	List<Node2D> m_NodeChildren = new List<Node2D>();
	
	/// <summary>
	/// 子ノードを追加。
	/// </summary>
	/// <param name="child"></param>
	public void AddNodeChildren(Node2D child)
	{   // 子ノードに重複していない場合、追加。
		if (!m_NodeChildren.Contains(child)) m_NodeChildren.Add(child);
	}
	
	/// <summary>
	/// 子ノードを削除
	/// </summary>
	/// <param name="child"></param>
	public void RemoveChildren(Node2D child)
	{   // 子ノードに含まれていた場合、追加
		if (m_NodeChildren.Contains(child)) m_NodeChildren.Remove(child);
	}
	
	/// <summary>
	/// トランスフォームのキャッシュ(this.transformだと低速なため)
	/// </summary>
	public Transform CachedTransform { get { if (null == cachedTransform) cachedTransform = this.transform; return cachedTransform; } }
	Transform cachedTransform = null;
	
	/// <summary>
	/// スプライトコンポーネント(アタッチされてない場合はnull)
	/// </summary>
	public SpriteRenderer CachedSpriteRenderer { get { if (null == cachedSpriteRenderer) cachedSpriteRenderer = this.GetComponent<SpriteRenderer>(); return cachedSpriteRenderer; } }
	SpriteRenderer cachedSpriteRenderer = null;
	
	public void Init()
	{
		if (null == cachedTransform) cachedTransform = this.transform;
		if (null == cachedSpriteRenderer) cachedSpriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	/// <summary>
	/// 色を更新
	/// </summary>
	void RefreshColor()
	{
		if (null != CachedSpriteRenderer)
		{
			cachedSpriteRenderer.color = m_Color;
		}   
	}
	
	/// <summary>
	/// フェードイン処理
	/// </summary>
	/// <param name="fadeTime"></param>
	public void FadeIn(float fadeTime)
	{
		m_LinearValueAlpha.Init(fadeTime, 0, Alpha);
		StopCoroutine(ColorTo(false));
		StartCoroutine(ColorTo(false));
	}
	
	/// <summary>
	/// フェードアウト処理
	/// </summary>
	/// <param name="fadeTime"></param>
	/// <param name="isAutoDestroy"></param>
	public void FadeOut(float fadeTime, bool isAutoDestroy = false)
	{
		m_LinearValueAlpha.Init(fadeTime, Alpha, 0);
		StopCoroutine(ColorTo());
		StartCoroutine(ColorTo(isAutoDestroy));
	}
	
	IEnumerator ColorTo(bool isAutoDestroy = false)
	{
		while (!m_LinearValueAlpha.IsEnd())
		{
			m_LinearValueAlpha.IncTime();
			Alpha = m_LinearValueAlpha.GetValue();
			yield return 0; // 次のフレームまで待機。
		}
		if (isAutoDestroy)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
	
	public void MoveTo(Vector3 target, float time, bool isAutoDestroy = false)
	{
		m_LinearValueX.Init(time, CachedTransform.position.x, target.x);
		m_LinearValueY.Init(time, CachedTransform.position.y, target.y);
		StopCoroutine(MoveToTarget());
		StartCoroutine(MoveToTarget(isAutoDestroy));
	}
	
	public void MoveTo(Vector3 target, float time, System.Action<bool> callBack)
	{
		m_LinearValueX.Init(time, CachedTransform.position.x, target.x);
		m_LinearValueY.Init(time, CachedTransform.position.y, target.y);
		StopCoroutine(MoveToTarget());
		StartCoroutine(MoveToTarget(callBack));
	}
	
	IEnumerator MoveToTarget(bool isAutoDestroy = false)
	{
		while (!m_LinearValueX.IsEnd() && !m_LinearValueY.IsEnd())
		{
			m_LinearValueX.IncTime();
			m_LinearValueY.IncTime();
			CachedTransform.position = new Vector3(m_LinearValueX.GetValue(), m_LinearValueY.GetValue(), 0);
			yield return 0; // 次のフレームまで待機。
		}
		if (isAutoDestroy)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
	
	IEnumerator MoveToTarget(System.Action<bool> callBack)
	{
		while (!m_LinearValueX.IsEnd() && !m_LinearValueY.IsEnd())
		{
			m_LinearValueX.IncTime();
			m_LinearValueY.IncTime();
			CachedTransform.position = new Vector3(m_LinearValueX.GetValue(), m_LinearValueY.GetValue(), 0);
			yield return 0; // 次のフレームまで待機。
		}
		callBack(true);
	}
	
	public void ScaleTo(Vector3 target, float time, bool isAutoDestroy = false)
	{
		m_LinearValueScaleX.Init(time, CachedTransform.localScale.x, target.x);
		m_LinearValueScaleY.Init(time, CachedTransform.localScale.y, target.y);
		StopCoroutine(ScaleToTarget());
		StartCoroutine(ScaleToTarget(isAutoDestroy));
	}
	
	IEnumerator ScaleToTarget(bool isAutoDestroy = false)
	{
		while (!m_LinearValueScaleX.IsEnd())
		{
			m_LinearValueScaleX.IncTime();
			m_LinearValueScaleY.IncTime();
			CachedTransform.localScale = new Vector3(m_LinearValueScaleX.GetValue(), m_LinearValueScaleY.GetValue(), 0);
			yield return 0; // 次のフレームまで待機。
		}
		if (isAutoDestroy)
		{
			GameObject.Destroy(this.gameObject);
		}
		
	}
	
	
	
	
	/////////////////////////////////MainMethod//////////////////////////////////////////////
	/// <summary>
	/// 毎フレームの最後の更新
	/// </summary>
	protected virtual void LateUpdate()
	{
		if (hasChanged)
		{
			RefreshColor();
			
			hasChanged = false;
		}
	}
	
	/// <summary>
	/// 有効になったとき
	/// </summary>
	protected virtual void OnEnable()
	{
		MarkAsChanged();
	}
	
	/// <summary>
	/// インスペクターから値が変更されたとき
	/// </summary>
	protected virtual void OnValidate()
	{
		MarkAsChanged();
	}
	
}

public class iTweenEx
{	// (EX == extend)
	public static void MoveTo(GameObject gameobject, Vector3 target, float time, bool isAutoDestroy = false)
	{
		Node2D node = gameobject.AddComponent<Node2D>();
		node.MoveTo(target, time, false);
	}
}