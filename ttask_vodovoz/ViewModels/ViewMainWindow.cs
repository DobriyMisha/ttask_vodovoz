using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ttask_vodovoz
{
    class ViewMainWindow: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        private ServiceSQL sq = new ServiceSQL();

        #region Private Members

        private string Selected_Employee_ID;

        private ObservableCollection<Employee> _employee = new ObservableCollection<Employee>();
        private ObservableCollection<Departments> _departments = new ObservableCollection<Departments>();
        private ObservableCollection<Orders> _orders = new ObservableCollection<Orders>();

        private Employee _select_employee = new Employee();
        private Departments _select_department = new Departments();
        private Orders _select_order = new Orders();

        private string _tb_employee_sname;
        private string _tb_employee_name;
        private string _tb_employee_fname;
        private string _tb_employee_birthdate;
        private string _tb_employee_gender;
        private Departments _cb_employee_dep_curr;
        private ObservableCollection<Departments> _cb_employee_dep;

        private string _tb_dep_desc;
        private string _tb_dep_emp_id;

        private string _tb_order_ctrg;
        private string _tb_order_date;
        private string _tb_order_emp_id;

        #endregion

        #region Public Properties

        //lists for datagrid
        public ObservableCollection<Employee> employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
            }
        }
        public ObservableCollection<Departments> departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
            }
        }
        public ObservableCollection<Orders> orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
            }
        }

        //select requests
        public Employee Select_employee
        {
            get { return _select_employee; }
            set
            {
                _select_employee = value;

                if(value != null)
                {
                    Selected_Employee_ID = value.id.ToString();

                    tb_employee_sname = value.sname;
                    tb_employee_name = value.name;
                    tb_employee_fname = value.fname;
                    tb_employee_birthdate = value.birthday.ToString();
                    tb_employee_gender = value.gender;

                    foreach (Departments dep in cb_employee_dep)
                    {
                        if (dep.desc.ToString() == value.department.ToString())
                        {
                            cb_employee_dep_curr = dep;
                        }
                    }
                }

                
            }
        }
        public Departments Select_department
        {
            get { return _select_department; }
            set
            {
                _select_department = value;

                if(value != null)
                {
                    tb_dep_desc = value.desc.ToString();
                    tb_dep_emp_id = value.executive_id.ToString();
                }


            }
        }
        public Orders Select_order
        {
            get { return _select_order; }
            set
            {
                _select_order = value;

                if(value != null)
                {
                    tb_order_ctrg = value.contragent.ToString();
                    tb_order_date = value.orderdate.ToString();
                    tb_order_emp_id = value.autor_id.ToString();
                }

            }
        }

        //employee tb's and cb
        public string tb_employee_sname
        {
            get { return _tb_employee_sname; }
            set
            {
                _tb_employee_sname = value;
            }
        }
        public string tb_employee_name
        {
            get { return _tb_employee_name; }
            set
            {
                _tb_employee_name = value;
            }
        }
        public string tb_employee_fname
        {
            get { return _tb_employee_fname; }
            set
            {
                _tb_employee_fname = value;
            }
        }
        public string tb_employee_birthdate
        {
            get { return _tb_employee_birthdate; }
            set
            {
                _tb_employee_birthdate = value;
            }
        }
        public string tb_employee_gender
        {
            get { return _tb_employee_gender; }
            set
            {
                _tb_employee_gender = value;
            }
        }

        public Departments cb_employee_dep_curr
        {
            get
            {
                return _cb_employee_dep_curr;
            }
            set
            {
                _cb_employee_dep_curr = value;
            }
        }
        public ObservableCollection<Departments> cb_employee_dep
        {
            get { return _cb_employee_dep; }
            set
            {
                _cb_employee_dep = value;
            }
        }

        //department tb's
        public string tb_dep_desc
        {
            get { return _tb_dep_desc; }
            set
            {
                _tb_dep_desc = value;
            }
        }

        public string tb_dep_emp_id
        {
            get { return _tb_dep_emp_id; }
            set
            {
                _tb_dep_emp_id = value;
            }
        }

        //order tb's
        public string tb_order_ctrg
        {
            get { return _tb_order_ctrg; }
            set
            {
                _tb_order_ctrg = value;
            }
        }

        public string tb_order_date
        {
            get { return _tb_order_date; }
            set
            {
                _tb_order_date = value;
            }
        }

        public string tb_order_emp_id
        {
            get { return _tb_order_emp_id; }
            set
            {
                _tb_order_emp_id = value;
            }
        }

        #endregion

        #region Commands

        //EMPLOYEE controls
        private readonly DelegateCommand<string> _click_clear_employee;
        public DelegateCommand<string> Click_clear_employee
        {
            get { return _click_clear_employee; }
        }
        private readonly DelegateCommand<string> _click_add_employee;
        public DelegateCommand<string> Click_add_employee
        {
            get { return _click_add_employee; }
        }
        private readonly DelegateCommand<string> _click_update_employee;
        public DelegateCommand<string> Click_update_employee
        {
            get { return _click_update_employee; }
        }

        //DEPARTMENT controls
        private readonly DelegateCommand<string> _click_clear_dep;
        public DelegateCommand<string> Click_clear_dep
        {
            get { return _click_clear_dep; }
        }
        private readonly DelegateCommand<string> _click_add_dep;
        public DelegateCommand<string> Click_add_dep
        {
            get { return _click_add_dep; }
        }
        private readonly DelegateCommand<string> _click_update_dep;
        public DelegateCommand<string> Click_update_dep
        {
            get { return _click_update_dep; }
        }

        //ORDER controls
        private readonly DelegateCommand<string> _click_clear_order;
        public DelegateCommand<string> Click_clear_order
        {
            get { return _click_clear_order; }
        }
        private readonly DelegateCommand<string> _click_add_order;
        public DelegateCommand<string> Click_add_order
        {
            get { return _click_add_order; }
        }
        private readonly DelegateCommand<string> _click_update_order;
        public DelegateCommand<string> Click_update_order
        {
            get { return _click_update_order; }
        }

        #endregion

        #region Functions

        public void fill_emp()
        {
            employee.Clear();
            employee = sq.emp_fill();
        }

        public void fill_dep()
        {
            departments.Clear();
            departments = sq.dep_fill();
        }

        public void fill_orders()
        {
            orders.Clear();
            orders = sq.orders_fill();
        }

        public void clear_emp()
        {
            tb_employee_sname = "";
            tb_employee_name = "";
            tb_employee_fname = "";
            tb_employee_gender = "";
            tb_employee_birthdate = "";
            cb_employee_dep_curr = null;

            Select_employee = null;
            
        }

        public void clear_dep()
        {
            tb_dep_desc = "";
            tb_dep_emp_id = "";
            Select_department = null;
        }

        public void clear_order()
        {
            tb_order_ctrg = "";
            tb_order_date = "";
            tb_order_emp_id = "";
            Select_order = null;
        }

        public void add_emp()
        {
            sq.add_new_emp(tb_employee_sname, tb_employee_name, tb_employee_fname, tb_employee_gender, tb_employee_birthdate, cb_employee_dep_curr.desc.ToString());
            employee.Clear();
            fill_emp();
        }

        public void add_dep()
        {
            sq.add_new_depart(tb_dep_desc,tb_dep_emp_id);
            departments.Clear();
            fill_dep();
        }

        public void add_order()
        {
            sq.add_new_order(tb_order_ctrg, tb_order_date, tb_order_emp_id);
            orders.Clear();
            fill_orders();
        }

        public void update_emp()
        {
            sq.update_emp(Select_employee.id.ToString(), tb_employee_sname, tb_employee_name, tb_employee_fname, tb_employee_gender, tb_employee_birthdate, cb_employee_dep_curr.desc.ToString());
            employee.Clear();
            fill_emp();
        }

        public void update_dep()
        {
            sq.update_dep(Select_department.id.ToString(), tb_dep_desc, tb_dep_emp_id);
            departments.Clear();
            fill_dep();
        }

        public void update_order()
        {
            sq.update_order(Select_order.id.ToString(), tb_order_ctrg, tb_order_date, tb_order_emp_id);
            orders.Clear();
            fill_orders();
        }

        public void delete_emp()
        {
            if(Select_employee != null)
            {

            }
        }

        public void delete_dep()
        {
            if(Select_department != null)
            {

            }
        }

        public void delete_order()
        {

        }

        #endregion

        public ViewMainWindow()
        {
            fill_emp();
            fill_dep();
            fill_orders();
            cb_employee_dep = sq.fill_cb_employee();

            #region Commands Init

            //команда клика на очистку тб employee
            _click_clear_employee = new DelegateCommand<string>(
            (s) => { /* perform some action */ clear_emp(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на добавление employee
            _click_add_employee = new DelegateCommand<string>(
            (s) => { /* perform some action */ clear_emp(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на изменение employee
            _click_update_employee = new DelegateCommand<string>(
            (s) => { /* perform some action */ clear_emp(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на очистку тб department
            _click_clear_dep = new DelegateCommand<string>(
            (s) => { /* perform some action */ clear_dep(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на добавление department
            _click_add_dep = new DelegateCommand<string>(
            (s) => { /* perform some action */ add_dep(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на изменить department
            _click_update_dep = new DelegateCommand<string>(
            (s) => { /* perform some action */ update_dep(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на очистку тб order
            _click_clear_order = new DelegateCommand<string>(
            (s) => { /* perform some action */ clear_order(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на добавление order
            _click_add_order = new DelegateCommand<string>(
            (s) => { /* perform some action */ add_order(); }//, //Execute
            //(s) => {  } //CanExecute
            );
            //команда клика на изменение order
            _click_update_order = new DelegateCommand<string>(
            (s) => { /* perform some action */ update_order(); }//, //Execute
            //(s) => {  } //CanExecute
            );

            #endregion
        }
    }
}
