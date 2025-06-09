using PlanetaryMigration.Domain.Enums;

namespace PlanetaryMigration.Domain.Constants
{
    public static class Roles
    {
        public const string SuperAdmin = nameof(UserRole.SuperAdmin);
        public const string PlanetAdmin = nameof(UserRole.PlanetAdmin);
        public const string ViewerType1 = nameof(UserRole.ViewerType1);
        public const string ViewerType2 = nameof(UserRole.ViewerType2);

        public const string AllViewers = ViewerType1 + "," + ViewerType2;
        public const string Admins = SuperAdmin + "," + PlanetAdmin;
        public const string AllUser =
            nameof(UserRole.SuperAdmin) + "," +
            nameof(UserRole.PlanetAdmin) + "," +
            nameof(UserRole.ViewerType1) + "," +
            nameof(UserRole.ViewerType2);
    }

}
