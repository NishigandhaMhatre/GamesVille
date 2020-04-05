
namespace Game.Models
{
    /// <summary>
    /// List of special abilities, these are always used as attack items
    /// </summary>
    public enum SpecialAbilityEnum
    {
        Unknown = 0,
        //Characters can have laser eyes as attack weapon, the value is the damage caused by character 
        Laser_Eyes = 10,
        //Charaters can have freeze as attack on monster, the value is the damage caused by character
        Freeze = 15,
    }
}
