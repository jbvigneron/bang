using Bang.Domain.Entities;

namespace Bang.App.Repositories
{
    public interface IWeaponsRepository
    {
        Weapon GetFirstWeapon();
    }
}