using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Management_Software.Classes
{
    class ProjectThing
    {
        List<Version> versions;
        public ProjectThing()
        {
            versions = new List<Version>();
            versions.Add(new Version(1));

        }
        public void addVersion(int vnum)
        {
            versions.Add(new Version(vnum));
        }

    }
}
