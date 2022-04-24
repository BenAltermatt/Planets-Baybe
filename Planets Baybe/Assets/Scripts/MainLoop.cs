using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainLoop : MonoBehaviour
{
    public Dialogue currentDialog;

    public TMP_Text currentComment;
    public GameObject responseOptionSelect;
    public GameObject responseButton;
    public GameObject talkingPanel;
    public GameObject respondingPanel;
    public GameObject backgroundSprite;
    public GameObject characterSprite;

    // these manaage the game logic and are more hidden and only 
    // really useful to this script
    private int curSentence = 0;
    private bool writing = false;
    private const float STDWRITE = .05F;
    private const float FASTWRITE = .01F;
    private float writeTime = STDWRITE;
    private const float animTimeFrames = .01F; 
    private float speedModifier = .0015F;
    private const float startX = -100.1011F;
    private const float endX = 400F;
    private const float midPoint = startX + (endX - startX) / 2;


    // this keeps track of the buttons we have in use for options
    private List<GameObject> buttons = new List<GameObject>();
    private Planet oldPlan;

    // Start is called before the first frame update
    void Start()
    {
        oldPlan = currentDialog.currentCharacter;
        updateGUI();
    }

    // this will be called regularly
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(writing)
            {
                writeTime = FASTWRITE;
            }
            else
                renderSentence();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void optionClicked(int number)
    {
        // update our dialog properly
        Planet oldPlan = currentDialog.currentCharacter;

        currentDialog = currentDialog.options[number];

        if(oldPlan != currentDialog.currentCharacter) // character changed
        {
            StartCoroutine(SwapCharacter());
        }

        for(int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }

        buttons = new List<GameObject>();
        curSentence = 0;
        updateGUI();
    }

    public void updateGUI()
    {
        // lets update the text in the button fields
        /*
        firstButtonText.text = currentDialog.responses[0];
        secondButtonText.text = currentDialog.responses[1];
        thirdButtonText.text = currentDialog.responses[2];
        */
        float buttonHeight = responseButton.GetComponent<RectTransform>().rect.height;

        responseOptionSelect.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
        (buttonHeight + 2) * currentDialog.responses.Count);

        // we need to make our buttons in our selection screen
        for(int i = 0; i < currentDialog.responses.Count; i++)
        {
            // lets start off by getting the transform of the height
            buttons.Add(Instantiate(responseButton, new Vector3(0, 0, 0), Quaternion.identity, responseOptionSelect.transform));
            buttons[i].GetComponentInChildren<TMP_Text>().text = currentDialog.responses[i];
            ButtonBehavior script = buttons[i].GetComponent<ButtonBehavior>();
            int idx = i;
            buttons[i].GetComponent<Button>().onClick.AddListener(() => optionClicked(idx));

        }


        backgroundSprite.GetComponent<Image>().sprite = currentDialog.currentBackground;
        if(currentDialog.currentCharacter == oldPlan)
            updateCharModel(null);
        // lets update the writing in the button fields
        respondingPanel.SetActive(false);
        renderSentence();
    }

    public void renderSentence()
    {
        writeTime = STDWRITE;
        writing = true;
        
        if(curSentence < currentDialog.sentences.Count)
        {
            StartCoroutine(RenderText(currentDialog.sentences[curSentence]));
            curSentence++;
        }
    }

    private void updateCharModel(string emote)
    {
        if(emote != null)
            currentDialog.currentEmote = emote;
        characterSprite.GetComponent<Image>().sprite = currentDialog.currentCharacter.getEmote(currentDialog.currentEmote);  
    }

    IEnumerator SwapCharacter()
    {
        RectTransform currentTrans = characterSprite.GetComponent<RectTransform>();


        while(currentTrans.offsetMax.x < endX - .01)
        {
            currentTrans.Translate(new Vector3(-1 * speedModifier * (endX - currentTrans.offsetMax.x) * (startX - currentTrans.offsetMax.x), 0, 0));
            yield return new WaitForSeconds(animTimeFrames);
        }
        currentTrans.Translate(new Vector3(endX - currentTrans.offsetMax.x, 0, 0));
        oldPlan = currentDialog.currentCharacter;
        updateCharModel(null);
        while(currentTrans.offsetMax.x > startX + .01)
        {
            currentTrans.Translate(new Vector3(speedModifier * (endX - currentTrans.offsetMax.x) * (startX - currentTrans.offsetMax.x), 0, 0));
            yield return new WaitForSeconds(animTimeFrames);
        }
        currentTrans.Translate(new Vector3(startX - currentTrans.offsetMax.x, 0, 0));
    }


    IEnumerator RenderText(string textToRender)
    {
        if(currentDialog.currentEmote == "base" && oldPlan == currentDialog.currentCharacter)
            updateCharModel("talking");

        for(int i = 1; i < textToRender.Length + 1; i++)
        {
            yield return new WaitForSeconds(writeTime);
            currentComment.text = textToRender.Substring(0, i);
        }

        if(currentDialog.currentEmote == "talking")
            updateCharModel("base");

        if(curSentence == currentDialog.sentences.Count && oldPlan == currentDialog.currentCharacter)
            respondingPanel.SetActive(true);
        
        writing = false;
    }

}
