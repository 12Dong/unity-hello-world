using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    class Dialogue
    {
        public int dialogueId;
        public string characterName;
        public string characterIcon;
        public string dialogue;
        public List<int> toDialogueIdList;

        public Dialogue(int dialogueId, string characterName, string characterIcon, string dialogue, List<int> toDialogueIdList)
        {
            this.dialogueId = dialogueId;
            this.characterName = characterName;
            this.characterIcon = characterIcon;
            this.dialogue = dialogue;
            this.toDialogueIdList = toDialogueIdList;
        }
    }

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;

    private static string dialogueFilePrefix = "Dialogue/event_dialogue_";
    private static string characterIconPrefix = "Character/";
    private Dictionary<int, Dialogue> dialogueDictionary = new Dictionary<int, Dialogue>();

    private int currentDialogueIndex = 1;

    public Image dialogueMutliBG;
    public Button optionButton;
    public Button nextButton;

    public int lastEvent;

    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eventDialogueFileProcess(int eventId)
    {   
        transform.gameObject.SetActive(true);
        // 文件处理成 对话 id 和 对话对象 的映射
        if(!lastEvent.Equals(eventId))
        {
            dialogueDictionary.Clear();
            string filePath = dialogueFilePrefix + eventId;
            TextAsset textFile = Resources.Load<TextAsset>(filePath);
            processDialogueFile(textFile);
        }

        // 显示第一段对话
        List<int> dialogueIdList = new List<int>(1);
        dialogueIdList.Add(1);
        showDialogue(dialogueIdList);
        lastEvent = eventId;
    }

    private void processDialogueFile(TextAsset dialogueFile)
    {
        string[] dialogueArrays = dialogueFile.text.Split("\r\n");
        foreach(string strDialogue in dialogueArrays)
        {
            if(string.IsNullOrEmpty(strDialogue))
            {
                continue;
            }
            string[] cols = strDialogue.Split(',');
            string[] strToDialogueIds = cols[4].Split('|');
            List<int> toDialogueIdList = new List<int>();
            foreach(string strToDialogueId in strToDialogueIds)
            {   
                if(string.IsNullOrEmpty(strToDialogueId))
                {
                    break;
                }
                Debug.Log(strToDialogueId);
                toDialogueIdList.Add(int.Parse(strToDialogueId));
            }
            Dialogue dialogue = new Dialogue(int.Parse(cols[0]), cols[1], cols[2], cols[3], toDialogueIdList);
            dialogueDictionary.Add(dialogue.dialogueId, dialogue);
        }
    }

    private void showDialogue(List<int> dialogueIdList)
    {
        if(dialogueIdList.Count == 0)
        {
            return;
        }

        if(dialogueIdList.Count > 1)
        {
            // 可选对话选择
            List<Dialogue> dialogues = new List<Dialogue>();
            foreach(int index in dialogueIdList)
            {
                if(dialogueDictionary.TryGetValue(index, out Dialogue dialogue))
                {
                    dialogues.Add(dialogue);
                }
            }
            // 隐藏正常对话
            dialogueText.enabled = false;
            nextButton.gameObject.SetActive(false);
            // 显示多选
            dialogueMutliBG.enabled = true;
            foreach(Dialogue dialogue in dialogues)
            {
                // 依次生成对话 Button
                optionButton = Instantiate(optionButton, dialogueMutliBG.transform);
                TextMeshProUGUI textMeshPro = optionButton.GetComponentInChildren<TextMeshProUGUI>();
                textMeshPro.text = dialogue.dialogue;
                // 添加事件监听
                optionButton.GetComponent<Button>().onClick.AddListener(delegate 
                    {
                        // 点完以后 销毁选择对话框
                        Button[] optionalButtons = dialogueMutliBG.GetComponentsInChildren<Button>();
                        foreach(Button button in optionalButtons)
                        {
                            Destroy(button.gameObject);
                        }
                        // 显示下一句对话
                        showDialogue(dialogue.toDialogueIdList);
                        
                        
                    });
            }
        }
        else 
        {
            // 正常对话
            int nextDialogueId = dialogueIdList[0];
            if(dialogueDictionary.TryGetValue(nextDialogueId, out Dialogue dialogue))
            {
                // 隐藏多选
                dialogueMutliBG.enabled = false;
                // 显示正常对话
                dialogueText.enabled = true;
                nextButton.gameObject.SetActive(true);
                characterIcon.sprite = Resources.Load<Sprite>(characterIconPrefix + dialogue.characterIcon);
                characterName.text = dialogue.characterName;
                dialogueText.text = dialogue.dialogue;
                currentDialogueIndex = dialogue.dialogueId;
                
            }
        }
    }

    public void nextDialogue()
    {
        if(dialogueDictionary.TryGetValue(currentDialogueIndex, out Dialogue dialogue))
        {
            if(dialogue.toDialogueIdList.Count == 0)
            {
                transform.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(false);
            }
            showDialogue(dialogue.toDialogueIdList);
        }
    }

}
