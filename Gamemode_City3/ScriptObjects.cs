if(!isObject(C3Wallet))
{
	new scriptObject(C3Wallet) {};
}

function C3Wallet::add(%this,%obj,%arg)
{	
	switch$(%obj)
	{
		case "money":
			%this.money += %arg;
		case "bankm":
			%this.bankm += %arg;
	}
	%this.cashUpdated = 1;
}

function C3Wallet::subtract(%this,%client,%obj,%arg)
{
	switch$(%obj)
	{
		case "money":
			%this.money -= %arg;
		case "bankm":
			%this.bankm -= %arg;
	}
	%this.cashUpdated = 1;
}

function C3Wallet::trade(%this,%client,%target,%obj,%task,%arg)
{
	switch$(%obj)
	{
		case "money":
			%type = 1;
		case "bankm":
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
				%this.bankm -= %arg;
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
				%this.bankm += %arg;
				messageClient(%this.client, '', "\c2$" @ %arg @ " has been transferred to your account by " @ %target.name @ ".");
			}
	}
	%this.cashUpdated = 1;
}

function C3Wallet::Create(%this,%client)
{
	if(isObject(%client))
	{
		if(!isObject(%client.C3Wallet))
		{
			%client.C3SetAlias = 0;
			%this.client = %client;
			%this.client.C3Wallet = %this;
			if($City3::DebugMode)
				messageClient(%client, '', "\c2CITY3 DEBUG: You now have a script object!");
		}
	}
	else
		return 1;
		
	return 0;
}

if(!isObject(C3Group))
{
	new scriptObject(C3Group) {};
}

function C3Group::Create(%this,%client,%groupname,%type)
{
	if(C3CheckGroupName(%groupname))
	{
		messageClient(%client, '', "\c2A group with that name already exists!");
		return;
	}
	%this.name = %groupname;
	%this.membernum = 0;	
	%this.type = %type;

	$City3::Groupnum += 1;
	$City3::Groups[$City3::Groupnum] = %groupname;

	%client.C3JoinGroup(%this);
	%this.addmember(%client);
	%this.admin = %client.name;

	messageClient(%client, '', "\c2You have created the group " @ %groupname @ "!");
}
function C3Group::addmember(%this,%client)
{
	%this.membernum += 1;
	%this.member[%this.membernum] = %client.name;
}
function C3Group::listmembers(%this,%client)
{
	for(%i=0; %i < %this.membernum; %i++)
	{
		messageClient(%client, '', "\c2" @ %this.member[%i]);
	}
}
function C3Group::getMembers(%this,%client)
{
	for(%i=0; %i < %this.membernum; %i++)
	{
		%num = %i;
	}
	return %num;
}