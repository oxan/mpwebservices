using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.IO.Pipes;

using MediaPortal.TvServer.WebServices;

namespace MediaPortal.TvServer.WebServices
{
    public class EncoderWrapper : Stream
    {
        // Transport Pipes
        private TransportStream encoderInput;
        private TransportStream encoderOutput;

        private Process applicationThread;
        private ProcessStartInfo applicationDetails;
        private EncoderConfig encCfg;


        // Media
        private Stream media;
        private String mediaUrl;

        public EncoderWrapper(Stream input,EncoderConfig encCfg)
        {
          this.encCfg = encCfg;
          if (!encCfg.useTranscoding)
            return;
          SetupPipes();
          media = input;
          Start();
        }

        private void SetupPipes()
        {
            switch (encCfg.inputMethod)
            {
                case TransportMethod.NamedPipe:
                    encoderInput = new NamedPipe();
                    break;
                case TransportMethod.StandardIn:
                    encoderInput = new BasicStream();
                    break;
                default:
                    throw new ArgumentException("Invalid option.");
            }

            switch (encCfg.outputMethod)
            {
                case TransportMethod.NamedPipe:
                    encoderOutput = new NamedPipe();
                    break;
                case TransportMethod.StandardOut:
                    encoderOutput = new BasicStream();
                    break;
                default:
                    throw new ArgumentException("Invalid option.");
            }
        }

        protected void Start()
        {
            if (media != null)
                StartPipe(media);
            else
                throw new ArgumentNullException("Input media is set to null.");
        }

        private void StartPipe(Stream media)
        {
            encoderInput.Start(false);

            // Start the transcoder.
            StartProcess(encoderInput.Url, encoderOutput.Url);

            encoderInput.CopyStream(media);

            encoderOutput.Start(false);

            // Wait for the output encoder to connect.
            int tries = 10000;

            do
            {
                if (encoderOutput.IsReady)
                    break;

                System.Threading.Thread.Sleep(1);
            } while (--tries != 0);
        }

        private void StartPipe(String media)
        {
            // Start the transcoder.
            if (media.StartsWith("\""))
            {
                StartProcess(media, encoderOutput.Url);
            }
            else
            {
                StartProcess(String.Format("\"{0}\"", media), encoderOutput.Url);
            }

            encoderOutput.Start(false);
        }

        public override bool CanRead
        {
            get
            {
                return encoderOutput.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return encoderOutput.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get { throw new NotSupportedException(); }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Length
        {
            get
            {
                return encoderOutput.Length;
            }
        }

        public override long Position
        {
            get
            {
                return encoderOutput.Position;
            }
            set
            {
                encoderOutput.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return encoderOutput.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return encoderOutput.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            encoderOutput.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public void StartProcess(String input, String output)
        {
          if (applicationThread != null)
            applicationThread.Kill();
          applicationDetails = new ProcessStartInfo(encCfg.fileName,String.Format(encCfg.args, input, output));
          applicationDetails.UseShellExecute = false;
          if (encCfg.inputMethod == TransportMethod.StandardIn)
            applicationDetails.RedirectStandardInput = true;
          if (encCfg.outputMethod == TransportMethod.StandardOut)
            applicationDetails.RedirectStandardOutput = true;
          applicationThread = new Process();
          applicationThread.StartInfo = applicationDetails;
          applicationThread.Start();
          if (encCfg.inputMethod == TransportMethod.StandardIn)
            encoderInput.UnderlyingStreamObject = applicationThread.StandardInput.BaseStream;
          if (encCfg.outputMethod == TransportMethod.StandardOut)
            encoderOutput.UnderlyingStreamObject = applicationThread.StandardOutput.BaseStream;
        }

        public void StopProcess()
        {
          if (applicationThread != null)
            applicationThread.Kill();
        }
    }
}
