using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CppKnacker
{
    abstract class IntelNode : TreeNode//intelligens fa csomópont
    {
        // node beállítása
        abstract public void SetPath(string accesspath);
    }
    // projekt
    class IntelNodeProject : IntelNode
    {
        public IntelNodeProject(string projectfilepath)
        {
            SetPath(projectfilepath);
        }
        public IntelNodeProject()
        {}
        override public void SetPath(string projectpath)
        {
            Text = projectpath.Substring(projectpath.LastIndexOf(@"\") + 1);
            Tag = projectpath;
        }
        public void AddFile(string filename)
        {
            IntelNodeFile new_file;
            // lepucoljuk róla a könyvtárat
            filename = Path.GetFileName(filename);
            if (Tools.IsSourceFile(filename))
                Nodes.Add(new_file = new IntelNodeSource(filename));
            else if (Tools.IsHeaderFile(filename))
                Nodes.Add(new_file = new IntelNodeHeader(filename));
            else
                throw new KException(KException.ExceptionType.UnknownFileType);
            // ha nem létezik létrehozzuk
            if (!System.IO.File.Exists(ProjectManager.GetFullPath(new_file)))
                using (System.IO.File.Create(ProjectManager.GetFullPath(new_file))) { ;};
        }
    }
    // fájl
    class IntelNodeFile : IntelNode
    {
        public IntelNodeFile(string filename)
        {
            SetPath(filename);
        }
        override public void SetPath(string filename)
        {
            Text = filename;
        }
    }
    // forrásfájl
    class IntelNodeSource : IntelNodeFile
    {
        public IntelNodeSource(string filename)
            : base(filename)
        {}
    }
    // headerfájl
    class IntelNodeHeader : IntelNodeFile
    {
        public IntelNodeHeader(string filename)
            : base(filename)
        {}
    }
}
