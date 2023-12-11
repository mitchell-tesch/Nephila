using Grasshopper.Kernel;
using System;
using System.Text;
using Nephila.St7API;
using Nephila.St7API.ErrorHandler;
using Nephila.St7API.Exceptions;


namespace Nephila.Components.Model
{
    public class St7Init : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public St7Init()
          : base("Initialise Strand7 R3", "St7Init",
            "Initialises the Strand7 R3 API.",
            "Nephila", "Model")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            // Use the pManager object to register your input parameters.
            // You can often supply default values when creating parameters.
            // All parameters must have the correct access type. If you want 
            // to import lists or trees of values, modify the ParamAccess flag.
            pManager.AddBooleanParameter("Initialise?", "I?", "Initialise Strand7 R3 API", GH_ParamAccess.item, false);

            // If you want to change properties of certain parameters, 
            // you can use the pManager instance to access them by index:
            //pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            // Use the pManager object to register your output parameters.
            // Output parameters do not have default values, but they too must have the correct access type.
            pManager.AddTextParameter("Output", "O", "Output log of initialisation", GH_ParamAccess.item);
            //pManager.AddCurveParameter("Spiral", "S", "Spiral curve", GH_ParamAccess.item);

            // Sometimes you want to hide a specific parameter from the Rhino preview.
            // You can use the HideParameter() method as a quick way:
            // pManager.HideParameter(0);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // First, we need to retrieve all data from the input parameters.
            // We'll start by declaring variables and assigning them starting values.
            bool initialise = false;

            // Then we need to access the input parameters individually. 
            // When data cannot be extracted from a parameter, we should abort this method.
            if (!DA.GetData(0, ref initialise)) return;

            StringBuilder st7InitLog = new StringBuilder();
            if (initialise)
            {
                try
                {
                    // get Strand7 version
                    int versionMajor = 0;
                    int versionMinor = 0;
                    int versionPoint = 0;
                    ErrorHandler.Handle(St7.St7Version(ref versionMajor, ref versionMinor, ref versionPoint));
                    
                    // get Strand7 API build
                    StringBuilder st7ApiBuild = new StringBuilder(St7.kMaxStrLen);
                    ErrorHandler.Handle(St7.St7BuildString(st7ApiBuild, st7ApiBuild.Capacity));

                    // get Strand7 path
                    StringBuilder st7Path = new StringBuilder(St7.kMaxStrLen);
                    ErrorHandler.Handle(St7.St7GetAPIPath(st7Path, st7Path.Capacity));

                    // initialise Strand7 API
                    ErrorHandler.Handle(St7.St7Init());
                    st7InitLog.AppendFormat("Strand7 Release {0}.{1}.{2}\n", versionMajor, versionMinor, versionPoint);
                    st7InitLog.AppendFormat("API Build {0}\n", st7ApiBuild);
                    st7InitLog.AppendFormat("({0})\n", st7Path);
                    st7InitLog.Append("Successfully initialised.");
                }
                catch (Strand7Exception ex)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, ex.Message);
                    st7InitLog.Append("Strand7 initialisation unsuccessful.\n");
                    st7InitLog.AppendFormat("{0}", ex.ErrorMessage);
                }
            }
            else
            {
                try
                {
                    ErrorHandler.Handle(St7.St7Release());
                }
                catch (Strand7Exception ex)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, ex.Message);
                }
            }
            // return output data
            DA.SetData(0, st7InitLog.ToString());
        }

        /// <summary>
        /// The Exposure property controls where in the panel a component icon 
        /// will appear. There are seven possible locations (primary to septenary), 
        /// each of which can be combined with the GH_Exposure.obscure flag, which 
        /// ensures the component will only be visible on panel dropdowns.
        /// </summary>
        public override GH_Exposure Exposure => GH_Exposure.primary;

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("d819b855-0a7f-43c9-bdc3-8d688d325f0c");
    }
}