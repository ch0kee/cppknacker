using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;

namespace CppKnacker
{
    static class CompilerManager
    {
        //L EZ AZ SVN TÉMA BIZTOS NAGYON KIRÁLY Még beleraktam ezt a pár szót
        static string m_CompilerPath;
        static string m_CompilerParameters;
        static Process m_CompilerExe = new Process();
        static public void SetupCompiler()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(MainForm.ProgramDirectory+"options.xml");
            XmlNode CppKnackerNode = doc.ChildNodes[0];
            XmlNode CompilerNode = CppKnackerNode.ChildNodes[0];
            m_CompilerPath = CompilerNode.Attributes["Path"].Value;
            m_CompilerParameters = CompilerNode.Attributes["Parameters"].Value;
        }
        // a source nodeokból fordít
        static public bool Compile()
        {
            SetupCompiler();
            if (!System.IO.File.Exists(m_CompilerPath))
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
            compiler_parameters += m_CompilerParameters + " -o "+exefile;
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
            ProjectManager.Output.Clear();
            ProjectManager.Output.Write("Fordítás megkezdése...");
            ProjectManager.Output.Write(m_CompilerExe.StandardOutput.ReadToEnd());
            ProjectManager.Output.Write(m_CompilerExe.StandardError.ReadToEnd());
            m_CompilerExe.WaitForExit();
            // exe létezésének tesztelése
            if (!System.IO.File.Exists(exefile)) 
            {
                ProjectManager.Output.Write("Sikertelen fordítás.");
                return false;
            }
            ProjectManager.Output.Write("A fordítás sikeresen befejezõdött.");
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
