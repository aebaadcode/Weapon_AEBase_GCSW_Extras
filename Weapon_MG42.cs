datablock AudioProfile(GCats_MG42FireSound)
{
   filename    = "./Sounds/MG42.wav";
   description = MediumClose3D;
   preload = true;
};

AddDamageType("GCats_MG42",'<bitmap:Add-ons/Weapon_AEBase_GCSW_Extras/Icons/CI_MG42> %1','%2 <bitmap:Add-ons/Weapon_AEBase_GCSW_Extras/Icons/CI_MG42> %1',0.2,1);

//////////
// item //
//////////
datablock ItemData(GCats_MG42Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./MG42/MG42.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "SW: MG42";
	iconName = "./Icons/icon_MG42";
	doColorShift = true;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = GCats_MG42Image;
	canDrop = true;

	AEAmmo = 100;
	AEType = AE_HeavyRAmmoItem.getID();
	AEBase = 1;

	Auto = true; 
	RPM = 1200;
	recoil = "Heavy"; 
	uiColor = "0.75 0 0";
	description = "The MG 42 is a general-purpose machine gun designed in Nazi Germany. It was intended to replace the earlier MG 34, which was more expensive to produce. The MG 42 was used extensively by the Wehrmacht as well as the Waffen-SS after entering production in 1942. This infamous weapon was nicknamed Hitler's Buzzsaw by the allies because of it's staggeringly high rate of fire and lethality.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(GCats_MG42Image)
{
   // Basic Item properties
	shapeFile = "./MG42/MG42.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = GCats_MG42Item;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = GCats_ShellLongDebris;
   shellExitDir        = "1 0.2 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = GCats_MG42SafetyImage;
    scopingImage = GCats_MG42IronsightImage;
	doColorShift = true;
	colorShiftColor = GCats_MG42Item.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 23;
	projectileCount = 1;
	projectileHeadshotMult = 2.2;
	projectileVelocity = 500;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate
    projectileRDismemberBody = 1;
    projectileRDismemberHead = 1;

	recoilHeight = 0.4;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 200; // m
	spreadBase = 320;
	spreadMin = 320;
	spreadMax = 640;

	screenshakeMin = "0.15 0 0"; 
	screenshakeMax = "0.25 0 0"; 

    directDamageType = $DamageType::GCats_MG42;
    directHeadDamageType = $DamageType::GCats_MG42;

	farShotSound = RifleADistantSound;
	farShotDistance = 40;
	
	projectileFalloffStart = 96;
	projectileFalloffEnd = 192;
	projectileFalloffDamage = 0.5;
	
	R_MovePenalty = 0.75;
	
	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	staticHitscan = true;
	staticEffectiveRange = 110;
	staticTotalRange = 2000;
	staticGravityScale = 1.5;
	staticSwayMod = 2;
	staticEffectiveSpeedBonus = 5;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = ""; // defaults to AEBulletStaticShape
	staticScaleCalibre = 0.25;
	staticScaleLength = 0.25;
	staticUnitsPerSecond = $ae_RifleUPS;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.
   // Initial start up state
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
	
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 6.85; //-0.15 for magazine weps since reloadend is now 0.25 instead of 0.1
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadEnd";
	stateWaitForTimeout[7]			= true;
    stateSound[7] = GCats_MG42ReloadSound;
	
	stateName[8]				= "ReloadEnd";
	stateTimeoutValue[8]			= 0.25;
	stateScript[8]				= "onReloadEnd";
	stateTransitionOnTimeout[8]		= "Reloaded";
	stateWaitForTimeout[8]			= true;
	
	stateName[9]				= "FireLoadCheckA";
	stateScript[9]				= "AEMagLoadCheck";
	stateTimeoutValue[9]			= 0.05;
	stateTransitionOnTimeout[9]		= "FireLoadCheckB";
	
	stateName[10]				= "FireLoadCheckB";
	stateTransitionOnAmmo[10]		= "Ready";
	stateTransitionOnNoAmmo[10]		= "Reload";	
	stateTransitionOnNotLoaded[10]  = "Ready";
		
	stateName[11]				= "Reloaded";
	stateTimeoutValue[11]			= 0.1;
	stateScript[11]				= "AEMagReloadAll";
	stateTransitionOnTimeout[11]		= "Ready";

	stateName[12]				= "ReadyLoop";
	stateTransitionOnTimeout[12]		= "Ready";

	stateName[13]          = "Empty";
	stateTransitionOnTriggerDown[13]  = "Dryfire";
	stateTransitionOnLoaded[13] = "Reload";
	stateScript[13]        = "AEOnEmpty";

	stateName[14]           = "Dryfire";
	stateTransitionOnTriggerUp[14] = "Empty";
	stateWaitForTimeout[14]    = false;
	stateScript[14]      = "onDryFire";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function GCats_MG42Image::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
    %obj.playAudio(0, GCats_MG42FireSound);
  
	cancel(%obj.reload1Schedule);
	cancel(%obj.reload2Schedule); 
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	cancel(%obj.reload5Schedule);
	cancel(%obj.reload6Schedule);

	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function GCats_MG42Image::onReloadEnd(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function GCats_MG42Image::onDryFire(%this, %obj, %slot)
{
	serverPlay3D(GCats_EmptySound, %obj.getHackPosition());
}

// MAGAZINE DROPPING

function GCats_MG42Image::onReloadStart(%this,%obj,%slot)
{
   %obj.schedule(100, "aeplayThread", "2", "plant");
   %obj.schedule(350, "aeplayThread", "2", "plant");
   %obj.reload1Schedule = %obj.schedule(1000, "aeplayThread", "3", "shiftleft");
   %obj.reload2Schedule = %obj.schedule(1000, "aeplayThread", "2", "shiftright");
   %obj.reload4Schedule = %obj.schedule(2250, "aeplayThread", "3", "plant");
   %obj.reload5Schedule = %obj.schedule(4250, "aeplayThread", "2", "shiftright");
   %obj.reload6Schedule = %obj.schedule(5500, "aeplayThread", "3", "shiftleft");
}

function GCats_MG42Image::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function GCats_MG42Image::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function GCats_MG42Image::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload1Schedule);
	cancel(%obj.reload2Schedule); 
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	cancel(%obj.reload5Schedule);
	cancel(%obj.reload6Schedule);
	
	parent::onUnMount(%this,%obj,%slot);	
}

///////// IRONSIGHTS?

datablock ShapeBaseImageData(GCats_MG42IronsightImage : GCats_MG42Image)
{
	recoilHeight = 0.1;

	scopingImage = GCats_MG42Image;
	sourceImage = GCats_MG42Image;
	
	isScopedImage = true;
	
	offset = "0 0 0";
	eyeOffset = "-0.0009 0.1 -0.68";
	rotation = eulerToMatrix( "0 -20 0" );

	spreadBase = 100;
	spreadMin = 100;
	spreadMax = 100;

	desiredFOV = 53;
	projectileZOffset = 0;
	R_MovePenalty = 0.25;
	
	stateName[7]				= "Reload";
	stateScript[7]				= "onDone";
	stateTimeoutValue[7]			= 1;
	stateTransitionOnTimeout[7]		= "";
	stateSound[7]				= "";
};

function GCats_MG42IronsightImage::onDone(%this,%obj,%slot)
{
	%obj.reloadTime[%this.sourceImage.getID()] = getSimTime();
	%obj.mountImage(%this.sourceImage, 0);
}

function GCats_MG42IronsightImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);
}

function GCats_MG42IronsightImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
    %obj.playAudio(0, GCats_MG42FireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function GCats_MG42IronsightImage::onDryFire(%this, %obj, %slot)
{
	serverPlay3D(GCats_EmptySound, %obj.getHackPosition());
}

// HIDES ALL HAND NODES

function GCats_MG42IronsightImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsIn3Sound);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function GCats_MG42IronsightImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection"))
		%obj.client.play2D(AEAdsOut3Sound);
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}