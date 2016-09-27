using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {

	public GameObject textBox;

	private readonly object _locker = new object();

	public Text text;
	private string[] texts = { 
				 "You are in a prison cell and you want to escape. There are some " +
				 "dirty sheets on the bed, a mirror on the wall, and the door " +
	    		 "is locked from the outside.\n\n" +
				 "Press S to view Sheets, Press M to view Mirror and L to view Lock", 

				 "I look good, I wonder if the mirror can be useful\n\n" +
				 "Press B to break the mirror and R to Return to the cell",

			   	 "Well those are my sheets so.. Had a good sleep but I need to get out of here.\n\n" +
				 "Press R to Return to the cell",

				 "This Gate won't budge... Need to some how unlock this thing.\n\n"+
				 "Press R to Return"
				 };  

	private string[] texts1 = { 
				 "You are in a prison cell and you want to escape. There are some " +
				 "dirty sheets on the bed and the door " +
	    		 "is locked from the outside.\n\n" +
				 "Press S to view Sheets and L to view Lock", 

				 "The gate is still locked... I'm sure there is a way to open this thing.\n\n"+
				 "Press O to open gate with mirror and R to Return to cell",

				 "This cell is a waist of time, I should rather just get out of here.\n\n" +
				 "Press R to return to the corridors"
				 };  
	public bool isActive;

	private bool isTyping = false;
	private bool cancelTyping = false;

	private int letter = 0;

	private enum States {sheet_0,cell_0,mirror,sheet_1,lock_0,cell_1,lock_1,lock_2};
	private States myState;

	private static int savePoint = 0;
	// Use this for initialization
	void Start () {
		myState = States.cell_0;
		if(savePoint==1){
		myState = States.lock_2;
		}
	}
	
	// Update is called once per frame
	/// <summary>
	/// Adds to what state to move to, add in the switch case when adding a new scenario. Create a mthod in that scenario saying where it go go to.
	/// Add at the end a method the quickSentence method so user can press space to type out the text fast.
	/// Adds in the texts[] the storry you would like to write in the scenario you are in.
	/// </summary>
	void Update ()
	{
		if (!isActive) {
			return;
		}

		switch (myState) {
		case States.cell_0:
			cell_0 ();
			break;
		case States.sheet_0:
			sheets_0 ();
			break;
		case States.mirror:
			mirror ();
			break;
		case States.lock_0:
			lock_0 ();
			break;
		case States.sheet_1:
			sheets_1 ();
			break;
		case States.cell_1:
			cell_1 ();
			break;
		case States.lock_1:
			lock_1 ();
			break;
		case States.lock_2:
			lock_2 ();
			break;
		}

	}

	void cell_0 ()
	{
		code(this.texts[0]);
		if(Input.GetKeyDown(KeyCode.S)){
		resetSentence (this.texts[0], States.sheet_0);
		}
		else if(Input.GetKeyDown(KeyCode.M)){

		resetSentence (this.texts[0], States.mirror);
		}
		else if(Input.GetKeyDown(KeyCode.L)){

		resetSentence (this.texts[0], States.lock_0);
		}
		qucikSentence (this.texts[0]);


	}

	void mirror ()
	{
		code (this.texts [1]);
		if (Input.GetKeyDown (KeyCode.R)) {
			resetSentence (this.texts [1], States.cell_0);
		}
		else if(Input.GetKeyDown (KeyCode.B)){
			string word = "It is not typin";

			code(word);
			resetSentence (word, States.cell_1);

		}
		qucikSentence (this.texts[1]);
	}

	void sheets_0 ()
	{
		code (this.texts [2]);
		if (Input.GetKeyDown (KeyCode.R)) {
			resetSentence (this.texts [2], States.cell_0);
		}
		qucikSentence (this.texts[2]);
	}

	void lock_0 (){
		code (this.texts [3]);
		if (Input.GetKeyDown (KeyCode.R)) {
			resetSentence (this.texts [3], States.cell_0);
		}
		qucikSentence (this.texts[3]);
	}
/*-------------------------------------------------------- */

	void cell_1 ()
	{
		code(this.texts1[0]);
		if(Input.GetKeyDown(KeyCode.S)){
			resetSentence (this.texts1[0], States.sheet_1);
		}
		else if(Input.GetKeyDown(KeyCode.L)){
			resetSentence (this.texts1[0], States.lock_1);
		}
		qucikSentence (this.texts1[0]);
	}

	void sheets_1 ()
	{
		code (this.texts [2]);
		if (Input.GetKeyDown (KeyCode.R)) {
			resetSentence (this.texts [2], States.cell_1);
		}
		qucikSentence (this.texts[2]);
	}

	void lock_1 ()
	{
		code(this.texts1[1]);
		if(Input.GetKeyDown(KeyCode.R)){
			resetSentence (this.texts1[1], States.cell_1);
		}
		else if(Input.GetKeyDown(KeyCode.O)){
			savePoint = 1;
			SceneManager.LoadScene ("Corridors");
		}
		qucikSentence (this.texts1[1]);
	}

	void lock_2 ()
	{
		code(this.texts1[2]);
		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene ("Corridors");
						}
		qucikSentence (this.texts1[2]);
		}


/*======================================================== */
	//Private method do not edit the code

	///<summary>Sets the letter value to zero and text area to nothing and changes to the state.</summary>
	///<param name="s"> refers to the Text needed for that scene. </param>
	///<param name="state"> Gets the state that you are going to moving to.
	private void resetSentence (string s, States state)
	{
		if (letter >= s.Length - 1) {
			letter = 0;
			text.text = null;
			myState = state;

		}
	}

	private void qucikSentence (string s)
	{
		if(Input.GetKeyDown (KeyCode.Space) && s.Length >= letter ){
			text.text = null;
			text.text = s;
			letter = s.Length;
		}
	}

	private void code (string s)
	{
		if (!isTyping) {
			StartCoroutine (TextScroll (s));

		} else if (isTyping && !cancelTyping) {
			cancelTyping = true;
		}
	}

	private IEnumerator TextScroll (string lineOfText) {
		isTyping=true;
		cancelTyping = false;

		while(isTyping && !cancelTyping && (letter <= lineOfText.Length - 1)){
			text.text += lineOfText[letter];
			letter ++;

			yield return new WaitForSecondsRealtime (0.01F);
		}

		isTyping = false;

	}
}
