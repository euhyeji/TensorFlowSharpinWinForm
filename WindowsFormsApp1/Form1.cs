using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TensorFlow;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Select Photo to inference
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_InputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Images (*.JPG)|*.JPG|" + "All files (*.*)|*.*";
            openFileDialog1.Title = "Select Photo";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                InputFileName.Text = openFileDialog1.FileName;
                ShowInputPhoto(InputFileName.Text);
                tb_Result.Text = Inference(InputFileName.Text);
            }
        }

        /// <summary>
        /// Show photo on Picturebox
        /// </summary>
        /// <param name="filename"></param>
        private void ShowInputPhoto(string filename)
        {
            Bitmap myBitmap = new Bitmap(filename);
            pB_ShowPhoto.Image = myBitmap;
        }

        static string dir, modelFile, labelsFile;
        /// <summary>
        /// Inference using TensorFlow model
        /// </summary>
        /// <param name="filename"></param>
        private string Inference(string filename)
        {
            //ModelFiles(@"C:\Users\jinwh\source\repos\WindowsFormsApp1\WindowsFormsApp1\model");
            ModelFiles(Application.StartupPath);

            // Construct an in-memory graph from the serialized form.
            var graph = new TFGraph();
            // Load the serialized GraphDef from a file.
            var model = File.ReadAllBytes(modelFile);

            graph.Import(model, "");

            using (var session = new TFSession(graph))
            {
                var labels = File.ReadAllLines(labelsFile);
                
                // Run inference on the image files
                // For multiple images, session.Run() can be called in a loop (and
                // concurrently). Alternatively, images can be batched since the model
                // accepts batches of image data as input.
                var tensor = CreateTensorFromImageFile(filename);

                var runner = session.GetRunner();


                //runner.AddInput(graph["input"][0], tensor).Fetch(graph["output"][0]);
                runner.AddInput(graph["input"][0], tensor).Fetch(graph["InceptionV1/Logits/Predictions/Softmax"][0]);
                var output = runner.Run();
                // output[0].Value() is a vector containing probabilities of
                // labels for each image in the "batch". The batch size was 1.
                // Find the most probably label index.

                var result = output[0];
                var rshape = result.Shape;
                if (result.NumDims != 2 || rshape[0] != 1)
                {
                    var shape = "";
                    foreach (var d in rshape)
                    {
                        shape += $"{d} ";
                    }
                    shape = shape.Trim();
                    Console.WriteLine($"Error: expected to produce a [1 N] shaped tensor where N is the number of labels, instead it produced one with shape [{shape}]");
                    Environment.Exit(1);
                }

                // You can get the data in two ways, as a multi-dimensional array, or arrays of arrays, 
                // code can be nicer to read with one or the other, pick it based on how you want to process
                // it
                bool jagged = true;

                var bestIdx = 0;
                float p = 0, best = 0;

                if (jagged)
                {
                    var probabilities = ((float[][])result.GetValue(jagged: true))[0];
                    for (int i = 0; i < probabilities.Length; i++)
                    {
                        if (probabilities[i] > best)
                        {
                            bestIdx = i;
                            best = probabilities[i];
                        }
                    }

                }
                else
                {
                    var val = (float[,])result.GetValue(jagged: false);

                    // Result is [1,N], flatten array
                    for (int i = 0; i < val.GetLength(1); i++)
                    {
                        if (val[0, i] > best)
                        {
                            bestIdx = i;
                            best = val[0, i];
                        }
                    }
                }

                return  $" best match: [{bestIdx}] {best * 100.0}% {labels[bestIdx]}";
                
            }

            
        }

        static void ModelFiles(string dir)
        {
            string url = "https://storage.googleapis.com/download.tensorflow.org/models/inception5h.zip";

            //modelFile = Path.Combine(dir, "tensorflow_inception_graph.pb");
            modelFile = Path.Combine(dir, "Tensorflow_inception_v1_caltech256_graph.pb");
            //labelsFile = Path.Combine(dir, "imagenet_comp_graph_label_strings.txt");
            labelsFile = Path.Combine(dir, "caltech256_comp_graph_label_strings.txt");
            var zipfile = Path.Combine(dir, "inception5h.zip");

            if (File.Exists(modelFile) && File.Exists(labelsFile))
                return;

            Directory.CreateDirectory(dir);
            var wc = new WebClient();
            wc.DownloadFile(url, zipfile);
            ZipFile.ExtractToDirectory(zipfile, dir);
            File.Delete(zipfile);
        }

        // Convert the image in filename to a Tensor suitable as input to the Inception model.
        public static TFTensor CreateTensorFromImageFile(string file, TFDataType destinationDataType = TFDataType.Float)
        {
            var contents = File.ReadAllBytes(file);

            // DecodeJpeg uses a scalar String-valued tensor as input.
            var tensor = TFTensor.CreateString(contents);

            TFOutput input, output;

            // Construct a graph to normalize the image
            using (var graph = ConstructGraphToNormalizeImage(out input, out output, destinationDataType))
            {
                // Execute that graph to normalize this one image
                using (var session = new TFSession(graph))
                {
                    var normalized = session.Run(
                        inputs: new[] { input },
                        inputValues: new[] { tensor },
                        outputs: new[] { output });

                    return normalized[0];
                }
            }
        }

        // The inception model takes as input the image described by a Tensor in a very
        // specific normalized format (a particular image size, shape of the input tensor,
        // normalized pixel values etc.).
        //
        // This function constructs a graph of TensorFlow operations which takes as
        // input a JPEG-encoded string and returns a tensor suitable as input to the
        // inception model.
        private static TFGraph ConstructGraphToNormalizeImage(out TFOutput input, out TFOutput output, TFDataType destinationDataType = TFDataType.Float)
        {
            // Some constants specific to the pre-trained model at:
            // https://storage.googleapis.com/download.tensorflow.org/models/inception5h.zip
            //
            // - The model was trained after with images scaled to 224x224 pixels.
            // - The colors, represented as R, G, B in 1-byte each were converted to
            //   float using (value - Mean)/Scale.

            const int W = 224;
            const int H = 224;
            const float Mean = 0.5f;            
            const float Scale = 2.0f;
                        
            var graph = new TFGraph();
            input = graph.Placeholder(TFDataType.String);            

            var scale = graph.Div(
                    x: graph.Const(1.0f),
                    y: graph.Const(255.0f)
                    );
            
            output = graph.Mul(
                x: graph.Sub(
                    x: graph.Squeeze(
                        input: graph.ResizeBilinear(
                            images: graph.ExpandDims(
                                input: graph.Mul(
                                        x: graph.Cast(graph.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
                                        y: scale),
                                dim: graph.Const(0, "make_batch")),
                            size: graph.Const(new int[] { W, H }, "size"), 
                            align_corners:false),
                        squeeze_dims: new long[] { 0 }),
                    y: graph.Const(Mean, "mean")),
                y: graph.Const(Scale, "scale"));

            output = graph.ExpandDims(
                input: output,
                dim: graph.Const(0));


            //output = graph.Cast(graph.Div(
            //    x: graph.Sub(
            //        x: graph.ResizeBilinear(
            //            images: graph.ExpandDims(
            //                input: graph.Cast(
            //                    graph.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
            //                dim: graph.Const(0, "make_batch")),
            //            size: graph.Const(new int[] { W, H }, "size")),
            //        y: graph.Const(Mean, "mean")),
            //    y: graph.Const(Scale, "scale")), destinationDataType);


            //output = graph.Mul(
            //    x: graph.Sub(
            //        x: graph.Mul(
            //            x: graph.Cast( 
            //                graph.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
            //            y: scale),
            //        y: graph.Const(Mean, "mean")),
            //    y: graph.Const(Scale, "scale"));



            return graph;
        }

    }
}
