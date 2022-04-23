using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainLoop : MonoBehaviour
{
    public Dialogue currentDialog;

    public TMP_Text currentComment;
    public TMP_Text firstButtonText;
    public TMP_Text secondButtonText;
    public TMP_Text thirdButtonText;

    // Start is called before the first frame update
    void Start()
    {
        updateGUI();
    }

    // this will be called regularly
    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void optionClicked(int number)
    {
        // update our dialog properly
        switch(number) 
        {
            case 1:
                currentDialog = currentDialog.firstOption;
                break;
            case 2:
                currentDialog = currentDialog.secondOption;
                break;
            case 3:
                currentDialog = currentDialog.thirdOption;
                break;
        }

        updateGUI();
    }

    public void updateGUI()
    {
        // lets update the text in the button fields
        firstButtonText.text = currentDialog.responses[0];
        secondButtonText.text = currentDialog.responses[1];
        thirdButtonText.text = currentDialog.responses[2];

        // lets update the writing in the button fields
        renderSentence(currentDialog.getCurSentence());
    }

    public void renderSentence(string sent)
    {
        currentComment.text = sent;
    }
}
