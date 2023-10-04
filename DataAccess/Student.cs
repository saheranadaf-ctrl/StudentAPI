using System;
using System.Collections.Generic;

namespace UpdateAPI.DataAccess;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string? Gender { get; set; }

    public string? NationalIty { get; set; }

    public string? PlaceofBirth { get; set; }

    public string? StageId { get; set; }

    public string? GradeId { get; set; }

    public string? SectionId { get; set; }

    public string? Topic { get; set; }

    public string? Semester { get; set; }

    public string? Relation { get; set; }

    public double? Raisedhands { get; set; }

    public double? VisItedResources { get; set; }

    public double? AnnouncementsView { get; set; }

    public double? Discussion { get; set; }

    public string? ParentAnsweringSurvey { get; set; }

    public string? ParentschoolSatisfaction { get; set; }

    public string? StudentAbsenceDays { get; set; }

    public double? StudentMarks { get; set; }

    public string? Class { get; set; }
}
