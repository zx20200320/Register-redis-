using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Day18.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }

        public SexEnum Sex { get; set; }


        public int Age { get; set; }


        public StudyExperience StudyExp { get; set; }


        public string ImagePath { get; set; }


        public string Motto { get; set; }


        public string Fav { get; set; }

        public string Fav1 { get; set; }

        public string Fav2 { get; set; }

        public string Fav3 { get; set; }
    }


    public enum SexEnum
    {
        [Description("女")]
        Female = 1,

        [Description("男")]
        Male = 0
    }

    public enum StudyExperience
    {
        [Description("本科")]
        Bachelor = 0,

        [Description("大专")]
        College = 1,

        [Description("高中")]
        HighSchool = 2

    }


}