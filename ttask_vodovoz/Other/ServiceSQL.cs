using System;
using System.Collections.Generic;
using System.Text;
//using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Linq;
using MySqlConnector;
using System.Windows.Controls;

namespace ttask_vodovoz
{
    class ServiceSQL
    {
        private string MyConString = "server = localhost; userid=root;password=novasik007;database=testtask_vodovoz;CharSet=utf32";

        #region Fill Tables

        #region Private
        private ObservableCollection<Employee> _emp_fill = new ObservableCollection<Employee>();
        private ObservableCollection<Departments> _dep_fill = new ObservableCollection<Departments>();
        private ObservableCollection<Orders> _orders_fill = new ObservableCollection<Orders>();
        #endregion

        #region Public Properties

        public ObservableCollection<Employee> emp_fill()
        {
            string sql = "SELECT * FROM testtask_vodovoz.view_employee";
            DataTable dt = new DataTable();
            using (var connection = new MySqlConnector.MySqlConnection(MyConString))
            {
                try
                {
                    connection.Open();
                    using (MySqlConnector.MySqlCommand cmdSel = new MySqlConnector.MySqlCommand(sql, connection))
                    {
                        MySqlConnector.MySqlDataAdapter da = new MySqlConnector.MySqlDataAdapter(cmdSel);
                        da.Fill(dt);
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        var emp = new Employee
                        {
                            id = Convert.ToInt32(row["id"]),
                            sname = row["_surname"].ToString(),
                            name = row["_name"].ToString(),
                            fname = row["_fathername"].ToString(),
                            gender = row["gender"].ToString(),
                            birthday = row["birthday"].ToString(),
                            department = row["description"].ToString(),
                        };
                        _emp_fill.Add(emp);
                    }
                    //обрезаем время у datetime для отображения только даты
                    foreach (Employee emp in _emp_fill)
                    {
                        emp.birthday = String.Concat(emp.birthday.Reverse().Skip(8).Reverse());
                    }
                    return _emp_fill;
                }
                catch (Exception ex)
                {
                    //Вернём пустое либо неполное в случае ошибки
                    MessageBox.Show(ex.Message);
                    return _emp_fill;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public ObservableCollection<Departments> dep_fill()
        {
            string sql = "SELECT * FROM testtask_vodovoz.view_department";
            DataTable dt = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(MyConString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                        da.Fill(dt);
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        var emp = new Departments
                        {
                            id = Convert.ToInt32(row["id"]),
                            desc = row["description"].ToString(),
                            sname = row["_surname"].ToString(),
                            executive_id = row["executive_id"].ToString(),

                        };
                        _dep_fill.Add(emp);
                    }
                    
                    return _dep_fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //Вернём пустое либо неполное в случае ошибки
                    return _dep_fill;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public ObservableCollection<Orders> orders_fill()
        {
            string sql = "SELECT * FROM testtask_vodovoz.view_orders";
            DataTable dt = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(MyConString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmdSel = new MySqlCommand(sql, connection))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                        da.Fill(dt);
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        var ord = new Orders
                        {
                            id = Convert.ToInt32(row["id"]),
                            contragent = row["contragent"].ToString(),
                            orderdate = row["orderdate"].ToString(),
                            sname = row["_surname"].ToString(),
                            autor_id = Convert.ToInt32(row["autor_id"]),
                        };
                        _orders_fill.Add(ord);
                    }
                    //обрезаем время у datetime для отображения только даты
                    foreach (Orders ord1 in _orders_fill)
                    {
                        ord1.orderdate = String.Concat(ord1.orderdate.Reverse().Skip(8).Reverse());
                    }
                    return _orders_fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //Вернём пустое либо неполное в случае ошибки
                    return _orders_fill;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion

        #endregion

        #region Fill Other

        private ObservableCollection<Departments> _fill_cb_employee = new ObservableCollection<Departments>();
        public ObservableCollection<Departments> fill_cb_employee()
        {
            try
            {
                using(MySqlConnection conn = new MySqlConnection(MyConString))
                {
                    conn.Open();
                    MySqlDataAdapter SDA2 = new MySqlDataAdapter("select id, description from department", conn);
                    DataTable DATA2 = new DataTable();
                    SDA2.Fill(DATA2);
                    foreach (DataRow row in DATA2.Rows)
                    {
                        var add1 = new Departments
                        {
                            id = Convert.ToInt32(row["id"]),
                            desc = row["description"].ToString(),
                        };
                        _fill_cb_employee.Add(add1);
                    }
                    conn.Close();
                }
                return _fill_cb_employee;
            }
            catch
            {
                return _fill_cb_employee;
            }

        }

        #endregion

        #region Add Data

        public void add_new_emp(string sname, string name, string fname, string gender, string birthday, string description)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConString))
                {
                    try
                    {
                        conn.Open();
                        string str = "insert into employee(_surname, _name) values('" + sname + "','" + name + "');";
                        MySqlCommand command = new MySqlCommand(str, conn);
                        command.ExecuteNonQuery();

                        command = new MySqlCommand("select LAST_INSERT_ID();", conn);
                        string lii = command.ExecuteScalar().ToString();

                        if (fname != null && fname != "")
                        {
                            command = new MySqlCommand("update employee set _fathername='" + fname + "' where id = " + lii + " ;", conn);
                            command.ExecuteNonQuery();
                        }
                        if (gender != null && gender != "")
                        {
                            command = new MySqlCommand("update employee set birthday='" + birthday + "' where id = " + lii + " ;", conn);
                            command.ExecuteNonQuery();
                        }
                        if (birthday != null && birthday != "")
                        {
                            command = new MySqlCommand("update employee set gender='" + gender + "' where id = " + lii + " ;", conn);
                            command.ExecuteNonQuery();
                        }
                        if (description != null && description != "")
                        {

                            command = new MySqlCommand("select id from department where description = '" + description + "'", conn);
                            string id_de = command.ExecuteScalar().ToString();

                            command = new MySqlCommand("update employee set description='" + id_de + "' where id = " + lii + " ;", conn);
                            command.ExecuteNonQuery();

                        }
                        MessageBox.Show("Успешно!");
                        conn.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        conn.Close();
                    }
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void add_new_depart(string description, string emp_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConString))
                {
                    try
                    {
                        conn.Open();
                        string str = "insert into department(description) values('" + description + "');";
                        MySqlCommand command = new MySqlCommand(str, conn);
                        command.ExecuteNonQuery();

                        command = new MySqlCommand("select LAST_INSERT_ID();", conn);
                        string lii = command.ExecuteScalar().ToString();

                        if (emp_id != null && emp_id != "")
                        {

                            command = new MySqlCommand("update department set executive_id=" + emp_id + " where id = " + lii + " ;", conn);
                            command.ExecuteNonQuery();

                        }
                        MessageBox.Show("Успешно!");
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        conn.Close();
                    }
                    
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void add_new_order(string contragent, string orderdate, string autor_id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConString))
                {
                    try
                    {
                        conn.Open();
                        string str = "insert into orders(contragent, autor_id) values('" + contragent + "'," + autor_id + ");";
                        MySqlCommand command = new MySqlCommand(str, conn);
                        command.ExecuteNonQuery();

                        command = new MySqlCommand("select LAST_INSERT_ID();", conn);
                        string lii = command.ExecuteScalar().ToString();

                        if (orderdate != null && orderdate != "")
                        {

                            command = new MySqlCommand("update orders set orderdate='" + orderdate + "' where id = '" + lii + "' ;", conn);
                            command.ExecuteNonQuery();

                        }
                        MessageBox.Show("Успешно!");
                        conn.Close();
                    }
                    catch(Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        #endregion

        #region Update Data

        public void update_emp(string id, string sname, string name, string fname, string birthday, string gender, string description)
        {
            MySqlCommand command;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConString))
                {
                    try
                    {
                        conn.Open();

                        command = new MySqlCommand("update Employee set sname = '" + sname + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();
                        command = new MySqlCommand("update Employee set name = '" + name + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();
                        command = new MySqlCommand("update Employee set fname = '" + fname + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();
                        command = new MySqlCommand("update Employee set birthday = '" + birthday + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();
                        command = new MySqlCommand("update Employee set gender = '" + gender + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();

                        command = new MySqlCommand("select id from department where description = '" + description + "'", conn);
                        string thing = command.ExecuteScalar().ToString();

                        command = new MySqlCommand("update Employee set department_id = '" + thing + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();

                        MessageBox.Show("Успешно!");

                        conn.Close();
                    }
                    catch(Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void update_dep(string id, string description, string executive_id)
        {
            MySqlCommand command;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConString))
                {
                    try
                    {
                        conn.Open();
                        command = new MySqlCommand("update department set description = '" + description + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();

                        command = new MySqlCommand("update department set executive_id=" + executive_id + " where id = " + id + " ;", conn);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Успешно!");

                        conn.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        conn.Close();
                    }
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {

            }
        }

        public void update_order(string id, string contragent, string orderdate, string autor_id)
        {
            MySqlCommand command;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(MyConString))
                {
                    try
                    {
                        conn.Open();

                        command = new MySqlCommand("update orders set contragent= '" + contragent + "' where id = " + id + " ;", conn);
                        command.ExecuteNonQueryAsync();

                        command = new MySqlCommand("update orders set orderdate=" + orderdate + " where id = " + id + " ;", conn);
                        command.ExecuteNonQuery();

                        command = new MySqlCommand("update orders set autor_id=" + autor_id + " where id = " + id + " ;", conn);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Успешно!");

                        conn.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        conn.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        #endregion

    }
}
