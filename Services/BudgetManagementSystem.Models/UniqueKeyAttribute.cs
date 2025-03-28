﻿namespace BudgetManagementSystem.Models;
/// <summary>
/// Used on an EntityFramework Entity class to mark a property to be used as a Unique Key
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class UniqueKeyAttribute : ValidationAttribute
{
    /// <summary>
    /// Marker attribute for unique key
    /// </summary>
    /// <param name="groupId">Optional, used to group multiple entity properties together into a combined Unique Key</param>
    /// <param name="order">Optional, used to order the entity properties that are part of a combined Unique Key</param>
    public UniqueKeyAttribute(string groupId = null, int order = 0)
    {
        GroupId = groupId;
        Order = order;
    }

    public string GroupId { get; set; }
    public int Order { get; set; }
}
