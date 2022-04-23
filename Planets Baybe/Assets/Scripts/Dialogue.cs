using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public List<Sentence> sentence;
    public Sprite currentImage;
    public Sprite currentBackground;

    public Dialogue firstOption;
    public Dialogue secondOption;
    public Dialogue thirdOption;
}
