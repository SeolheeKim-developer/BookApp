using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace BookApp.Shared
{
    public class BookBase
    {
        /// <summary>
        /// Serial Number
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Title of Book
        /// </summary>
        [MaxLength(255)]
        [Required(ErrorMessage ="Enter the title of book")]
        [Column(TypeName = "NVarChar(255)")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the Book
        /// </summary>
        public string Description { get; set; }
    }
    [Table("Books")]
    public class Book : BookBase
    {
        //Empty
    }
}
