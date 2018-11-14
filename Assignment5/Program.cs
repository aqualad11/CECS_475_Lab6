using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5
{
    public class Program
    {

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World");
            
            BusinessLogic bl = new BusinessLogic();
            
            
            bool cont = true;

            while(cont)
            {
                Console.WriteLine("1. Teacher");
                Console.WriteLine("2. Course");
                Console.WriteLine("3. Quit");

                string select = Console.ReadLine();

                switch(select)
                {
                    case "1":
                        bool teach = true;
                        while (teach)
                        {
                            Console.WriteLine("1. Create");
                            Console.WriteLine("2. Update by name");
                            Console.WriteLine("3. Update by ID");
                            Console.WriteLine("4. Delete");
                            Console.WriteLine("5. Display all courses for a teacher");
                            Console.WriteLine("6. Display all teachers");
                            Console.WriteLine("7. Exit to main menu");

                            string selectTeach = Console.ReadLine();

                            switch(selectTeach)
                            {
                                case "1"://Create
                                    CreateTeacher(bl);
                                    break;
                                case "2"://Update by name
                                    Teacher t1 = getTeacherByName(bl);
                                    UpdateTeacher(bl,t1);
                                    break;
                                case "3"://Update by ID
                                    Teacher t2 = getTeacherByID(bl);
                                    UpdateTeacher(bl, t2);
                                    break;
                                case "4"://Delete
                                    DeleteTeacher(bl);
                                    break;
                                case "5"://Display all courses for a teacher
                                    Console.WriteLine("Enter Teacher ID");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    IEnumerable<Course> c = bl.GetCoursesByTeacher(id);
                                    foreach (var a in c)
                                    {
                                        Console.WriteLine(a.CourseName);
                                    }
                                    break;
                                case "6"://Display all teachers
                                    DisplayAllTeachers(bl);
                                    break;
                                case "7"://Exit to main menu
                                    teach = false;
                                    break;
                            }
                        }
                        break;
                    case "2":
                        bool stand = true;
                        while (stand)
                        {
                            Console.WriteLine("1. Create");
                            Console.WriteLine("2. Update by ID");
                            Console.WriteLine("3. Delete");
                            Console.WriteLine("4. Display all Courses");
                            Console.WriteLine("5. Exit to main menu");

                            string selectTeach = Console.ReadLine();

                            switch (selectTeach)
                            {
                                case "1"://Create
                                    CreateCourse(bl);
                                    break;
                                case "2"://update by ID
                                    Course cid = getCourseByID(bl);
                                    UpdateCourse(bl, cid);
                                    break;
                                case "3"://Delete
                                    DeleteCourse(bl);
                                    break;
                                case "4"://Display all teachers
                                    DisplayAllCourses(bl);
                                    break;
                                case "5"://Exit to main menu
                                    stand = false;
                                    break;
                            }
                        }
                        break;
                    case "3":
                        cont = false;
                        break;
                }


            }
        }

        #region Teacher
        //gets teacher by ID
        public static Teacher getTeacherByID(BusinessLogic bl)
        {
            Console.WriteLine("Enter Teacher ID");
            int teachID = Convert.ToInt32(Console.ReadLine());

            return bl.GetTeacherByID(teachID);
        }

        //gets teacher by name
        public static Teacher getTeacherByName(BusinessLogic bl)
        {
            Console.WriteLine("Enter Teacher Name");
            string teachName = Console.ReadLine();

            return bl.GetTeacherByName(teachName);
        }

        //creates and adds teacher to DB
        public static void CreateTeacher(BusinessLogic bl)
        {
            Teacher t = new Teacher();

            Console.WriteLine("Enter Name:");
            t.TeacherName = Console.ReadLine();
            Console.WriteLine("Enter StandardID");
            t.StandardId = Convert.ToInt32(Console.ReadLine());
            
            bl.AddTeacher(t);
        }

        //get's teacher by ID then allows user to update teacher's attributes
        public static void UpdateTeacher(BusinessLogic bl, Teacher t)
        {
            Console.WriteLine("Teacher Name: " + t.TeacherName);
            Console.WriteLine("Teacher ID: " + t.TeacherId);
            Console.WriteLine("Standard ID: " + t.StandardId);

            bool cont = true;
            while(cont)
            {
                Console.WriteLine("Would you like to update:");
                Console.WriteLine("1. Teacher Name");
                Console.WriteLine("2. Standard ID");
                Console.WriteLine("3. Done updating");

                string select = Console.ReadLine();

                switch(select)
                {
                    case "1":
                        Console.WriteLine("Enter updated name");
                        t.TeacherName = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Enter updated standard ID");
                        t.StandardId = Convert.ToInt32(Console.ReadLine());
                        break;
                    case "3":
                        cont = false;
                        break;
                }

                
            }
            Teacher newTeach = t;
            bl.UpdateTeacher(newTeach);
        }

        //gets teacher by ID then deletes teacher
        public static void DeleteTeacher(BusinessLogic bl)
        {
            Console.WriteLine("Enter Teacher ID of teacher you wish to delete");
            int teachID = Convert.ToInt32(Console.ReadLine());

            Teacher temp = bl.GetTeacherByID(teachID);

            bl.RemoveTeacher(temp);
        }

        //displays all teachers
        public static void DisplayAllTeachers(BusinessLogic bl)
        {
            foreach(var a in bl.GetAllTeachers())
            {
                Console.WriteLine(a.TeacherName + " " + a.TeacherId + " " + a.StandardId);
            }
        }
        #endregion

        #region Course
        public static void CreateCourse(BusinessLogic bl)
        {
            Course c = new Course();

            Console.WriteLine("Enter Name:");
            string name = Console.ReadLine();
            c.CourseName = name;
            
            bl.AddCourse(c);
            
        }

        //gets Course by ID
        public static Course getCourseByID(BusinessLogic bl)
        {
            Console.WriteLine("Enter Course ID");
            int courseID = Convert.ToInt32(Console.ReadLine());

            return bl.GetCourseByID(courseID);
        }

        /// <summary>
        /// updates current Course's name
        /// </summary>
        /// <param name="bl"> business logic</param>
        /// <param name="c"> Course to be updated</param>
        public static void UpdateCourse(BusinessLogic bl, Course c)
        {
            
            Console.WriteLine("Course Name: " + c.CourseName);
            Console.WriteLine("Course ID: " + c.CourseId);
            
            Console.WriteLine("Enter updated Course name");
            c.CourseName = Console.ReadLine();
            Console.WriteLine("Enter Teacher ID");
            c.TeacherId = Convert.ToInt32(Console.ReadLine());

            bl.UpdateCourse(c);

        }

        public static void DeleteCourse(BusinessLogic bl)
        {
            Console.WriteLine("Enter CourseID");
            int id = Convert.ToInt32(Console.ReadLine());
            Course c = bl.GetCourseByID(id);
            bl.RemoveCourse(c);
        }

        public static void DisplayAllCourses(BusinessLogic bl)
        {
            foreach (var a in bl.GetAllCourses())
            {
                Console.WriteLine(a.CourseName + " " + a.CourseId + "TeacherID" + a.TeacherId);
            }
        }
        #endregion

    }



}
