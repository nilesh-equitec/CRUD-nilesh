using Dapper;
using DapperMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DapperMVC.Repo
{
    public class EmployeeRepo
    {
        private readonly string _connectionString;

        public  EmployeeRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable< Employee> GetAllEmployee()
        {
            DbConnection conn = new SqlConnection( _connectionString );
            conn.Open();
            //string query = "SELECT Id, name, email,phone,gender,skill,adderess FROM Employee WHERE isactive=1";
            return conn.Query<Employee>("AllEmployee", new { value = true},commandType:System.Data.CommandType.StoredProcedure);
        }
        public IEnumerable<Employee> DeleteAllEmployee()
        {
            DbConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn.Query<Employee>("AllEmployee", new { value = false }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public Employee GetByEmployeeID(int empID) { 
            DbConnection dbConnection = new SqlConnection( _connectionString );
            dbConnection.Open();
            //string query = "SELECT Id, name, email,phone,gender,skill,adderess FROM Employee WHERE isactive=1";
            Employee employee = dbConnection.QueryFirstOrDefault<Employee>("SelectById1", new { value = empID }, commandType: System.Data.CommandType.StoredProcedure);
            return employee; 
        }

        public void InsertEmployee(Employee employee) {
            DynamicParameters ObjParm = new DynamicParameters();
            ObjParm.Add("@Name",employee.Name);
            ObjParm.Add("@Email",employee.Email);
            ObjParm.Add("@Phone", employee.Phone);
            ObjParm.Add("@Gender", employee.Gender);
            ObjParm.Add("@Skill", employee.Skill);
            ObjParm.Add("@Address", employee.Adderess);

            DbConnection dbConnection= new SqlConnection( _connectionString );
           dbConnection.Open();
            //string query = "insert into Employee (name,email,phone,adderess) values (@Name, @Email,@Phone,@Adderess)";
            dbConnection.Execute("InsertEmployee", ObjParm, commandType:System.Data.CommandType.StoredProcedure);
        }

        public void UpdateEmployee(Employee employee)
        {
            DynamicParameters ObjParm = new DynamicParameters();
            ObjParm.Add("@Id", employee.Id);
            ObjParm.Add("@Name", employee.Name);
            ObjParm.Add("@Email", employee.Email);
            ObjParm.Add("@Phone", employee.Phone);
            ObjParm.Add("@Address", employee.Adderess);
            ObjParm.Add("@Skill", employee.Skill);
            DbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            //string query = "update Employee set name=@Name,email=@Email,phone=@Phone,Adderess=@Adderess where id=@Id";
            dbConnection.Execute("UpdateEmployee", ObjParm, commandType:System.Data.CommandType.StoredProcedure);
        }
        public void DeleteEmployee(int id) { 
            DbConnection dbConnection = new SqlConnection( _connectionString );
            dbConnection.Open();
            //string query = "delete from Employee Where id=@Id";
            dbConnection.Execute("DelectEmployee1", new {Id=id,value1=false }, commandType:System.Data.CommandType.StoredProcedure);
       
        }
        public void ActiveEmployee(int id) {
            DbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            //string query = "delete from Employee Where id=@Id";
            dbConnection.Execute("DelectEmployee1", new { Id = id, value1 = true }, commandType: System.Data.CommandType.StoredProcedure);
        }

       

    }
}