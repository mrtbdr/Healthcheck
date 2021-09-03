        using System;
        using System.Threading;
        using System.Threading.Tasks;
        using Microsoft.Extensions.Diagnostics.HealthChecks;
        using System.Data.SqlClient;
        using System.Data.Common;
        using System.Data.OracleClient;
        using MySql.Data.MySqlClient;
        using Npgsql;

        namespace LoadBalancer
        {
            public class SqlConnectionHealthCheck
            {
                public static string ConnectDB(string nameOfDataBase,string cs)
                {
            
                    switch(nameOfDataBase) //According The Name Of The DB
                    {
                        case "PostgreSQL":
                    
                            try
                                {
                                    using var connectionPostgre = new NpgsqlConnection(cs); //Connection
                                    connectionPostgre.Open(); //Open Connection

                                    try
                                    {
                                        var query = "SELECT 1"; //Query Select 1

                                        using var cmd = new NpgsqlCommand(query, connectionPostgre); //Command

                                        return "Healthy"; //If command is true returns Healthy
                                    }
                                    catch(NpgsqlException) //Otherwise returns Unhealthy
                                    {
                                        return "Unhealthy";
                                    }
                                }
                                catch(NpgsqlException) //In case of the user writes the one of the infos as wrong
                                {
                                    throw new NpgsqlException("Wrong Host name or Password or ID or DB Name");
                                }


                                break;
                
                            case "OracleSQL":

                                    try
                                    {
                                    using var connectionOracle = new OracleConnection(cs);
                                    connectionOracle.Open();

                                    try
                                    {
                                        var query = "SELECT 1 FROM DUAL";
                                        using var cmd = new OracleCommand(query, connectionOracle);

                                        return "Healthy";
                                    }
                                    catch(OracleException)
                                    {
                                        return "Unhealthy";
                                    }
                                    }
                                catch(OracleException)
                                {
                                    throw new Exception("Wrong Host name or Password or ID or DB Name");
                                }
                            case "MySQL":

                        try
                        {
                            using var connectionMySQL = new MySqlConnection(cs);
                            connectionMySQL.Open();

                            try
                            {
                                var query = "SELECT 1";
                                using var cmd = new MySqlCommand(query, connectionMySQL);

                                return "Healthy";
                            }
                            catch (MySqlException)
                            {
                                return "Unhealthy";
                            }
                        }
                        catch (MySqlException)
                        {
                            throw new Exception("Wrong Host name or Password or ID or DB Name");
                        }






                        break;
                        default:
                            throw new Exception("Invalid DB Name!");
                            break;
                  
                    }
                    return "Unhealthy";

                }
            }
        }
