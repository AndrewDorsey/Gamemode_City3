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
