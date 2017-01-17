using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExerciseTask.Startup))]
namespace ExerciseTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
