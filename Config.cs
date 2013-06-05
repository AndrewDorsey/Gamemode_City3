// ============================================================
// Project            :  City3
// File               :  .\Common.cs
// Created on         :  Tuesday, June 4, 2013 2:02 PM
// Description        :  Configuration file for City3
// ============================================================

//  ____ _ _           _____
// / ___(_) |_ _   _  |___ / ®
//| |   | | __| | | |   |_ \
//| |___| | |_| |_| |  ___) |
// \____|_|\__|\__, | |____/
//             |___/

$City3::DebugMode = 1;
$City3::UserSaves = "Config/Server/City3/Users/";
$City3::SaveTime = 10; //in minutes (will be used later)

$City3::ClientObject = new AiConnection(City3ClientObject)
{
   name = "City3ClientObject";
   BL_ID = 9999999999;
};
$City3ClientObject.isAdmin = 1;
