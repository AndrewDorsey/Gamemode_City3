// ============================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  Common operations of City3
// ============================================================

if(!isObject(TazerItem))
{
	//AddDamageType("Tazer",   '<bitmap:Add-Ons/Gamemode_City3/Shapes/CI/CI_Tazer> %1',    '%2 <bitmap:Add-Ons/Gamemode_City3/Shapes/CI/CI_Tazer> %1', 0.5, 1);
	
	datablock AudioProfile(taserExplosionSound)
	{
		filename	 = "Add-Ons/Gamemode_City3/Data/Sounds/Tazer.wav";
		description = AudioClosest3d;
		preload = true;
	};
	
	datablock ExplosionData(TazerExplosion : hammerExplosion)
	{
		soundProfile = taserExplosionSound;
	};
	datablock ProjectileData(TazerProjectile)
	{	
		directDamage		= 15;
		directDamageType	= $DamageType::Tazer;
		radiusDamageType	= $DamageType::Tazer;
		explosion		= TazerExplosion;
		
		muzzleVelocity		= 50;
		velInheritFactor	= 1;
	
		armingDelay		= 0;
		lifetime		= 100;
		fadeDelay		= 70;
		explodeOnDeath		= false;
		bounceElasticity	= 0;
		bounceFriction		= 0;
		isBallistic		= false;
		gravityMod 		= 0.0;
	
		hasLight		= false;
		lightRadius		= 1.0;
		lightColor		= "0 0.25 0.5";
	};
	datablock ItemData(TazerItem)
	{
		category		= "Weapon";
		className		= "Weapon";
		
		shapeFile		= "Add-Ons/Gamemode_City3/Data/Shapes/Tazer/Tazer.dts";
		mass			= 1;
		density 		= 0.2;
		elasticity		= 0.2;
		friction		= 0.6;
		emap			= true;
	
		uiName			= "Tazer";
		iconName		= "Add-Ons/Gamemode_City3/Shapes/ItemIcons/Icon_Tazer";
		doColorShift		= false;
	
		image			= TazerImage;
		canDrop			= true;
	
	};
	datablock ShapeBaseImageData(TazerImage)
	{
		
		shapeFile		= "Add-Ons/Gamemode_City3/Data/Shapes/Tazer/Tazer.dts";
		emap			= true;
		mountPoint		= 0;
		eyeOffset		= "0 0 0";
		offset			= "0 0 0";
		correctMuzzleVector	= false;
		className		= "WeaponImage";
		
		item			= TazerItem;
		ammo			= " ";
		projectile		= TazerProjectile;
		projectileType		= Projectile;
		
		melee			= true;
		doRetraction		= false;
		armReady		= true;
		
		doColorShift		= false;
		colorShiftColor		= "0 0 0 1";

		stateName[0]			= "Activate";
		stateTimeoutValue[0]		= 0.1;
		stateTransitionOnTimeout[0]	= "Ready";

		stateName[1]			= "Ready";
		stateTransitionOnTriggerDown[1]	= "PreFire";
		stateAllowImageChange[1]	= true;
	
		stateName[2]			= "PreFire";
		stateScript[2]			= "onPreFire";
		stateAllowImageChange[2]	= false;
		stateTimeoutValue[2]		= 0.085;
		stateTransitionOnTimeout[2]	= "Fire";
	
		stateName[3]			= "Fire";
		stateTimeoutValue[3]		= 0.1;
		stateTransitionOnTimeout[3]	= "PreCheckFire";
		stateFire[3]			= true;
		stateAllowImageChange[3]	= false;
		stateSequence[3]		= "Fire";
		stateScript[3]			= "onFire";
		stateWaitForTimeout[3]		= true;

		stateName[4]			= "PreCheckFire";
		stateTimeoutValue[4]		= 0.15;
		stateTransitionOnTimeout[4]	= "CheckFire";
		stateScript[4]			= "onStopFire";

		stateName[5]			= "CheckFire";
		stateTransitionOnTriggerUp[5]	= "StopFire";
		stateTransitionOnTriggerDown[5]	= "PreFire";
	
		stateName[6]			= "StopFire";
		stateTransitionOnTimeout[6]	= "Ready";
		stateTimeoutValue[6]		= 0.1;
		stateAllowImageChange[6]	= false;
		stateWaitForTimeout[6]		= true;
		stateSequence[6]		= "StopFire";
		stateScript[6]			= "onStopFire";
	};
}
function TazerImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armAttack);
}
function TazerImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}

// misc shit functions

function taserProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	if((%col.getType() & $typeMasks::playerObjectType) && isObject(%col.client))
	{
		tumble(%col);
	}
}