// =================================================================
// Project: CityRPG 
// Author: Dionysus
// Date: Sunday, November 11, 2012 1:26 AM
// Description: Labor system
// =================================================================

// =================================================================
// Section 1: Mining system
// =================================================================

function fxDTSBrick::onMine(%this, %client)
{
if(%this.resources > 0)
{
        %this.hasOre = true;
				//Iron
        if(getRandom(1, 100) > 90 - %client.CityRPGData.value["PickaxeType"] * 3 && %this.getDataBlock().isIron)
        {
                %this.resources--;
                
                if(getRandom(1, 100) > 100 - %client.CityRPGData.value["PickaxeType"] && !%client.CityRPGData.value["JailTick"])
                	%gemstone = true;
         
                if(%gemstone)
                {
                        %value = getRandom(5, 50);
                        commandToClient(%client, 'centerPrint', "\c1Gemstone obtained.", 1);
                        messageClient(%client, '', "\c6You extracted a gem from the rock worth \c3$" @ %value @ "\c6.");
                        %client.CityRPGData.value["Money"] += %value;
                        %client.CityRPGData.value["JobEXP"] += 0.1;
                        %client.CityRPGData.value["MiningExp"] += 0.1;
                        if(isObject(%client.CityRPGLotBrick))
                        {
                            %client.CityRPGData.value["JobEXP"] += 0.1;
                        }
                        
                        %client.SetInfo();
                }
                else
                {
                        if(!%this.resources)
                        {
                                commandToClient(%client, 'centerPrint', "\c6You have emptied this rock of its resources!", 3);
                                %this.adjustColorOnOreContent();
                        }
                        else
                                commandToClient(%client, 'centerPrint', "\c6Iron ore obtained.", 1);
                        
                        //%client.CityRPGData.value["Resources"] = getWord(%client.CityRPGData.value["Resources"], 0) SPC (getWord(%client.CityRPGData.value["Resources"], 1) + 1);
					    %client.CityRPGData.value["Iron"] += 1.00;
                }
                if(!%client.CityRPGData.value["JailTick"])
                {
                        %client.CityRPGData.value["JobEXP"] += 0.05;
                }
                if(isObject(%client.CityRPGLotBrick))
                {
                        %client.CityRPGData.value["JobEXP"] += 0.05;
                }
                        %client.CityRPGData.value["MiningExp"] += 0.05;
                        %client.levelMining();
                        %client.setInfo();
				}
				//Silver
		else if(getRandom(1, 100) > 90 - %client.CityRPGData.value["PickaxeType"] * 3 && %this.getDataBlock().isSilver)
       	{
            %this.resources--;
    		
            if(getRandom(1, 100) > 100 - %client.CityRPGData.value["PickaxeType"] && !%client.CityRPGData.value["JailTick"])
            	%gemstone = true;
     	
            if(%gemstone)
            {
                    %value = getRandom(5, 100);
                    commandToClient(%client, 'centerPrint', "\c1Gemstone obtained.", 1);
                    messageClient(%client, '', "\c6You extracted a gem from the rock worth \c3$" @ %value @ "\c6.");
                    %client.CityRPGData.value["Money"] += %value;
                    %client.CityRPGData.value["JobEXP"] += 0.2;
                    %client.CityRPGData.value["MiningExp"] += 0.2;
                    if(isObject(%client.CityRPGLotBrick))
                    {
                        %client.CityRPGData.value["JobEXP"] += 0.2;
                    }
                    
                    %client.SetInfo();
            }
            else
            {
                    if(!%this.resources)
                    {
                            commandToClient(%client, 'centerPrint', "\c6You have emptied this rock of its resources!", 3);
                            %this.adjustColorOnOreContent();
                    }
                    else
                            commandToClient(%client, 'centerPrint', "\c6Silver ore obtained.", 1);
                    
                    //%client.CityRPGData.value["Resources"] = getWord(%client.CityRPGData.value["Resources"], 0) SPC (getWord(%client.CityRPGData.value["Resources"], 1) + 1);
					%client.CityRPGData.value["Silver"] += 1.00;
            }
            if(!%client.CityRPGData.value["JailTick"])
            {
                    %client.CityRPGData.value["JobEXP"] += 0.05;
            }
            if(isObject(%client.CityRPGLotBrick))
            {
                    %client.CityRPGData.value["JobEXP"] += 0.05;
            }
                    %client.CityRPGData.value["MiningExp"] += 0.05;
                    %client.levelMining();
                    %client.setInfo();
			}
				//Platinum
		else if(getRandom(1, 100) > 90 - %client.CityRPGData.value["PickaxeType"] * 3 && %this.getDataBlock().isPlatinum)
       	{
            %this.resources--;
    		
            if(getRandom(1, 100) > 100 - %client.CityRPGData.value["PickaxeType"] && !%client.CityRPGData.value["JailTick"])
            	%gemstone = true;
     	
            if(%gemstone)
            {
                    %value = getRandom(5, 200);
                    commandToClient(%client, 'centerPrint', "\c1Gemstone obtained.", 1);
                    messageClient(%client, '', "\c6You extracted a gem from the rock worth \c3$" @ %value @ "\c6.");
                    %client.CityRPGData.value["Money"] += %value;
                    %client.CityRPGData.value["JobEXP"] += 0.3;
                    %client.CityRPGData.value["MiningExp"] += 0.3;
                    if(isObject(%client.CityRPGLotBrick))
                    {
                        %client.CityRPGData.value["JobEXP"] += 0.3;
                    }
                    
                    %client.SetInfo();
            }
            else
            {
                    if(!%this.resources)
                    {
                            commandToClient(%client, 'centerPrint', "\c6You have emptied this rock of its resources!", 3);
                            %this.adjustColorOnOreContent();
                    }
                    else
                            commandToClient(%client, 'centerPrint', "\c6Platinum ore obtained.", 1);
                    
                    //%client.CityRPGData.value["Resources"] = getWord(%client.CityRPGData.value["Resources"], 0) SPC (getWord(%client.CityRPGData.value["Resources"], 1) + 1);
					%client.CityRPGData.value["Platinum"] += 1.00;
            }
            if(!%client.CityRPGData.value["JailTick"])
            {
                    %client.CityRPGData.value["JobEXP"] += 0.05;
            }
            if(isObject(%client.CityRPGLotBrick))
            {
                    %client.CityRPGData.value["JobEXP"] += 0.05;
            }
                    %client.CityRPGData.value["MiningExp"] += 0.05;
                    %client.levelMining();
                    %client.setInfo();
			}
        }
}