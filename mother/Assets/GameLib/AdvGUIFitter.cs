/*
*   @author Kyuzen
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// OnGUI上でアスペクト比を設定したいときに使う
/// </summary>
public class AdvGUIFitter
{
    static float m_Width = 1920.0f;
    static float m_Height = 1080.0f;

    static List<Matrix4x4> stack = new List<Matrix4x4>();

    public static void BeginGUI()
    {
        stack.Add(GUI.matrix);
        Matrix4x4 m = new Matrix4x4();
        float w = (float)Screen.width;
        float h = (float)Screen.height;
        float aspect = w / h;
        float scale = 1f;
        Vector3 offset = Vector3.zero;
        if (aspect < (m_Width / m_Height))
        {
            scale = (Screen.width / m_Width);
            offset.y += (Screen.height - (m_Height * scale)) * 0.5f;
        }
        else
        {
            scale = (Screen.height / m_Height);
            offset.x += (Screen.width - (m_Width * scale)) * 0.5f;
        }
        m.SetTRS(offset, Quaternion.identity, Vector3.one * scale);
        GUI.matrix *= m;
    }

    public static void EndGUI()
    {
        GUI.matrix = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
    }

    public static float Width { get { return m_Width; }  set { m_Width = value; } }

    public static float Height { get { return m_Height; } set { m_Height = value; } }

    /// <summary>
    /// アスペクト比の変更 画面サイズをセットすればいい
    /// </summary>
    /// <param name="width">横の長さ</param>
    /// <param name="height">縦の長さ</param>
    public static void SetScreenSize(float width, float height)
    {
        m_Width = width;
        m_Height = height;
    }

}