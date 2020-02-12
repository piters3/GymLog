namespace GymLog.API.DTOs
{
    public class UserWithRoleDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}
