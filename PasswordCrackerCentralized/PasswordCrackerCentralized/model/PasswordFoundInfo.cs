using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerCentralized.model
{
    public class PasswordFoundInfo
    {
        public Guid _id { get; set; }
        public string _username { get; set; }
        public string _password { get; set; }
    }
}
