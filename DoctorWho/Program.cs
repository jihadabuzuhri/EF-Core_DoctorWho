using DoctorWho.Db;
using DoctorWho.Db.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Entity;

namespace DoctorWho
{
    public class Program
    {
        static void Main(string[] args)
        {
            DoctorWhoCoreDbContext dbContext= new DoctorWhoCoreDbContext();

            CallCompanionsFunction(dbContext,1);
            CallEnemiesFunction(dbContext,3);
            CallSummariseEpisodesProcedure(dbContext);
            CallEpisodesView(dbContext);

            Console.ReadLine();


        }

        public static void CallSummariseEpisodesProcedure(DoctorWhoCoreDbContext dbContext)
        {
            Console.Write("\nSummarise Episodes: \n");

            var connection = dbContext.Database.GetDbConnection();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "dbo.spSummariseEpisodes";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            for (int i = 0; i < reader.FieldCount; i++)
                            {

                                if (reader.GetFieldType(i) == typeof(int))
                                {
                                    var columnValue = reader.GetInt32(i);
                                    Console.Write($"{columnValue}  ");
                                }
                                else if (reader.GetFieldType(i) == typeof(string))
                                {
                                    var columnValue = reader.GetString(i);
                                    Console.Write($"{columnValue}  ");
                                }

                            }
                            Console.WriteLine();

                        }
                        reader.NextResult();
                    }
                }
            }
            connection.Close();

        }

        public static void CallCompanionsFunction(DoctorWhoCoreDbContext dbContext, int Id)
        {
            Console.Write("\nCompanions : ");
            var companions = dbContext.Companions.Select(c => dbContext.CallFnCompanions(Id)).FirstOrDefault();
            Console.WriteLine(companions);

        }

        public static void CallEnemiesFunction(DoctorWhoCoreDbContext dbContext, int Id)
        {
            Console.Write("\nEnemies : ");

            var enemies = dbContext.Enemies.Select(e => dbContext.CallFnEnemies(Id)).FirstOrDefault();
            Console.WriteLine(enemies);
        }

        public static void CallEpisodesView(DoctorWhoCoreDbContext dbContext)
        {
            Console.WriteLine("\nEpisodes View : ");


            var Query = dbContext.viewEpisodes.ToList();

            
            foreach(var item in Query) {

                Console.WriteLine($"EpisodeId {item.EpisodeId} :");
                Console.WriteLine(item.AuthorName);
                Console.WriteLine(item.Companions);
                Console.WriteLine(item.DoctorName);
                Console.WriteLine(item.Enemies);
                Console.WriteLine();


            };


        }
      
    }
}