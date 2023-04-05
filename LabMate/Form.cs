using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Reflection;

namespace LabMate
{
    internal class Form
    {

        // ***-------- NEW REQUEST --------***//
        public static bool NewRequestToDb(Request newRequest)
        {
            bool requestSentToDb = false;
            SqlConnection dbConnection = NewDatabaseConnection();
            if (CheckFormFilled(newRequest) == true && CheckLabRefNoExists(newRequest.LabRefNo, dbConnection, true) == false)
            {
                SaveNewRequestToDb(newRequest, dbConnection);
                MessageBox.Show($"Request {newRequest.LabRefNo} entered.");
                requestSentToDb = true;
            }

            CloseDatabaseConnection(dbConnection);

            return requestSentToDb;
        }

        // ***-------- CHECK DB FOR REQUEST ***--------***//
        public static bool CheckDbForRequest(string labRefNo)
        {
            SqlConnection dbConnection = NewDatabaseConnection();
            bool labRefNoExists = CheckLabRefNoExists(labRefNo, dbConnection);
            CloseDatabaseConnection(dbConnection);
            return labRefNoExists;
        }

        // ***-------- UPDATE FORM WITH REQUEST FROM DB ***--------***//
        public static Request GetExistingRequestFromDb(string labRefNo)
        {
            SqlConnection dbConnection = NewDatabaseConnection();

            Request existingRequest = new Request();

            using (SqlCommand dbCommand = new SqlCommand("SELECT labRefNo, hcNo, hospitalNo, surname, forename, sex, dob, address1, address2, address3, postCode, hospital, consGp, ward, specialty, specimenType, specimenDate, recdDate, flagCode, urgency FROM requests WHERE labRefNo = @labRefNo", dbConnection))
            {
                dbCommand.Parameters.AddWithValue("@labRefNo", labRefNo);

                using (SqlDataReader reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Loop through the properties of the Request
                        foreach (PropertyInfo property in typeof(Request).GetProperties())
                        {
                            // Set the value of each property based on the data in the SQL Server table
                            int ordinal = reader.GetOrdinal(property.Name);
                            Console.WriteLine("Ordinal: " + ordinal);
                            var value = reader.GetValue(ordinal);
                            Console.WriteLine("Value: " + value);
                            property.SetValue(existingRequest, value);
                        }
                        Console.WriteLine($"Retrieved request for labRefNo {existingRequest.LabRefNo}");
                    }
                }

            }

            CloseDatabaseConnection(dbConnection);

            return existingRequest;
        }

        // ***-------- AMEND REQUEST --------***//
        public static void AmendedRequestToDb(Request oldRequest, Request amendedRequest)
        {
            SqlConnection dbConnection = NewDatabaseConnection();

            if (CheckFormFilled(amendedRequest) == true && CheckLabRefNoExists(amendedRequest.LabRefNo, dbConnection) == true)
            {
                UpdateRequestInDb(oldRequest, amendedRequest, dbConnection);
                MessageBox.Show($"Request {amendedRequest.LabRefNo} amended.");
            }

            CloseDatabaseConnection(dbConnection);
        }

        // ***-------- FIND REQUEST --------***//
        public static List<Request> FindRequestsFromDb(Request searchRequest)
        {
            SqlConnection dbConnection = NewDatabaseConnection();
            List<Request> resultsList = SearchDbForRequest(searchRequest, dbConnection);
            CloseDatabaseConnection(dbConnection);

            return resultsList;
        }

        // ***-------- AUTHENTICATE LOGIN --------***//
        public static bool AuthenticateLogin(User loginCredentials)
        {
            bool userMatches = CheckDbForUsernameAndPassword(loginCredentials.Username, loginCredentials.Password);
            if (userMatches == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // -------------------------------- Open Database Connection -------------------------------- //
        private static SqlConnection NewDatabaseConnection()
        {
            Console.WriteLine("Attempting to access database...");
            SqlConnection dbConnection = new SqlConnection(@"Data Source=LAPTOP-OEQV0P7R\SQLEXPRESS; Initial Catalog=LabMateDB; Integrated Security=True;");
            try
            {
                if (dbConnection.State == System.Data.ConnectionState.Closed)
                    dbConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dbConnection;
        }

        // -------------------------------- Close Database Connection -------------------------------- //
        private static void CloseDatabaseConnection(SqlConnection dbConnection)
        {
            dbConnection.Close();
        }


        // ---------------------- UPDATE REQUEST DATA IN DATABASE ---------------------- //
        private static void UpdateRequestInDb(Request oldRequest, Request amendedRequest, SqlConnection dbConnection)
        {

            Console.WriteLine($"Attempting to update request {oldRequest.LabRefNo}...");

            foreach (var property in amendedRequest.GetType().GetProperties())
            {
                Console.WriteLine("Property: " + property.Name);
                Console.WriteLine("Old Value: " + property.GetValue(oldRequest) as string);
                Console.WriteLine("New Value: " + property.GetValue(amendedRequest) as string);

                if (property.GetValue(amendedRequest) as string != property.GetValue(oldRequest) as string)
                {
                    string sqlColumn = property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1);

                    string dbQuery = $"UPDATE requests SET {sqlColumn} = @newValue";

                    string sqlOldValue = property.GetValue(oldRequest) as string;

                    string sqlNewValue = property.GetValue(amendedRequest) as string;

                    SqlCommand dbCommand = new SqlCommand(dbQuery, dbConnection);

                    dbCommand.CommandType = System.Data.CommandType.Text;

                    dbCommand.Parameters.AddWithValue("@newValue", sqlNewValue);

                    Console.WriteLine("Executing statement...");

                    int rowsAffected = dbCommand.ExecuteNonQuery();

                    Console.WriteLine("Executed." + $"{rowsAffected} rows affected.");
                    Console.WriteLine($"{property.Name} changed from {sqlOldValue} to {sqlNewValue}");

                }
            }
        }

        // ---------------------- SAVE NEW REQUEST DATA TO DATABASE ---------------------- //
        private static void SaveNewRequestToDb(Request newRequest, SqlConnection dbConnection)
        {
            string dbQuery = "INSERT INTO requests (labRefNo, hcNo, hospitalNo, surname, forename, sex, dob, address1, address2, address3, postCode, hospital, consGp, ward, specialty, specimenType, specimenDate, recdDate, flagCode, urgency) VALUES (@labRefNo, @hcNo, @hospitalNo, @surname, @forename, @sex, @dob, @address1, @address2, @address3, @postCode, @hospital, @consGp, @ward, @specialty, @specimenType, @specimenDate, @recdDate, @flagCode, @urgency)";
            SqlCommand dbCommand = new SqlCommand(dbQuery, dbConnection);
            dbCommand.CommandType = System.Data.CommandType.Text;

            foreach (var property in newRequest.GetType().GetProperties())
            {
                string sqlParameter = $"@{property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1)}";
                string sqlValue = property.GetValue(newRequest) as string;
                dbCommand.Parameters.AddWithValue(sqlParameter, sqlValue);

                /**/
                Console.WriteLine("Property: " + property.Name);
                Console.WriteLine("Parameter: " + sqlParameter);
                Console.WriteLine("Value: " + sqlValue);
                /**/
            }

            /**/
            Console.WriteLine("Executing statement...");

            int rowsAffected = dbCommand.ExecuteNonQuery();

            /**/
            Console.WriteLine("Executed." + $"{rowsAffected} rows affected.");

        }

        // ---------------------- CHECK INPUTS FROM REQUEST FORM ---------------------- //
        private static bool CheckFormFilled(Request request)
        {
            bool formFilled = true;

            foreach (var property in request.GetType().GetProperties())
            {
                string input = property.GetValue(request) as string;

                bool isRequired = request.IsPropertyRequired(property.Name);

                if (isRequired)
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine($"Required {property.Name} has no input.");
                        MessageBox.Show($"{property.Name} is a required field");
                        formFilled = false;
                    }
                }

            }

            return formFilled;
        }

        // -- CHECK IF LAB REF NO EXISTS IN DB -- //
        private static bool CheckLabRefNoExists(string labRefNo, SqlConnection dbConnection, bool errorMsg = false)
        {
            bool labRefNoExists = false;

            string dbQuery = "SELECT COUNT(*) FROM requests WHERE labRefNo = @labRefNo";
            SqlCommand dbCommand = new SqlCommand(dbQuery, dbConnection);
            dbCommand.CommandType = System.Data.CommandType.Text;
            dbCommand.Parameters.AddWithValue("@labRefNo", labRefNo);
            int count = (int)dbCommand.ExecuteScalar();
            if (count > 0)
            {
                labRefNoExists = true;
                Console.WriteLine($"Lab Ref No: {labRefNo} already exists");
                if (errorMsg == true)
                {
                    MessageBox.Show($"Lab Ref No: {labRefNo} already exists");
                }
            }

            return labRefNoExists;

        }

        // -- SEARCH DB FOR REQUESTS THAT MATCH FIELDS -- //
        private static List<Request> SearchDbForRequest(Request searchRequest, SqlConnection dbConnection)
        {
            string dbQuery = "SELECT * FROM requests WHERE";

            SqlCommand dbCommand = new SqlCommand(dbQuery, dbConnection);
            dbCommand.CommandType = System.Data.CommandType.Text;

            bool firstIteration = true;
            foreach (var property in searchRequest.GetType().GetProperties())
            {
                string sqlColumn = $"{property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1)}";
                string sqlParameter = $"@{property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1)}";
                string sqlValue = property.GetValue(searchRequest) as string;

                if (String.IsNullOrEmpty(sqlValue))
                {
                    // do nothing
                }
                else if (firstIteration == true)
                {
                    dbQuery += $" LOWER({sqlColumn}) = LOWER({sqlParameter})";
                }
                else if (firstIteration == false){
                    dbQuery += $" AND LOWER({sqlColumn}) = LOWER({sqlParameter})"; // After first iteration of loop, add 'OR' keyword to query
                }                

                Console.WriteLine("Column: " + sqlColumn);
                Console.WriteLine("Parameter " + sqlParameter);
                Console.WriteLine("Value " + sqlValue);
                Console.WriteLine("New Query = " + dbQuery);

                if (String.IsNullOrEmpty(sqlValue))
                {
                    // do nothing
                }
                else
                {
                    dbCommand.Parameters.AddWithValue(sqlParameter, sqlValue);

                    firstIteration = false;
                }
            }

            dbCommand.CommandText = dbQuery;

            Console.WriteLine("Final query = " + dbCommand.CommandText);

            List<Request> resultsList = new List<Request>();

            if (firstIteration == false)
            {
                using (SqlDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Request foundRequest = new Request();

                        foreach (var property in foundRequest.GetType().GetProperties())
                        {
                            string propertyValue = reader.GetString(reader.GetOrdinal(property.Name));
                            Console.WriteLine($"Property: {property.Name} Value: {propertyValue}");
                            property.SetValue(foundRequest, propertyValue);
                        }

                        resultsList.Add(foundRequest);
                        Console.WriteLine($"Request {foundRequest.LabRefNo} added to list.");
                    }
                }
            }

            return resultsList;

        }

        // -- CHECK USERNAME & PASSWORD MATCH ON DB -- //
        private static bool CheckDbForUsernameAndPassword(string username, string password)
        {
            SqlConnection dbConnection = NewDatabaseConnection();

            string dbQuery = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
            SqlCommand dbCommand = new SqlCommand(dbQuery, dbConnection);
            dbCommand.CommandType = System.Data.CommandType.Text;
            dbCommand.Parameters.AddWithValue("@username", username);
            dbCommand.Parameters.AddWithValue("@password", password);

            int count = 0;

            try
            {
                count = (int)dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }      
        }
    }
}
