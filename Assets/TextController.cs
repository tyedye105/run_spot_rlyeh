using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TextController : MonoBehaviour {
	public Text hitpoints;
	public Text sanity;
	public Text text;
	public Text timer;
	
	private enum States {
		title_screen, story_0, story_1, story_2, story_3, the_spot_0, the_spot_1 , the_base_0, the_base_1, 
		doppl_battle_0, doppl_battle_1, doppl_battle_2,doppl_battle_2a, doppl_battle_2b, doppl_battle_2c, doppl_battle_2f,
		doppl_battle_3, doppl_battle_3a, doppl_battle_3b, doppl_battle_3c, doppl_battle_3d, doppl_battle_3f, game_over_doppl_helm, game_over_reg,
		tunnel_0, tunnel_1, tunnel_1c, tunnel_passage, tunnel_cover, tunnel_fire
	};
	
	private enum Scenes { title, story, spot, destroyed_base, doppl_battle, game_over, the_tunnel };
	
	private Scenes currentScene;
	private States title_state;
	private States story_state;
	private States spot_state;
	private States base_state;
	private States doppl_state;
	private States tunnel_state;
	private States game_over_state;
	//private int mech_mode; scrapped due to scope creep.
	// 0 is explore mode, 1 is battle mode, 2 is boss mode.	
	private float startTime;
	private int eye_distance = 500;
	
	
	
	int currentHp;
	int currentSanity;
	
	bool gameover;
	bool has_shield;
	bool has_ggun;
	bool doppl_start;
	bool doppl_dead;
	bool stare_into_maw;
	bool active_eye;
	bool has_cover;
	
	
	
	

	// Use this for initialization
	void Start () {
	gameover = false;
	title_state = States.title_screen;
	story_state = States.story_0;
	spot_state = States.the_spot_0;
	base_state = States.the_base_0;
	doppl_state = States.doppl_battle_0;
	currentHp = 200;
	currentSanity = 100;
	hitpoints.text = "HP: " + currentHp;
	sanity.text = "Sanity: " + currentSanity;
	currentScene = Scenes.title;
	has_shield = false;
	stare_into_maw = true;
	doppl_start = false;
	startTime = 3f;
	}
	
	void HpHurt(int hurtBy) {
		currentHp -= hurtBy;
	}
	
	void SanityHurt(int hurtBy) {
		currentSanity -= hurtBy;
	}
	
	void displayTime() {
		startTime -= Time.deltaTime;
		timer.text= startTime.ToString() + " s";
	}
	
	
	#region lose states
	void GameOver () {
	 if (currentHp <= 0){
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
		 "The only hope is that bringing that damn dog back, so we can figure out how to close it.\n\n" +
		 "You must catch Spot!\n\n" +
		 "Press space to continue";
		  
		
		if (Input.GetKeyDown(KeyCode.Space)) { story_state = States.story_1; }
	}
	
	void story_1 () {
		text.text = "Alright all systems are green.  Spot's vitals have been detected in the middle of 'The Spot'. Your will be landing just in the  outer edges of the perimiter. " +
		  "The sensor arrays on your mech have been programed to recognize Spot’s signature. " +
		  "Your mech suit will be able to protect you from many of the physical dangers from beyond the veil, but it is not invicinble. " +
		  "\n\n" +
				"Press space to continue";
		
		if (Input.GetKeyDown(KeyCode.Space)) { story_state = States.story_2; }
	}
	
	void story_2 () {
		text.text = "The creatures, and entities you will encounter are nothing like you have seen before. You are not the first to go after that damned dog. Most never come back, " + 
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
		text.text = "Your scanner's start beeping at you furiously as you have come with 2000 meters your objective. " +
		  "In front of you appears to be the remains of some giant creature, the vitals of spot deep inside it." + 
		   "To your left appears to be remains of the forward camp from the first battles that took place after the incident." +
		    "You walk up to the giant maw like structure, you step on some big scrap of metal. Removing your foot you quickly realize it’s a sign " + 
		    "'Welcome To Stoneview Stadium Home of the Quarrymen' A shiver goes down your spine as you realize it’s no creature. "  +
		    "Your moment of clarity is interrupted as your mech is rocked by the force of a nearby explosion. Your systems indicate a functional mech in the nearby shipyard.\n\n" +
				"B to inspect the forward camp\n\n" + 
				"M to enter the 'maw'\n\n" + 
				"I to invistage the shipyard.";
		if (Input.GetKeyDown(KeyCode.B)) { currentScene = Scenes.destroyed_base; }
		if (Input.GetKeyDown(KeyCode.M)) { }
		if (Input.GetKeyDown(KeyCode.I)) { currentScene = Scenes.doppl_battle;}
	}
	
	void the_spot_1 () {
	if (stare_into_maw == true) {
		text.text = "You have returned back to the position in front of the entrace to the destroyed stadium. Through the camera on your mechanized suit you stare into "+
			"the darkness,  your suit's hand hovers at the hilt of your sword." +
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
		text.text+= "Thanks to your clever thinking your had taken out the corrupted mech pursuing you.";
		}
		else if (doppl_start == false && doppl_dead == false) {
			text.text+= "Sounds of weapons fire and explosions echo off the ruins. The battle at the shipyard continues.\n\n";
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
		
 
		
			
			text.text += "B to inspect the forward camp\n\n" + 
			"M to enter the 'maw'\n\n" + 
				"I to invistage the shipyard.";
			

		if (Input.GetKeyDown(KeyCode.B)) { stare_into_maw = false; currentScene = Scenes.destroyed_base; }
		if (Input.GetKeyDown(KeyCode.M)) { stare_into_maw = false; }
		if (Input.GetKeyDown(KeyCode.I)) { stare_into_maw = false; currentScene = Scenes.doppl_battle; }
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
			doppl_state = States.doppl_battle_1; doppl_start= true; 
			}
		if (Input.GetKeyDown(KeyCode.R)) { spot_state = States.the_spot_1; currentScene=Scenes.spot; }
		
	}
	
	void doppl_battle_1 () {
		text.text = "Ahh its a corrupted mech, its enemy slain it starts firing shots at you!\n\n"+
			"E to escape\n\n"+
				"F to fight";
		if (Input.GetKeyDown(KeyCode.E)) { doppl_state = States.doppl_battle_2;} 
	
		if (Input.GetKeyDown(KeyCode.F)) { doppl_state = States.doppl_battle_3;
			if (has_shield == false) {HpHurt(50);}
				} 
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
		doppl_state = States.doppl_battle_2b;
		if (has_shield == true) { HpHurt(10);} 
			else { HpHurt(50);}
			}
		}
		
	void doppl_battle_2a () {
		text.text = "Oblivous to thier surroundings the corrupted mech is crushed by the dropping of the dreadnought on top of them." +
				"Your mech's alarm systems start blaring. The dreadnoughts core was already in bad shape and the drop made it worse.\n\n"+
				"R to get out of there!\n\n";
		if (Input.GetKeyDown(KeyCode.R)) { 
			doppl_state = States.doppl_battle_2c; doppl_dead = false;} 
		}
		
	void doppl_battle_2b () {
		text.text = "You manage to escape from the sights of the corrupted mech. Pateintly you wait behind the generators out of sight.\n\n";
			if (has_shield == true) { has_ggun = true; 
				text.text+= "With the aid of your sheild you mitigate most the damage from the gatling gun. You knock the mech off blanace and slice off the arm with gatling gun."+
				"it falls to the ground, still firing. Grazes your torso and hits the generator causing it to malfunction, dropping the dreadnought on the corrupted mech.\n\n";
				}
				 else {has_ggun = true;
				text.text+= " The muzzle of the gatling gun turns in your direction, barrels blazing doing signifgant damage to your mech. Despite the damage you charge at the enemy." +
				 "As you bum rush the mech, the lasers of the gatling gun land on the generator. You see the repulsors start to flick." +
				 "With your laser sword you hack off the arm holding the gatling laser, and deliver a forceful kick to the your enemy's torso pushing them underneath the dreadnought."+
				 "There is the audible sound of the repulsors of the gravity dock suddenly losing power, followed by the cacophony of metal crunching metal. ";}
			text.text+="Your mech's alarm systems start blaring. The dreadnoughts core was already in bad shape and the drop made it worse.\n\n"+
			"R to get out of there!\t\n\n";
	if (Input.GetKeyDown(KeyCode.R)) { doppl_dead = false;
			doppl_state = States.doppl_battle_2c;}
		}
	
	void doppl_battle_2c () {
		text.text = "You dash out of the area utlizing your boosters to get as much distance from the dreadnought as you can. " +
		"Just as the decribid stadium comes into view you are rocked by a violent shockwave, sending you tumbling." +
		" As you recover you see the giant cloud of destruction rising up from the area where the shipyard was.\n\n"+
		"C to continue to the spot\n\n";
		if (Input.GetKeyDown(KeyCode.C)) { 
			doppl_state = States.doppl_battle_2f; doppl_dead = false; currentScene = Scenes.spot; spot_state = States.the_spot_1;} 
	}
	
	void doppl_battle_2f () {
		text.text = " You try to make your way back to the shipyard, but thanks to the destruction of the dreadnought, it has created an enviroment that your mech cannot withstand.\n\n"+
		"R to return to the spot";
		if (Input.GetKeyDown(KeyCode.R)) {currentScene = Scenes.spot;}
	}
	
		
	void doppl_battle_3 () {
		text.text = "Staring down the 7 barrels of a laser gatling gun, you throw caution to the wind and charge the corrupted mech.\n\n";
		
		if (has_shield == true) {  
			text.text+= "Your shield effortlessly protects you from the gattling laser. With a shield bash you knock the gatling laser up into the air, and hack it off with your sword"+
			"Unintelligble screams com over your comm system as you watch as the mech swings its large tentacle arm at you. But your reflexes have been finally honed from your training"+
			"With another swipe you hack off the limb. You go for another swipe, and another, leaving the mech as just a torso.";
		}
		else {  
			text.text+= " The gatling gun roars into action fireing a hail of lasers, damaging your mech." +
				"With a mighty slash of your laser sword, you disarm the mech." +
					"With your laser sword you hack off the arm holding the gatling laser, and deliver a forceful kick to the your enemy's torso pushing them underneath the dreadnought."+
					"Unintelligble screams com over your comm system as you watch as the mech swings its large tentacle arm at you. But your reflexes have been finally honed from your training"+
			"With another swipe you hack off the limb. You go for another swipe, and another, leaving the mech as just a torso.";}
			
		text.text+="Your enemy had been soundly defeated, but they continue to scream over your comms unitelligbly. As it continues on it starts to sound like something.\t\n\n"+
		"F to finish them and grab the gun.\n\n"+
		"G to grab the gun and leave\n\n"+
		"L to listen to the shouting";
		if (Input.GetKeyDown(KeyCode.F)) { 
			doppl_state = States.doppl_battle_3a; has_ggun = true;}
		if (Input.GetKeyDown(KeyCode.G)) { 
			doppl_state = States.doppl_battle_3b; has_ggun = true; doppl_dead = false;}
		if (Input.GetKeyDown(KeyCode.L)) { 
			doppl_state = States.doppl_battle_3c; doppl_dead = false;SanityHurt(20);}
		}
	void doppl_battle_3a () {
		text.text = " Without a second through you walk closer to the downed mech, and drive your sword through the torso. Your comms imeadiate become quiet."+
		"you pick up the gattling gun and get ready to return to the spot when moans start coming through the comms. Quickly you unload several rounds from the gattling laser into "+
		"the torso. Silence. You walk away not waiting else to happen.\n\n"+
		"R to return to the spot";
		if (Input.GetKeyDown(KeyCode.R)) { doppl_state = States.doppl_battle_3f; spot_state = States.the_spot_1; doppl_dead = true; currentScene = Scenes.spot;}
	}
	void doppl_battle_3b () {
		text.text = "Ignoring the angry unearthly yells overwhelming your comms, you pick up the gattling gun and begin to walk towards the spot.  The yelling only seems to get louder."+
		"You simply flip a switch to turn off your comms and walk back to the spot in silence.\n\n"+
		"C to continue";
		if (Input.GetKeyDown(KeyCode.C)) {doppl_state = States.doppl_battle_3f; spot_state = States.the_spot_1; currentScene = Scenes.spot;}
		}
		
	void doppl_battle_3c () {
		text.text="You focus on listening to the angry uneartly yells.  Slowly the voice starts to become more intelligible... More human...You start to question if you are really hearing it speak...\n\n"+
		"End ... Suffering ... you must ...\n\n"+
		"The torso of the mech opens up and the pilot reveals themself, its body vaugely humaniod creature with extra tentacles. The torn military uniform, and helmet indicate it was once a pilot "+
		"like yourself. It collapses onto the top of the mech, falling silent.  Your curiousty of what is under the helmet eats away at you, but in the back of your mind you know something"+
		"is horrifcally off.\n\n"+
		"K to make sure they're dead\n\n"+
		"H to take off thier helmet\n\n"+
		"W to walk away with the gattling gun.";
		if (Input.GetKeyDown(KeyCode.K)) { 
			doppl_state = States.doppl_battle_3a; has_ggun = true;}
		if (Input.GetKeyDown(KeyCode.H)) { 
			doppl_state = States.doppl_battle_3d;}
		if (Input.GetKeyDown(KeyCode.W)) { 
			doppl_state = States.doppl_battle_3f; doppl_dead = false; has_ggun = true; spot_state = States.the_spot_1; currentScene = Scenes.spot;}
	}
	
	void doppl_battle_3d () {
	text.text = "Wary, you approach the mech and pick up the pilot. Carefully you pull the helmet off...\n\n"+
		"C to Continue";
		if (Input.GetKeyDown(KeyCode.C)) { gameover= true; currentScene = Scenes.game_over; game_over_state = States.game_over_doppl_helm; currentHp = 0; currentSanity = 0;}
	}
	void doppl_battle_3f () {
		text.text = "You have returned to the shipyard where fought the corrupted mech.";
			if (doppl_dead == true) { text.text+= "You move closer to inspect the remains of the fallen mech. Yep. Still dead.\n\n"+
			"R to return to the spot";}
			else if (doppl_dead == false) { text.text+= "You approach the area where the corrupted mech was last at, only to discover the corrupted mech had dissappeared. "+
			"Only a trail of green blood, and piles of flesh leading away from the area were to be found. You make a note to keep your gaurd up.\n\n"+
			"R to return to the spot";}
		if (Input.GetKeyDown(KeyCode.R)) {currentScene = Scenes.spot;}
	}		
	
	void tunnel_0 () {
		text.text= " Weapons at the ready you slowly make your way down the dark tunnel.  You turn on your camera systems for low light enviroments. " +
		" The feed camera transforms from shapless darkness, into discernable obstacles in various shades of green.  Despite the monsterous outside apperances, the  " +
		"passage was still steel and concrete a small comfort in an area overun with monsters.\n\n" +
		"With out warning, agigantic eyeball the size of your mech appears "+
		"500 meters infront of you. It's black pupil narrows as it starts to glow, and a indeciperable language floods your comm stystem.\n\n" +
		"press SPACE to continue";
			if(Input.GetKeyDown(KeyCode(Space))){ }
		
			}
			
	void tunnel_1 () {
		text.text= "The glow of the continues to intensify becoming almost unbearable, if you movements were not constratin by the enviroment you could use "+
		"your boosters to thier full potential... The joys of tight spaces.  Your navigation systems highlights places to the take cover along the tunnel as "+
		"you advance towards the eye.\n\n"+
		"Target is " + eye_distance + "m away..." + "Hostile action in: ";
		;
		if(Input.GetKeyDown(KeyCode(Space))){ }
		
	}
	
	void tunnel_1c () {
		text.text= " Quickly you reteat back the way you came. You hope that when you return that the eye has gone away, but you know it will be watching, waiting for you.\n\n"+
		"press SPACE to continue";
		;
		if(Input.GetKeyDown(KeyCode(Space))){ }
		
	}
	
	void_tunnel_cover() {
		text.text= "Urgently you move into one of small "
	}
	
	void game_over_reg () {
		text.text = "You find the source of the explosions. Another mech desperately fighting against a horde creatures.\n\n"+
			"H to help\n\n"+
				"R to go back to the spot";
	}
	
	void game_over_doppl_helm () {
		text.text = "The helmet sildes of the pilot. Impossible! It can't be reall. Perhaps it was camera system was malfunctioning.  You open up the torso of your mech to see for yourself. "+
			"You start to shake violently as your eyes confirm the horiffic truth. The familar hair, the unmistakable eyes, you have know this pilot your entire life. How could it be?"+
			"Closing your eyes you to to unsee it, but it as if your eyelids had become transparent. Rushing back into your mech, you throw the body far away from you as possible. You close the cockpit, "+
			"but you still see the pilot. When the cameras turn back on, they are stuck on the image of the pilot.\n\n"+
			"'Have I gone mad?' you shout\n\n"+
			"'You have, but I have the cure for what ails us.'"+
			"Your heart stops as you feel a slimy tentacle wrap around your neck."+
			"With a loud snap your world goes black. The horrific image is gone.\n\n"+
			"Game Over. Press Space to go back to the title.";
		
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
		else if (doppl_state == States.doppl_battle_2a) {doppl_battle_2a();}
		else if (doppl_state == States.doppl_battle_2b) {doppl_battle_2b();}
		else if (doppl_state == States.doppl_battle_2c) {doppl_battle_2c();}
		else if (doppl_state == States.doppl_battle_2f) {doppl_battle_2f();}
		else if (doppl_state == States.doppl_battle_3) {doppl_battle_3();}
		else if (doppl_state == States.doppl_battle_3a) {doppl_battle_3a();}
		else if (doppl_state == States.doppl_battle_3b) {doppl_battle_3b();}
		else if (doppl_state == States.doppl_battle_3c) {doppl_battle_3c();}
		else if (doppl_state == States.doppl_battle_3d) {doppl_battle_3d();}
		else if (doppl_state == States.doppl_battle_3f) {doppl_battle_3f();}
	}
	
	void the_tunnel () {
		print(tunnel_state);
			if ( tunnel_state == States.tunnel_0) {tunnel_0();}
			else if ( tunnel_state ==States.tunnel_1) {tunnel_1();}
			else if ( tunnel_state ==States.tunnel_1c) {tunnel_1c();}
			else if ( tunnel_state == States.tunnel_passage) {tunnel_passage();}
			else if ( tunnel__state == States.tunnel_cover) {tunnel_cover();}
			else if ( tunnel_state == States.tunnel_fire) {tunnel_fire();}
		
	}
	
	void game_over_screen() {
		print(game_over_state);
		if (game_over_state == States.game_over_doppl_helm) {game_over_doppl_helm();}
		else if (game_over_state == States.game_over_reg) {game_over_reg();}
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
		else if (currentScene == Scenes.doppl_battle) {doppl_battle();}
		else if (currentScene == Scenes.game_over) { game_over_screen();}
		else if (currentScene == Scenes.the_tunnel) { the_tunnel();}
		

		
		if (gameover == true) {
			currentScene = Scenes.game_over;
			if (Input.GetKeyDown(KeyCode.Space)) {
				Start ();
			}
		}
		
		if (currentScene == Scenes.the_tunnel && active_eye == true  ) {displayTime();}
		
		hitpoints.text = "HP: " + currentHp;
		sanity.text= "Sanity: " + currentSanity;
		
			
		GameOver();
		if (Input.GetKeyDown(KeyCode.P)) {
			Start ();
		}
		
		
			} 
		}
	
