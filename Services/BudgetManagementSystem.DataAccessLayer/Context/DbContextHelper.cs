namespace BudgetManagementSystem.DataAccessLayer.Context;

public static class DbContextHelper
{
    public static void SoftDeleteAutomaticBuilder(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            //other automated configurations left out
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQueryFilter();
            }
        }
    }

    public static void TemporalTableAutomaticBuilder(ModelBuilder builder)
    {
        // builder.Entity<Ward>().ToTable("Wards", b => b.IsTemporal());

    }
    public static void UniqueKeyAutomaticBuilder(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            #region Convert UniqueKeyAttribute on Entities to UniqueKey in DB
            var properties = entityType.GetProperties();
            if (properties != null && properties.Any())
            {
                foreach (var property in properties)
                {
                    var uniqueKeys = GetUniqueKeyAttributes(entityType, property);
                    if (uniqueKeys != null)
                    {
                        foreach (var uniqueKey in uniqueKeys.Where(x => x.Order == 0))
                        {
                            // Single column Unique Key
                            if (string.IsNullOrWhiteSpace(uniqueKey.GroupId))
                            {
                                entityType.AddIndex(property).IsUnique = true;
                            }
                            // Multiple column Unique Key
                            else
                            {
                                var mutableProperties = new List<IMutableProperty>();
                                properties.ToList().ForEach(x =>
                                {
                                    var uks = GetUniqueKeyAttributes(entityType, x);
                                    if (uks != null)
                                    {
                                        foreach (var uk in uks)
                                        {
                                            if (uk != null && uk.GroupId == uniqueKey.GroupId)
                                            {
                                                mutableProperties.Add(x);
                                            }
                                        }
                                    }
                                });
                                entityType.AddIndex(mutableProperties).IsUnique = true;
                            }
                        }
                    }
                }
            }
            #endregion Convert UniqueKeyAttribute on Entities to UniqueKey in DB
        }
    }
    private static IEnumerable<UniqueKeyAttribute> GetUniqueKeyAttributes(IMutableEntityType entityType, IMutableProperty property)
    {
        //if (entityType == null)
        //{
        //    throw new ArgumentNullException(nameof(entityType));
        //}
        //else if (entityType.ClrType == null)
        //{
        //    throw new ArgumentNullException(nameof(entityType.ClrType));
        //}
        //else if (property == null)
        //{
        //    throw new ArgumentNullException(nameof(property));
        //}
        //else if (property.Name == null)
        //{
        //    throw new ArgumentNullException(nameof(property.Name));
        //}
        var propInfo = entityType.ClrType.GetProperty(
            property.Name,
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);
        if (propInfo == null)
        {
            return null;
        }
        return propInfo.GetCustomAttributes<UniqueKeyAttribute>();
    }


    /// <summary>
    /// Seeds the application user.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static void SeedAppUser(ModelBuilder builder)
    {
        //// any guid
        //string SUPERADMIN_ID = "a18be9c0-aa65-4af8-bd17-ds00bd9344e675";
        //string SUPERADMIN_ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9712345";
        //// any guid, but nothing is against to use the same one


        //builder.Entity<IdentityRole>().HasData(new IdentityRole
        //{
        //    Id = SUPERADMIN_ROLE_ID,
        //    Name = RoleName.SuperAdmin,
        //    NormalizedName = RoleName.SuperAdmin.ToUpper()
        //});

        //var hasher = new PasswordHasher<ApplicationUser>();

        //builder.Entity<ApplicationUser>().HasData(new ApplicationUser
        //{
        //    Id = SUPERADMIN_ID,
        //    LastName = "Super",
        //    FirstName = "User",
        //    UserName = "superadmin@hrcbn.com",
        //    Email = "superadmin@BudgetManagementSystem.com",
        //    NormalizedUserName = "superadmin@hrcbn.com".ToUpper(),
        //    NormalizedEmail = "superadmin@hrcbn.com".ToUpper(),
        //    EmailConfirmed = true,
        //    PasswordHash = hasher.HashPassword(null, "superadmin@hrcbn12345"),
        //    SecurityStamp = string.Empty
        //});


        //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        //{
        //    RoleId = SUPERADMIN_ROLE_ID,
        //    UserId = SUPERADMIN_ID
        //});
    }
}
