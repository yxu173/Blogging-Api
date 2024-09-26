using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastracture.Extensions;

internal static class ConfigrationExtension
{
    internal static void AddConfiguration(this ModelBuilder builder)
    {
        var configrationClasses =
            Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();
        foreach (Type type in configrationClasses)
        {
            dynamic configuration = Activator.CreateInstance(type) ?? throw new InvalidOperationException();
            builder.ApplyConfiguration(configuration);
        }
    }
}