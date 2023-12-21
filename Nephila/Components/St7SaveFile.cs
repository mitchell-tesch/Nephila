using System;
using Grasshopper.Kernel;
using Nephila.St7API;
using Nephila.St7API.ErrorHandler;

namespace Nephila.Components
{
    public class St7SaveFile : GH_Component
    {
        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("ECCE6CDD-8EEE-4B1C-87F0-DFA5F66966CA");
        
        public St7SaveFile()
            : base("Save Strand7 File", "St7SaveFile",
                "Saves a Strand7 File",
                "Nephila", "Model")
        {
        }
        
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("fileID", "ID", "uID of the file that is open", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("fileID", "ID", "uID of the file that is open", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int fileId = -1;
            if (!DA.GetData("fileID", ref fileId)) return;
            ErrorHandler.Handle(St7.St7SaveFile(fileId));
            DA.SetData("fileID", fileId);

        }
    }
}