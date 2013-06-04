function serverCmdPay(%client,%target,%amount)
{
	%target = findclientbyname(%target);

	if(!%client.player)
		return;
	if(!%target.player)
		return;
	if(%client.C3Wallet.money < %amount)
	{
		messageClient(%client, '', "\c2You don't have enough money to pay that!");
		return;
	}
	%client.C3Wallet.trade(%client,%target,"money","give",%amount);
	%target.C3Wallet.trade(%target,%client,"money","recieve",%amount);

}

function serverCmdGui(%client)
{
	if(%client.C3HasClient)
	{
		if(%client.isAdmin)
			commandToClient('C3_OpenGui',1); //Open Gui with admin perms. Will be used later.
		else
			commandToClient('C3_OpenGui');
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
		if(!%client.C3SetAlias)
		{
			%client.C3Alias_First = %arg0;
			%client.C3Alias_Last = %arg1;
			messageClient(%client, '', "\c2Your alias has been set to " @ %client.C3Alias_First @ " " @ %client.C3Alias_Last);	
			%client.C3SetAlias = 1;
		}
		else
			messageClient(%client, '', "\c2You have already set your alias!");
	}
}