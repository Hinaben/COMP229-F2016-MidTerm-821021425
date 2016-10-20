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
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
                // if loading the page for the first time
                // populate the TodoList grid
                if (!IsPostBack)
                {
                    // Get the TodoList data
                    this.GetTodoList();
                }
            }

        /// <summary>
        /// This method gets the TodoList data from the DB
        /// </summary>
        private void GetTodoList()
        {
            // connect to EF DB
            using (TodoContext db = new TodoContext())
            {
                // query the TodoList Table using EF and LINQ
                var TodoData = (from allTodo in db.Todos
                                   select allTodo);

                // bind the result to the TodoList GridView
                ToDoGridView.DataSource = TodoData.ToList();
                ToDoGridView.DataBind();
            }
        }
        
           
        protected void ToDoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected TodoID using the Grid's DataKey collection
            int selectedTodoID = Convert.ToInt32(ToDoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF and LINQ to find the selected Todo in the DB and remove it
            using (TodoContext db = new TodoContext())
            {
                // create object ot the course class and store the query inside of it

                Todo deletedTodo= (from todoRecords in db.Todos
                                        where todoRecords.TodoID == selectedTodoID
                                        select todoRecords).FirstOrDefault();


                // remove the selected course from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the db
                db.SaveChanges();

                // refresh the grid
                this.GetTodoList();
            }
        }

        protected void ToDoGridView_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ToDoGridView_RowDeleting2(object sender, GridViewDeleteEventArgs e)
        {

        }
    }

}
   