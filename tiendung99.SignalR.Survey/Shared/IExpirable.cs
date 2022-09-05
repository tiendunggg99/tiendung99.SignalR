using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiendung99.SignalR.Survey.Shared
{
    public interface IExpirable
    {
        DateTime ExpiresAt { get; }
        bool IsExpired => DateTime.Now > ExpiresAt;
        int ExpiresInMin => (int)Math.Ceiling((decimal)ExpiresInSec / 60);
        int ExpiresInSec => (int)Math.Ceiling(ExpiresAt.Subtract(DateTime.Now).TotalMilliseconds / 1000);
    }
}
