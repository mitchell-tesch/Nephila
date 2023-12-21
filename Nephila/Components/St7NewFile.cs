using System;
using Grasshopper.Kernel;
using Nephila.St7API;
using Nephila.St7API.ErrorHandler;

namespace Nephila.Components
{
    public class St7NewFile : GH_Component
    {
        public override GH_Exposure Exposure => GH_Exposure.primary;

        protected override System.Drawing.Bitmap Icon => null;

        public override Guid ComponentGuid => new Guid("9A3D73B2-97DE-4C17-BD8F-1AEF4020D343");
        
        public St7NewFile()
            : base("New Strand7 File", "St7NewFile",
                "Creates a New Strand7 File",
                "Nephila", "Model")
        {
        }
        
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("File Path", "Path", "File path to a new Strand 7 file, if a model exist, it will override the file on save", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("fileID", "ID", "uID of the file that is open", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string filePath = "";
            if (!DA.GetData("File Path", ref filePath)) return;
            if (!filePath.ToLower().EndsWith(".st7"))
            {
                filePath += ".st7";
            }
            var fileId = StateManager.GetFileId(filePath);
            ErrorHandler.Handle(St7.St7NewFile(fileId, filePath, "C:\\TEMP"));
            
            DA.SetData("fileID", fileId);
        }
    }
}