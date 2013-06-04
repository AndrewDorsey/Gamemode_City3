datablock ItemData(droppedcashItem)
{
	category = "Weapon";
	className = "Weapon";
	
	shapeFile = "base/data/shapes/brickWeapon.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	
	doColorShift = true;
	colorShiftColor = "0 0.5 0 1";
	image = dropcashImage;
	candrop = true;
	canPickup = false;
};
datablock ShapeBaseImageData(droppedcashImage)
{
	shapeFile = "base/data/shapes/brickWeapon.dts";
	emap = true;
	
	doColorShift = true;
	colorShiftColor = dropCashItem.colorshiftcolor;
	canPickup = false;
};