// ============================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  Game overrides
// ============================================================

package City3Pack
{
	function GameConnection::autoAdminCheck(%client)
	{
		%v = Parent::autoAdminCheck(%client);
		schedule(500,0,"commandToClient",%client,'City3_Handshake',$City3::Server::Version);
		City3Wallet.create(%client);
		City3GetRandomAlias(%client);
		if($City3::DebugMode)
			messageClient(%client, '', "Your temporary alias is " @ %client.City3Alias_First @ " " @ %client.City3Alias_Last);

		Jassy.loadData(%this);
		%this.lvd = getDateTime();

		return %v;
	}

	function GameConnection::onClientLeaveGame(%this)
	{
		Jassy.SaveData();

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
					%obj.client.City3wallet.add("cash",%col.value);
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
activatepackage(City3Pack);

function City3_returnHandshake(%client)
{
	%client.City3HasClient = 1;
}