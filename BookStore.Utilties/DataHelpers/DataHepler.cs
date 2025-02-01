using System;

namespace BookStore.Utilties.DataHelpers
{
    /// <summary>
    /// Provides helper methods and enumerations for managing data-related operations.
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// Represents database schemas used in the application.
        /// </summary>
        public enum Schemas
        {
            /// <summary>
            /// Schema for books-related entities.
            /// </summary>
            Books,

            /// <summary>
            /// Schema for people-related entities.
            /// </summary>
            People,

            /// <summary>
            /// Schema for sales-related entities.
            /// </summary>
            Sales,

            /// <summary>
            /// Schema for history-related entities.
            /// </summary>
            History,

            /// <summary>
            /// Default schema (dbo).
            /// </summary>
            dbo
        };

        /// <summary>
        /// Constructs the fully qualified name of a stored procedure using the specified schema and procedure name.
        /// </summary>
        /// <param name="procedureName">The name of the stored procedure.</param>
        /// <param name="schema">The schema under which the stored procedure is defined.</param>
        /// <returns>A string representing the fully qualified name of the stored procedure.</returns>
        public static string GetStoredProcedureWithSchema(string procedureName, Schemas schema)
        {
            return $"{schema.ToString()}.{procedureName}";
        }
    }
}
