using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CppKnacker
{
    static class CompilerManager
    {
        //EZ AZ SVN TÉMA BIZTOS NAGYON KIRÁLY
        static string m_CompilerPath;
        static Process m_CompilerExe = new Process();
        static public void SetupCompilerPath( string compilerpath )
        {
            m_CompilerPath = compilerpath;
        }
        // a source nodeokból fordít
        static public bool Compile()
        {
            if (m_CompilerPath == null)
                return false;
            // forrásfájlok lekérdezése és paraméter összeállítása
            IntelNodeSource[] sourcefiles = ProjectManager.SourceNodes;
            string compiler_parameters = "";
            foreach (IntelNodeSource source in sourcefiles)
                compiler_parameters += " "+source.Text;
            if (compiler_parameters.Length == 0)
                return false;
            // kimeneti könyvtár létrehozása
            string exefile = ProjectManager.ProjectPath+@"\bin\"+System.IO.Path.GetFileNameWithoutExtension(ProjectManager.ProjectFile)+".exe";
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(exefile));
            // fordító beállítása
            compiler_parameters += " -Wall -o "+exefile;
            m_CompilerExe.StartInfo.FileName = m_CompilerPath;
            m_CompilerExe.StartInfo.Arguments = compiler_parameters;
            m_CompilerExe.StartInfo.WorkingDirectory = ProjectManager.ProjectPath;
            m_CompilerExe.StartInfo.RedirectStandardOutput = true;
            m_CompilerExe.StartInfo.RedirectStandardError = true;
            m_CompilerExe.StartInfo.UseShellExecute = false;
            // exe törlése
            System.IO.File.Delete(exefile);
            // fordítás
            m_CompilerExe.Start();
            ProjectManager.Output = "Fordítás megkezdése...";
            ProjectManager.Output = m_CompilerExe.StandardOutput.ReadToEnd();
            ProjectManager.Output = m_CompilerExe.StandardError.ReadToEnd();
            m_CompilerExe.WaitForExit();
            // exe létezésének tesztelése
            if (!System.IO.File.Exists(exefile)) 
            {
                ProjectManager.Output = "Sikertelen fordítás.";
                return false;
            }
            ProjectManager.Output = "A fordítás sikeresen befejezõdött.";
            return true;
        }

        public static void Run()
        {
            string exefile = ProjectManager.ProjectPath+@"\bin\"+System.IO.Path.GetFileNameWithoutExtension(ProjectManager.ProjectFile)+".exe";

            if (!System.IO.File.Exists(exefile))
                return;
            Process exe = new Process();

            exe.StartInfo.FileName = exefile;
            exe.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(exefile);
            exe.StartInfo.UseShellExecute = true;
            exe.Start();
            exe.WaitForExit();

        }
    }
}
