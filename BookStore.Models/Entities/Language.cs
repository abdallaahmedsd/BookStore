using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    /// <summary>
    /// Represents a language entity with an ID and a name.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Gets or sets the unique identifier for the language.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        public string LanguageName { get; set; }
    }

}
