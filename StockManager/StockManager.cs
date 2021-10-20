using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StockManager
{
    public partial class StockManger : Form
    {
        public StockManger()
        {
            InitializeComponent();
        }

        //**************//
        
        //SqlConnection(@"Data Source= SERVER NAME ;Initial Catalog= DATABASE NAME (Task) ;Integrated Security=True");

        SqlConnection con = new SqlConnection(@"Data Source= HOME-PC ;Initial Catalog= Task ;Integrated Security=True"); 
        SqlCommand cmd ;                       
        SqlDataAdapter da;
        DataTable dt;

        //*************//
       

        private void btnProduct_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("exec SelectAllProducts ",con); //Procedure >> Select * from Products 
        

            SqlDataAdapter da = new SqlDataAdapter(cmd);
              DataTable dt = new DataTable();
                da.Fill(dt);
                 
            dataGridView2.DataSource = dt;
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
             cmd = new SqlCommand("Select * from Warehouse", con);
               da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                  da.Fill(dt);

            dataGridView2.DataSource = dt;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
             cmd = new SqlCommand("Select * from Stock", con);
               da = new SqlDataAdapter(cmd);
                 dt = new DataTable();
                   da.Fill(dt);
            
            dataGridView2.DataSource = dt;
        }

        

        TaskEntities1 db = new TaskEntities1(); // общий объект для доступа к другим классам, которые находятся в таблицах базы данных



        //>>>>>>>> PRODUCT BUTTONS <<<<<<<<<//



        public void ListProduct()  //Функция для списка продуктов
        {

            cmd = new SqlCommand("exec SelectAllProducts ", con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void btnAddP_Click(object sender, EventArgs e) //кнопка добавления продукта

        {
            //>>>> С помощью команд sql <<<<<<//
            //SqlCommand cmd = new SqlCommand("insert into Products(Product_ID,Product,TypeOfProduct,Measurement,TypeOfPacking)" +
            //    "values(" + Convert.ToInt32(Pidbox) + "," + PBox.Text + "," + PtypeBox.Text + "," + MesureBox.Text + "," + PackBox.Text + ")", con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView2.DataSource = dt;

            try
            {
                //Ado net команды для добавления значений в базу данных
                Product product = new Product(); // таблица из базы данных
                  product.Product_ID = Convert.ToInt32(Pidbox.Text); //столбцы
                    product.Product1 = PBox.Text; // столбцы
                      product.TypeOfProduct = PtypeBox.Text;//столбцы
                    product.Measurement = MesureBox.Text;//столбцы
                  product.TypeOfPacking = PackBox.Text;//столбцы


                db.Products.Add(product);
                db.SaveChanges();

                //>> Чистим Textbox <<//
                Pidbox.Clear();    //Product Id
                  PBox.Clear();      //Product
                    PtypeBox.Clear();  //Type Product
                  MesureBox.Clear(); //Measurement
                PackBox.Clear();  //Packet Type

                MessageBox.Show("Product Added ");

                ListProduct(); // Список продуктов


            }
            catch (Exception )
            {
                MessageBox.Show("Something Went wrong !");
                
            }
            finally
            {
                con.Close();
            }
        }

      
        private void btnDeleteP_Click(object sender, EventArgs e)//кнопка удаления продукта
        {
            try
            {
                int id = Convert.ToInt32(Pidbox.Text);
                  var x = db.Products.Find(id);//Ищем id 
                    db.Products.Remove(x);//Удаление Переменное 
                      db.SaveChanges();// сохранение изменений в базе данных
                        Pidbox.Clear();// Clear text box 
                MessageBox.Show("Product Deleted ! ");
                ListProduct();
            }
            catch(Exception)
            {
                MessageBox.Show("Something Went wrong !");
            }
            finally
            {
                con.Close();
            }

        }

        private void btnUpdateP_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Pidbox.Text);
                 var x = db.Products.Find(id);

                    x.Product1 = PBox.Text;
                      x.TypeOfProduct = PtypeBox.Text;
                        x.Measurement = MesureBox.Text;
                      x.TypeOfPacking = PackBox.Text;
                    db.SaveChanges();
                 
                Pidbox.Clear();
                  PBox.Clear();
                    PtypeBox.Clear();
                  MesureBox.Clear();
                PackBox.Clear();
               ListProduct();
            }
            catch(Exception)
            {
                MessageBox.Show("Something Went wrong!");
            }
            finally
            {
                con.Close();
            }
        }//Кнопка обновления продукта


        //>>>>>>> WAREHOUSE BUTTONS  <<<<<<<<// 

        public void ListWarehouse()
        {
            cmd = new SqlCommand("Select * from Warehouse",con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }//Функця для спика складов
        private void btnAddW_Click(object sender, EventArgs e)
        {
            try
            {
                Warehouse w = new Warehouse();
                 w.WarehouseID = Convert.ToInt32(WidBox.Text);
                   w.Adress = AdressBox.Text;
                     w.Warehouse_Area = WareaBox.Text;
                       w.Status = StatusBox.Text;
                db.Warehouses.Add(w);
                  db.SaveChanges();
                    ListWarehouse();

                WidBox.Clear();
                  AdressBox.Clear();
                    WareaBox.Clear();
                      StatusBox.Clear();
                MessageBox.Show("Warehouse successfuly added to database !! ");
            }
            catch (Exception)
            {
                MessageBox.Show("Something Went wrong !");
            }
            finally
            {
                con.Close();
            }
        }//Кнопка добавления склада 
        private void btnDeleteW_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(WidBox.Text);
                  var x = db.Warehouses.Find(id);
                    db.Warehouses.Remove(x);
                       db.SaveChanges();
                    WidBox.Clear();
                  ListWarehouse();
                MessageBox.Show("Warehouse succesfuly deleted ! ");
            }
            catch (Exception)
            {
                MessageBox.Show("Something Went Wrong ! ");
            }
            finally
            {
                con.Close();
            }
        }//Кнопка удаления склада
        private void btnUpdateW_Click(object sender, EventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(WidBox.Text);
                   var x = db.Warehouses.Find(id);
                     x.Adress = AdressBox.Text;
                       x.Warehouse_Area = WareaBox.Text;
                     x.Status = StatusBox.Text;
                db.SaveChanges();

                Pidbox.Clear();
                   PBox.Clear();
                     PtypeBox.Clear();
                    MesureBox.Clear();
                PackBox.Clear();

                ListWarehouse();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong !");
            }
            finally
            {
                con.Close();

            }
        }//Кнопка обновления склада



        //>>>>>>> STOCK BUTTONS <<<<<<<<<//

        public void ListStock()
        {
            cmd = new SqlCommand("Select * from Stock",con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            dataGridView2.DataSource = dt; 

        }
        private void btnAddS_Click(object sender, EventArgs e)
        {
            try
            {
                Stock st = new Stock();
                   st.Product = Convert.ToInt32(StockPbox.Text);
                      st.Warehouse = Convert.ToInt32(StockWbox.Text);
                         st.EntryDate = EntryBox.Text;
                             st.ExitDate = ExitBox.Text;
                
                db.SaveChanges();

                StockPbox.Clear();
                   StockWbox.Clear();
                      ExitBox.Clear();
                         EntryBox.Clear();
                ListStock();


            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong ! ");
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDeleteS_Click(object sender, EventArgs e)
        {
            try
            {
                //int id = Convert.ToInt32(StockPbox.Text);
                //var x = db.Stocks.Find(id);
                //db.Stocks.Remove(x);
                //db.SaveChanges();
                cmd = new SqlCommand("delete from Stock where Product= " + Convert.ToInt32(StockPbox.Text) + "", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                ListStock();
                MessageBox.Show("Product successfuly deleted from Warehouse");
            }
            catch(Exception)
            {
               MessageBox.Show("Something went wrong !");
            } 
            finally
            {
                con.Close();
            }
        }

        private void btnUpdateS_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(StockPbox.Text);
                var x = db.Stocks.Find(id);
                x.Product =Convert.ToInt32 (StockPbox.Text);
                x.Warehouse = Convert.ToInt32(StockWbox.Text);
                x.EntryDate = EntryBox.Text;
                x.ExitDate = ExitBox.Text;
                db.SaveChanges();
                ListStock();
                MessageBox.Show("Stocks successfuly updated ! ");

            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong !");
            }
            finally
            {
                con.Close();
            }
        }

        private void btnQuery1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand(" select Product,EntryDate from Stock order by (Case when Warehouse = " + Convert.ToInt32(QidBox.Text) + " then Product else null end);",con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();

                da.Fill(dt);
                dataGridView2.DataSource = dt;



            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong !");
            }
            finally
            {
                con.Close();
            }

        }

        private void btnQuery2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand(" Select WarehouseID,Adress from Warehouse where[Status] = 'Empty'",con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch(Exception)
            {
                MessageBox.Show("There is no empty warehouse !");
            }
            finally
            {
                con.Close();
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this,@"Help.txt");
        }
    }

}
