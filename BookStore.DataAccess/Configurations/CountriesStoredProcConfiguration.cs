using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using RepoSP.Net.Interfaces;
using BookStore.Models.Entities;
using static BookStore.Utilties.DataHelpers.DataHelper;

namespace BookStore.DataAccess.Configurations
{
    public class CountriesStoredProcConfiguration : IStoredProcConfiguration<Country>
    {

        /// <summary>
        /// Singleton instance of CountriesStoredProcConfiguration to ensure a single instance is used throughout the application.
        /// </summary>
        private readonly static CountriesStoredProcConfiguration _instance = new CountriesStoredProcConfiguration();

        /// <summary>
        /// Provides access to the singleton instance of CountriesStoredProcConfiguration.
        /// </summary>
        public static CountriesStoredProcConfiguration Instance => _instance;

        /// <summary>
        /// Gets the stored procedure name for retrieving a country by ID.
        /// </summary>
        public string GetByIdProcedure => GetStoredProcedureWithSchema("SP_GetCountryByID", Schemas.People);

        /// <summary>
        /// Gets the stored procedure name for retrieving all countries.
        /// </summary>
        public string GetAllProcedure => GetStoredProcedureWithSchema("SP_GetCountries", Schemas.People);

        /// <summary>
        /// Throws a NotImplementedException for the insert procedure, indicating it is not yet implemented.
        /// </summary>
        public string InsertProcedure => throw new NotImplementedException();

        /// <summary>
        /// Throws a NotImplementedException for the update procedure, indicating it is not yet implemented.
        /// </summary>
        public string UpdateProcedure => throw new NotImplementedException();

        /// <summary>
        /// Throws a NotImplementedException for the delete procedure, indicating it is not yet implemented.
        /// </summary>
        public string DeleteProcedure => throw new NotImplementedException();

        /// <summary>
        /// Gets the stored procedure name for retrieving a country by name.
        /// </summary>
        public string SP_GetCountryByName => GetStoredProcedureWithSchema("SP_GetCountryByName", Schemas.People);


        /// <summary>
        /// Gets the stored procedure name that checks if a country exists by ID in the People schema.
        /// </summary>
        /// <returns>
        /// A string that represents the name of the stored procedure "Fun_IsCountryExistsByID" in the People schema.
        /// </returns>
        public string IsExistsByIdProcedure => GetStoredProcedureWithSchema("Fun_IsCountryExistsByID", Schemas.People);

        public string IdParameterName => "Id";

        public string Id_Output_ParameterName => "NewId_output";

        /// <summary>
        /// Maps the SQL data reader to a Country entity by reading values from the data reader.
        /// </summary>
        /// <param name="reader">The SqlDataReader object containing the country data.</param>
        /// <returns>A mapped Country entity.</returns>
        public Country MapEntity(SqlDataReader reader)
        {
            return new Country
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };
        }

        /// <summary>
        /// Maps a Country entity to a new instance with a specified ID and copies other properties from the input entity.
        /// </summary>
        /// <param name="Id">The ID to assign to the new Country entity.</param>
        /// <param name="entity">The source Country entity.</param>
        /// <returns>A new Country entity with the specified ID and copied properties.</returns>
        public Country MapEntity(int Id, Country entity)
        {
            return new Country
            {
                Id = Id,
                Name = entity.Name
            };
        }

        /// <summary>
        /// Throws a NotImplementedException for setting parameters for inserting a new country, indicating it is not yet implemented.
        /// </summary>
        /// <param name="command">The SqlCommand object to configure.</param>
        /// <param name="entity">The Country entity containing the data to insert.</param>
        public void SetInsertParameters(SqlCommand command, Country entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throws a NotImplementedException for setting parameters for updating a country, indicating it is not yet implemented.
        /// </summary>
        /// <param name="command">The SqlCommand object to configure.</param>
        /// <param name="entity">The Country entity containing the updated data.</param>
        public void SetUpdateParameters(SqlCommand command, Country entity)
        {
            throw new NotImplementedException();
        }

    }
}
