using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NREIG.ContractMove.DataLayer
{
    internal class DLService
    {
        var ConnectionString = Program.Configuration.GetConnectionString("DefaultConnection")
    }
}
