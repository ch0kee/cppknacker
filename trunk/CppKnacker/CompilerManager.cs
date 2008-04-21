using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CppKnacker
{
    static class CompilerManager
    {
        //EZ AZ SVN T�MA BIZTOS NAGYON KIR�LY
        static string m_CompilerPath;
        static Process m_CompilerExe = new Process();
        static public void SetupCompilerPath( string compilerpath )
        {
            m_CompilerPath = compilerpath;
        }
        // a source nodeokb�l ford�t
        static public bool Compile()
        {
            if (m_CompilerPath == null)
                return false;
            // forr�sf�jlok lek�rdez�se �s param�ter �ssze�ll�t�sa
            IntelNodeSource[] sourcefiles = ProjectManager.SourceNodes;
            string compiler_parameters = "";
            foreach (IntelNodeSource source in sourcefiles)
                compiler_parameters += " "+source.Text;
            if (compiler_parameters.Length == 0)
                return false;
            // kimeneti k�nyvt�r l�trehoz�sa
            string exefile = ProjectManager.ProjectPath+@"\bin\"+System.IO.Path.GetFileNameWithoutExtension(ProjectManager.ProjectFile)+".exe";
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(exefile));
            // ford�t� be�ll�t�sa
            compiler_parameters += " -Wall -o "+exefile;
            m_CompilerExe.StartInfo.FileName = m_CompilerPath;
            m_CompilerExe.StartInfo.Arguments = compiler_parameters;
            m_CompilerExe.StartInfo.WorkingDirectory = ProjectManager.ProjectPath;
            m_CompilerExe.StartInfo.RedirectStandardOutput = true;
            m_CompilerExe.StartInfo.RedirectStandardError = true;
            m_CompilerExe.StartInfo.UseShellExecute = false;
            // exe t�rl�se
            System.IO.File.Delete(exefile);
            // ford�t�s
            m_CompilerExe.Start();
            ProjectManager.Output = "Ford�t�s megkezd�se...";
            ProjectManager.Output = m_CompilerExe.StandardOutput.ReadToEnd();
            ProjectManager.Output = m_CompilerExe.StandardError.ReadToEnd();
            m_CompilerExe.WaitForExit();
            // exe l�tez�s�nek tesztel�se
            if (!System.IO.File.Exists(exefile)) 
            {
                ProjectManager.Output = "Sikertelen ford�t�s.";
                return false;
            }
            ProjectManager.Output = "A ford�t�s sikeresen befejez�d�tt.";
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
