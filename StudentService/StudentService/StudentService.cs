using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StudentService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StudentService" in both code and config file together.
    public class StudentService : IStudentService
    {
        public string GetStudentInfo(int studentId)
        {
            string studentName = string.Empty;
            switch (studentId)
            {
                case 1:
                    {
                        studentName = "Ankit";
                        break;
                    }
                case 2:
                    {
                        studentName = "Viraj";
                        break;
                    }
                default:
                    {
                        studentName = "Sachin";
                        break;
                    }
            }
            return studentName;
        }
    }
}
