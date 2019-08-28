using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

    public Image charPicture;

	public Canvas DialogueCanvas;

	private Queue<string> sentences;

	public GameObject player;
	public bool canInteract = true;

    public static DialogueManager i;

    private void Awake() {
        if (!i) {
            i = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

        // Use this for initialization
        void Start () {
		sentences = new Queue<string>();
		this.DialogueCanvas.enabled = false;
		
	}

	 void Update()
    {

        if (Input.GetButtonDown("Fire1") && !canInteract )
        {
            DisplayNextSentence();
			Debug.Log("Next");
        }

    }

	public void StartDialogue (Dialogue dialogue)
	{
		player.GetComponent<PlayerMovement>().enabled = false;
		this.canInteract = false;
		
		this.DialogueCanvas.enabled = true;

		nameText.text = dialogue.charName;
        charPicture.sprite = Resources.Load<Sprite>(dialogue.picturUrl); // Resources.Load<Sprite>("Assets/Sprites/Overworld/char.png");

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		this.DialogueCanvas.enabled = false;
		player.GetComponent<PlayerMovement>().enabled = true;
		Invoke("setCanInteractTrue",0.5f);
	}

	void setCanInteractTrue(){
		this.canInteract = true;
	}

}