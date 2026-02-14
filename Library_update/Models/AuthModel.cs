namespace Library_update.Models
{
   
        public sealed class RegisterRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public sealed class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public sealed class AuthResponse
        {
            public string AccessToken { get; set; }
        }
    }

