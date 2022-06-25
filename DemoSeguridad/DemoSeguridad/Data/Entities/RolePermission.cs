namespace DemoSeguridad.Data.Entities
{
    public class RolePermission
    {
        public string RoleName { get; set; }
        public Role Role { get; set; }
        public string PermissionName { get; set; }
        public Permission Permission { get; set; }
    }
}