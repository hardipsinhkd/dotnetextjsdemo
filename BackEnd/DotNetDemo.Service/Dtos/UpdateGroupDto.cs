using System.ComponentModel.DataAnnotations;

namespace DotNetDemo.Service.Dtos
{
    public class UpdateGroupDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
