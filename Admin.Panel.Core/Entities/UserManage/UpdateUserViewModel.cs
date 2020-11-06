namespace Admin.Panel.Core.Entities.UserManage
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }
        public bool IsUsed { get; set; }
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public string Nickname { get; set; }
        
        public string Role { get; set; }
        
        public bool IsAdminLastActive { get; set; }
        
        //public int ApplicationCompanyId { get; set; }
    }
}
