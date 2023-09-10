using TasksManager.Model;
using TasksManager.service;

namespace TasksManager.controller{

    public class ProjectController
    {
        private ProjectService? _projectService;

        public ProjectController(ProjectService? projectService){
            _projectService = projectService;
        }

        public void UpdateProjectData(Project? project){
            _projectService?.AddProject(project);
        }

        
    }
}
