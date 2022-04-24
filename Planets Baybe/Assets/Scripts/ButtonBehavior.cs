using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public int idNum;
    public GameObject main;

    private MainLoop ml;

    // Start is called before the first frame update
    void Start()
    {
        ml = main.GetComponent<MainLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        ml.optionClicked(idNum);
    }

}
