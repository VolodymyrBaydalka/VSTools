using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof(VisualStudioVisualizers.BitmapFileDebuggerVisualizer),
    typeof(VisualStudioVisualizers.BitmapFileVisualizerObjectSource),
    Target = typeof(Bitmap), Description = "Bitmap Visualizer")]
namespace VisualStudioVisualizers
{

    class BitmapFileVisualizerObjectSource : VisualizerObjectSource
    {
        #region Implementation
        public override void GetData(object target, Stream outgoingData)
        {
            var bitmap = target as Bitmap;
            var fileName = Path.GetTempFileName() + ".png";

            bitmap.Save(fileName);

            new BinaryWriter(outgoingData).Write(fileName);
        }
        #endregion
    }

    public class BitmapFileDebuggerVisualizer : DialogDebuggerVisualizer
    {
        #region Imlementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowService"></param>
        /// <param name="objectProvider"></param>
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            Process.Start(new BinaryReader(objectProvider.GetData()).ReadString());
        }
        #endregion
    }
}
 
