namespace AuthWebAPIDemo.Model
{
    public class UserDto //DataTransferObject
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
 //no need of save data of this only use to share data into API