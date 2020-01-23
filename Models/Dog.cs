using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogIntelligence.Models
{
    [Serializable]
    public class Dog
    {
        public string breed;
        public string classification;
        public int obey;
        public int lowRep;
        public int highRep;
        public int medianInch;
        public int medianLbs;
    }
}