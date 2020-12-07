using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WingtipToys
{
    public partial class Reviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Write the data for the dropdownlist 
            if (!IsPostBack)
            {
                PopulateListBox("Products", "ProductName", ProductDropDownList);
                PopulateListBox("comment", "first_name", FNameDropDownList);
                PopulateListBox("comment", "last_name", LNameDropDownList);
                PopulateListBox("Products", "ProductName", SearchItemDropDownList);
            }
            //PopulateListBox("comment", "first_name", FNameDropDownList);


        }

        protected void SubmitReview_Click(object sender, EventArgs e)
        {

            if (ReviewTextbox.Text == "")
            {
                ErrorLabel.Text = "Please write a review!";
            }
            else if (EmailAddressTextbox.Text == "")
            {
                ErrorLabel.Text = "Please provide a valid email address";
            }
            else if(ProductDropDownList.SelectedValue == "All")
            {
                ErrorLabel.Text = "You must review a specific item!";
            }
            else
            {
                AddReview(ProductDropDownList.SelectedValue);
                //ErrorLabel.Text = GetProductID().ToString();
                //ErrorLabel.Text = "Review Added!";
                ProductDropDownList.SelectedValue = "All";
                ReviewTextbox.Text = "";
                EmailAddressTextbox.Text = "";
                FirstNameTextbox.Text = "";
                LastNameTextbox.Text = "";
            }
        }
        protected void SearchReview_Click(object sender, EventArgs e)
        {
            string s = WebConfigurationManager.ConnectionStrings["WingtipToys"].ConnectionString;
            SqlConnection con = new SqlConnection(s);
            SqlCommand cmd;
            StringBuilder sqlString = new StringBuilder("SELECT * FROM comment c");
            int additions = 0;
            if(SearchItemDropDownList.SelectedValue != "All")
            {
                if(additions == 0)
                {
                    sqlString.Append(" WHERE c.comment_product_id = @productid");
                }
                else
                {
                    sqlString.Append(" AND c.comment_product_id = @productid");
                }
                additions++;


            }
            if (FNameDropDownList.SelectedValue != "All")
            {
                if (additions == 0)
                {
                    sqlString.Append(" WHERE c.first_name = @firstname");
                }
                else
                {
                    sqlString.Append(" AND c.first_name = @firstname");
                }
                additions++;

            }
            if (LNameDropDownList.SelectedValue != "All")
            {
                if (additions == 0)
                {
                    sqlString.Append(" WHERE c.last_name = @lastname");
                }
                else
                {
                    sqlString.Append(" AND c.last_name = @lastname");
                }
                additions++;

            }
            cmd = new SqlCommand(sqlString.ToString(), con);
            try
            {
                int id = GetProductID(SearchItemDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@productid", id);
                Debug.WriteLine($"IT IS REPLACING PRODUCT ID with {id}");
            }
            catch
            {

            }
            try
            {
                cmd.Parameters.AddWithValue("@firstname", FNameDropDownList.SelectedValue);
            }
            catch
            {

            }
            try
            {
                cmd.Parameters.AddWithValue("@lastname", LNameDropDownList.SelectedValue);
            }
            catch
            {

            }
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ReviewGridView.DataSource = dr;
            ReviewGridView.DataBind();
            if (!dr.HasRows)
            {
                NoReivewLabel.Text = "No reviews are available that match that query";
            }
            else
            {
                NoReivewLabel.Text = "";
            }
            dr.Close();
            con.Close();
            //RefreshListboxes();


            /*
            if (FNameDropDownList.SelectedValue != "All" && LNameDropDownList.SelectedValue != "All" && ProductDropDownList.SelectedValue != "All")
            {
                sqlString = "SELECT * FROM comment c WHERE comment_product_id = @productid AND first_name = @firstname AND last_name = @lastname";
                cmd = new SqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@productid", GetProductID(ProductDropDownList.SelectedValue));
                cmd.Parameters.AddWithValue("@firstname", FNameDropDownList.SelectedValue);
                cmd.Parameters.AddWithValue("@lastname", LNameDropDownList.SelectedValue);

            }
            else if(FNameDropDownList.SelectedValue != "All")
            {
                sqlString = "SELECT * FROM comment c WHERE comment_product_id = @productid AND first_name = @firstname";
                cmd = new SqlCommand(sqlString, con); 
                cmd.Parameters.AddWithValue("@productid", GetProductID(ProductDropDownList.SelectedValue));
                cmd.Parameters.AddWithValue("@firstname", FNameDropDownList.SelectedValue);
            }
            else if(LNameDropDownList.SelectedValue != "All")
            {
                sqlString = "SELECT * FROM comment c WHERE comment_product_id = @productid AND last_name = @lastname";
                cmd = new SqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@productid", GetProductID(ProductDropDownList.SelectedValue));
                cmd.Parameters.AddWithValue("@lastname", LNameDropDownList.SelectedValue);
            }
            else
            {
                sqlString = "SELECT * FROM comment c WHERE comment_product_id = @productid";
                cmd = new SqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@productid", GetProductID(ProductDropDownList.SelectedValue));

            }
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ReviewGridView.DataSource = dr;
            ReviewGridView.DataBind();
            dr.Close();
            con.Close();

            */
        }

        protected void AddReview(string product)
        {
            //generate all the strings for it
            int productID = GetProductID(product);
            string comment = ReviewTextbox.Text;
            string email = EmailAddressTextbox.Text;
            string firstname = FirstNameTextbox.Text;
            string lastname = LastNameTextbox.Text;

            //create actual statement
            string sqlString = "INSERT INTO " +
                "comment " +
                "(comment_id," +
                "comment_product_id," +
                "comment_user_id," +
                "first_name," +
                "last_name," +
                "email_address," +
                "comment_content) " +
                "VALUES " +
                "(@comment_id," +
                "@prod_id," +
                "@user_id," +
                "@fname," +
                "@lname," +
                "@eaddress," +
                "@content)";

            //Generate connection
            string s = WebConfigurationManager.ConnectionStrings["WingtipToys"].ConnectionString;
            SqlConnection con = new SqlConnection(s);
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.Parameters.AddWithValue("@comment_id", GetTableSize() + 1);
            cmd.Parameters.AddWithValue("@prod_id", productID);
            cmd.Parameters.AddWithValue("@user_id", DBNull.Value);
            cmd.Parameters.AddWithValue("@fname", firstname);
            cmd.Parameters.AddWithValue("@lname", lastname);
            cmd.Parameters.AddWithValue("@eaddress", email);
            cmd.Parameters.AddWithValue("@content", comment);

            //open the connection
            con.Open();
            cmd.ExecuteNonQuery();
            /*
            //update comment section
            string sqlString2 = "SELECT * FROM comment c WHERE  c.comment_product_id = @productid";
            SqlCommand cmd2 = new SqlCommand(sqlString2, con);
            //cmd2.Parameters.AddWithValue("@productid", ProductID.GetProductID());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            ReviewGridView.DataSource = dr2;
            ReviewGridView.DataBind();
            dr2.Close();
            */
            con.Close();
            RefreshListboxes();
        }

        protected int GetTableSize()
        {
            string sqlString = "SELECT COUNT(c.comment_id) FROM comment c";
            string s = WebConfigurationManager.ConnectionStrings["WingtipToys"].ConnectionString;
            SqlConnection con = new SqlConnection(s);
            SqlCommand cmd = new SqlCommand(sqlString, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int tableSize = 0;
            while (dr.Read())
            {
                tableSize = dr.GetInt32(0);
            }
            dr.Close();
            con.Close();
            return tableSize;
        }

        protected int GetProductID(string productname)
        {
            //Debug.WriteLine(productname);
            string sqlString = "SELECT p.ProductID FROM Products p WHERE p.ProductName = @productname";
            string s = WebConfigurationManager.ConnectionStrings["WingtipToys"].ConnectionString;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.Parameters.AddWithValue("@productname", productname);
            SqlDataReader dr = cmd.ExecuteReader();
            int productID = 0;
            //Debug.WriteLine(dr.Read());
            while (dr.Read())
            {
                //Debug.WriteLine("Reading");
               productID = dr.GetInt32(0);
               Debug.WriteLine($"Product ID is {productID}");
            }
            dr.Close();
            con.Close();
            return productID;
        }

        protected void PopulateListBox(string table, string column, DropDownList listbox)
        {
            //Response.Write("<center><h1>Read data from a database</h1></center><hr/>");
            //Response.Write("<br/>");

            //step 1 read connection string
            string s = WebConfigurationManager.ConnectionStrings["WingtipToys"].ConnectionString;
            //step 2 - create sqlconnection
            SqlConnection con = new SqlConnection(s);
            con.Open();
            //setup query string
            //string sqlString = "SELECT * FROM Customers";
            //setup sql command object
            string sqlStringDropDownList = $"SELECT DISTINCT {column} FROM {table}";
            SqlCommand cmd2 = new SqlCommand(sqlStringDropDownList, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            listbox.Items.Add("All");
            while (dr2.Read())
            {
                if(dr2[column] == DBNull.Value)
                {
                    listbox.Items.Add(new ListItem("NULL".ToString(),"NULL".ToString()));
                }
                else
                {
                    //Debug.WriteLine($"Reading {dr2[column].ToString()}");
                    listbox.Items.Add(new ListItem(dr2[column].ToString(),
                    dr2[column].ToString()));
                }
                
            }
            dr2.Close();
            //close the connection
            con.Close();
            Response.Write(s);
        }
        protected void PopulateListBox(string table, string column, ListBox listbox)
        {
            //Response.Write("<center><h1>Read data from a database</h1></center><hr/>");
            //Response.Write("<br/>");

            //step 1 read connection string
            string s = WebConfigurationManager.ConnectionStrings["WingtipToys"].ConnectionString;
            //step 2 - create sqlconnection
            SqlConnection con = new SqlConnection(s);
            con.Open();
            //setup query string
            //string sqlString = "SELECT * FROM Customers";
            //setup sql command object
            string sqlStringDropDownList = $"SELECT DISTINCT {column} FROM {table}";
            SqlCommand cmd2 = new SqlCommand(sqlStringDropDownList, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            listbox.Items.Add("All");
            while (dr2.Read() == true)
            {
                if (dr2[column] == DBNull.Value)
                {
                    listbox.Items.Add(new ListItem("NULL".ToString(), "NULL".ToString()));
                }
                else
                {
                    Debug.WriteLine($"Reading {dr2[column].ToString()}");
                    listbox.Items.Add(new ListItem(dr2[column].ToString(),
                    dr2[column].ToString()));
                }
            }
            dr2.Close();
            //close the connection
            con.Close();
            Response.Write(s);
        }

        protected void ClearListBox(DropDownList listbox)
        {
            listbox.Items.Clear();
        }
        protected void ClearListBox(ListBox listbox)
        {
            listbox.Items.Clear();
        }

        protected void RefreshListboxes()
        {
            ClearListBox(ProductDropDownList);
            ClearListBox(FNameDropDownList);
            ClearListBox(LNameDropDownList);
            ClearListBox(SearchItemDropDownList);
            PopulateListBox("Products", "ProductName", ProductDropDownList);
            PopulateListBox("comment", "first_name", FNameDropDownList);
            PopulateListBox("comment", "last_name", LNameDropDownList);
            PopulateListBox("Products", "ProductName", SearchItemDropDownList);
        }


    }
}