/*
*   @author Kyuzen
*/
using UnityEngine;
using System.Collections;

/// <summary>
/// カメラのアスペクト比を変更する
/// 基本となる画面サイズを設定すれば自動で計算する。 
/// Iphone4Sの場合 width = 640 height = 960
/// </summary>
public class AdvCameraFitter : MonoBehaviour
{

    /// <summary>
    /// インスペクター上で設定
    /// </summary>
    public float m_Width = 1920f;

    /// <summary>
    /// インスペクター上で設定
    /// </summary>
    public float m_Height = 1080f;
    
    float aspectRate;
    Camera comCamera;
    static Camera m_BackGroundCamera = null;

    void Start()
    {
        comCamera = Camera.main;

        InitAspectRate();

        UpdateScreenRate();
        CreateBackgroundCamera();
    }

    /// <summary>
    /// 背景用マスク これがないと描画にバグがでる
    /// </summary>
    void CreateBackgroundCamera()
    {
#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlaying)
        { // エディターのとき、自動生成を防ぐ
            return;
        }
#endif

        if (m_BackGroundCamera != null)
        {   // 既に生成されている場合、処理しない
            return;
        }

        GameObject backGroundCameraObject = new GameObject("BackGround Mask");
        m_BackGroundCamera = backGroundCameraObject.AddComponent<Camera>();
        m_BackGroundCamera.depth = -0xff;
        m_BackGroundCamera.fieldOfView = 1;
        m_BackGroundCamera.farClipPlane = 1.1f;
        m_BackGroundCamera.nearClipPlane = 1;
        m_BackGroundCamera.cullingMask = 0;
        m_BackGroundCamera.depthTextureMode = DepthTextureMode.None;
        m_BackGroundCamera.backgroundColor = Color.black;
        m_BackGroundCamera.renderingPath = RenderingPath.VertexLit;
        m_BackGroundCamera.clearFlags = CameraClearFlags.SolidColor;
        m_BackGroundCamera.useOcclusionCulling = false;
        backGroundCameraObject.hideFlags = HideFlags.NotEditable;
    }
    
    /// <summary>
    /// アスペクト比を計算してカメラに反映
    /// </summary>
    void UpdateScreenRate()
    {
        float baseAspect = m_Height / m_Width;
        float nowAspect = (float)Screen.height / Screen.width;

        if (baseAspect > nowAspect)
        {
            var changeAspect = nowAspect / baseAspect;
            comCamera.rect = new Rect((1 - changeAspect) * 0.5f, 0, changeAspect, 1);
        }
        else
        {
            var changeAspect = baseAspect / nowAspect;
            comCamera.rect = new Rect(0, (1 - changeAspect) * 0.5f, 1, changeAspect);
        }
    }

    /// <summary>
    /// 実行中にアスペクト比が変わったかどうか
    /// </summary>
    /// <returns>変更がない場合false 変更があったらtrueを返す</returns>
    bool IsChangeAspect() {
        return comCamera.aspect == aspectRate;
    }

    void Update()
    {
        if (IsChangeAspect())
        {
            return;
        }

        UpdateScreenRate();
        comCamera.ResetAspect();
    }

    /// <summary>
    /// アスペクト比を計算
    /// </summary>
    public void InitAspectRate()
    {
        aspectRate = (float)m_Width / m_Height;
    }
    
}
