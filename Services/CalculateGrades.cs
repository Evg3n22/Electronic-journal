using System;
using System.Collections.Generic;

namespace ElectronicJournal.Services
{
    public static class CalculateGrades
    {
        private static readonly Dictionary<(double Min, double Max), (string GPA, string score)> GradeMappings = new Dictionary<(double, double), (string, string)>
{
    { (93, 101), ("5.00", "A") },
    { (89, 94), ("4.67", "A-") },
    { (86, 90), ("4.33", "B+") },
    { (82, 87), ("4.00", "B") },
    { (79, 83), ("3.67", "B-") },
    { (76, 80), ("3.33", "C+") },
    { (72, 77), ("3.00", "C") },
    { (69, 73), ("2.67", "C-") },
    { (59, 70), ("1.00", "D")  },
    { (-1, 60), ("0.00", "F") }
};

        public static string GetGPA(double grade)
        {
            foreach (var mapping in GradeMappings)
            {
                if (grade > mapping.Key.Min && grade < mapping.Key.Max)
                {
                    return mapping.Value.GPA;
                }
            }
            return "N/A"; // Якщо оцінка поза відомго діапозона
        }

        public static string GetScore(double grade)
        {
            foreach (var mapping in GradeMappings)
            {
                if (grade > mapping.Key.Min && grade < mapping.Key.Max)
                {
                    return mapping.Value.score;
                }
            }
            return "F"; // Якщо оцінка поза відомого діапозона
        }
    }
}
