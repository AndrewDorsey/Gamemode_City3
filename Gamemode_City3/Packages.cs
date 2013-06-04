package C3Pack
{
	function GameConnection::autoAdminCheck(%client)
	{
		%v = Parent::autoAdminCheck(%client);
		schedule(500,0,"commandToClient",%client,'C3_Handshake',$C3::Server::Version);
		C3Wallet.create(%client);
		C3GetRandomAlias(%client);
		if($City3::DebugMode)
			messageClient(%client, '', "Your temporary alias is " @ %client.C3Alias_First @ " " @ %client.C3Alias_Last);

		CityDB_loadUser(%this);
		%this.lvd = getDateTime();

		return %v;
	}

	function GameConnection::onClientLeaveGame(%this)
	{
		CityDB_saveUser(%this);

		parent::onClientLeaveGame(%this);
	}

	function Armor::onCollision(%this,%obj,%col,%thing,%other) //Cash pickup
	{
		if(%col.getDatablock().getName() $= "droppedcashitem")
		{
			if(isObject(%obj.client))
			{
				if(isObject(%col))
				{
					%obj.client.c3wallet.add("cash",%col.value);
					messageClient(%obj.client, '', "\c6You have picked up" SPC CashStr(%col.value,"") @ "\c6.");
					%col.canPickup = false;
					%col.delete();
					
					return;
				} else {
					%col.delete();
					MissionCleanup.remove(%col);
				}
			}
		} else {
			Parent::onCollision(%this,%obj,%col,%thing,%other);
		}
	}
};
activatepackage(C3Pack);

function C3_returnHandshake(%client)
{
	%client.c3HasClient = 1;
}