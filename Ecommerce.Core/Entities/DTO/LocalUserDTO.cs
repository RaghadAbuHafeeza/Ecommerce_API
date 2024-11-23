using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities.DTO
{
    public class LocalUserDTO
    {
        public string UserName { get; set; }
    }
    // LocalUserDTO is used to send only essential user data (e.g., UserName)
    // through the API, ensuring security and reducing data transfer.
}
