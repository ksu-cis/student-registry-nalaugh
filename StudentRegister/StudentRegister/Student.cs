using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StudentRegister
{
    /// <summary>
    /// A class representing a student
    /// </summary>
    public class Student : INotifyPropertyChanged
    {
        private List<CourseResult> courseHistory;

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string first;
        private string last;

        /// <summary>
        /// Gets and sets the first name
        /// </summary>
        public string First
        {
            get { return first; }
            set
            {
                first = value;
               /// NotifyPropertyChanged("First");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("First"));
            }
        }
        /// <summary>
        /// Gets and sets the last name
        /// </summary>
        public string Last
        {
            get { return last; }
            set
            {
                last = value;
               // NotifyPropertyChanged("Last");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Last"));
            }
        }
        /// <summary>
        /// Gets the course history
        /// </summary>
        public CourseResult[] CourseHistory
        {
            get
            {
                return courseHistory.ToArray();
            }
        }

        /// <summary>
        /// Gets the student's GPA
        /// </summary>
        public double GPA
        {
            get
            {
                double credits = 0;
                double hours = 0;
                courseHistory.ForEach(course =>
                {
                    switch (course.Grade)
                    {
                        case Grade.A:
                            credits += 4 * course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.B:
                            credits += 3 * course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.C:
                            credits += 2 * course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.D:
                            credits += course.CreditHours;
                            hours += course.CreditHours;
                            break;
                        case Grade.F:
                        case Grade.XF:
                            hours += course.CreditHours;
                            break;
                    }
                });
                return credits / hours;
            }
        }

        /// <summary>
        /// Constructs a new student instance
        /// </summary>
        /// <param name="first">The first name</param>
        /// <param name="last">The last name</param>
        public Student(string first, string last)
        {
            First = first;
            Last = last;
            courseHistory = new List<CourseResult>();
        }

        /// <summary>
        /// overided the to sting method
        /// </summary>
        /// <returns>the prpper informaiton for the window to display</returns>
        public override string ToString()
        {
                return $"{Last}, {First} ({GPA})";
        }


        public void CourseComplete(string name, uint hours, Grade grade, string semester)
        {
            CourseResult cr =new CourseResult(name, hours, grade, semester);
            courseHistory.Add(cr);
            NotifyPropertyChanged("GPA");
        }

    }
}
