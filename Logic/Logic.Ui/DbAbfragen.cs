using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videothek.Logic.Ui.ViewModel;

namespace Videothek.Logic.Ui
{
	public class DbAbfragen
	{
			SqlConnection con = new SqlConnection();
		string connectionString = "Data Source = W0110027SYS\\SQLEXPRESS; Initial Catalog = Videothek; Integrated Security = SSPI";
			SqlDataAdapter adapter = new SqlDataAdapter();

		public List<CustomerClass> GetAllCustomers()
        {

				con.ConnectionString = connectionString;
			using (SqlCommand cmd = new SqlCommand("SELECT * FROM Kunde", con))
			{
				List<CustomerClass> cs = new List<CustomerClass>();
				cmd.CommandType = CommandType.Text;
				con.Open();
				using (SqlDataReader rd = cmd.ExecuteReader())
				{
					while (rd.Read())
					{
							cs.Add(new CustomerClass
							{
								ID = Convert.ToInt32(rd["ID"]),
								Vorname = Convert.ToString(rd["Vorname"]),
								Name = Convert.ToString(rd["Name"]),
								Strasse = Convert.ToString(rd["Strasse"]),
								Hausnummer = Convert.ToString(rd["Hausnummer"]),
								PLZ = Convert.ToInt32(rd["PLZ"]),
								Ort = Convert.ToString(rd["Ort"])
							});
					}
				}
				con.Close();
				return cs;
			}
        }
		public List<ArtikelClass> GetAllArticles()
		{

			using (SqlCommand cmd = new SqlCommand("SELECT * FROM Artikel", con))
			{
				List<ArtikelClass> ca = new List<ArtikelClass>();
				cmd.CommandType = CommandType.Text;
				con.ConnectionString = connectionString;
				con.Open();
				using (SqlDataReader rd = cmd.ExecuteReader())
				{
					while (rd.Read())
					{
						ca.Add(new ArtikelClass
						{
							ID = Convert.ToInt32(rd["ID"]),
							Bezeichnung = Convert.ToString(rd["Bezeichnung"]),
                            Menge = Convert.ToInt32(rd["Menge"]),
                            Leihpreis = Convert.ToDecimal(rd["Leihpreis"])
                        });
					}
				}
				con.Close();
				return ca;
			}
		}

		public List<KategorieClass> GetAllCategories()
		{

			using (SqlCommand cmd = new SqlCommand("SELECT * FROM Kategorie", con))
			{
				List<KategorieClass> ca = new List<KategorieClass>();
				cmd.CommandType = CommandType.Text;
				con.ConnectionString = connectionString;
				con.Open();
				using (SqlDataReader rd = cmd.ExecuteReader())
				{
					while (rd.Read())
					{
						ca.Add(new KategorieClass
						{
							ID = Convert.ToInt32(rd["ID"]),
							Bezeichnung = Convert.ToString(rd["Bezeichnung"])
						});
					}
				}
				con.Close();
				return ca;
			}
		}

		public List<Artikel_AusgeliehenClass> GetAllArt_Ausgeliehen()
		{

			using (SqlCommand cmd = new SqlCommand("SELECT  Art_Ausgeliehen.ID, Art_Ausgeliehen.KundeID, ArtikelID, Artikel.Bezeichnung, Abgabedatum, Leihdatum, Kunde.Name, Kunde.Vorname  " +
													"FROM Art_Ausgeliehen " +
													"INNER JOIN Kunde " +
													"ON Kunde.ID = Art_Ausgeliehen.KundeID " +
													"INNER JOIN Artikel " +
													"ON Artikel.ID = Art_Ausgeliehen.ArtikelID " +
													"ORDER BY Abgabedatum DESC;", con))
			{
				List<Artikel_AusgeliehenClass> ca = new List<Artikel_AusgeliehenClass>();
				cmd.CommandType = CommandType.Text;
				con.ConnectionString = connectionString;
				con.Open();
				using (SqlDataReader rd = cmd.ExecuteReader())
				{
					while (rd.Read())
					{
						ca.Add(new Artikel_AusgeliehenClass
						{
							ID = Convert.ToInt32(rd["ID"]),
							PickCustomerID = Convert.ToInt32(rd["KundeID"]),
							ArtikelID = Convert.ToInt32(rd["ArtikelID"]),
							Bezeichnung = Convert.ToString(rd["Bezeichnung"]),
							Abgabedatum = Convert.ToDateTime(rd["Abgabedatum"]),
							Leihdatum = Convert.ToDateTime(rd["Leihdatum"]),
							Vorname = Convert.ToString(rd["Vorname"]),
							Name = Convert.ToString(rd["Name"])
						});
					}
				}
				con.Close();
				return ca;
			}
		}
		public bool AddOrEditCustomer(int ID, string Nachname, string Vorname, string Strasse, string Hausnummer, int PLZ, string Ort, bool WurdeHinzufügenGeklickt)
		{
			try
			{
				using (con)
				{
					con.ConnectionString = connectionString;
					con.Open();
					string Query;
					if (WurdeHinzufügenGeklickt == true)
                    {
						Query = "INSERT INTO Kunde(Name, Vorname, Strasse, Hausnummer, PLZ, Ort)" +
												"VALUES (@Name, @Vorname, @Strasse, @Hausnummer, @PLZ, @Ort)";
					}
                    else
                    {
						Query = "UPDATE Kunde SET Name = @Name, Vorname = @Vorname, Strasse = @Strasse, Hausnummer = @Hausnummer, PLZ = @PLZ, Ort = @Ort " +
								"WHERE ID = @ID";
					}
					var query = Query;
					SqlCommand cmd = new SqlCommand(query, con);
					cmd.Parameters.AddWithValue("@Name", Nachname);
					cmd.Parameters.AddWithValue("@Vorname", Vorname);
					cmd.Parameters.AddWithValue("@Strasse", Strasse);
					cmd.Parameters.AddWithValue("@Hausnummer", Hausnummer);
					cmd.Parameters.AddWithValue("@PLZ", PLZ);
					cmd.Parameters.AddWithValue("@Ort", Ort);
					cmd.Parameters.AddWithValue("@ID", ID);
                    //cmd.Prepare
                    cmd.ExecuteNonQuery();
                    //con.Close();
                }

				return true;
			}
			catch (Exception ex)
			{
				return false;
				throw ex;
			}
			finally
			{
				con.Close();
			}
		}
		public bool DeleteCustomer(int ID)
		{
			try
			{
				using (con)
				{
					con.ConnectionString = connectionString;
					con.Open();
					var query = "DELETE FROM Kunde WHERE ID = @ID; " +
						"UPDATE Kunde SET Vorname = " +
						"( " +
						"SELECT Vorname FROM Kunde WHERE ID = " +
							"( " +
								"SELECT MIN(ID) FROM Kunde " +
							")" +
						") " +
						"WHERE ID = " +
							"( " +
								"SELECT MIN(ID) FROM Kunde " +
							") " +
						"DELETE FROM KundeArchiv WHERE ID = " +
						"( " +
							"SELECT MAX(ID) FROM KundeArchiv WHERE KundeID = " +
							"( " +
								"SELECT MIN(ID) FROM Kunde " +
							") " +
						")";
					SqlCommand cmd = new SqlCommand(query, con);
					cmd.Parameters.AddWithValue("@ID", ID);
					//cmd.Prepare
					cmd.ExecuteNonQuery();
					//con.Close();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
				throw ex;
			}
			finally
			{
				con.Close();
			}
		}
		public DataView ShowColumnWhichShouldBeDeleted (int ID, string Tabellenname)
        {
			try
			{
				using (con)
				{
					DataTable dt = new DataTable();
					con.ConnectionString = connectionString;
					con.Open();
					adapter.SelectCommand = new SqlCommand("SELECT * FROM " + Tabellenname + " WHERE ID = " + ID, con);
					adapter.Fill(dt);
					return dt.DefaultView;
				}
			}
			catch (Exception e)
			{
				con.Close();
				throw e;
			}
			finally
			{
				con.Close();
			}
		}
		public DataView PickCategory(string Spaltenname, string Spaltenname1, string Spaltenname2, string Spaltenname3, string Spaltenname4, string Tabellenname)
        {
            //Die Tabelle von Kategorie /bzw nur Kategorienamen ausgeben
            try
            {
                using (con)
                {
					DataTable dt = new DataTable();
					con.ConnectionString = connectionString;
					con.Open();
					adapter.SelectCommand = new SqlCommand("SELECT " + Spaltenname + Spaltenname1 + Spaltenname2 + Spaltenname3 + Spaltenname4 + " FROM " + Tabellenname, con);
					adapter.Fill(dt);
					return dt.DefaultView;
				}
            }
			catch (Exception e)
			{
				con.Close();
				throw e;
			}
			finally
			{
				con.Close();
			}
		}
		public bool AddCategory (string Bezeichnung)
        {
			//Prüfen, ob diese Kategorie schon vorhanden ist fehlt!
			try
			{
				using (con)
				{
					con.ConnectionString = connectionString;
					con.Open();
					var query = "INSERT INTO Kategorie(Bezeichnung)" +
										"VALUES (@Bezeichnung)";
					SqlCommand cmd = new SqlCommand(query, con);
					cmd.Parameters.AddWithValue("@Bezeichnung", Bezeichnung);
					//cmd.Prepare
					cmd.ExecuteNonQuery();
					con.Close();
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
				throw ex;
			}
			finally
			{
				con.Close();
			}
		}
		public DataView PickArtikelID()
		{
			//Die Tabelle von Kategorie /bzw nur Kategorienamen ausgeben
			try
			{
				using (con)
				{
					DataTable dt = new DataTable();
					con.ConnectionString = connectionString;
					con.Open();
					adapter.SelectCommand = new SqlCommand("SELECT Artikel.ID, Artikel.Bezeichnung AS Artikel, Kategorie.Bezeichnung AS Kategorie " +
															"FROM Artikel " +
															"INNER JOIN Kategorie " +
															"ON Kategorie.ID = Artikel.KategorieID " +
															"ORDER BY Artikel.ID ASC", con);
					adapter.Fill(dt);
					return dt.DefaultView;
				}
			}
			catch (Exception e)
			{
				con.Close();
				throw e;
			}
			finally
			{
				con.Close();
			}
		}

		
	}
}
