using Bang.App.Repositories;
using Bang.Domain.Entities;
using Bang.Domain.Enums;
using System.Linq;

namespace Bang.Persistence.Database.Repositories
{
    public class WeaponsRepository : IWeaponsRepository
    {
        private readonly BangDbContext context;

        public WeaponsRepository(BangDbContext context)
        {
            this.context = context;
        }

        public Weapon GetFirstWeapon() =>
            this.context.Weapons.Single(w => w.Id == WeaponKind.Colt45);
    }
}