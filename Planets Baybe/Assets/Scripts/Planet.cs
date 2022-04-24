using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Planet : ScriptableObject
{
    public List<Sprite> emotes;

    public Sprite getEmote(string name) 
    {
        switch(name) 
        {
            case "base":
                return emotes[0];
            case "talking":
                return emotes[1];
            case "happy":
                return emotes[2];
            case "disgust":
                return emotes[3];
            default:
                return emotes[0];
        }
    }
}
