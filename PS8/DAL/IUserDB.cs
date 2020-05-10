using PS8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS8.DAL
{
    public interface IUserDB
    {
        public List<User> List();
    }
}
