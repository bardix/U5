namespace EsSettimanaleU5S3.DataModel
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
