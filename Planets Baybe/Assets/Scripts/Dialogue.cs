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

    public List<Dialogue> options;

    public Sprite currentImage;
    public Sprite currentBackground;

}
