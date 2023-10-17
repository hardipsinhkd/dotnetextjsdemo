using System.ComponentModel.DataAnnotations;

namespace DotNetDemo.Service.Dtos
{
    public class CreateGroupDto
    {
        [Required]
        public string Name { get; set; }
    }
}
