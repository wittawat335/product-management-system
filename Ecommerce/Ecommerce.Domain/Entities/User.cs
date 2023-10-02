using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public Guid PositionId { get; set; }

    public string Address { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Position Position { get; set; }

    public virtual ICollection<UserPosition> UserPositions { get; set; } = new List<UserPosition>();
}
