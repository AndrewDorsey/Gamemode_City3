// ================================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  Datblocks file
// ================================================================= 

// =================================================================
// Main 
// =================================================================

exec("./Package.cs");
exec("./ScriptObject.cs");
exec("./Saving.cs"); //I'll change it to Database.cs when someone explains how that shit works.
exec("./Labor.cs");
exec("./Common.cs");
exec("./Time.cs");
exec("./Datablocks.cs");
exec("./Config.cs");
exec("./Commands.cs");

// =================================================================
// Bricks
// =================================================================

exec("./Bricks/Resources/Iron.cs");
exec("./Bricks/Resources/Silver.cs");
exec("./Bricks/Resources/Platinum.cs");
exec("./Bricks/Resources/Oak.cs");
exec("./Bricks/Resources/Maple.cs");
exec("./Bricks/Resources/Morning.cs");

// =================================================================
// Tools
// =================================================================

exec("./Items/Tools/Pickaxe.cs");
exec("./Items/Tools/Axe.cs");
exec("./Items/Tools/Tazer.cs");

// =================================================================
// Startup
// =================================================================

City3Startup();


function City3GetLineCount(%doc)
{
	%lineCt = -1; //Start at line -1 so that the first line read is line 0.
	%file = new fileObject();
	%file.openForRead(%doc);
	while(!%file.isEOF())
	{
		%lineCt++;
		%file.readLine();
		//echo("Line" SPC %lineCt @ ":" SPC %file.readLine());
	}
	%file.close();
	%file.delete();
	
	return %lineCt;
}

function City3GetSpecificLine(%doc,%line)
{
	%lineCt = -1; //Start at line -1 so that the first line read is line 0.
	%file = new fileObject();
	%file.openForRead(%doc);
	while(!%file.isEOF())
	{
		if(%lineCt == (%line - 1)) //The line before the line we want.
		{
			%contents = %file.readLine(); //Set contents equal to the line we want.
			return;
		}
		%file.readLine();
		%lineCt++;
	}
	%file.close();
	%file.delete();
	
	return %contents;
}

function City3GetRandomAlias(%client)
{
	%lineCtFirst = City3GetLineCount("Add-Ons/Gamemode_City3/Data/FirstNames.txt");
	%lineCtLast = City3GetLineCount("Add-Ons/Gamemode_City3/Data/LastNames.txt");
	%randomFirst = getRandom(0,%lineCtFirst);
	%randomLast = getRandom(0,%lineCtLast);
	%first = City3GetSpecificLine("Add-Ons/Gamemode_City3/Data/FirstNames.txt",%randomFirst);
	%last = City3GetSpecificLine("Add-Ons/Gamemode_City3/Data/LastNames.txt",%randomLast);
	%client.City3Alias_First = %first;
	%client.City3Alias_Last = %last;
	%client.City3SetAlias = 0;
	if($City3::DebugMode)
		echo("City3 Console: " @ %client.name @ "'s ALIAS is " @ %client.City3Alias_First @ " " @ %client.City3Alias_Last);
}

function City3JoinGroup(%client,%group)
{
	%groupnum = %client.City3Groupnum + 1;
	%client.City3Group[%groupnum] = %group;
}

function City3CheckGroupName(%groupname)
{
	for(%i=0; %i < $City3::Groupnum; %i++)
	{
		%task = $City3::Groups[%i];
		if(%task $= %groupname)
			return 1;
	}
	return 0;
}