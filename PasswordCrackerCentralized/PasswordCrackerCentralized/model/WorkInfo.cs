using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerCentralized.model
{

    public class WorkInfo
    {
        private Guid _id;
        private List<UserInfo> _usersList;
        private List<string> _wordList;

        public List<string> WordList
        {
            get { return _wordList; }
            set { _wordList = value; }
        }

        public List<UserInfo> UsersList
        {
            get { return _usersList; }
            set { _usersList = value; }
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
