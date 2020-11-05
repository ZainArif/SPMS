using System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Data
{
    public class DbFunction
    {
        private readonly SPMSContext _context;
        private int key;

        public DbFunction(SPMSContext context)
        {
            _context = context;
        }

        public int GetKey(string seqName)
        {
            string query = "select dbo.GetKey(next value for "+seqName+")";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                _context.Database.OpenConnection();
                key = Convert.ToInt32(command.ExecuteScalar().ToString());
                _context.Database.CloseConnection();
            }
            return key;
        }
    }
}
