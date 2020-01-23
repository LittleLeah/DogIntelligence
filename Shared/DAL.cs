using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DogIntelligence.Models;
using System.Configuration;

namespace DogIntelligence.Shared
{
    public class DAL
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dogDb"].ConnectionString);
        SqlCommand cmd;

        public List<Dogintelligence> GetDogIntelligences()
        {
            List<Dogintelligence> di = new List<Dogintelligence>();
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "Select * from DogIntelligence";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Dogintelligence dogI = new Dogintelligence();
                    dogI.breed = reader[0].ToString();
                    dogI.classification = reader[1].ToString();
                    dogI.obey = int.Parse(reader[2].ToString());
                    dogI.lowReps = int.Parse(reader[3].ToString());
                    dogI.highReps = int.Parse(reader[4].ToString());
                    di.Add(dogI);
                }
                reader.Close();

            }
            catch (Exception e)
            {

                Console.Write(e);
            }
            cmd.Connection.Close();
            return di;
        }

        public List<BreedInfo> GetBreedInfo()
        {
            List<BreedInfo> bi = new List<BreedInfo>();
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "Select * from AKCBreedInfo";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BreedInfo bInf = new BreedInfo();
                    bInf.breed = reader[0].ToString();
                    bInf.lowInches = int.Parse(reader[1].ToString());
                    bInf.highInches = int.Parse(reader[2].ToString());
                    bInf.lowLbs = int.Parse(reader[3].ToString());
                    bInf.highLbs = int.Parse(reader[4].ToString());
                    bi.Add(bInf);
                }
                reader.Close();
            }
            catch (Exception e)
            {

                Console.Write(e);
            }
            cmd.Connection.Close();
            return bi;
        }

        public List<Dog> SendDogs()
        {
            List<BreedInfo> bi = GetBreedInfo();
            List<Dogintelligence> di = GetDogIntelligences();
            List<Dog> dogs = new List<Dog>();

            foreach (Dogintelligence item in di)
            {
                Dog dog = new Dog();
                dog.breed = item.breed;
                dog.classification = item.classification;
                if (item.obey < 29)
                {
                    dog.obey = 0;
                }
                else dog.obey = item.obey;
                dog.lowRep = item.lowReps;
                dog.highRep = item.highReps;
                BreedInfo bInf = bi.FirstOrDefault(x => x.breed == item.breed) as BreedInfo;
                if (bInf != null)
                {
                    dog.medianInch = (bInf.lowInches + bInf.highInches) / 2;
                    dog.medianLbs = (bInf.lowLbs + bInf.highLbs) / 2;
                }
                dogs.Add(dog);
            }
            return dogs;
        }
    }
}