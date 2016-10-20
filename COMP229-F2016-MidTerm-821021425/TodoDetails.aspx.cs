using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements required for EF DB access
using COMP229_F2016_MidTerm_821021425.Models;
using System.Web.ModelBinding;

namespace COMP229_F2016_MidTerm_821021425
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to the students page
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to conect to the server
            using (TodoContext db = new TodoContext())
            {
                // use the Todo model to create a new Todo object and 
                // save a new record

                Todo newTodoList = new Todo();
              

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // our URL has a todoID in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

               
                    newTodoList = (from item in db.Todos
                                  where item.TodoID == TodoID
                                  select item).FirstOrDefault();
                }

                // add form data to the new TodoList record


                newTodoList.TodoDescription = TodoNameTextBox.Text;
                newTodoList.TodoNotes = NotesTextBox.Text;

                // use LINQ to ADO.NET to add / insert new name into the db

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodoList);
                }

                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated TodoList page
                Response.Redirect("~/TodoList.aspx");
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void CommentCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox.Visible = true;
        }
    }
}