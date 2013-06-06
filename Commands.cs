// ============================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  Commands for City3
// ============================================================

function serverCmdPay(%client,%target,%amount)
{
	%target = findclientbyname(%target);

	if(!%client.player)
		return;
	if(!%target.player)
		return;
	if(%client.City3Wallet.money < %amount)
	{
		messageClient(%client, '', "\c2You don't have enough money to pay that!");
		return;
	}
	%client.City3Wallet.trade(%client,%target,"money","give",%amount);
	%target.City3Wallet.trade(%target,%client,"money","recieve",%amount);

}

function serverCmdGui(%client)
{
	if(%client.City3HasClient)
	{
		if(%client.isAdmin)
			commandToClient('City3_OpenGui',1); //Open Gui with admin perms. Will be used later.
		else
			commandToClient('City3_OpenGui');
		return;
	}
	messageClient(%client, '', "\c2You don't have the City3 Client! Please download it: (link here)");
}

function serverCmdSetAlias(%client,%arg0,%arg1,%arg2)
{
	if(isObject(%client))
	{
		if(!arg2 $= "")
		{
			messageClient(%client, '', "\c2Please keep your alias less than two names. (First and last)");
			return;
		}
		if(!%client.City3SetAlias)
		{
			%client.City3Alias_First = %arg0;
			%client.City3Alias_Last = %arg1;
			messageClient(%client, '', "\c2Your alias has been set to " @ %client.City3Alias_First @ " " @ %client.City3Alias_Last);	
			%client.City3SetAlias = 1;
		}
		else
			messageClient(%client, '', "\c2You have already set your alias!");
	}
}