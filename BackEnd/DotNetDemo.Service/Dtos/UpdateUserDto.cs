using System.ComponentModel.DataAnnotations;

namespace DotNetDemo.Service.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public int? GroupId { get; set; }
    }
}
