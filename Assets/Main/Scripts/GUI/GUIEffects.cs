using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIEffects:MonoBehaviour {
    
    public Text m_Text,m_TextShadow;

    public GameObject m_StartTrans,m_EndTrans;
    public GameObject m_EndTransVaule;
    bool StartTween, EndTween;

    float time;

    void Start()
    {
        m_EndTransVaule = m_EndTrans;
        StartTween = true;
        EndTween = false;
    }

    public void ScaleText()
    {
        m_Text.transform.localScale = Vector3.Lerp(m_Text.transform.localScale, m_EndTransVaule.transform.localScale, 0.5f);
        print(m_Text.transform.localScale.x + " , " + (m_EndTransVaule.transform.localScale.x - 0.2f));


        if ((m_Text.transform.localScale.x > m_EndTransVaule.transform.localScale.x -0.2) && StartTween)
        {
            EndTween = true;
            StartTween = false;
            m_EndTransVaule = m_StartTrans;
        }
        else if ((m_Text.transform.localScale.x < m_EndTransVaule.transform.localScale.x + 0.2) && EndTween)
            {
                EndTween = false;
                StartTween = true;
                m_EndTransVaule = m_EndTrans;

        }
        time = Time.deltaTime;
    }
}
