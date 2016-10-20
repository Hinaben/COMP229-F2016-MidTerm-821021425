<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP229_F2016_MidTerm_821021425.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-6">


                <h1>Todo List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm">
                    <i class="fa fa-plus"></i>Add Todo
                </a>

                <asp:GridView ID="ToDoGridView" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped table-hover" DataKeyNames="TodoID"
                    OnRowDeleting="ToDoGridView_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="TodoDescription" HeaderText="To Do Description" Visible="true" />
                        <asp:BoundField DataField="ToDoNotes" HeaderText="To Do Notes" Visible="true" />
                        <asp:BoundField DataField="Completed" HeaderText="Completed" Visible="true" />

                          <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit"
                            NavigateUrl="/TodoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm"
                            runat="server" DataNavigateUrlFields="TodoID"
                            DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" />

                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>

</asp:Content>
