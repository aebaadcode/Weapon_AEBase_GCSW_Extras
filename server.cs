//we need the base add-on for this, so force it to load
%error = ForceRequiredAddOn("Weapon_AEBase");
%error2 = ForceRequiredAddOn("Weapon_AEBase_GCatsSpecialWeapons");

if(%error == $Error::AddOn_NotFound || %error2 == $Error::AddOn_NotFound)
{
	//we don't have the base, so we're screwed =(
	error("ERROR: AEBase_GCSW_Extras - required add-on Weapon_AEBase not found");
}
else
{
	exec("./Sounds/Sounds.cs");	
	exec("./Weapon_MG42.cs");
	exec("./Weapon_STG44.cs");
}