function CityDB_saveUser(%client)
{
	%path = $City3::UserSaves @ %client.BL_ID @ ".dat";

	%file = new FileObject();
	%file.openForWrite(%path);

	%file.writeLine("name lvd cash bank alias");

	%file.writeLine(%client.name);
	%file.writeLine(%client.lvd);
	%file.writeLine(%client.C3Wallet.money);
	%file.writeLine(%client.C3Wallet.bankm);

	%file.writeLine(%client.C3Alias_First NL %client.C3Alias_Last);

	%file.close();
	%file.delete();
}

function CityDB_loadUser(%client)
{
	%path = $City3::UserSaves @ %client.BL_ID @ ".dat";

	if(!fileExists(%path))
	{
		if($City3::DebugMode)
			warn("Client with BLID" SPC %client.BL_ID SPC "has no profile, skipping loading sequence.");

		return false;
	}


	%file = new FileObject();
	%file.openForRead(%path);

	%file.readline(); %file.readLine(); %file.readLine();

	%client.C3Wallet.money = %file.readline();
	%client.C3Wallet.bankm = %file.readline();
	%client.C3Alias_First = %file.readline();
	%client.C3Alias_Last = %file.readline();

	%file.close();
	%file.delete();
}

function CityDB_saveGroup(%group)
{
	%path = $City3::GroupSaves @ %group.id @ ".dat";

	%file = new FileObject();
	%file.openForWrite(%path);

	%file.writeLine(%group.name);

	%file.writeLine(%group.cash);
	%file.writeLine(%group.minerals);
	%file.writeLine(%group.wood);
}