using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WEBMVC.Models;

namespace WEBMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        string strConn = "Data source=localhost\\MSSQLSERVER01;Initial Catalog=DB-PERSON;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlDataAdapter sdp = new SqlDataAdapter("select * from [Table]", conn);
                sdp.Fill(dt);
                conn.Close();
            }

            return View(dt);
        }



        // GET: Student/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new PersonModel());
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(PersonModel pers)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string req = "insert into [Table](NCIN,NomPers,PrenomPers) values(@cin,@nom,@prenom)";
                SqlCommand cmd = new SqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@cin", pers.NCIN);
                cmd.Parameters.AddWithValue("@nom", pers.NomPers);
                cmd.Parameters.AddWithValue("@prenom", pers.PrenomPers);
                cmd.ExecuteNonQuery();
                conn.Close();



            }
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
       [HttpGet]
        public ActionResult Edit(int id)
        {
            PersonModel pers = new PersonModel();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string req = "select * from [Table] where NCIN=@cin";
                SqlDataAdapter sdp = new SqlDataAdapter(req, conn);
                sdp.SelectCommand.Parameters.AddWithValue("@cin", id);


                sdp.Fill(dt);
            }
            if (dt.Rows.Count == 1)
            {
                pers.NCIN =Convert.ToInt32(dt.Rows[0][0].ToString());
                pers.NomPers = dt.Rows[0][1].ToString();
                pers.PrenomPers = dt.Rows[0][2].ToString();
                return View(pers);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(PersonModel pers)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string req = "update [Table] set  NomPers=@nom,PrenomPers=@prenom where NCIN=@cin" ;
                SqlCommand cmd = new SqlCommand(req, conn);
                cmd.Parameters.AddWithValue("@cin", pers.NCIN);
                cmd.Parameters.AddWithValue("@nom", pers.NomPers);
                cmd.Parameters.AddWithValue("@prenom", pers.PrenomPers);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Index");

        }

        // GET: Student/Delete/5
       public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                {
                    conn.Open();
                    string req = "delete from [Table] where NCIN=@cin";
                    SqlCommand cmd = new SqlCommand(req, conn);
                    cmd.Parameters.AddWithValue("@cin", id);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }


        }
    }
}