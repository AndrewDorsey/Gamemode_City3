// ============================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  Common operations of City3
// ============================================================

// ============================================================
// Section 1: Startup
// ============================================================

function City3Startup()
{
	if(!$City3::HasStarted)
	{
		echo("-Starting Up City3");
		echo("-Loading City3 Data");
		new scriptObject(City3Data)
		{
			class = "Jassy";
			AutomaticLoading = 1;
			filePath = "config/server/City3/Data/";
		};
		City3Data.addField("Name", "Name");
		City3Data.addField("LastOnline", "Currently");
		City3Data.addField("Mute", 0);

		City3Data.addField("Money", 0);
		City3Data.addField("Bank", 0);

		City3Data.addField("Demerits", 0);
		City3Data.addField("JailData", 0);
		City3Data.addField("Record", 0);
		City3Data.addField("Bounty", 0);

		City3Data.addField("JobID", "Job1");
		City3Data.addField("JobEXP", 0);
		City3Data.addField("Education", 0);
		City3Data.addField("Student", 0);

		City3Data.addField("Lumber", 0);
		City3Data.addField("Ore", 0);

		City3Data.addField("CellPhone", 0);

		City3Data.addField("Hunger", "10");
		City3Data.addField("Tools", "");

		echo("   Creating City3 Minigame");
		new ScriptObject(City3Mini) 
			{
			class = miniGameSO;
				
			brickDamage = 1;
			brickRespawnTime = 15000;
			colorIdx = -1;
			
			enableBuilding = 1;
			enablePainting = 1;
			enableWand = 1;
			fallingDamage = 1;
			inviteOnly = 0;
			
			points_plantBrick = 0;
			points_breakBrick = 0;
			points_die = 0;
			points_killPlayer = 0;
			points_killSelf = 0;
			
			playerDatablock = playerCity3;
			respawnTime = 5500;
			selfDamage = 1;
			
			playersUseOwnBricks = 0;
			useAllPlayersBricks = 1;
			useSpawnBricks = 0;
			VehicleDamage = 1;
			vehicleRespawnTime = 10000;
			weaponDamage = 1;
		
			numMembers = 1;

			vehicleRunOverDamage = 1;
		};
	}
}

// ============================================================
// Section 2: Datablocks
// ============================================================

datablock ItemData(droppedcashItem)
{
	category = "Weapon";
	className = "Weapon";
	
	shapeFile = "base/data/shapes/brickWeapon.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	doColorShift = true;
	colorShiftColor = "0 0.5 0 1";
	image = dropcashImage;
	candrop = true;
	canPickup = false;
};
datablock ShapeBaseImageData(droppedcashImage)
{
	shapeFile = "base/data/shapes/brickWeapon.dts";
	emap = true;
	
	doColorShift = true;
	colorShiftColor = dropCashItem.colorshiftcolor;
	canPickup = false;
};

// ============================================================
// Section 3: Misc. functions
// ============================================================

function serverCmdMute(%client, %target)
{
	if(!%client.isAdmin)
		return MessageClient(%client,'',"\c6You must be an admin to use this command.");
	if(%target $= "")
		return MessageClient(%client,'',"\c6You must enter someone to mute.");
	%target = findclientbyname(%target);
	if(%target == %client)
		return MessageClient(%client,'',"\c6You cannot mute yourself.");
	if(!isobject(%target))
		return MessageClient(%client,'',"\c6Target not found.");
	if(%target.isAdmin)
		return MessageClient(%client,'',"\c6You cannot mute an admin.");
	if(CRPData.data[%target.bl_id].Value["Mute"])
		return MessageClient(%client,'',"\c3"@ %target.name @"\c6 is already muted.");
	MessageClient(%client,'',"\c6You have muted \City3"@ %target.name @"\c6.");
	MessageClient(%target,'',"\c3"@ %client.name @"\c6 has muted you.");
	CRPData.data[%target.bl_id].Value["Mute"] = 1;
}