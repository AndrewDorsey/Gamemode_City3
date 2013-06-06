// ============================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  Time system
// ============================================================

function City3GetTime(%value)
{
	%realTime = getWord(getDateTime(),1);
	%strippedTime = strreplace(%realTime,":"," ");

	switch$(%value)
	{
		case "strip":
			return %strippedTime;
		case "hour":
			return getWord(%strippedTime,0);
		case "minute":
			return getWord(%strippedTime,1);
		case "second":
			return getWord(%strippedTime,2);
		case "real":
			return %realTime;
	}
}

function getEnvironmentTime()
{
	%pas = $Sim::Time/Daycycle.dayLength;
	%tim = %pas - mFloor(%pas);
	while(%tim >= 1)
		%tim--;
	while(%tim < 0)
		%tim++;
	return %tim;
}

function syncEnvironmentTime()
{
	$Sky::dayCycleEnabled = 1;
	$Sky::dayLength = 86400;
	%hour = City3GetTime("Hour");
	switch(%hour)
	{
		case 0:
			%time = 0.75;
		case 1:
			%time = 0.79;
		case 2:
			%time = 0.83;
		case 3:
			%time = 0.87;
		case 4:
			%time = 0.91;
		case 5:
			%time = 0.95;
		case 6:
			%time = 0.0;
		case 7:
			%time = 0.1;
		case 8:
			%time = 0.13;
		case 9:
			%time = 0.16;
		case 10:
			%time = 0.19;
		case 11:
			%time = 0.22;
		case 12:
			%time = 0.25;
		case 13:
			%time = 0.28;
		case 14:
			%time = 0.31;
		case 15:
			%time = 0.34;
		case 16:
			%time = 0.37;
		case 17:
			%time = 0.40;
		case 18:
			%time = 0.43;
		case 19:
			%time = 0.46;
		case 20:
			%time = 0.50;
		case 21:
			%time = 0.56;
		case 22:
			%time = 0.62;
		case 23:
			%time = 0.68;
	}
	%mintime = 0.001666666666666667;
	%add = mceil((City3gettime(minute) / 10) * %mintime);
	%time = %time + %add;

	setEnvironmentTime(%time);
}

function setEnvironmentTime(%time)
{
   dayCycle.setDayOffset(%time-getEnvironmentTime());
}
//Thanks for those above 2 functions, marble.

function City3Tick(%value)
{
	cancel($City3::Tick);
	if(%value)
	{
		echo("City3: Tick system started. Syncing Environment Time.");
		syncEnvironmentTime();
		$City3::Ticking = 1;
	}
	$City3::Time = City3GetTime("strip");
	$City3::Tick = schedule(1000, 0, City3Tick());

	//Insert everything that happens each tick here.
	Jassy.SaveData();
}