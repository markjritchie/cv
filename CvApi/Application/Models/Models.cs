namespace CvApi;
    public class Award
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Awarder { get; set; }
        public string Summary { get; set; }
    }

    public class Basics
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public Location Location { get; set; }
        public List<Profile> Profiles { get; set; }
    }

    public class Certificate
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Issuer { get; set; }
        public string Url { get; set; }
    }

    public class Education
    {
        public string Institution { get; set; }
        public string Url { get; set; }
        public string Area { get; set; }
        public string StudyType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Score { get; set; }
        public List<string> Courses { get; set; }
    }

    public class Interest
    {
        public string Name { get; set; }
        public List<string> Keywords { get; set; }
    }

    public class Language
    {
        public string Name { get; set; }
        public string Fluency { get; set; }
    }

    public class Location
    {
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
    }

    public class Profile
    {
        public string Network { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
    }

    public class Project
    {
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public List<string> Highlights { get; set; }
        public string Url { get; set; }
    }

    public class Publication
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string ReleaseDate { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
    }

    public class Reference
    {
        public string Name { get; set; }
        public string Contents { get; set; }
    }

    public class CurriculumVitae
    {
        public Basics Basics { get; set; }
        public Work[] Work { get; set; }
        public List<Volunteer> Volunteer { get; set; }
        public List<Education> Education { get; set; }
        public List<Award> Awards { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Publication> Publications { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Language> Languages { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Reference> References { get; set; }
        public List<Project> Projects { get; set; }
    }

    public class Skill
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public List<string> Keywords { get; set; }
    }

    public class Volunteer
    {
        public string Organization { get; set; }
        public string Position { get; set; }
        public string Url { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Summary { get; set; }
        public List<string> Highlights { get; set; }
    }

    public class Work
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Url { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Summary { get; set; }
        public List<string> Highlights { get; set; }
    }
