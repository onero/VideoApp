﻿using System;

namespace VideoAppBLL.BusinessObjects
{
    public class RoleBO : IComparable<RoleBO>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CompareTo(RoleBO other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}