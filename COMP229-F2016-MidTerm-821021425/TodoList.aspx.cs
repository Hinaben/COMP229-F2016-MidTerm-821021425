﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements required for EF DB access
using COMP229_F2016_MidTerm_821021425.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

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


        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the new Page size
            ToDoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the GridView
            this.GetTodoList();
        }

        protected void ToDoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            ToDoGridView.PageIndex = e.NewPageIndex;

            // refresh the Gridview
            this.GetTodoList();
        }

        protected void ToDoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // refresh the GridView
            this.GetTodoList();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void ToDoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < ToDoGridView.Columns.Count - 1; index++)
                    {
                        if (ToDoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }

}
   