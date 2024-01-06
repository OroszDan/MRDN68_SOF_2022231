namespace MRDN68_SOF_2022231.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoContentType { get; set; }
        public byte[] PhotoData { get; set; }
        public string Password { get; set; }
    }

}
