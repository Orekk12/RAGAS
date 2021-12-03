using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("- - - References - - -")]
    [SerializeField] gameState gameState;
    Text txt;
    void Start()
    {
        txt = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = gameState.get_score().ToString();
    }
}
