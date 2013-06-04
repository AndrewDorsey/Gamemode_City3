exec("./City3/Commands.cs");
exec("./City3/Config.cs");
exec("./City3/Database.cs");
exec("./City3/Packages.cs");
exec("./City3/ScriptObjects.cs");
exec("./City3/Time.cs");

function C3GetLineCount(%doc)
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

function C3GetSpecificLine(%doc,%line)
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

function C3GetRandomAlias(%client)
{
	%lineCtFirst = C3GetLineCount("Add-Ons/Gamemode_City3/City3/Data/FirstNames.txt");
	%lineCtLast = C3GetLineCount("Add-Ons/Gamemode_City3/City3/Data/LastNames.txt");
	%randomFirst = getRandom(0,%lineCtFirst);
	%randomLast = getRandom(0,%lineCtLast);
	%first = C3GetSpecificLine("Add-Ons/Gamemode_City3/City3/Data/FirstNames.txt",%randomFirst);
	%last = C3GetSpecificLine("Add-Ons/Gamemode_City3/City3/Data/LastNames.txt",%randomLast);
	%client.C3Alias_First = %first;
	%client.C3Alias_Last = %last;
	%client.C3SetAlias = 0;
	if($City3::DebugMode)
		echo("City3 Console: " @ %client.name @ "'s ALIAS is " @ %client.C3Alias_First @ " " @ %client.C3Alias_Last);
}

function C3JoinGroup(%client,%group)
{
	%groupnum = %client.C3Groupnum + 1;
	%client.C3Group[%groupnum] = %group;
}

function C3CheckGroupName(%groupname)
{
	for(%i=0; %i < $City3::Groupnum; %i++)
	{
		%task = $City3::Groups[%i];
		if(%task $= %groupname)
			return 1;
	}
	return 0;
}