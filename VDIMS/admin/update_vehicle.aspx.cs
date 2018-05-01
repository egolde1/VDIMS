﻿using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace VDIMS.admin
{

    public partial class update_vehicle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                MySqlConnection conn = new MySqlConnection("ADD ME");
                DataSet ds = new DataSet();
                var sql = "SELECT IMN FROM VEHICLE";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    da.Fill(ds, "VEHICLE");
                IMNdrop.DataSource = ds.Tables["VEHICLE"];
                IMNdrop.DataValueField = "IMN";
                IMNdrop.DataTextField = "IMN";
                IMNdrop.DataBind();
                conn.Close();
                IMNdrop.Items.Insert(0, new ListItem("Select IMN", "0"));

            }


        }

        protected void IMNdrop_SelectedIndexChanged(object sender, EventArgs e)
        {

                string connection = "ADD ME";
                string sql = "select * FROM VEHICLE WHERE IMN = " + IMNdrop.SelectedItem.ToString();
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = null;
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    make.Text = reader["MAKE"].ToString();
                    model.Text = reader["MODEL"].ToString();
                    year.Text = reader["VEHICLE_YEAR"].ToString();
                    extColor.Text = reader["EXTERIOR_COLOR"].ToString();
                    intColor.Text = reader["INTERIOR_COLOR"].ToString();
                    Mileage.Text = reader["MILEAGE"].ToString();
                    cMpg.Text = reader["CITY_MPG"].ToString();
                    hMpg.Text = reader["HIGHWAY_MPG"].ToString();
                    engine.Text = reader["VEHICLE_ENGINE"].ToString();
                    transmission.Text = reader["TRANSMISSION"].ToString();
                    price.Text = reader["PRICE"].ToString();
                    vin.Text = reader["VIN"].ToString();
                    String cond = reader["VEHICLE_CONDITION"].ToString();
                    foreach(ListItem i in condition.Items)
                    {
                        if(i.Value == cond)
                        {
                            condition.SelectedIndex = condition.Items.IndexOf(condition.Items.FindByValue(cond));
                        } 
                    }
                     location.SelectedIndex = Convert.ToInt32(reader["DEALERSHIP_ID"].ToString()) - 1;
                }
                reader.Close();
                reader.Dispose();
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


            }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("ADD ME");
            conn.Open();
            MySqlCommand comm = conn.CreateCommand();
            String imn = IMNdrop.SelectedValue;
            String make1 = make.Text;
            String model1 = model.Text;
            String year1 = year.Text;
            String extColor1 = extColor.Text;
            String intColor1 = intColor.Text;
            String condition1 = condition.SelectedValue;
            int mileage1 = int.Parse(Mileage.Text);
            int cMpg1 = int.Parse(cMpg.Text);
            int hMpg1 = int.Parse(hMpg.Text);
            String engine1 = engine.Text;
            String transmission1 = transmission.Text;
            int location1 = int.Parse(location.SelectedValue);
            int price1 = int.Parse(price.Text);
            String vin1 = vin.Text;
            var sql = "UPDATE VEHICLE SET VIN = @vin1, MAKE = @make1, MODEL = @model1, VEHICLE_YEAR = @year1, MILEAGE = @mileage1, EXTERIOR_COLOR = @extColor1, INTERIOR_COLOR = @intColor1, TRANSMISSION = @transmission1, VEHICLE_CONDITION = @condition1, DEALERSHIP_ID = @location1, HIGHWAY_MPG = @hMpg1, CITY_MPG = @cMpg1, PRICE = @price1, VEHICLE_ENGINE = @engine1 WHERE IMN = " + imn;
            comm.CommandText = sql;
            comm.Parameters.AddWithValue("@vin1", vin1);
            comm.Parameters.AddWithValue("@make1", make1);
            comm.Parameters.AddWithValue("@model1", model1);
            comm.Parameters.AddWithValue("@year1", year1);
            comm.Parameters.AddWithValue("@mileage1", mileage1);
            comm.Parameters.AddWithValue("@extColor1", extColor1);
            comm.Parameters.AddWithValue("@intColor1", intColor1);
            comm.Parameters.AddWithValue("@transmission1", transmission1);
            comm.Parameters.AddWithValue("@condition1", condition1);
            comm.Parameters.AddWithValue("@location1", location1);
            comm.Parameters.AddWithValue("@hMpg1", hMpg1);
            comm.Parameters.AddWithValue("@cMpg1", cMpg1);
            comm.Parameters.AddWithValue("@price1", price1);
            comm.Parameters.AddWithValue("@engine1", engine1);
            comm.ExecuteNonQuery();
            conn.Close();
            msgTxt.Text = "Vehicle successfully updated in the database";
            }
    }
}

