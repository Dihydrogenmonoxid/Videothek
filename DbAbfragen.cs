using System;


public class DbAbfragen
{
	public DbAbfragen()
	{
		SqlConnection con = new SqlConnection("Data Source = DESKTOP-76G58PK\\AJMYSQLSERVER;" +
											  "Initial Catalog=Videothek;" +
											  "Integrated Security=SSPI";)
		public bool AddCustomer(string Nachname, string Vorname, string Strasse, string Hausnummer, int Plz, string Ort)
	{
		try
		{
			using (con)
            {
				con.Open();
				var query = "INSERT INTO Kunde(Name, Vorname, Strasse, Hausnummer, PLZ, Ort)" +
									"VALUES (@Name, @Vorname, @Strasse, @Hausnummer, @PLZ, @Ort)";
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@Name", Nachname);
				cmd.Parameters.AddWithValue("@Vorname", Vorname);
				cmd.Parameters.AddWithValue("@Strasse", Strasse);
				cmd.Parameters.AddWithValue("@Hausnummer", Hausnummer);
				cmd.Parameters.AddWithValue("@PLZ", Plz);
				cmd.Parameters.AddWithValue("@Ort", Ort);
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
}
}
