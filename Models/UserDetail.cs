namespace HelloBear.Models
{
    public class UserDetail
    {
        public UserDetail()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            Phone = string.Empty;
            PhoneType = string.Empty;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string PhoneType { get; set; }
    }
}
