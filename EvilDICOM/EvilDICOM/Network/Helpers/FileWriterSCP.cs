#region

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core;
using System;
using static EvilDICOM.Network.DICOMSCP;

#endregion

namespace EvilDICOM.Network.Helpers
{
    /// <summary>
    /// A SCP that writes incoming files to a specified location
    /// </summary>
    public class FileWriterSCP
    {
        private readonly string _storagePath;
        private CancellationTokenSource _tokenSource;
        private DICOMSCP _listener;
        private Task _listenTask;
        private Entity _entity;

        public FileWriterSCP(Entity ae, string storageLocation)
        {
            _entity = ae;
            _tokenSource = new CancellationTokenSource();
            _storagePath = storageLocation;
            OrganizeByPatientId = true;
        }

        public bool OrganizeByPatientId { get; set; }

        public async Task Start()
        {
            _listenTask = Task.Run(() =>
            {
                _listener = _listener ?? new DICOMSCP(_entity);
                _listener.SupportedAbstractSyntaxes = AbstractSyntax.ALL_RADIOTHERAPY_STORAGE;
                _listener.DIMSEService.CStoreService.CStorePayloadAction = (dcm,asc)=>
                {
                    if (!_tokenSource.Token.IsCancellationRequested)
                    {
                        var id = dcm.GetSelector().PatientID.Data;
                        string path = OrganizeByPatientId?
                        Path.Combine(_storagePath, id, dcm.GetSelector().SOPInstanceUID.Data + ".dcm"):
                        Path.Combine(_storagePath, dcm.GetSelector().SOPInstanceUID.Data + ".dcm");
                        var dir = Path.GetDirectoryName(path);
                        System.IO.Directory.CreateDirectory(dir);
                        dcm.Write(path);
                        return true;
                    }
                    return false;
                };
                _listener.ListenForIncomingAssociations(true);
            }, _tokenSource.Token);
            await _listenTask;
        }

        public async Task Stop()
        {
            await Task.Run(() =>
            {
                var mr = new ManualResetEvent(false);
                StopHandler stopAction = null;
                stopAction = () =>
                {
                    _listener.SCPStopped -= stopAction;
                    mr.Set();
                };
                _listener.SCPStopped += stopAction;
                _tokenSource.Cancel();
                _listener.Stop();
                mr.WaitOne();
            });
        }
    }
}