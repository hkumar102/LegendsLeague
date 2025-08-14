using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LegendsLeague.Infrastructure.Persistence.Extensions
{
    /// <summary>
    /// Provides helpers to force snake_case naming for tables, columns, keys, foreign keys, and indexes.
    /// Apply via <c>modelBuilder.UseSnakeCaseNames()</c> inside each DbContext's <c>OnModelCreating</c>.
    /// </summary>
    public static class NamingConventions
    {
        /// <summary>
        /// Converts table/column/index/constraint names produced by EF Core mappings to snake_case.
        /// Keeps the schema that was already configured (e.g., via <c>HasDefaultSchema</c>).
        /// </summary>
        /// <param name="modelBuilder">The EF model builder.</param>
        public static void UseSnakeCaseNames(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Table name
                var tableName = entity.GetTableName();
                if (!string.IsNullOrEmpty(tableName))
                {
                    entity.SetTableName(ToSnakeCase(tableName!));
                }

                // Columns
                var schema = entity.GetSchema();
                var identifier = StoreObjectIdentifier.Table(entity.GetTableName()!, schema);
                foreach (var property in entity.GetProperties())
                {
                    var current = property.GetColumnName(identifier);
                    if (!string.IsNullOrEmpty(current))
                    {
                        property.SetColumnName(ToSnakeCase(current!));
                    }
                }

                // Keys
                foreach (var key in entity.GetKeys())
                {
                    var name = key.GetName();
                    if (!string.IsNullOrEmpty(name))
                    {
                        key.SetName(ToSnakeCase(name!));
                    }
                }

                // Foreign keys
                foreach (var fk in entity.GetForeignKeys())
                {
                    var name = fk.GetConstraintName();
                    if (!string.IsNullOrEmpty(name))
                    {
                        fk.SetConstraintName(ToSnakeCase(name!));
                    }
                }

                // Indexes
                foreach (var index in entity.GetIndexes())
                {
                    var name = index.GetDatabaseName();
                    if (!string.IsNullOrEmpty(name))
                    {
                        index.SetDatabaseName(ToSnakeCase(name!));
                    }
                }
            }
        }

        /// <summary>
        /// Converts a PascalCase or camelCase identifier to snake_case.
        /// Keeps existing underscores and lower-case text intact.
        /// </summary>
        /// <param name="input">The identifier to convert.</param>
        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var sb = new System.Text.StringBuilder(capacity: input.Length + 8);
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsUpper(c))
                {
                    if (i > 0 && input[i - 1] != '_')
                        sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
