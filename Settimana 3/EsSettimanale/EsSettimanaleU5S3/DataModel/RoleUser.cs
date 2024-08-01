namespace EsSettimanaleU5S3.DataModel
{
    public class RoleUser
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
