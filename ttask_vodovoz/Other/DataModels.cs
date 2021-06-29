using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ttask_vodovoz
{
    //Класс, описывающий структуру данных таблицы Сотрудники(Employee) для модели MVVM
    class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private int _id;
        private string _name;
        private string _sname;
        private string _fname;
        private string _birthday;
        private string _gender;
        private string _department;

        #region Public Properties

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public string sname
        {
            get { return _sname; }
            set
            {
                _sname = value;
            }
        }
        public string fname
        {
            get { return _fname; }
            set
            {
                _fname = value;
            }
        }
        public string birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
            }
        }
        public string gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
            }
        }
        public string department
        {
            get { return _department; }
            set
            {
                _department = value;
            }
        }

        #endregion
    }

    //Класс, описывающий структуру данных таблицы Подразделения(Departments) для модели MVVM
    class Departments : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private int _id;
        private string _desc;
        private string _sname;
        private string _executive_id;

        #region Public Properties

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public string desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
            }
        }
        public string sname
        {
            get { return _sname; }
            set
            {
                _sname = value;
            }
        }
        public string executive_id
        {
            get { return _executive_id; }
            set
            {
                _executive_id = value;
            }
        }

        #endregion
    }

    //Класс, описывающий структуру данных таблицы Заказы(Orders) для модели MVVM
    class Orders : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private int _id;
        private string _contragent;
        private string _orderdate;
        private string _sname;
        private int _autor_id;

        #region Public Properties

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public string contragent
        {
            get { return _contragent; }
            set
            {
                _contragent = value;
            }
        }
        public string orderdate
        {
            get { return _orderdate; }
            set
            {
                _orderdate = value;
            }
        }
        public string sname
        {
            get { return _sname; }
            set
            {
                _sname = value;
            }
        }
        public int autor_id
        {
            get { return _autor_id; }
            set
            {
                _autor_id = value;
            }
        }

        #endregion 

    }
}
