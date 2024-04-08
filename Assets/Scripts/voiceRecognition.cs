using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class voiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private LogisticsManagementScript logisticsManagementScript;


    // Start is called before the first frame update
    void Start()
    {
        actions.Add("restart", call_reset_ball);
        actions.Add("reset", call_reset_ball);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += recognizedSpeech;
        keywordRecognizer.Start();

        logisticsManagementScript = transform.GetComponentInParent<LogisticsManagementScript>();
    }

    private void recognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void call_reset_ball()
    {
        if (logisticsManagementScript.get_ballHitForReset()) { logisticsManagementScript.restart_ball_position(); }
    }
   
}
