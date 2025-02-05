namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents Author
    /// </summary>
    public class Author 
    {

        /// <summary>
        /// Gets or sets the unique identifier for the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the biography of the author.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the author record.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets the full name of the author.
        /// </summary>
        public string FullName { get { return FirstName + ' ' + LastName; } private set { } }


        /// <summary>
        /// Gets or sets the identifier for the author's nationality.
        /// </summary>
        public int NationalityID { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the author (nullable).
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the email address of the author.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the profile image URL for the author (nullable).
        /// </summary>
        public string? ProfileImage { get; set; }

      
    }
}
