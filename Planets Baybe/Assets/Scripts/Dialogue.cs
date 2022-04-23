using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [TextArea]
    public List<string> sentences;
    
    [TextArea]
    public List<string> responses;

    public Sprite currentImage;
    public Sprite currentBackground;

    public Dialogue firstOption;
    public Dialogue secondOption;
    public Dialogue thirdOption;

    public int curSentence = 0;

    public string getCurSentence()
    {
        string retVal = sentences[curSentence];
        if(curSentence < sentences.Count - 1)
            curSentence++;
        return retVal;

    }
}
