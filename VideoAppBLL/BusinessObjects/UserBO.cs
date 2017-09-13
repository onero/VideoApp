using System;

namespace VideoAppBLL.BusinessObjects
{
    public class UserBO : IComparable<UserBO>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int CompareTo(UserBO other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;
            var usernameComparison = string.Compare(Username, other.Username, StringComparison.Ordinal);
            if (usernameComparison != 0) return usernameComparison;
            return string.Compare(Password, other.Password, StringComparison.Ordinal);
        }
    }
}