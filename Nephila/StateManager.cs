using System;
using System.Collections.Generic;
using System.Linq;
using Nephila.St7API;
using Nephila.St7API.Exceptions;

namespace Nephila
{
    public class StateManager
    {
        private StateManager() { }
        
        
        private static Dictionary<string, int> _openedFileIds = new Dictionary<string, int>();

        public static void RemoveFileId(int fileId)
        {
            if (_openedFileIds.ContainsValue(fileId))
            {
                var key = _openedFileIds.Where(x => x.Value == fileId).First().Key;

                _openedFileIds.Remove(key);
            }
        }
        public static int GetFileId(string filePath)
        {
            int fileId = -1;
            if (_openedFileIds.TryGetValue(filePath, out fileId))
            {
                return fileId;
            }
            else
            {
                int maxFileId = 0;
                St7.St7GetMaxModelFileID(ref maxFileId);

                if (_openedFileIds.Count > maxFileId)
                {
                    throw new Strand7Exception("The number of files that can be at once by Strand 7 has reached it's limit");
                }
                else
                {
                    var newId = _openedFileIds.Count + 1;
                    _openedFileIds.Add(filePath, newId);
                    return newId;
                }
            }
        }
    }
}