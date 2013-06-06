// ============================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  ScriptObjects heh
// ============================================================

if(!isObject(City3Wallet))
{
	new scriptObject(City3Wallet) {};
}

function City3Wallet::add(%this,%obj,%arg)
{	
	switch$(%obj)
	{
		case "money":
			%this.money += %arg;
		case "bank":
			%this.bank += %arg;
	}
	%this.cashUpdated = 1;
}

function City3Wallet::subtract(%this,%client,%obj,%arg)
{
	switch$(%obj)
	{
		case "money":
			%this.money -= %arg;
		case "bank":
			%this.bank -= %arg;
	}
	%this.cashUpdated = 1;
}

function City3Wallet::trade(%this,%client,%target,%obj,%task,%arg)
{
	switch$(%obj)
	{
		case "money":
			%type = 1;
		case "bank":
			%type = 2;
	}

	switch$(%task)
	{
		case "give":
			if(%type == 1)
			{
				%this.money -= %arg;
				messageClient(%this.client, '', "\c2You have paid $" @ %arg @ " to " @ %target.name @ ".");
			}
			if(%type == 2)
			{
				%this.bank -= %arg;
				messageClient(%this.client, '', "\c2You have transfered $" @ %arg @ " to " @ %target.name @ ".");
			}
		case "recieve":
			if(%type == 1)
			{
				%this.money += %arg;
				messageClient(%this.client, '', "\c2You have recieved $" @ %arg @ " from " @ %target.name @ ".");
			}
			if(%type == 2)
			{
				%this.bank += %arg;
				messageClient(%this.client, '', "\c2$" @ %arg @ " has been transferred to your account by " @ %target.name @ ".");
			}
	}
	%this.cashUpdated = 1;
}

function City3Wallet::Create(%this,%client)
{
	if(isObject(%client))
	{
		if(!isObject(%client.City3Wallet))
		{
			%client.City3SetAlias = 0;
			%this.client = %client;
			%this.client.City3Wallet = %this;
			if($City3::DebugMode)
				messageClient(%client, '', "<color:ffffff>You now have a script object.");
		}
	}
	else
		return 1;
		
	return 0;
}

if(!isObject(City3Group))
{
	new scriptObject(City3Group) {};
}

function City3Group::Create(%this,%client,%groupname,%type)
{
	if(City3CheckGroupName(%groupname))
	{
		messageClient(%client, '', "\c2A group with that name already exists!");
		return;
	}
	%this.name = %groupname;
	%this.membernum = 0;	
	%this.type = %type;

	$City3::Groupnum += 1;
	$City3::Groups[$City3::Groupnum] = %groupname;

	%client.City3JoinGroup(%this);
	%this.addmember(%client);
	%this.admin = %client.name;

	messageClient(%client, '', "\c2You have created the group " @ %groupname @ "!");
}
function City3Group::addmember(%this,%client)
{
	%this.membernum += 1;
	%this.member[%this.membernum] = %client.name;
}
function City3Group::listmembers(%this,%client)
{
	for(%i=0; %i < %this.membernum; %i++)
	{
		messageClient(%client, '', "\c2" @ %this.member[%i]);
	}
}
function City3Group::getMembers(%this,%client)
{
	for(%i=0; %i < %this.membernum; %i++)
	{
		%num = %i;
	}
	return %num;
}