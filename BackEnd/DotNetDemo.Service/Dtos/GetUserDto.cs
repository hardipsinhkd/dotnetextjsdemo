namespace DotNetDemo.Service.Dtos
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
