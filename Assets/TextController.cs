using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TextController : MonoBehaviour {
	public Text hitpoints;
	public Text sanity;
	public Text text;
	
	private enum States {
		title_screen, story_0, story_1, story_2, story_3, the_spot_0, the_spot_1 , the_base_0, the_base_1, 
		doppl_battle_0, doppl_battle_1, doppl_battle_2,doppl_battle_2a, doppl_battle_2b, doppl_battle_2c, doppl_battle_2f,
		doppl_battle_3, doppl_battle_3a, doppl_battle_3b, doppl_battle_3c, doppl_battle3f, game_over_doppl
	};
	
	private enum Scenes { title, story, spot, destroyed_base, doppl_battle };
	
	private Scenes currentScene;
	private States title_state;
	private States story_state;
	private States spot_state;
	private States base_state;
	private States doppl_state;
	private States maw_state;
	private States currentState;
	
	int currentHp;
	int currentSanity;
	
	bool gameover;
	bool has_shield;
	bool has_ggun;
	bool doppl_start;
	bool doppl_dead;
	bool stare_into_maw;
	
	
	
	

	// Use this for initialization
	void Start () {
	gameover = false;
	title_state = States.title_screen;
	story_state = States.story_0;
	spot_state = States.the_spot_0;
	base_state = States.the_base_0;
		
	currentHp = 200;
	currentSanity = 100;
	hitpoints.text = "HP: " + currentHp;
	sanity.text = "Sanity: " + currentSanity;
	currentScene = Scenes.title;
	has_shield = false;
	stare_into_maw = true;
	doppl_start = false;
	}
	
	void HpHurt(int hurtBy) {
		currentHp -= hurtBy;
	
	}
	
	
	#region lose states
	void GameOver () {
	 if (currentHp <= 0){
	 text.text = "Game Over, Press space to start over";
	 hitpoints.text="*****************************************";
	 sanity.text="*****************************************";
	 gameover = true ;	 
		}
	}
	#endregion
	#region states
	void title_screen () {
		text.text = "Welcome to Run, Spot, R'lyeh.\n\n" +
			"\tPiloting a combat mecha, can you stand against insanity inducing creatures, and retrieve the dog known as spot?\n\n" +
				"Controls: "+
				"\tOn each screen there will be prompts to press a key.  Pressing the corresponding key, will do the coressponding thing.\n\n"+
				"Game Over: " + 
				"\tThere are three ways to get a game over.  If your HitPoints(HP) are reduced to 0, your mecha has been destroyed, leaving you defenseless."+
				"If your Sanity is reduced to 0, you have sucumbed to insanity and cannot complete the mission.\n\n" +
				"There are other ways to die, but you will find out for yourself... Press space to begin.";
		
	if (Input.GetKeyDown(KeyCode.Space)) { currentScene = Scenes.story; }
	}
	
	void story_0 () {
		text.text = "There once was a dog name Spot.\n\n" +
		 "We watched him run fast. So fast that he broke land speed records of all kinds.\n\n" + 
		 "It was good fun until he ran so fast he pierced the veil between our world and theirs.\n\n" +
		 "Thiers? Poor sweet naive child.\n\n" +
		 "The hole Spot created in our dimension has never sealed back up, and if it is not closed our world will end as we know it!" + 
		 "The only hope is that bringing that damn dog will restore the barrier between our worlds.\n\n" +
		 "You must catch Spot!\n\n" +
		 "Press space to continue";
		  
		
		if (Input.GetKeyDown(KeyCode.Space)) { story_state = States.story_1; }
	}
	
	void story_1 () {
		text.text = "Alright all systems are green.  Your mission is to enter through the rift to the otherworld at ground zero, and search their world for Spot, and bring him back through. " +
		  "The sensor arrays on your mech have been programed to recognize Spot’s signature. " +
		  "Once on the otherside your mech suit will be able to protect you from many of the physical dangers lurking beyond the veil, but it is not invincible. " +
		  "As I mentioned, the mech will protect your from the physical dangers, but they are not the only danger.\n\n" +
				"Press space to continue";
		
		if (Input.GetKeyDown(KeyCode.Space)) { story_state = States.story_2; }
	}
	
	void story_2 () {
		text.text = "The creatures, and entities you will encounter are nothing like you have seen before. Multiple expedition teams have been sent through the rift, most never come back, " + 
		"and those that have been driven insane, saying one word over, and over again. “R’lyeh”. " +
		"Make no mistake, you will have no choice but to fight you way through the creatures, but don’t lose yourself in the process! " + 
		"You are approaching The Spot now.  Good Luck" +
				"Press space to continue";
		
		if (Input.GetKeyDown(KeyCode.Space)) { story_state = States.story_3; }
	}
	
	void story_3 () {
		text.text = " Without incident you breach the perimeter of the area known simply referred to as “The Spot”, " +
		"the place that the dog of the same name pierced the veil, creating a rift from our world to theirs. " +
		 "Cautiously you move through what remains of Neon Stoneview, a once bustling city  now in ruins. " +
		  "As you get closer to “The Spot” the ruins subtly shift from debris of steel and concrete, to an unsettling dark green material that seems to breath." +
				"Press space to continue";
		
		if (Input.GetKeyDown(KeyCode.Space)) { currentScene = Scenes.spot; }
	}
	
	void the_spot_0 () {
		text.text = "Your scanner's start beeping at you furiously as you have come with 2000 meters of the rift. " +
		  "In front of you appears to be the remains of some giant creature, the energy signature of the rift deep inside it." + 
		   "To your left appears to be remains of the base camp for the last expedition teams condemned to explore the otherside." +
		    "You walk up to the giant maw like structure, you step on some big scrap of metal. Removing your foot you quickly realize it’s a sign " + 
		    "'Welcome To Stoneview Stadium Home of the Quarrymen' A shiver goes down your spine as you realize it’s no creature. "  +
		    "Your moment of clarity is interrupted as your mech is rocked by the force of a nearby explosion. Several warnings pop up on your screen of another mech fighting nearby.\n\n" +
				"B to inspect the base camp\n\n" + 
				"M to enter the 'maw'\n\n" + 
				"I to move towards the unknown mech";
		if (Input.GetKeyDown(KeyCode.B)) { currentScene = Scenes.destroyed_base; }
		if (Input.GetKeyDown(KeyCode.M)) { currentState = States.the_spot_0; }
		if (Input.GetKeyDown(KeyCode.I)) { currentState = States.the_spot_0; }
	}
	
	void the_spot_1 () {
	if (stare_into_maw == true) {
		text.text = "You have returned back to the position in front of the entrace to the destroyed stadium. Through the camera on your mechanized suit you stare into "+
			"the darkness,  your suit's hand hovers at the hilt of an energy sword." +
			"You jump back with your weapon drawn. It's blue glow casts a light into the darknes, revealing nothing.\n\n" +
			"Was there anything there to begin with?\n\n";
			}
		else if (stare_into_maw ==false) {
			text.text = "You have returned back to the position in front of the entrace to the destroyed stadium. Through the camera on your mechanized suit you stare into "+
				"the darkness..\n\n";
		}
		
	if (doppl_start == true && doppl_dead == true) {
		text.text+= "You walked away victorious from the ecounter with the corrupted mech, but the demise of your opponent keeps on comming to the front of your mind. " +
		"The dead silence that now permeates your surroudnings amplifying your horrific revelation. They were once human.\n\n";
		
	}
		else if (doppl_start == true && doppl_dead == false) {
		text.text+= "Thanks to your clever thinking your had taken out the corrupted mech pursuing you.  If only you could have gotten the gattling laser out of it.\n\n " +
		"an errie silence washes over the area.";
		}
		
		else if (doppl_start == false && doppl_dead == false) {
			text.text+= "Sounds of weapons fire and explosions echo off the ruins. The energy signature of the mysterious mech still going strong.\n\n";
		}
	
	if (has_ggun == true && has_shield == true) {
		text.text+= "Armed with gattling laser, and a standard blast shield you decided your next course of action.\n\n";
	}
		else if (has_ggun == true && has_shield == false) {
		text.text+= "Armed with a gattling laser you decided your next course of action.\n\n";
		}
		else if (has_ggun == false && has_shield == true) {
			text.text+= "Armed with a standard blast shield you decided your next course of action.\n\n";
		}
		
 
		
			
			text.text += "B to inspect the base camp\n\n" + 
			"M to enter the 'maw'\n\n" + 
			"I to move towards the unknown mech";
			

		if (Input.GetKeyDown(KeyCode.B)) { stare_into_maw = false; currentScene = Scenes.destroyed_base; }
		if (Input.GetKeyDown(KeyCode.M)) { stare_into_maw = false; currentState = States.the_spot_0; }
		if (Input.GetKeyDown(KeyCode.I)) { stare_into_maw = false; currentState = States.doppl_battle_1; }
	}
	
	void the_base_0 () {
		text.text = "The Base. There is a shield. Pres T to pick it up.\n\n" +
		"Press R to go back to the Spot";
		if (Input.GetKeyDown(KeyCode.T)) { 
		spot_state = States.the_spot_1; 
		base_state = States.the_base_1;
		has_shield = true;
		currentScene = Scenes.spot;
		}
		if (Input.GetKeyDown(KeyCode.R))  { spot_state = States.the_spot_1;  currentScene = Scenes.spot; }
	}
	
	void the_base_1 () {
		text.text = "Nothing left but death.\n\n" +
			"Press R to go back to the Spot";

		if (Input.GetKeyDown(KeyCode.R))  { currentScene = Scenes.spot; }
	}
	
	void doppl_battle_0 () {
		text.text = "You find the source of the explosions. Another mech desperately fighting against a horde creatures.\n\n"+
		"H to help\n\n"+
		"R to go back to the spot";
		if (Input.GetKeyDown(KeyCode.H)) { 
			doppl_state = States.doppl_battle_1; doppl_start= true }
		if (Input.GetKeyDown(KeyCode.R)) { spot_state = States.the_spot_1; currentScene=Scenes.spot; }
		
	}
	
	void doppl_battle_1 () {
		text.text = "Ahh its a corrupted mech, its enemy slain it starts firing shots at you!\n\n"+
			"E to escape\n\n"+
				"F to fight";
		if (Input.GetKeyDown(KeyCode.E)) { doppl_state = States.doppl_battle_2;} 
	
		if (Input.GetKeyDown(KeyCode.F)) { doppl_state = States.doppl_battle_3;} 
			}
			
	void doppl_battle_2 () {
		text.text = "Through the remains of the shipyard, you play a dangerous game of hide and seek.  Each hiding spot quickly reduced to scrap by the laser gatling gun."+
		"The chase has forced you to run through a gravity dock holding up a large dreadnought cruiser, though the flickering of the repulsors told you it wouldn't be floating for long" + 
		"Quickly you dash to the other end of the dock, while the corrupted continues to pursure you oblivious to thier surroundings. As you prepare to make your next move" +
		"You find the genrators powering the dock.\n\n"+
			"D to destory the generators\n\n"+
				"A to Ambush the Mech";
		if (Input.GetKeyDown(KeyCode.D)) { 
			doppl_state = States.doppl_battle_2a;} 
		if (Input.GetKeyDown(KeyCode.A)) { 
		doppl_state = States.doppl_battle_2b;} 
		}
		
	void doppl_battle_2a () {
		text.text = "Oblivous to thier surroundings the corrupted mech is crushed by the dropping of the dreadnought on top of them." +
				"Your mech's alarm systems start blaring. The dreadnoughts core was already in bad shape and the drop made it worse.\n\n"+
				"R to return to the spot\n\n";
		if (Input.GetKeyDown(KeyCode.R)) { 
			doppl_state = States.doppl_battle_2a; doppl_dead = false;} 
		}
		
	void doppl_battle_2b () {
		text.text = "You manage to escape from the sights of the corrupted mech. Pateintly you wait behind the generators out of sight.\n\n";
			if (has_shield == true) { HpHurt(10); has_ggun = true; 
				text.text+= "With the aid of your sheild you mitigate the damage from the gatling gun. You knock the mech off blanace and slice off the arm with gatling gun."+
				"it falls to the ground, still firing. Grazes your torso and hits the generator causing it to malfunction, dropping the dreadnought on the corrupted mech.\n\n";
				}
				else { HpHurt(30); has_ggun = true;
				text.text+= " The muzzle of the gatling gun turns in your direction, barrels blazing doing signifgant damage to your mech. Despite the damage you charge at the enemy." +
				 "As you bum rush the mech, the lasers of the gatling gun land on the generator. You see the repulsors start to flick." +
				 "With your laser sword you hack off the arm holding the gatling laser, and deliver a forceful kick to the your enemy's torso pushing them underneath the dreadnought."+
				 "There is the audible sound of the repulsors of the gravity dock suddenly losing power, followed by the cacophony of metal crunching metal. "}
			text.text+="Your mech's alarm systems start blaring. The dreadnoughts core was already in bad shape and the drop made it worse.\n\n"+
				"R to return to the spot\n\n";
	if (Input.GetKeyDown(KeyCode.R)) { doppl_dead = false;
			doppl_state = States.doppl_battle_2f; currentScene = Scenes.spot;}
		}
		
	void doppl_battle_3 () {
		text.text = "Staring down the 7 barrels of a laser gatling gun, you throw caution to the wind and charge the corrupted mech.\n\n";
		
		if (has_shield == true) {  
			text.text+= "Your shield effortlessly protects you from the gattling laser. With a shield bash you knock the gatling laser up into the air, and hack it off with your sword"+
			"Unintelligble screams com over your comm system as you watch as the mech swings its large tentacle arm at you. But your reflexes have been finally honed from your training"+
			"With another swipe you hack off the limb. You go for another swipe, and another, leaving the mech as just a torso.";
		}
		else { HpHurt(40); 
			text.text+= " The gatling gun roars into action fireing a hail of lasers, damaging your mech." +
				"With a mighty slash of your laser sword, you disarm the mech." +
					"With your laser sword you hack off the arm holding the gatling laser, and deliver a forceful kick to the your enemy's torso pushing them underneath the dreadnought."+
					"Unintelligble screams com over your comm system as you watch as the mech swings its large tentacle arm at you. But your reflexes have been finally honed from your training"+
			"With another swipe you hack off the limb. You go for another swipe, and another, leaving the mech as just a torso.";}
			
		text.text+="Your enemy had been soundly defeated, but they continue to scream unitelligbly. As it continues on it starts to sound like something.\t\n\n"+
		"F to finish them and grab the gun.\n\n"+
		"G to grab the gun and leave"+
		"L to listen to the shouting";
		if (Input.GetKeyDown(KeyCode.F)) { 
			doppl_state = States.doppl_battle_3a; has_ggun = true; doppl_dead = true;}
		if (Input.GetKeyDown(KeyCode.G)) { 
			doppl_state = States.doppl_battle_3b; has_ggun = true; doppl_dead = false; currentScene = Scenes.spot}
		if (Input.GetKeyDown(KeyCode.L)) { 
			doppl_state = States.doppl_battle_3c; doppl_dead = false;}
	}
		
		
	void game_over () {
		text.text = "You find the source of the explosions. Another mech desperately fighting against a horde creatures.\n\n"+
			"H to help\n\n"+
				"R to go back to the spot";
		if (Input.GetKeyDown(KeyCode.T)) { 
			currentState = States.the_spot_0; 
			has_shield = true;}
		if (Input.GetKeyDown(KeyCode.R)) { currentState = States.the_spot_0; }
	}
	
	
	#endregion
	
	#region scenes
	void title () {
		print (title_state);
			if (title_state == States.title_screen) {title_screen();}
	}
	
	void story () {
		print (story_state);
			if (story_state == States.story_0) {story_0();}
		else if (story_state == States.story_1) {story_1();}
		else if (story_state == States.story_2) {story_2();}
		else if (story_state == States.story_3) {story_3();}
	}
	
	
	void spot () {
		print (spot_state);
			if (spot_state == States.the_spot_0) {the_spot_0 ();}
			else if (spot_state == States.the_spot_1 ) {the_spot_1();}
	}
	
	void destroyed_base () {
		print (base_state);
		if (base_state == States.the_base_0) {the_base_0 ();}
		else if (base_state == States.the_base_1 ) {the_base_1();}
	}
	
	void doppl_battle () {
		print (doppl_state);
		if ( doppl_state == States.doppl_battle_0) {doppl_battle_0();}
		else if (doppl_state == States.doppl_battle_1) {doppl_battle_1();}
		else if (doppl_state == States.doppl_battle_2) {doppl_battle_2();}
		else if (doppl_state == States.doppl_battle_3) {doppl_battle_3();}
	}
	#endregion
	
	#region items
	
	#endregion

	
	// Update is called once per frame
	void Update () { 
		print (currentScene);
		if (currentScene == Scenes.title) {title();}
		else if (currentScene == Scenes.story) {story();}
		else if (currentScene == Scenes.spot)	{spot();}
		else if (currentScene == Scenes.destroyed_base) {destroyed_base();}
	

		
		if (gameover == true) {
			currentState = States.game_over;
			if (Input.GetKeyDown(KeyCode.Space)) {
				Start ();
			}
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
		HpHurt(15);
		hitpoints.text="HP: " + currentHp;
		GameOver();
		
			} 
		}
	}
