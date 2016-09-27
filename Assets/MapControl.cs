using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapControl : MonoBehaviour {

	public Text map;

	public string[,] corridor = {
	//     0      1    2     3     4     5     6     7     8
		{" "  ," "  ," "  ," "  ,"[^]"," "  ," "  ," "  ," "  },//0
		{"[^]","[*]","[ ]","[ ]","[ ]","[ ]","[ ]","[ ]","[^]"},//1
		{" "  ," "  ," "  ,"[^]"," "  ," "  ,"[^]"," "  ," "  } //2


	};
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		displayMap ();

		if(Input.GetKeyDown (KeyCode.RightArrow)){
			getRight ();
		}
		else if(Input.GetKeyDown (KeyCode.LeftArrow)){
			getLeft ();
		}
		else if(Input.GetKeyDown (KeyCode.UpArrow)){
			getUp ();
		}
		else if(Input.GetKeyDown (KeyCode.DownArrow)){
			getDown ();
		}
	}

	void displayMap ()
	{
		int i = 0;
		if(this.corridor.Length - 1 >= map.text.Length){
		foreach(string s in this.corridor){
		map.text += (string.Format ("{0,-5}",s));
		i++;
		if(i>=this.corridor.GetLength (1)){
			map.text += "\n";
			i =0;
		}

		}
		}
	}

	void getRight ()
	{
		map.text = null;
		int r = 0;
		int c = 0;
		for (r = 0; r < this.corridor.GetLength (0)-1; r++) {
			for (c = 0; c < this.corridor.GetLength(1)-1; c++) {
				if (this.corridor [r, c].Contains ("[*]")) {
					print (c); 
					string t = this.corridor[r,c];
					this.corridor[r,c] = this.corridor[r,c+1];

						if(this.corridor[r,c+1].Equals(" ")){
							this.corridor[r,c]= "[*]";
						goto outerLoop;
               }
					   	else if(this.corridor[r,c+1].Equals("[^]")){
						this.corridor[r,c]= "[*]";
						goto enterNewRoom;
               }
					this.corridor[r,c+1] = t;
             		goto outerLoop;
           }
       }
       }
		outerLoop:;

		enterNewRoom:
			
		;
    }

	void getLeft ()
	{
		map.text = null;
		int r = 0;
		int c = 0;
		for (r = 0; r < this.corridor.GetLength (0)-1; r++) {
			for (c = 0; c < this.corridor.GetLength(1)-1; c++) {
				if (this.corridor [r, c].Contains ("[*]")) {
					print (c); 
					string t = this.corridor[r,c];
					this.corridor[r,c] = this.corridor[r,c-1];

						if(this.corridor[r,c-1].Equals(" ")){
							this.corridor[r,c]= "[*]";
						    goto outerLoop;
               }
					   	else if(this.corridor[r,c-1].Equals("[^]")){
						this.corridor[r,c]= "[*]";
						goto enterNewRoom;
               }
					this.corridor[r,c-1] = t;
             		goto outerLoop;
           }
       }
       }
		outerLoop:;

		enterNewRoom:
			if(r==1 && c ==1){
				Application.LoadLevel ("Game");
			}
		;
    }

	void getUp()
	{
		map.text = null;
		int r = 0;
		int c = 0;
		for (r = 0; r < this.corridor.GetLength (0)-1; r++) {
			for (c = 0; c < this.corridor.GetLength(1)-1; c++) {
				if (this.corridor [r, c].Contains ("[*]")) {
					print (c); 
					string t = this.corridor[r,c];
					this.corridor[r,c] = this.corridor[r-1,c];

					if(this.corridor[r-1,c].Equals(" ")){
							this.corridor[r,c]= "[*]";
						    goto outerLoop;
               }
					else if(this.corridor[r-1,c].Equals("[^]")){
						this.corridor[r,c]= "[*]";
						goto outerLoop;
               }
					this.corridor[r-1,c] = t;
             		goto outerLoop;
           }
       }
       }
		outerLoop:;

    }

	void getDown ()
	{
		map.text = null;
		int r = 0;
		int c = 0;
		for (r = 0; r < this.corridor.GetLength (0)-1; r++) {
			for (c = 0; c < this.corridor.GetLength(1)-1; c++) {
				if (this.corridor [r, c].Contains ("[*]")) {
					print (c); 
					string t = this.corridor[r,c];
					this.corridor[r,c] = this.corridor[r+1,c];

					if(this.corridor[r+1,c].Equals(" ")){
							this.corridor[r,c]= "[*]";
						    goto outerLoop;
               }
					else if(this.corridor[r+1,c].Equals("[^]")){
						this.corridor[r,c]= "[*]";
						goto outerLoop;
               }
					this.corridor[r+1,c] = t;
             		goto outerLoop;
           }
       }
       }
		outerLoop:;

    }

}
