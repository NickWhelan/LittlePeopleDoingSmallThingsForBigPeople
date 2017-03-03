using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIEffects:MonoBehaviour {
    
    public Text m_Text,m_TextShadow;

    public GameObject m_StartTrans,m_EndTrans;

    bool StartTween, EndTween;

    float time;

    void Start()
    {
        m_StartTrans = new GameObject();
        m_EndTrans = new GameObject();
        StartTween = true;
        EndTween = false;
    }

    public void ScaleText() {
        if (m_Text.transform.localScale.x < m_EndTrans.transform.localScale.x - 0.05 && StartTween)
            m_Text.transform.localScale = Vector3.Lerp (m_Text.transform.localScale, m_EndTrans.transform.localScale, time);
        else
        {
            EndTween = true;
            StartTween = false;
        }

        if (m_Text.transform.localScale.x > m_StartTrans.transform.localScale.x + 0.05 && EndTween)
            m_Text.transform.localScale = Vector3.Lerp(m_Text.transform.localScale, m_StartTrans.transform.localScale,time);
        else
        {
            EndTween = false;
            StartTween = true;
        }

        time = Time.deltaTime;
    }
}
