using System;
using System.Text;

namespace Nephila.St7API.ErrorHandler
{
    public class ErrorHandler
    {
        public static void Handle(int iErr)
        {
            if (iErr != St7.ERR7_NoError)
            {
                StringBuilder errorMessage = new StringBuilder(St7.kMaxStrLen);
                if (St7.ERR7_NoError == St7.St7GetAPIErrorString(iErr, errorMessage, errorMessage.Capacity))
                {
                    throw new Exceptions.Strand7Exception(errorMessage.ToString(), iErr);
                }
                else if (St7.ERR7_NoError == St7.St7GetSolverErrorString(iErr, errorMessage, errorMessage.Capacity))
                {
                    throw new Exceptions.Strand7SolverException(errorMessage.ToString(), iErr);
                }
                else
                {
                    throw new Exceptions.Strand7UnknownException("Unknown Strand7 Error has occurred.");
                }
            }
        }
    }
}
