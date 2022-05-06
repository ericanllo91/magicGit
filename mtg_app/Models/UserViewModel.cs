namespace mtg_app.Models
{
    public class UserViewModel {
        public long Id { get; set; }
        public string TokenableType { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Confirm {get; set;}

    }
}