using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour 
{
	public Text max;
	private enum textState {Begin, BlueRoom, BlueLeave, BlueExamine, BlueBinder, BlueBed, Live};
	textState axle;
	string str1 = "", str2 = "", str3 = "", str4 = "", str5 = "", str6 = "";
	bool binder = false, bed = false, bedKnow = false;
	int doorScratch = 0;

	// Use this for initialization
	void Start () 
	{
		axle = textState.Begin;
	}

	// Update is called once per frame
	void Update ()
	{
		if (axle == textState.Begin)
		{
			Beginning();
		}
		else if (axle == textState.BlueRoom)
		{
			BlueRooming();
		}
		else if (axle == textState.BlueLeave)
		{
			BlueLeaving();
		}
		else if (axle == textState.BlueExamine)
		{
			BlueExamining();
		}
		else if (axle == textState.BlueBinder)
		{
			BlueBindering();
		} 
		else if (axle == textState.BlueBed)
		{
			BlueBedding();
		}
		else if (axle == textState.Live)
		{
			Living();
		}
	}

	void Beginning ()
	{
		max.text = "Press Enter to begin";
		if (Input.GetKeyDown (KeyCode.Return))
		{
			axle = textState.BlueRoom;
		}
	}

	void BlueRooming ()
	{
		str1 = "Press E to examine the room. ";
		str2 = "Press Enter to try to leave the room. ";
		str3 = "Press M to move the binders. ";
		str4 = "Press P to push the bed. ";
		max.text = str1;
		max.text = max.text + str2;
		if (binder == false)
		{
			max.text = max.text + str3;
		}
		if ((bedKnow == true) & (bed == false))
		{
			max.text = max.text + str4;
		}

		if (Input.GetKeyDown (KeyCode.Return))
		{
			axle = textState.BlueLeave;
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			axle = textState.BlueExamine;
		}
		else if ((Input.GetKeyDown(KeyCode.M)) & (binder == false))
		{
			axle = textState.BlueBinder;
		}
		else if (Input.GetKeyDown(KeyCode.P) & (bedKnow == true) & (bed == false))
		{
			axle = textState.BlueBed;
		}
	}

	void BlueLeaving ()
	{
		str1 = "Press Enter to continue. ";
		str2 = "Press O to try to open the door again. ";
		str3 = "Press Enter to go back. ";

		if (binder == false)
		{
			max.text = str3;
		}
		else if (binder == true)
		{
			if (bed == false)
			{
				bedKnow = true;
				max.text = str3;
				if (doorScratch <= 5)
				{
					max.text = max.text + str2;
				}
				else
				{
					max.text = str1;
					bed = true;
				}
			}
			else if (bed == true)
			{
				max.text = str1;
			}
		}

		if (Input.GetKeyDown (KeyCode.Return))
		{
			if ((binder == true) & (bed == true))
			{
				axle = textState.Live;
			}
			else if (binder == false)
			{
				axle = textState.BlueRoom;
			}
			else if (bed == false)
			{
				doorScratch++;
				axle = textState.BlueRoom;
			}
		}

		else if (Input.GetKeyDown (KeyCode.O))
		{
			if ((binder == true) & (bedKnow == true) & (bed == false))
			{
				doorScratch++;
			}
		}
	}

	void BlueExamining ()
	{
		str1 = "Press Enter to stop looking around like a curious fool. ";

		max.text = str1;

		if (Input.GetKeyDown (KeyCode.Return))
		{
			bedKnow = true;
			axle = textState.BlueRoom;
		}
	}

	void BlueBindering ()
	{
		str1 = "Press Enter to feel proud of yourself. ";

		max.text = str1;

		if (Input.GetKeyDown (KeyCode.Return))
		{
			binder = true;
			axle = textState.BlueRoom;
		}
	}

	void BlueBedding ()
	{
		str1 = "Press Enter to stop straining yourself. ";

		max.text = str1;

		if (Input.GetKeyDown (KeyCode.Return))
		{
			if (binder == true)
			{
				bed = true;
			}
			axle = textState.BlueRoom;
		}
	}

	void Living()
	{
		str1 = "Press any key to feel proud about yourself, even more so than that one time you pushed those binders.";

		max.text = str1;
	}
}
