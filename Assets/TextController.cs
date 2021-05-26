using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour 
{

	public Text max;
	private enum textState {Begin, BlueRoom, BlueLeave, BlueExamine, BlueBinder, BlueBed, Live};
	textState axle;
	string str1 = "", str2 = "", str3 = "", str4 = "", str5 = "", str6 = "";
	bool binder = false, bed = false, bedKnow = false, first = false;
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

	#region States

	void Beginning ()
	{
		max.text = "Welcome to the horrid tale... of getting out of my house! ...Why are you in my house anyway??";
		if (Input.GetKeyDown (KeyCode.Return))
		{
			axle = textState.BlueRoom;
		}
	}

	void BlueRooming ()
	{
		str1 = "You have awoken from a deep slumber in a blue bedroom, on a bed decorated with strange mascot characters " +
		"from the early 90's. Peculiar. Being in this musty room is quite depressing, so you decide that you should leave. ";
		str2 = "However, the exit of the room seems to be blocked off by an obscenely large amount of binders. ";
		str3 = "However, the bed seems to be in the way of the door. ";
		str4 = "Not to mention that the bed is in the way of the door. ";
		str5 = "You wake up in a jolt. 'Where am I?' You ponder. ";

		if (first == false)
		{
			max.text = str5 + str1;
		}
		else
		{
			max.text = str1;
		}
		if (binder == false)
		{
			max.text = max.text + str2;
		}
		if ((bedKnow == true) & (binder == true))
		{
			max.text = max.text + str3;
		}
		else if ((bedKnow == true) & (binder == false))
		{
			max.text = max.text + str4;
		}
		if (Input.GetKeyDown (KeyCode.Return))
		{
			first = true;
			axle = textState.BlueLeave;
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			first = true;
			axle = textState.BlueExamine;
		}
		else if ((Input.GetKeyDown(KeyCode.M)) & (binder == false))
		{
			first = true;
			axle = textState.BlueBinder;
		}
		else if (Input.GetKeyDown(KeyCode.P) & (bedKnow == true) & (bed == false))
		{
			first = true;
			axle = textState.BlueBed;
		}
	}

	void BlueLeaving ()
	{
		str1 = "You open the door and see what awaits beyond. TO DESTINY!";
		str2 = "The binders are in the way of the door. Makes you question what kind of a slob lives here. ";
		str3 = "You tried to open the door, but the door bumps against the bed, scratching it.";
		str4 = "Looking at it, the bed is probably going to be in the way as well.";
		str5 = "You tried to open the door again, scratching the bed further.";
		str6 = "With a final mighty tug, you smash the side of the bed, allowing the door to be fully open. Hah! Serves the owner of the " +
			   "room right!... Nerd. ";
		
		if (binder == false)
		{
			max.text = str2;
			if (bedKnow == true)
			{
				max.text = max.text + str4;
			}
		}
		else if (binder == true)
		{
			if (bed == false)
			{
				bedKnow = true;
				if (doorScratch == 0)
				{
					max.text = str3;
				}
				else if (doorScratch <= 5)
				{
					max.text = str5;
				}
				else
				{
					max.text = str6 + str1;
					bed = true;
				}
			}
			else if (bed == true)
			{
				if (doorScratch > 5)
				{
					max.text = str6 + str1;
				}
				else
				{
					max.text = str1;
				}

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
		str1 = "You look around the room. There is a dresser with an absurdly large number of plush toys resting on top of it. " + 
			   "To the right of the dresser, there is a desk with a load of papers, magazines, and other assorted office supplies " +
			   "strewn about. There is an unused television set to the left of that dresser. Next to the bed is a nightstand. " + 
			   "Likewise, it too is covered with random bits of garbage. Whoever lives here needs to clean up their room.";
		str2 = "... Oh? The bed is jutting out, and it seems to be in the way of the door. ";
		str3 = " The bed seems to be in the way of the door.";

		max.text = str1;
	
		if (bedKnow == false)
		{
			max.text = max.text + str2;
		}
		else if ((bedKnow == true) & (bed == false))
		{
			max.text = max.text + str3;
		}

		if (Input.GetKeyDown (KeyCode.Return))
		{
			bedKnow = true;
			axle = textState.BlueRoom;
		}
	}

	void BlueBindering ()
	{
		str1 = "You painstakingly pushed the binders out of the way. It seems that those binders had nothing but sheet music inside. " +
			   "Whoever lives here must love their music. Funny, you don't notice any instruments in the room.";

		max.text = str1;

		if (Input.GetKeyDown (KeyCode.Return))
		{
			binder = true;
			axle = textState.BlueRoom;
		}
	}

	void BlueBedding ()
	{
		str1 = "You push the bed to the opposite wall with ease. It's strange how the bed was easier to push than the binders.";
		str2 = "You try to push the bed, but there is such an abundance of binders near the bed, you can't find the leverage to push it to " +
			   "the side. Seriously, the owner's obsession with these binders are insane. What the heck is even in them?";

		if (binder == false)
		{
			max.text = str2;
		}
		else
		{
			max.text = str1;
		}

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
		str1 = "You win! (For now, I'm still working on it.)";

		max.text = str1;
	}

	#endregion
}
