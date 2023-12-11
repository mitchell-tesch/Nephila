using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Nephila
{
    public class NephilaInfo : GH_AssemblyInfo
    {
        public override string Name => "Nephila";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "Plugin for Strand7 Release 3.1.3";

        public override Guid Id => new Guid("dd17a8e1-33f2-4831-bbd3-43182f109ccc");

        //Return a string identifying you or your company.
        public override string AuthorName => "Mitchell Tesch";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}