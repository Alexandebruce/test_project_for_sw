using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class Passport
    {
        private string _Type;
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        private string _Number;
        public string Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        public static bool operator !=(Passport p1, Passport p2)
        {
            return p1.Type != p2.Type || p1.Number != p2.Number;
        }

        public static bool operator ==(Passport p1, Passport p2)
        {
            return p1.Type == p2.Type && p1.Number == p2.Number;
        }

    }

    public class Employee
    {
        private Passport _Passport = new Passport();

        private int _Id;
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        private string _Surname;
        public string Surname
        {
            get
            {
                return this._Surname;
            }
            set
            {
                this._Surname = value;
            }
        }

        private string _Phone;
        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                this._Phone = value;
            }
        }

        private int _CompanyId = -1;
        public int CompanyId
        {
            get
            {
                return this._CompanyId;
            }
            set
            {
                this._CompanyId = value;
            }
        }

        public Passport Passport
        {
            get
            {
                return this._Passport;
            }
            set
            {
                this._Passport = value;
            }
        }

        public static bool operator !=(Employee e1, Employee e2)
        {
            return e1.Name != e2.Name || e1.Surname != e2.Surname || e1.Phone != e2.Phone || e1.CompanyId != e2.CompanyId || e1.Passport != e2.Passport;
        }

        public static bool operator ==(Employee e1,Employee e2)
        {
            return e1.Name == e2.Name && e1.Surname == e2.Surname && e1.Phone == e2.Phone && e1.CompanyId == e2.CompanyId && e1.Passport == e2.Passport;
        }
    }
}
