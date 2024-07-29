namespace EsSettimanaleU5S3.DataModel
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
    }

}
