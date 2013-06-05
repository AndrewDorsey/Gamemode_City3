//need someone to convert the '*' commands to /s, /w, /ooc, /me, etc.
if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
	RTB_registerPref("RP Chat", "DesoChat", "Deso::RoleplayChat", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("Chat Logs", "DesoChat", "Deso::RoleplayChat::Logs", "int 0 1", "Script_DesoChat", 1, 0, 0);	
	RTB_registerPref("Anti-Spam Toggle", "DesoChat", "Deso::RoleplayChat::AntiSpam", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("Anti-Spam: Spam Time(ms)", "DesoChat", "Deso::RoleplayChat::SpamTime", "int 0 5000", "Script_DesoChat", 1000, 0, 0);
	RTB_registerPref("Local Chat Radius", "DesoChat", "Deso::RoleplayChatArea", "int 0 5000", "Script_DesoChat", 15, 0, 0);
	RTB_registerPref("Yell Chat Radius", "DesoChat", "Deso::RoleplayYellChatArea", "int 0 5000", "Script_DesoChat", 45, 0, 0);
	RTB_registerPref("Yell Chat Toggle", "DesoChat", "Deso::RoleplayChat::Yell", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("OOC Chat Toggle", "DesoChat", "Deso::RoleplayChat::OOC", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("Me Chat Toggle", "DesoChat", "Deso::RoleplayChat::Me", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("Whisper Chat Toggle", "DesoChat", "Deso::RoleplayChat::Whisper", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("PM Chat Toggle", "DesoChat", "Deso::RoleplayChat::PM", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("Anonymous Chat Toggle", "DesoChat", "Deso::RoleplayChat::Anon", "int 0 1", "Script_DesoChat", 1, 0, 0);
	RTB_registerPref("Admin Chat Toggle", "DesoChat", "Deso::RoleplayChat::Admin", "int 0 1", "Script_DesoChat", 1, 0, 0);

}
else
{
	$Deso::RoleplayChat = 1;
	$Deso::RoleplayChat::Logs = 1;
	$Deso::RoleplayChat::SpamTime = 1000;
	$Deso::RoleplayChat::AntiSpam = 1;
	$Deso::RoleplayChatArea = 10;
	$Deso::RoleplayChat::OOC = 1;
	$Deso::RoleplayChat::Me = 1;
	$Deso::RoleplayChat::Whisper = 1;
	$Deso::RoleplayChat::PM = 1;
	$Deso::RoleplayChat::Anon = 1;
	$Deso::RoleplayChat::Admin = 1;
	$Deso::RoleplayChat::Yell = 1;
}

$Deso::RoleplayChat::AdminIcon = "add-ons/script_desochat/admin";
$Deso::RoleplayChat::SuperAdminIcon = "add-ons/script_desochat/superadmin";
		
package Deso_Chat
{	
	
	function Deso_Log(%client,%value)
	{
		if($Deso::RoleplayChat::Logs == 1)
		{
			if(%value $= "") {echo("ERROR LOG!"); return false;}
			%file = new FileObject();
			%file.openForAppend("config/server/ServerLogs/Chat/Chat.txt");
			%vals = %value SPC %value2 SPC %value3;
			%file.writeline(%value);
			%file.close();
			%file.delete();
		}
	}	
	
	function serverCmdDesoChat(%client)
	{
		messageClient(%client,'',"<font:arial bold:15>\c3There are two primary chat types, \c0Local Chat(NormalChat) \c3and \c2Global/OOC Chat(TeamChat)\c3.");
		messageClient(%client,'',"<font:arial bold:15>\c3There are also secondary chat types, add one of the following as a prefix to use them.");
		messageClient(%client,'',"<font:arial bold:14>\c1*Yell \c3- A wider ranged chat.");
		messageClient(%client,'',"<font:arial bold:14>\c1*Me \c3- Show an action.");
		messageClient(%client,'',"<font:arial bold:14>\c1*Whis \c3- Whispers to everyone in a small area.");
		messageClient(%client,'',"<font:arial bold:14>\c1*PM PlayerName/ID \c3- Send a private message to a player.");
		messageClient(%client,'',"<font:arial bold:14>\c1*Anon \c3- Send a anonymous message to everyone in the area.");
		if(%client.isadmin)
		{
			messageClient(%client,'',"<font:arial bold:14>\c1*Adm \c3- Send a message to all admins.");
		}
		messageClient(%client,'',"<font:arial bold:13>\c6Press \c1(Page-Up) \c6to view the rest of the list.");
	}
	
	function Deso_ChatTalkUndo(%client)
	{
		%client.DChatTalked = 0;
		%client.AntiSpamCaught = 0;
	}
	
	function Deso_AntiSpam(%client,%msg)
	{
		if($Deso::RoleplayChat::AntiSpam == 1)
		{
			schedule($Deso::RoleplayChat::SpamTime,0,Deso_ChatTalkUndo,%client);
			if(%client.LastDChatMessage $= %msg)
			{
				%client.AntiSpamCaught = 1;
				messageClient(%client,'',"\c5Don't repeat your self!");
				return;
			}
			if(%client.DChatTalked == 1)
			{
				%client.AntiSpamCaught = 1;
				messageClient(%client,'',"\c5Don't spam. Wait \c3" @ $Deso::RoleplayChat::SpamTime * 0.001@ " \c5second(s) before talking again.");
				return;
			}
			%client.DChatTalked = 1;			
		}
	}
	
	function serverCmdTeamMessageSent(%client, %msg)
	{
		if($Deso::RoleplayChat == 1)
		{
			if($Deso::RoleplayChat::AntiSpam == 1)
			{
				Deso_AntiSpam(%client,%msg);
				if(%client.AntiSpamCaught == 1)
				{
					return;
				}
			}
			Deso_RpChatCheck(%client,%msg);
			%t = getWord(%msg,0);
			if(%t $= "*Me" || %t $= "*Whis" || %t $= "*PM" || %t $= "*Anon" || %t $= "*Adm" || %t $= "*Yell" || !isObject(%client.player)){return;}
			if($Deso::RoleplayChat::OOC == 1 && isObject(%client.player))
			{
				if(%client.isadmin)
				{
					if(%client.IsSuperAdmin)
					{
						messageAll('',"\c7[\c2OOC\c7] <bitmap:" @ $Deso::RoleplayChat::SuperAdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
					}
					else
					{
						messageAll('',"\c7[\c2OOC\c7] <bitmap:" @ $Deso::RoleplayChat::AdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
					}
				}
				else
				{
					messageAll('',"\c7[\c2OOC\c7] \c3" @ %client.Name @ "\c7: \c6" @ %msg);
				}
				%client.LastDChatMessage = %msg;
				Deso_Log(%client,"CHAT: " @ %client.Name SPC " said: " @ %msg);
			}
			else
			{
				return parent::serverCmdTeamMessageSent(%client, %msg);
			}
		}
		else
		{
			return parent::serverCmdTeamMessageSent(%client, %msg);
		}
	}

	function serverCmdMessageSent(%client, %msg)
	{
		if($Deso::RoleplayChat == 1)
		{
			if($Deso::RoleplayChat::AntiSpam == 1)
			{
				Deso_AntiSpam(%client,%msg);
				if(%client.AntiSpamCaught == 1)
				{
					return;
				}
			}
			
			Deso_RpChatCheck(%client,%msg);
			%t = getWord(%msg,0);
			if(%t $= "*Me" || %t $= "*Whis" || %t $= "*PM" || %t $= "*Anon" || %t $= "*Adm" || %t $= "*Yell" || !isObject(%client.player)){return;}
			InitContainerRadiusSearch(%client.player.position,$Deso::RoleplayChatArea,$TypeMasks::PlayerObjectType);
			while((%targetobject=containerSearchNext()) !$= 0)
			{
				%clients=%targetobject.client;
				if(%client.isadmin)
				{
					if(%client.IsSuperAdmin)
					{
						messageClient(%clients,'',"\c7[\c1Local\c7] <bitmap:" @ $Deso::RoleplayChat::SuperAdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
					}
					else
					{
						messageClient(%clients,'',"\c7[\c1Local\c7] <bitmap:" @ $Deso::RoleplayChat::AdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
					}
				}
				else
				{
					messageClient(%clients,'',"\c7[\c1Local\c7] \c3" @ %client.Name @ "\c7: \c6" @ %msg);
				}
			}
			%client.LastDChatMessage = %msg;
			Deso_Log(%client,"CHAT: " @ %client.Name SPC " said: " @ %msg);
		}
		else
		{
			return parent::serverCmdMessageSent(%client, %msg);
		}
	}
	
	function Deso_RpChatCheck(%client,%msg)
	{
		if($Deso::RoleplayChat == 1)
		{
			if(getWord(%msg,0) $= "*PM")
			{
				if($Deso::RoleplayChat::PM == 1)
				{
					if(getWord(%msg,1) > 0)
					{
						%who = findclientbybl_id(getWord(%msg,1));
					}
					else
					{
						%who = findclientbyname(getWord(%msg,1));
					}

					if(%who $= "" || %who == 0)
					{
						messageClient(%client,'',"\c3Sorry, \c1" @ getWord(%msg,1) @ " \c3is not a valid player.");
						return;
					}
					if(%who == %client){return;}
					if(isObject(%who))
					{
						%msg2 = strReplace(%msg,getWord(%msg,0),"");
						%msg3 = strReplace(%msg2,getWord(%msg2,1),"");
						%msg4 = strReplace(%msg3,"  "," ");
						if(%client.isadmin)
						{
							if(%client.IsSuperAdmin)
							{
								messageClient(%client,'',"\c7[PM] <bitmap:" @ $Deso::RoleplayChat::SuperAdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
								messageClient(%who,'',"\c7[PM] <bitmap:" @ $Deso::RoleplayChat::SuperAdminIcon @ ">\c3" @ %client.Name @ "\c7:\c6" @ %msg4,"");
							}
							else
							{
								messageClient(%client,'',"\c7[PM] <bitmap:" @ $Deso::RoleplayChat::AdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
								messageClient(%who,'',"\c7[PM] <bitmap:" @ $Deso::RoleplayChat::AdminIcon @ ">\c3" @ %client.Name @ "\c7:\c6" @ %msg4,"");
							}
						}
						else
						{
							messageClient(%client,'',"\c7[PM] \c3" @ %client.Name @ "\c7:\c6" @ %msg4,"");
							messageClient(%who,'',"\c7[PM] \c3" @ %client.Name @ "\c7:\c6" @ %msg4,"");
						}
						%client.LastDChatMessage = %msg4;
					}
				}
				else
				{
					messageClient(%client,'',"\c3Sorry, \c1PM \c3chat is \c0OFF\c3.");
				}
				return;
			}
			if(getWord(%msg,0) $= "*Adm")
			{
				if($Deso::RoleplayChat::Admin == 1)
				{
					if(%client.IsAdmin)
					{
						for(%i=0;%i<clientgroup.getCount();%i++) 
						{
							%cl = clientgroup.getObject(%i);
							if(%cl.isadmin)
							{
								messageClient(%cl,'',"\c7[\c1Admin\c7] \c3" @ %client.Name @ "\c7:\c6" @ strReplace(%msg,getWord(%msg,0),""));
								Deso_Log(%client,"ADMINCHAT: " @ %client.Name SPC " said: " @ %msg);
							}
						}
					}
					else
					{
						messageClient(%client,'',"\c3Sorry, you're not an Admin.");
					}
					return;
				}
				else
				{
					messageClient(%client,'',"\c3Sorry, \c1Admin \c3chat is \c0OFF\c3.");
				}
			}
			if(!isObject(%client.player))
			{
				if(%client.isadmin)
				{
					if(%client.IsSuperAdmin)
					{
						messageAll('',"\c7[\c2OOC\c7] <bitmap:" @ $Deso::RoleplayChat::SuperAdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
					}
					else
					{
						messageAll('',"\c7[\c2OOC\c7] <bitmap:" @ $Deso::RoleplayChat::AdminIcon @ ">\c3" @ %client.Name @ "\c7: \c6" @ %msg);
					}
				}
				else
				{
					messageAll('',"\c7[\c2OOC\c7] \c3" @ %client.Name @ "\c7: \c6" @ %msg);
				}
				%client.LastDChatMessage = %msg;
				Deso_Log(%client,"OOC: " @ %client.Name SPC " said: " @ %msg);
				return;
			}
			if(getWord(%msg,0) $= "*Me")
			{
				if($Deso::RoleplayChat::Me == 1)
				{
					InitContainerRadiusSearch(%client.player.position,15,$TypeMasks::PlayerObjectType);
					while((%targetobject=containerSearchNext()) !$= 0)
					{
						%clients=%targetobject.client;
						messageclient(%clients,'',"\c6" @ %client.Name @ "\c6" @ strReplace(%msg,getWord(%msg,0),"") @ "");
					}
					%client.LastDChatMessage = %msg;
					Deso_Log(%client,"ACTION: " @ %client.Name SPC " said: " @ %msg);
					return;
				}
				else
				{
					messageClient(%client,'',"\c3Sorry, \c1Me \c3chat is \c0OFF\c3.");
				}
			}
			if(getWord(%msg,0) $= "*Whis")
			{
				if($Deso::RoleplayChat::Whisper == 1)
				{
					InitContainerRadiusSearch(%client.player.position,5,$TypeMasks::PlayerObjectType);
					while((%targetobject=containerSearchNext()) !$= 0)
					{
						%clients=%targetobject.client;
						messageclient(%clients,'',"\c7[\c5Whisper\c7] \c3" @ %client.Name @ "\c7:\c6" @ strReplace(%msg,getWord(%msg,0),""));
					}
					%client.LastDChatMessage = %msg;
					Deso_Log(%client,"WHIS: " @ %client.Name SPC " said: " @ %msg);
					return;
				}
				else
				{
					messageClient(%client,'',"\c3Sorry, \c1Whisper \c3chat is \c0OFF\c3.");
				}
			}
			if(getWord(%msg,0) $= "*Anon")
			{
				if($Deso::RoleplayChat::Anon == 1)
				{
					InitContainerRadiusSearch(%client.player.position,10,$TypeMasks::PlayerObjectType);
					while((%targetobject=containerSearchNext()) !$= 0)
					{
						%clients=%targetobject.client;
						if(%clients.isadmin)
						{
							messageclient(%clients,'',"\c7[\c5Anonymous\c7] \c3" @ %client.name @ "\c7: \c6" @ strReplace(%msg,getWord(%msg,0),""));
						}
						else
						{
						
							messageclient(%clients,'',"\c7[\c5Anonymous\c7]\c6" @ strReplace(%msg,getWord(%msg,0),""));
						}
					}
					%client.LastDChatMessage = %msg;
					Deso_Log(%client,"Anon: " @ %client.Name SPC " said: " @ %msg);
					return;
				}
				else
				{
					messageClient(%client,'',"\c3Sorry, \c1Anonymous \c3chat is \c0OFF\c3.");
				}
			}
			if(getWord(%msg,0) $= "*Yell")
			{
				if($Deso::RoleplayChat::Yell == 1)
				{
					%msg = strReplace(%msg,getWord(%msg,0),"");
					InitContainerRadiusSearch(%client.player.position,$Deso::RoleplayYellChatArea,$TypeMasks::PlayerObjectType);
					while((%targetobject=containerSearchNext()) !$= 0)
					{
						%clients=%targetobject.client;
						if(%client.isadmin)
						{
							if(%client.IsSuperAdmin)
							{
								messageClient(%clients,'',"\c7[\c0Yell\c7] <bitmap:" @ $Deso::RoleplayChat::SuperAdminIcon @ ">\c3" @ %client.Name @ "\c7:\c6" @ %msg);
							}
							else
							{
								messageClient(%clients,'',"\c7[\c0Yell\c7] <bitmap:" @ $Deso::RoleplayChat::AdminIcon @ ">\c3" @ %client.Name @ "\c7:\c6" @ %msg);
							}
						}
						else
						{
							messageClient(%clients,'',"\c7[\c0Yell\c7] \c3" @ %client.Name @ "\c7:\c6" @ %msg);
						}
					}
					%client.LastDChatMessage = %msg;
					Deso_Log(%client,"YELL: " @ %client.Name SPC " said: " @ %msg);
				}
				else
				{
					messageClient(%client,'',"\c3Sorry, \c1Yell \c3chat is \c0OFF\c3.");
				}
			}
		}
	}
};
activatepackage(Deso_Chat);