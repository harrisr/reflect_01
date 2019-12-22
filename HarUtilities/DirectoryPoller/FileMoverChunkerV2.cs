using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HarUtilities.LogUtility;
using System.Threading;

namespace HarUtilities.DirectoryPoller
{
    //  Source Directory:        C:\FileChunker\source
    //  Destication Directory:   C:\FileChunker\destination
    //  ChunkSize:               10
    //  ChunkPeriod:             5 (minutes)
    //  CheckDestEmpty:          true

    public class FileMoverChunkerV2
    {
        #region  APP CONFIG KEYS
        private static readonly string SRC_DIR_KEY = "sourceDirectory";
        private static readonly string DEST_DIR_KEY = "destDirectory";
        private static readonly string CHUNK_SIZE_KEY = "chunkSize";
        private static readonly string CHUNK_PERIOD_KEY = "chunkPeriod";
        private static readonly string CHEK_DEST_EMPTY_KEY = "checkDestEmpty";
        #endregion
        
        #region  PRIVATE MEMBER VARIABLES
        private string sourceDirectory;
        private string destDirectory;
        private int chunkSize;
        private int chunkPeriod;
        private bool checkDestEmpty;
        private DirectoryInfo sourceDirectoryInfo;
        private DirectoryInfo destDirectoryInfo;

        #endregion

        #region  CONSTRUCTORS
        public FileMoverChunkerV2()
        {
            SetSourceDirectory(ConfigurationManager.AppSettings[SRC_DIR_KEY]);
            SetDestDirectory(ConfigurationManager.AppSettings[DEST_DIR_KEY]);
            SetChunkSize(Convert.ToInt32(ConfigurationManager.AppSettings[CHUNK_SIZE_KEY]));
            SetChunkPeriod(Convert.ToInt32(ConfigurationManager.AppSettings[CHUNK_PERIOD_KEY]));
            SetCheckDestEmpty(Convert.ToBoolean(ConfigurationManager.AppSettings[CHEK_DEST_EMPTY_KEY]));
        }

        public FileMoverChunkerV2(string sourceDirectory, string destDirectory,
            int chunkSize, int chunkPeriod, bool checkDestEmpty)
        {
            SetSourceDirectory(sourceDirectory);
            SetDestDirectory(destDirectory);
            SetChunkSize(chunkSize);
            SetChunkPeriod(chunkPeriod);
            SetCheckDestEmpty(checkDestEmpty);
        }
        #endregion
        
        #region  MUTATORS/SETTERS
        public void SetSourceDirectory(string sourceDirectory)
        {
            if (sourceDirectory.Trim().Length <= 0)
            {
                throw new DirectoryPollerException("Source Directory must not be blank");
            }
            this.sourceDirectory = sourceDirectory;
        }

        public void SetDestDirectory(string destDirectory)
        {
            if (destDirectory.Trim().Length <= 0)
            {
                throw new DirectoryPollerException("Destination Directory must not be blank");
            }
            this.destDirectory = destDirectory;
        }

        public void SetChunkSize(int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new DirectoryPollerException("Chunk Size must be positive");
            }
            this.chunkSize = chunkSize;
        }

        public void SetChunkPeriod(int chunkPeriod)
        {
            if (chunkPeriod <= 0)
            {
                throw new DirectoryPollerException("Chunk Period must be positive");
            }
            this.chunkPeriod = chunkPeriod;
        }

        public void SetCheckDestEmpty(bool checkDestEmpty)
        {
            this.checkDestEmpty = checkDestEmpty;
        }
        #endregion
               
        #region  ACCESSORS/GETTERS
        public string GetSourceDirectory()
        {
            return sourceDirectory;
        }

        public string GetDestDirectory()
        {
            return destDirectory;
        }

        public int GetChunkSize()
        {
            return chunkSize;
        }

        public int GetChunkPeriod()
        {
            return chunkPeriod;
        }

        public bool GetCheckDestEmpty()
        {
            return checkDestEmpty;
        }
        #endregion


        public void MoveFiles()
        {
            sourceDirectoryInfo = new DirectoryInfo(sourceDirectory);
            destDirectoryInfo = new DirectoryInfo(destDirectory);

            if (!sourceDirectoryInfo.Exists || !destDirectoryInfo.Exists)
            {
                HarLogger.GetLogger(typeof(FileMoverChunker)).Debug("Source and Destination directories must both exist");
                throw new DirectoryPollerException("Source and Destination directories must both exist");
            }
            if (sourceDirectoryInfo.FullName.Equals(destDirectoryInfo.FullName))
            {
                HarLogger.GetLogger(typeof(FileMoverChunker)).Debug("Source and Destination directories must be different");
                throw new DirectoryPollerException("Source and Destination directories must be different");
            }

            // if dest dir is not empty, don't move files
            // take (5) (chunksize) at a time
            // keep moving while there are source files left
            // only move chunksize within the chunk period
            // keep looping while there are files left

            //foreach (FileInfo fi in sourceDir.GetFiles())
            //fi.CopyTo(Path.Combine(destDir.ToString(), fi.Name), true);  // overwrite: true

            int chunkPeriodMilliseconds = chunkPeriod * 1000 * 60;
            int srcFileCount = sourceDirectoryInfo.GetFiles().Count();
            int destFileCount = destDirectoryInfo.GetFiles().Count();

            HarLogger.GetLogger(typeof(FileMoverChunker)).Info($"FILE MOVE FROM: {sourceDirectoryInfo.FullName}  TO: {destDirectoryInfo.FullName}");
            HarLogger.GetLogger(typeof(FileMoverChunker)).Info($"Initial source file count: {srcFileCount}");
            HarLogger.GetLogger(typeof(FileMoverChunker)).Info($"Initial dest file count: {destFileCount}");

            try
            {
                while (srcFileCount > 0)
                {
                    if (destFileCount > 0)
                    {
                        // if the destination is not empty  ...  how long do we wait, how many times do we loop before giving up and quitting???
                        HarLogger.GetLogger(typeof(FileMoverChunker)).Debug($"DEST not empty - waiting: {destFileCount}");
                        Thread.Sleep(2000);     // wait a bit, don't copy
                    }
                    else
                    {
                        //MoveFilesUsingChunks(out tempFileCount, out moveCount);
                        MoveFilesUsingChunks();
                        HarLogger.GetLogger(typeof(FileMoverChunker)).Info($"MAX FILES MOVED -- waiting for {chunkPeriod} minutes...");
                        Thread.Sleep(chunkPeriodMilliseconds);
                    }
                    srcFileCount = sourceDirectoryInfo.GetFiles().Count();
                    destFileCount = destDirectoryInfo.GetFiles().Count();
                    HarLogger.GetLogger(typeof(FileMoverChunker)).Debug($"Current source file count: {srcFileCount}");
                    HarLogger.GetLogger(typeof(FileMoverChunker)).Debug($"Current dest file count: {destFileCount}");
                }
                HarLogger.GetLogger(typeof(FileMoverChunker)).Info($"File Move Complete  --  Exiting !");
            }
            catch (Exception ex)
            {
                HarLogger.GetLogger(typeof(FileMoverChunker)).Error(ex);
                throw;
            }
        }

        //private void MoveFilesUsingChunks(out int tempFileCount, out int moveCount)
        private void MoveFilesUsingChunks()
        {
            int moveCount = 0;
            int tempFileCount = 42;   //  use some value to loop at least once
            while (tempFileCount > 0 && moveCount < chunkSize)     // copy files up to chunksize
            {
                HarLogger.GetLogger(typeof(FileMoverChunker)).Debug($"MOVEMENT so far: {moveCount}  max: {chunkSize}");

                var fileList = sourceDirectoryInfo.GetFiles().Take(chunkSize);
                tempFileCount = fileList.Count();
                foreach (FileInfo fi in fileList)
                {
                    if (moveCount < chunkSize)
                    {
                        HarLogger.GetLogger(typeof(FileMoverChunker)).Debug($"Moving {destDirectoryInfo.FullName}\\{fi.Name}");
                        fi.MoveTo(Path.Combine(destDirectoryInfo.ToString(), fi.Name));
                        moveCount++;
                    }
                }
            }
        }
    }
}
