namespace TicketApp.Api.Models
{
    public class ProjectStore
    {
        private int id;
        private readonly List<Project> projects = [];
        public int GetNextId() => Interlocked.Increment(ref id);
        public void AddProject(Project project)
        {
            projects.Add(project);
        }
        public Project GetProject(int id)
        {
            var proj = projects.FirstOrDefault(t => t.Id == id);
            if (proj is not null)
                return proj;
            else throw new ArgumentException("Project does not exist.");
        }
        public List<Project> GetProjects()
        {
            return projects.ToList();
        }
        public bool ExistProject(int id)
        {
            if (projects.FirstOrDefault(t => t.Id == id) is not null) return true;
            else return false;
        }
    }
}