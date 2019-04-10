using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AcceptanceTests
{
    public class ProjectFinder
    {
        public static string FindProjectDir(string projectPath)
        {
            var callingAssemblyDirectory = Directory.GetParent(new Uri(Assembly.GetCallingAssembly().CodeBase).LocalPath).FullName;
            return FindSolutionRootPath(callingAssemblyDirectory, out var solutionRoot)
                ? Path.Combine(solutionRoot, projectPath)
                : null;

        }

        private static bool FindSolutionRootPath(string callingAssemblyDirectory, out string repositoryRoot)
        {
            var pathRoot = Path.GetPathRoot(callingAssemblyDirectory);
            var candidateDirectory = callingAssemblyDirectory;
            while (candidateDirectory != pathRoot)
            {
                if (Directory.EnumerateFiles(candidateDirectory, "*.sln").Any())
                {
                    repositoryRoot = candidateDirectory;
                    return true;
                }
                candidateDirectory = Directory.GetParent(candidateDirectory).FullName;
            }
            repositoryRoot = null;
            return false;
        }

    }
}