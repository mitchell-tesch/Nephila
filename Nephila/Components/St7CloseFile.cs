using System;
using Grasshopper.Kernel;
using Nephila.St7API;
using Nephila.St7API.ErrorHandler;

namespace Nephila.Components
{
    public class St7CloseFile : GH_Component
    {
        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("B04CE008-00EF-417F-B9BC-BC26A04D4ACE");
        
        public St7CloseFile()
            : base("Close Strand7 File", "St7CloseFile",
                "Closes a Strand7 File",
                "Nephila", "Model")
        {
        }
        
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("fileID", "ID", "uID of the file that is open", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            return;
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int fileId = -1;
            if (!DA.GetData("fileID", ref fileId)) return;
            ErrorHandler.Handle(St7.St7CloseFile(fileId));
            StateManager.RemoveFileId(fileId);

        }
    }
}